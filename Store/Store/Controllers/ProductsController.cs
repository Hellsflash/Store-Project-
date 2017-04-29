using Microsoft.AspNet.Identity;
using Store.Data;
using Store.Models;
using Store.Models.Products;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Store.Controllers
{
    public class ProductsController : Controller
    {

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            var db = new StoreDbContext();
            Product productId = db.Products.Find(id);
            var product = db.Products.Where(p => p.Id == id).FirstOrDefault();

            if (product == null)
            {
                return HttpNotFound();
            }
            var model = new ProductDetails();
            model.Id = product.Id;
            model.ImageUrl = product.ImageUrl;
            model.Name = product.Name;
            model.Description = product.Description;
            model.Categorie = product.Categorie;
            model.Price = product.Price;
            model.Author = product.Author;

            return View(model);

        }
        [HttpPost]
        public ActionResult Edit(ProductDetails model)
        {
            if (ModelState.IsValid)
            {
                var db = new StoreDbContext();

                var product = db.Products.FirstOrDefault(p => p.Id == model.Id);

                product.Id = model.Id;
                product.ImageUrl = model.ImageUrl;
                product.Name = model.Name;
                product.Description = model.Description;
                product.Categorie = model.Categorie;
                product.Price = model.Price;
                product.Author= model.Author;

                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("All");
            }
            return View(model);
        }

        [Authorize]
        public ActionResult Buy(int? id)
        {
            var db = new StoreDbContext();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);

        }

        [HttpPost, ActionName("Buy")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var db = new StoreDbContext();

            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("All");
        }


        public ActionResult All(int page = 1,
            string user = null, string search = null)
        {
            var db = new StoreDbContext();

            var pageSize = 5;

            var productsQuery = db.Products.AsQueryable();


            if (search != null)
            {
                productsQuery = productsQuery.Where(p => p.Name.ToLower().Contains(search.ToLower()));
            }

            if (user != null)
            {
                productsQuery = productsQuery.Where(p => p.Author.Email == user);

            }

            var products = productsQuery
                .OrderByDescending(p => p.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new HomeProducts
                {
                    Id = p.Id,
                    Categorie = p.Categorie,
                    Name = p.Name,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price

                })
            .ToList();

            ViewBag.CurrPage = page;


            return View(products);
        }

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
                    Id = p.Id,
                    Name = p.Name,
                    Categorie = p.Categorie,
                    Price = p.Price,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl

                })
                .FirstOrDefault();


            if (product == null)
            {
                return HttpNotFound();
            }


            return View(product);


        }
        private bool IsUserAuthorized(Product product)
        {
            bool isAdmin = this.User.IsInRole("Admin");
            bool isAuthor = product.IsAuthor(this.User.Identity.Name);

            return isAdmin || isAuthor;

        }
    }
}