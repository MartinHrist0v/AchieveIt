using AchieveIt.Data;
using AchieveIt.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AchieveIt.Web.Controllers
{
    public class ProfileController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        // GET: Profile example www.website.com/admin
        public ActionResult GetProfile(string username)
        {
            
            var profileDb = context.Users.Where(u => u.UserName == username).FirstOrDefault();
            if (profileDb == null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    return Redirect(User.Identity.GetUserName());
                }
                return RedirectToAction("Index", "Home", null);

            }
            string imgToString = null;
            try
            {
                imgToString = Convert.ToBase64String(profileDb.Image.ImageData);
            }
            catch (NullReferenceException a)
            {

            }

            var profileInfo = new ProfileInfoViewModel(profileDb.UserName, profileDb.Email, profileDb.Id,
              imgToString, profileDb.FacebookUrl, profileDb.Nationality, profileDb.DateRegistered, profileDb.Votes, profileDb.DirectorySavedPicture);
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
                .Skip(0)
                .Take(5)
                .ToList();
            var output = new ProfileVewModel() { ProfileInfo = profileInfo, AddPost = new AddPostBindingModel(), Posts = posts };
            return View(output);
        }

        public ActionResult ChangeProfilePicture(HttpPostedFileBase file)
        {
            if (file != null)
            {

                //save Img In directory

                string UserFolder = string.Format(@"~/Content/Images/Posts/{0}", User.Identity.GetUserName());
                string picName = string.Format("Profile-picture-{0}", User.Identity.GetUserName());
                string imgType = file.ContentType;
                string checkImage = string.Format("{0}/{1}.{2}", UserFolder, picName, imgType);
                if (System.IO.File.Exists(checkImage))
                {
                    System.IO.File.Delete(checkImage);

                }
                string path = System.IO.Path.Combine(
                                   Server.MapPath(UserFolder), picName);

                file.SaveAs(path);

                //save Img in DB
                var profileImg = new AchieveIt.Models.Image()
                {
                    Extention = imgType,
                    FileName = picName,
                    ImageSize = file.ContentLength,
                };

                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                    profileImg.ImageData = array;
                }
                var currentUser = context.Users.FirstOrDefault(u => u.UserName == User.Identity.GetUserName());
                currentUser.DirectorySavedPicture = string.Format("/Content/Images/Posts/{0}/{1}", User.Identity.GetUserName(), picName);
                currentUser.Image = profileImg;
                context.SaveChanges();
                // after successfully uploading redirect the user
                return RedirectToAction("GetProfile", "Profile", new { username = User.Identity.GetUserName() });
            }
            return RedirectToAction("GetProfile", "Profile", new { username = User.Identity.GetUserName() });
        }
    }
}