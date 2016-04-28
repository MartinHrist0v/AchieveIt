namespace AchieveIt.Models
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class User : IdentityUser
    {
        private ICollection<User> friends;
        private ICollection<Post> posts;
        private ICollection<Comment> comments;
        public User():base()
        {

            this.friends = new HashSet<User>();
            this.posts = new HashSet<Post>();
            this.comments = new HashSet<Comment>();
            this.DateRegistered = DateTime.Now;
            
        }
       // public int ImageId { get;set ;}
        public virtual Image Image{ get; set; }
        public string FacebookUrl { get; set; }
        public string Nationality { get; set; }
        public DateTime DateRegistered { get; set; }
        public int Votes { get; set; }
        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }
        public virtual ICollection<Post> Posts
        {
            get { return this.posts; }
            set { this.posts = value; }
        }
        public virtual ICollection<User> Friends
        {
            get
            {
                return this.friends;

            }
            set
            {
                this.friends = value;
            }
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

}
