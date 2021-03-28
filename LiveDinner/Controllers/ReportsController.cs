using LiveDinner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiveDinner.Controllers
{
    public class ReportsController : Controller
    {
        Model1 db = new Model1();
        // GET: Reports
        //working for the purchased reports
        public ActionResult PurchaseReport(FilterModel filterModel)
        {
            // strat code for filters product &category
            if (filterModel.DateFrom == null)
            {
                ViewBag.DateFrom = System.DateTime.Today.ToString("s");
                filterModel.DateFrom = System.DateTime.Today;
            }
            else
            {
                ViewBag.DateFrom = Convert.ToDateTime(filterModel.DateFrom).ToString("s");
            }
            if (filterModel.DateTo == null)
            {

                ViewBag.DateTo = System.DateTime.Now.ToString("s");
                filterModel.DateTo = System.DateTime.Now;
            }
            else
            {
                ViewBag.DateTo = Convert.ToDateTime(filterModel.DateTo).ToString("s");
            }
            ViewBag.Category = db.Categories.Select(x => new SelectListItem { Value = x.Category_Id.ToString(), Text = x.Category_Name });

            if (filterModel.Category == null)
            {
                ViewBag.Product = db.Products.Select(x => new SelectListItem { Value = x.Product_Id.ToString(), Text = x.Product_Name + " (" + x.Product_Description + " )" });
            }
            else
            {
                ViewBag.Product = db.Products.Where(x => x.Sub_Category_Fid == filterModel.Category).Select(x => new SelectListItem { Value = x.Product_Id.ToString(), Text = x.Product_Name + " (" + x.Product_Description + " )" });
            }

            var od = db.Order_Details.Select(x => x.Order_Fid).ToList();
            if (filterModel.Category != null)
            {
                var p = db.Products.Where(i => i.Sub_Category_Fid == filterModel.Category).Select(i => i.Product_Id).ToList();

                if (filterModel.Product != null)
                {
                    p = db.Products.Where(i => i.Product_Id == filterModel.Product).Select(i => i.Product_Id).ToList();
                }
                od = db.Order_Details.Where(i => p.Contains(i.Product_Fid)).Select(i => i.Order_Fid).ToList();
            }

            // End code for filters product &category


            var sr = db.Orders.Where(s => s.Order_Type == "Purchased" & s.Order_Date_Time >= filterModel.DateFrom & s.Order_Date_Time <= filterModel.DateTo & od.Contains(s.Order_Id)).OrderByDescending(x => x.Order_Id).ToList();
            return View(sr);
        }
        public ActionResult Invoice(int id,String Purchase )
        {
           
            ViewBag.id = id;
            ViewBag.Purchase = Purchase;


            return View();
        }
        //working for the sale report
        public ActionResult SaleReport(FilterModel filterModel) 
        {
            // strat code for filters product &category
            if (filterModel.DateFrom==null)
            {
                ViewBag.DateFrom = System.DateTime.Today.ToString("s");
                filterModel.DateFrom= System.DateTime.Today;
            }
            else
            {
                ViewBag.DateFrom = Convert.ToDateTime(filterModel.DateFrom).ToString("s");
            }
            if (filterModel.DateTo==null)
            {
                
                ViewBag.DateTo = System.DateTime.Now.ToString("s");
                filterModel.DateTo = System.DateTime.Now;
            }
            else
            {
                ViewBag.DateTo = Convert.ToDateTime(filterModel.DateTo).ToString("s");
            }
            ViewBag.Category= db.Categories.Select(x=>new SelectListItem {Value=x.Category_Id.ToString(),Text=x.Category_Name });

            if (filterModel.Category==null)
            {
                ViewBag.Product = db.Products.Select(x => new SelectListItem { Value = x.Product_Id.ToString(), Text = x.Product_Name + " (" + x.Product_Description + " )" });
            }
            else
            {
                ViewBag.Product = db.Products.Where(x=>x.Sub_Category_Fid==filterModel.Category).Select(x => new SelectListItem { Value = x.Product_Id.ToString(), Text = x.Product_Name + " (" + x.Product_Description + " )" });
            }

            var od = db.Order_Details.Select(x =>x.Order_Fid).ToList();
            if (filterModel.Category!=null)
            {
                var p = db.Products.Where(i=>i.Sub_Category_Fid==filterModel.Category).Select(i => i.Product_Id).ToList();

                if (filterModel.Product!=null)
                {
                    p = db.Products.Where(i => i.Product_Id == filterModel.Product).Select(i => i.Product_Id).ToList();
                }
                od = db.Order_Details.Where(i => p.Contains(i.Product_Fid)).Select(i => i.Order_Fid).ToList();
            }
            
            // End code for filters product &category


            var sr = db.Orders.Where(s => s.Order_Type == "Sale" & s.Order_Date_Time>=filterModel.DateFrom & s.Order_Date_Time<=filterModel.DateTo & od.Contains(s.Order_Id)).OrderByDescending(x=>x.Order_Id).ToList();
            return View(sr);
        }
        public ActionResult ProfitAndLossReport( FilterModel filterModel)
        {
            // strat code for filters product &category
            if (filterModel.DateFrom == null)
            {
                ViewBag.DateFrom = System.DateTime.Today.ToString("s");
                filterModel.DateFrom = System.DateTime.Today;
            }
            else
            {
                ViewBag.DateFrom = Convert.ToDateTime(filterModel.DateFrom).ToString("s");
            }
            if (filterModel.DateTo == null)
            {

                ViewBag.DateTo = System.DateTime.Now.ToString("s");
                filterModel.DateTo = System.DateTime.Now;
            }
            else
            {
                ViewBag.DateTo = Convert.ToDateTime(filterModel.DateTo).ToString("s");
            }
            ViewBag.Category = db.Categories.Select(x => new SelectListItem { Value = x.Category_Id.ToString(), Text = x.Category_Name });

            if (filterModel.Category == null)
            {
                ViewBag.Product = db.Products.Select(x => new SelectListItem { Value = x.Product_Id.ToString(), Text = x.Product_Name + " (" + x.Product_Description + " )" });
            }
            else
            {
                ViewBag.Product = db.Products.Where(x => x.Sub_Category_Fid == filterModel.Category).Select(x => new SelectListItem { Value = x.Product_Id.ToString(), Text = x.Product_Name + " (" + x.Product_Description + " )" });
            }

            var od = db.Order_Details.Select(x => x.Order_Fid).ToList();
            if (filterModel.Category != null)
            {
                var p = db.Products.Where(i => i.Sub_Category_Fid == filterModel.Category).Select(i => i.Product_Id).ToList();

                if (filterModel.Product != null)
                {
                    p = db.Products.Where(i => i.Product_Id == filterModel.Product).Select(i => i.Product_Id).ToList();
                }
                od = db.Order_Details.Where(i => p.Contains(i.Product_Fid)).Select(i => i.Order_Fid).ToList();
            }

            // End code for filters product &category


            var sr = db.Orders.Where(s => s.Order_Type == "Sale" & s.Order_Date_Time >= filterModel.DateFrom & s.Order_Date_Time <= filterModel.DateTo & od.Contains(s.Order_Id)).OrderByDescending(x => x.Order_Id).ToList();
            return View(sr);
        }
        public ActionResult StockReport(FilterModel filterModel) 
        {
            // strat code for filters product &category
            
            if (filterModel.DateTo == null)
            {

                ViewBag.DateTo = System.DateTime.Now.ToString("s");
                filterModel.DateTo = System.DateTime.Now;
            }
            else
            {
                ViewBag.DateTo = Convert.ToDateTime(filterModel.DateTo).ToString("s");
            }
            ViewBag.Category = db.Categories.Select(x => new SelectListItem { Value = x.Category_Id.ToString(), Text = x.Category_Name });

            if (filterModel.Category == null)
            {
                ViewBag.Product = db.Products.Select(x => new SelectListItem { Value = x.Product_Id.ToString(), Text = x.Product_Name + " (" + x.Product_Description + " )" });
            }
            else
            {
                ViewBag.Product = db.Products.Where(x => x.Sub_Category_Fid == filterModel.Category).Select(x => new SelectListItem { Value = x.Product_Id.ToString(), Text = x.Product_Name + " (" + x.Product_Description + " )" });
            }

            var o = db.Products.ToList();
            if (filterModel.Category != null)
            {
                o = db.Products.Where(i => i.Sub_Category_Fid == filterModel.Category).ToList();

                if (filterModel.Product != null)
                {
                    o = db.Products.Where(i => i.Product_Id == filterModel.Product).ToList();
                }
                
            }

            // End code for filters product &category


            
            return View(o);
        }
    }
}