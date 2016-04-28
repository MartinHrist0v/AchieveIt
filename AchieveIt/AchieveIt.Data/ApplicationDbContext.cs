namespace AchieveIt.Data
{
    using Migrations;
    using AchieveIt.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity;

    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer((new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>()));
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public IDbSet<Comment> Comments { get; set; }
        public IDbSet<Post> Posts { get; set; }
        public IDbSet<Image> Images { get; set; }
    }
}
