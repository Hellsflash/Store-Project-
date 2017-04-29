using Microsoft.Owin;
using Owin;
using Store.Migrations;
using Store.Data;
using System.Data.Entity;

[assembly: OwinStartupAttribute(typeof(Store.Startup))]
namespace Store
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<StoreDbContext, Configuration>());
            ConfigureAuth(app);
        }
    }
}
