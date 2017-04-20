using Microsoft.AspNet.Identity;
using Store.Data;
using Store.Models.Products;
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
        public ActionResult Create(CreateProductModel model)
        {
            if (this.ModelState.IsValid)
            {
                var authorId = this.User.Identity.GetUserId();

                var product = new Product
                {
                    Name = model.Name,
                    Categorie = model.Categorie,
                    Description = model.Description,
                    Price = model.Price,
                    ImageUrl = model.ImageUrl,
                    AuthorId = authorId,
                };

                var db = new StoreDbContext();

                db.Products.Add(product);
                db.SaveChanges();

                return RedirectToAction("Details", new { id = product.Id });
            }

            return View(model);
        }
      }
}