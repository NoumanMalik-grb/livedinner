using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LiveDinner.Models;

namespace LiveDinner.Controllers
{
    public class ProductsController : Controller
    {
        private Model1 db = new Model1();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Product1);
            return View(products.ToList());
        }
        [HttpPost]
        public ActionResult Index(string s, string se)
        {
            if (string.IsNullOrEmpty(s)==false)
            {
                if (se=="name")
                {
                    List<Product> pr = db.Products.Where(x => x.Product_Name.StartsWith(se)).ToList();
                    return View(pr);
                }
                List<Product> li = db.Products.Where(x => x.Product_Name.StartsWith(s) ||x.Product_Sale_Price.ToString().StartsWith(s)).ToList();
                return View(li);
            }
            else
            {
                var pro = db.Products.ToList();
                return View(pro);
            }
            
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
            ViewBag.Sub_Category_Fid = new SelectList(db.Products, "Product_Id", "Product_Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                product.pro_img.SaveAs(Server.MapPath("~/Content/productpic/" + product.pro_img.FileName));
                product.Product_Picture = "~/Content/productpic/" + product.pro_img.FileName;
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Sub_Category_Fid = new SelectList(db.Products, "Product_Id", "Product_Name", product.Sub_Category_Fid);
            return View(product);
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
            ViewBag.Sub_Category_Fid = new SelectList(db.Products, "Product_Id", "Product_Name", product.Sub_Category_Fid);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.pro_img!=null)
                {
                    product.pro_img.SaveAs(Server.MapPath("~/Content/productpic/" + product.pro_img.FileName));
                    product.Product_Picture = "~/Content/productpic/" + product.pro_img.FileName;
                }
                
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Sub_Category_Fid = new SelectList(db.Products, "Product_Id", "Product_Name", product.Sub_Category_Fid);
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
