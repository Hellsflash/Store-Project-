using Store.Data;
using Store.Models.Products;
using System.Linq;
using System.Web.Mvc;

namespace Store.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var db = new StoreDbContext();

            var products = db.Products.OrderByDescending(p => p.Id)
                .Take(6)
                .Select(p => new HomeProducts
                {
                    Id = p.Id,
                    Name = p.Name,
                    Categorie=p.Categorie,
                    ImageUrl = p.ImageUrl,
                    

                })
                .ToList();
            
            return View(products);
        }

    }
}