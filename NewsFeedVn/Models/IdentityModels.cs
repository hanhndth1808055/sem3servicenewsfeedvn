using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace NewsFeedVn.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("name=ApplicationDbContext")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<NewsFeedVn.Models.Category> Categories { get; set; }
        public System.Data.Entity.DbSet<NewsFeedVn.Models.Source> Sources { get; set; }
        public System.Data.Entity.DbSet<NewsFeedVn.Models.Article> Articles { get; set; }
        public System.Data.Entity.DbSet<NewsFeedVn.Models.Comment> Comments { get; set; }
        public System.Data.Entity.DbSet<NewsFeedVn.Models.QuestionAndAnswer> QuestionAndAnswers { get; set; }
        public System.Data.Entity.DbSet<NewsFeedVn.Models.Subscriber> Subscribers { get; set; }
    }
}