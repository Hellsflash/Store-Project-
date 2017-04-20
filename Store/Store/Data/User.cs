using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace Store.Data
{
    
    public class User : IdentityUser
    {
        public User()
        {
            this.Products = new HashSet<Product>();
        }

        public virtual ICollection<Product> Products { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            
            return userIdentity;
        }
    }

   
}