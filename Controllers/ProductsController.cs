using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using OnlineLibrary1.Models;
using OnlineLibrary1.ViewModels;
using PagedList;

namespace OnlineLibrary1.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Products

        public ActionResult Index(int category = 0, string search = null, string catName = null, string sortBy = null, int? page=null)
        {

            //Creem un nou obiect de tipul View-Model.
            ProductsNumberViewModel viewModel = new ProductsNumberViewModel();

            //Extragem toate cartile din baza de date pe baza categoriei
            var products = db.Products.Include(p => p.Category);

            //Filtram produsele pe baza categoriei
            if (category != 0)
            {
                products = products.Where(prod => prod.Category.ID == category);
            }

            //Cautam in produse si in categorie
            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(prod => prod.Title.Contains(search) ||
                prod.Author.Contains(search) || prod.Description.Contains(search) ||
                prod.Isbn.Contains(search) || prod.Category.Name.Contains(search));
                viewModel.Search = search;
            }

            //Punem categoriile in ViewBag
            //var categories = products.OrderBy(prod => prod.Category.Name).Select(prod => prod.Category.Name).Distinct();
            // ViewBag.catName = new SelectList(categories);
            viewModel.CategoryWithNumbers = from fitProduct in products
                                            where fitProduct.CategoryID != null
                                            group fitProduct by fitProduct.Category.Name into catGroup
                                            select new CategoryWithNumber()
                                            {
                                                CategoryName = catGroup.Key,
                                                ProductNumbers = catGroup.Count()
                                            };


            //Filtram produsele dupa numele categoriei
            if (!string.IsNullOrEmpty(catName))
            {
                products = products.Where(prod => prod.Category.Name == catName);
                viewModel.CatName = catName;
                    
            }
            //Sortarea dupa diferite criterii
            switch (sortBy)
            {
                case "lowest_price":
                    products = products.OrderBy(prod => prod.Price);
                    break;
                case "highest_price":
                    products = products.OrderByDescending(prod => prod.Price);
                    break;
                default:
                    products = products.OrderBy(prod => prod.Title);
                    break;

            }
            //stabilirea paginarii
            const int ItemsPerPage = 3; int currentPage = (page ?? 1);

            //Adaugam produsele in view model
            // viewModel.Products = products;
            viewModel.Products = products.ToPagedList(currentPage, ItemsPerPage);
            viewModel.SortBy = sortBy;

            //popularea listei cu criteriile de sortare
            viewModel.Sorts = new Dictionary<string, string>
            {
                { "Pret crescator","lowest_price" },
                { "Pret descrescator","highest_price" }
            };

            //Returnam View-ul.
            // return View(products.ToList());
            return View(viewModel);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
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

        // GET: Products/Create
        public ActionResult Create()
        {
            // ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name");
            //  return View();
            ProductViewModel viewModel = new ProductViewModel();
            viewModel.CategoryList = new SelectList(db.Categories, "ID", "Name");
            viewModel.ImageList = new List<SelectList>();
            for (int i = 0; i < 5; i++)
            { viewModel.ImageList.Add(new SelectList(db.productImages, "ID", "FileName")); }
            return View(viewModel);
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       // public ActionResult Create([Bind(Include = "ID,Isbn,Author,Title,Description,Price,CategoryID")] Product product)
         public ActionResult Create (ProductViewModel viewModel)
        {
            Product product = new Product();
            product.Isbn = viewModel.Isbn;
            product.Author = viewModel.Author;
            product.Title = viewModel.Title;
            product.Description = viewModel.Description;
            product.Price = viewModel.Price;
            product.CategoryID = viewModel.CategoryID;
            product.ProductImageMap = new List<ProductImageMap>();
            //Se elimina spatiile goale din lista imaginilor
            string[] productImages = viewModel.ProductImages.Where(img => !string.IsNullOrEmpty(img)).ToArray();
            //Se adauga imaginile produsului
            for (int i = 0;i < productImages.Length; i++)
            {
                product.ProductImageMap.Add(new ProductImageMap { ProductImage = db.productImages.Find(int.Parse(productImages[i])), ImageNumber = i });
            }

            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            // ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", product.CategoryID);
            viewModel.CategoryList = new SelectList(db.Categories, "ID", "Name", product.CategoryID);
            viewModel.ImageList = new List<SelectList>();
            for(int i=0;i<5;i++)
            { viewModel.ImageList.Add(new SelectList(db.productImages, "ID", "FileName", viewModel.ProductImages[i])); }

            // return View(product);
            return View(viewModel);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", product.CategoryID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Isbn,Author,Title,Description,Price,CategoryID")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", product.CategoryID);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
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

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
