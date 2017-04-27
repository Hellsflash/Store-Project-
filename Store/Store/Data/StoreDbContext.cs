using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Store.Data
{
    public class StoreDbContext : IdentityDbContext<User>
    {
        public StoreDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            this.Configuration.ProxyCreationEnabled = false;
        }
        public virtual IDbSet<Product> Products { get; set; }

        public static StoreDbContext Create()
        {
            return new StoreDbContext();
        }
    }
}