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
    public class Sub_CategoryController : Controller
    {
        private Model1 db = new Model1();

        // GET: Sub_Category
        public ActionResult Index()
        {
            var sub_Category = db.Sub_Category.Include(s => s.Category);
            return View(sub_Category.ToList());
        }
        [HttpPost]
        public ActionResult Index(string s)
        {
            if (string.IsNullOrEmpty(s)==false)
            {
                List<Sub_Category> li = db.Sub_Category.Where(x => x.Sub_Category_Name.StartsWith(s)).ToList();
                return View(li);
            }
            else
            {
                var sub = db.Sub_Category.ToList();
                return View(sub);
            }
            
        }
    

        // GET: Sub_Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sub_Category sub_Category = db.Sub_Category.Find(id);
            if (sub_Category == null)
            {
                return HttpNotFound();
            }
            return View(sub_Category);
        }

        // GET: Sub_Category/Create
        public ActionResult Create()
        {
            ViewBag.Category_Fid = new SelectList(db.Categories, "Category_Id", "Category_Name");
            return View();
        }

        // POST: Sub_Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sub_Category_ID,Sub_Category_Name,Category_Fid")] Sub_Category sub_Category)
        {
            if (ModelState.IsValid)
            {
                db.Sub_Category.Add(sub_Category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Category_Fid = new SelectList(db.Categories, "Category_Id", "Category_Name", sub_Category.Category_Fid);
            return View(sub_Category);
        }

        // GET: Sub_Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sub_Category sub_Category = db.Sub_Category.Find(id);
            if (sub_Category == null)
            {
                return HttpNotFound();
            }
            ViewBag.Category_Fid = new SelectList(db.Categories, "Category_Id", "Category_Name", sub_Category.Category_Fid);
            return View(sub_Category);
        }

        // POST: Sub_Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sub_Category_ID,Sub_Category_Name,Category_Fid")] Sub_Category sub_Category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sub_Category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Category_Fid = new SelectList(db.Categories, "Category_Id", "Category_Name", sub_Category.Category_Fid);
            return View(sub_Category);
        }

        // GET: Sub_Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sub_Category sub_Category = db.Sub_Category.Find(id);
            if (sub_Category == null)
            {
                return HttpNotFound();
            }
            return View(sub_Category);
        }

        // POST: Sub_Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sub_Category sub_Category = db.Sub_Category.Find(id);
            db.Sub_Category.Remove(sub_Category);
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
