using System;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using OnlineLibrary1.Models;

namespace OnlineLibrary1
{
    public class ProductImagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ProductImages
        public ActionResult Index()
        {
            return View(db.productImages.ToList());
        }

        // GET: ProductImages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductImage productImage = db.productImages.Find(id);
            if (productImage == null)
            {
                return HttpNotFound();
            }
            return View(productImage);
        }

        // GET: ProductImages/Create
        public ActionResult Upload()
        {
            return View();
        }

        // POST: ProductImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // public ActionResult Upload([Bind(Include = "ID,FileName")] HttpPostedFileBase file)
        public ActionResult Upload(HttpPostedFileBase[] files)
        {
            bool AllValid = true; string invalidFiles = "";

            //verificam daca exista fisiere adaugate
            if (files[0] != null)
            {
                //se verifica sa nu se incarce mai mult de 10 fisiere
                if (files.Length <= 10)
                {
                    // Se verifica  recursiv toate fisierele din vector pentru a fi valide
                    foreach (var file in files)
                    {
                        //Daca cel putin un fisier nu este valid ,se seteaza flagul si se memoreaza numele sau
                        if (!ValidateFile(file)) { AllValid = false; invalidFiles += file.FileName + ", "; }
                    }
                    //Daca toate fisierele sunt validate, se incearca incarcarea lor in sistemul de fisiere
                    if (AllValid)
                    {
                        foreach (var file in files)
                        {
                            try { SaveFile(file); }
                            catch (Exception)
                            { ModelState.AddModelError("FileName", "A apărut o eroare la încărcarea fișierelor pe server. Vă rugăm să încercați din nou"); }
                        }
                    }
                    //Daca exista cel putin un fisier nevalid, se genereaza o eroare
                    else
                    {
                        ModelState.AddModelError("FileName", "Toate fișierele trebuie să fie în format GIF, PNG, JPEG sau JPG, iar dimensiunea lor trebuie să fie mai mică de 2MB." + "Urmatoarele fisiere" + invalidFiles + "se pare ca nu sunt valide.");
                    }

                }
                // daca s-au introdus mai mult de 10 fisiere,se genereaza o eroare
                else { ModelState.AddModelError("FileName", "Va rugam sa nu introduceti mai mult de 10 fisiere odata"); }
            }
            //daca nu s-a introdus niciun fisier,se va genera o eroare
            else { ModelState.AddModelError("FileName", "Vă rugăm să introduceți cel puțin un fișier"); }

            //Verificarea validitatii modelului si salvarea in baza de date
            if (ModelState.IsValid)
            {
                //se definesc variabilele de control pentru inregistrarea in baza de date
                bool duplicate = false, otherError = false; string duplicateFiles = "";
                //Se salveaza fiecare imagine din vector
                foreach (var file in files)
                {
                    // Se declara un nou obiect de tip fisier si se inregistreaza numele in baza de date
                    var bookImage = new ProductImage { FileName = System.IO.Path.GetFileName(file.FileName) };
                    try { db.productImages.Add(bookImage);
                        db.SaveChanges(); }
                    catch (DbUpdateException e)
                    {
                        SqlException innerException = e.InnerException.InnerException as SqlException;
                        if (innerException != null && innerException.Number == 2601)
                        { duplicateFiles += ", " + file.FileName; duplicate = true;
                            //detasarea contextului de date
                            db.Entry(bookImage).State = EntityState.Detached;
                        }
                        else { otherError = true; }
                    }
                }
                //Daca exista fisiere duplicat in baza de date,acestea vor fi afisate
                if (duplicate)
                {
                    ModelState.AddModelError("FileName", "Toate fișierele au fost încărcate, cu excepția următoarelor: " + duplicateFiles + ", care deja există." +
                " Vă rugăm să le ștergeți înainte de a le putea adăuga din nou");
                    return View();
                }
                //Daca a aparut alta eroare,se va afisa un mesaj generic
                else if (otherError)
                {
                    ModelState.AddModelError("FileName", "Ne pare rău, însă a apărut o eroare la înregistrarea fișierelor în baza de date.Vă rugăm să încercați din nou.");
                    return View();
                }
                return RedirectToAction("Index");
            }
           
            //Returnarea vizualizarii implicite
            return View();
        }

        

        // GET: ProductImages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductImage productImage = db.productImages.Find(id);
            if (productImage == null)
            {
                return HttpNotFound();
            }
            return View(productImage);
        }

        // POST: ProductImages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FileName")] ProductImage productImage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productImage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productImage);
        }

        // GET: ProductImages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductImage productImage = db.productImages.Find(id);
            if (productImage == null)
            {
                return HttpNotFound();
            }
            return View(productImage);
        }

        // POST: ProductImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductImage productImage = db.productImages.Find(id);
            db.productImages.Remove(productImage);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //Metoda de validare a tipului de fisier incarcat
        private bool ValidateFile(HttpPostedFileBase file)
        {
            //Memoram intr-o variabila extensia fisierului incarcat
            string extension = System.IO.Path.GetExtension(file.FileName).ToLower();
            //Memoram intr-un vector tipurile de extensii permise
            string[] allowedExtensions = { ".gif", ".jpg", "jpeg", "png" };
            // se verifica daca fisierul are pana in 2Mb, nu este un fisier gol si are o extensie acceptata
            if (file.ContentLength > 0 && file.ContentLength < 2097152 && allowedExtensions.Contains(extension)) { return true; }
            //Daca aceste criterii nu se indeplinesc, se returneaza fals.
            return false;
        }

        //Metoda de redimensionare pentru diapozitiv si de salvare a imaginii in sistemul de fisiere
        private void SaveFile(HttpPostedFileBase file)
        {
            //Memoram intr-o variabila numele fisierului incarcat si construim un obiect de tip imagine  
            string fileName = System.IO.Path.GetFileName(file.FileName);
            WebImage image = new WebImage(file.InputStream);
            //Daca imaginea este prea lata, o redimensionam la 200 de pixeli si o salvam in sistemul de fisiere
            if (image.Width > 200) { image.Resize(200, image.Height); }
            image.Save(Constants.ProductImagePath + fileName);
            //Se construieste diapozitivul imaginii si se salveaza in sistemul de fisiere
            image.Resize(100, image.Height); image.Save(Constants.ProductThumbnailPath + fileName);
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
