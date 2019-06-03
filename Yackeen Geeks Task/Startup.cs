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

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
