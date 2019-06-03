using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using Yackeen_Geeks_Task.Models;

[assembly: OwinStartupAttribute(typeof(Yackeen_Geeks_Task.Startup))]
namespace Yackeen_Geeks_Task
{
    public partial class Startup
    {
        private ApplicationDbContext _dbContext;

        public Startup()
        {
            _dbContext = new ApplicationDbContext();
        }

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            ConfigRoles();
        }

        public void ConfigRoles()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_dbContext));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_dbContext));

            if (!roleManager.RoleExists("Admins"))
            {
                var role = new IdentityRole
                {
                    Name = "Admins"
                };

                roleManager.Create(role);

                var user = new ApplicationUser
                {
                    UserName = "mohamedasemsyam@outlook.com",
                    Email = "mohamedasemsyam@outlook.com"
                };

                var userCreated = userManager.Create(user, "Qwer@admin1996");

                if (userCreated.Succeeded)
                {
                    userManager.AddToRole(user.Id, role.Name);
                }
            }
        }
    }
}
