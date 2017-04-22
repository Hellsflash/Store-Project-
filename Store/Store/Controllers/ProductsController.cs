using Microsoft.AspNet.Identity;
using Store.Data;
using Store.Models.Products;
using System.Linq;
using System.Web.Mvc;

namespace Store.Controllers
{
    public class ProductsController : Controller
        {


        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(CreateProductModel productModel)
        {
            if (this.ModelState.IsValid)
            {
                var authorId = this.User.Identity.GetUserId();

                var product = new Product
                {
                    Name = productModel.Name,
                    Categorie = productModel.Categorie,
                    Description = productModel.Description,
                    Price = productModel.Price,
                    ImageUrl = productModel.ImageUrl,
                    AuthorId = authorId,
                };

                var db = new StoreDbContext();

                db.Products.Add(product);
                db.SaveChanges();

                return RedirectToAction("Details", new { id = product.Id });
            }

            return View(productModel);
        }

        public ActionResult Details(int id)
        {
            var db = new StoreDbContext();

            var product = db.Products
                .Where(p => p.Id == id)
                .Select(p => new ProductDetails
                {
                    Name =p.Name,
                    Categorie =p.Categorie,
                    Price =p.Price,
                    Description =p.Description,
                    ImageUrl =p.ImageUrl
                    
                })
                .FirstOrDefault();
            if (product== null)
            {
                return HttpNotFound();
            }
            return View(product);

        }
      }
}