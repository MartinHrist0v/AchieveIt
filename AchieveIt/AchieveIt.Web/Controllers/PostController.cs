using AchieveIt.Data;
using AchieveIt.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
//using System.Web.Http;
using System.Web.Mvc;

namespace AchieveIt.Web.Controllers
{
    public class PostController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        // GET: Post
        [HttpGet]
        public ActionResult Post(int id)
        {

            var currentPostInDB = context.Posts.FirstOrDefault(p => p.Id == id);
            var model = new PostsViewModel();
            model.Title = currentPostInDB.Title;
            model.Url = currentPostInDB.Url;
            model.DateCreated = currentPostInDB.DateCreated;
            model.DirectorySavedPicture = currentPostInDB.DirectorySavedPicture ?? "Null";
            
            model.Sender = currentPostInDB.Sender.UserName;
            model.Text = currentPostInDB.Text;
            model.Id = currentPostInDB.Id;


            if (currentPostInDB.Image != null)
            {
                model.Picture = currentPostInDB.Image.ImageData;
            }
            else
            {
                model.Picture = new byte[1];
            }
            if (currentPostInDB.Comments.Any())
            {
                    var comments = currentPostInDB.Comments.OrderBy(c => c.Vote).ThenByDescending(c => c.DateCreated)
                    .Select(c => new CommentViewModel()
                    {

                        Text = c.Text,
                        Author = c.Author.UserName,
                        AuthorId = c.AuthorId,
                        DateCreated = c.DateCreated,
                        Id = c.Id,
                        Vote = c.Vote,
                    }).ToList();
                    model.Comments = comments;
                }
            else
            {
                model.Comments = new List<CommentViewModel>();
            }

            return this.PartialView("PostView", model);
           

        }
        [Authorize]
        [HttpPost]
        public ActionResult AddPost(AddPostBindingModel post)
        {
            if ((post != null && this.ModelState.IsValid) && (post.Image != null || post.Uri != null))
            {
                
                var currentUser = User.Identity.GetUserId();
                var newPost = new AchieveIt.Models.Post()
                {
                    SenderId = currentUser,
                    Text = post.Text,
                    Url = post.Uri,
                    Vote = 0,
                    Title = post.Title,

                };


                if (post.Image != null)
                {
                    //save Img In directory
                    string UserFolder = string.Format(@"~/Content/Images/Posts/{0}", User.Identity.GetUserName());
                    string pic = System.IO.Path.GetFileName(post.Image.FileName);
                    string path = System.IO.Path.Combine(
                                       Server.MapPath(UserFolder), pic);
                    string extention = post.Image.ContentType;

                    post.Image.SaveAs(path);

                    //save Img in DB
                    var postImg = new AchieveIt.Models.Image()
                    {
                        Extention = extention,
                        FileName = pic,
                        ImageSize = post.Image.ContentLength,
                    };

                    using (MemoryStream ms = new MemoryStream())
                    {
                        post.Image.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();
                        postImg.ImageData = array;
                    }
                    newPost.DirectorySavedPicture = string.Format("/Content/Images/Posts/{0}/{1}", User.Identity.GetUserName(), pic);
                    newPost.Image = postImg;
                }
                //if there is no picture Image in Input

                context.Posts.Add(newPost);

                context.SaveChanges();
                //return All Posts of current user
                var postsOfCurrentUser = context.Posts.Where(p => p.Sender.UserName == User.Identity.Name).Select(p => new PostsViewModel
                {
                    Text = p.Text,
                    Url = p.Url,
                    DirectorySavedPicture = p.DirectorySavedPicture,
                    Picture = p.Image.ImageData,
                    Title = p.Title,
                    DateCreated = p.DateCreated,
                    Sender = p.Sender.UserName,
                    Id = p.Id,

                })
           .OrderByDescending(p => p.DateCreated);
                ModelState.Clear();
                return RedirectToAction("GetProfile", "Profile", new { username = User.Identity.GetUserName() });
                //return this.PartialView("_PostsView", postsOfCurrentUser);
            }
            return Json("Error");
        }
        [HttpPost]
        [Authorize]
        public ActionResult DeletePost(int id)
        {
            var post = context.Posts.FirstOrDefault(i => i.Id == id);
            if (User.Identity.Name == post.Sender.UserName)
            {
                string identityUser = User.Identity.Name;
                var commentsFromCurrentPost = post.Comments.ToList();
                int commentCount = commentsFromCurrentPost.Count;
                commentsFromCurrentPost.RemoveRange(0, commentCount);
                context.Posts.Remove(post);
                context.SaveChanges();
                return Content(string.Empty);
            }


            return new HttpStatusCodeResult(401);// status code for Unauthorized 
        }
        [Authorize]
        [HttpPost]
        public ActionResult AddComment(string comment,int id){
            if (User.Identity.IsAuthenticated && ModelState.IsValid )
            {
                if ( string.IsNullOrEmpty(comment))
                {
                    return Json("EMPTY comment", JsonRequestBehavior.AllowGet);
                }
                var currentUser = context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
                var newComment = new AchieveIt.Models.Comment()
                {
                    Text = comment,
                    Author = currentUser,
                };

                var currentPost = context.Posts.FirstOrDefault(p => p.Id == id);
                currentPost.Comments.Add(newComment);
                context.SaveChanges();

                var allComments = currentPost.Comments.OrderBy(c => c.Vote).ThenByDescending(c => c.DateCreated).Select(c => new CommentViewModel()
                {

                    Text = c.Text,
                    Author = c.Author.UserName,
                    AuthorId = c.AuthorId,
                    DateCreated = c.DateCreated,
                    Id = c.Id,
                    Vote = c.Vote,
                }).ToList();
               
                return this.PartialView("_CommentsView", allComments);
            }
            //TODO ne stava
                
                return Json("You can't comment if you are not logged in!",JsonRequestBehavior.AllowGet);  
        }

        
        public ActionResult LoadMorePosts(int start, string username)
        {
            //var count = context.Posts.Where(p => p.Sender.UserName == username).Count();
            //int skipper = 10;
            //if (count-start>10)
            //{

            //}
            var posts = context.Posts.Where(i => i.Sender.UserName == username).Select(p => new PostsViewModel()
            {
                Text = p.Text,
                Url = p.Url,
                DirectorySavedPicture = p.DirectorySavedPicture,
                Picture = p.Image.ImageData,
                Title = p.Title,
                DateCreated = p.DateCreated,
                Sender = p.Sender.UserName,
                Id = p.Id,
            }).OrderByDescending(p => p.DateCreated)
           .Skip(start)
           .Take(10)
           .ToList();
            return this.PartialView("_PostsView", posts);
        }
        //public List<PostsViewModel> Posts(string username){
        //    var posts = context.Posts.Where(i => i.Sender.UserName == username).Select(p => new PostsViewModel()
        //    {
        //        Text = p.Text,
        //        Url = p.Url,
        //        DirectorySavedPicture = p.DirectorySavedPicture,
        //        Picture = p.Image.ImageData,
        //        Title = p.Title,
        //        DateCreated = p.DateCreated,
        //        Sender = p.Sender.UserName,
        //        Id = p.Id,
        //    }).OrderByDescending(p => p.DateCreated).ToList();
        //    return posts;
        //}
    }

}
