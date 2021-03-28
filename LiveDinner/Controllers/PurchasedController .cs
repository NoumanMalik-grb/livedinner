using LiveDinner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;


namespace LiveDinner.Controllers
{
    public class PurchasedController : Controller
    {
        Model1 db = new Model1();
                
        public ActionResult Purchased(int? id)
        {
            //menu model has two list 1 is category list and second is product list 
            Classproducts classproducts = new Classproducts();
            classproducts.cat = db.Categories.ToList();
            if (id==null)
            {
                classproducts.pro = db.Products.ToList();
            }
            else
            {
                classproducts.pro = db.Products.Where(p => p.Sub_Category_Fid == id).ToList();
            }
            return View(classproducts); 
        }
        public ActionResult buyer()
        {
            return View();
        }


        // add to cart working
        
        public ActionResult Purchasedcart()
        {
            return View();
        }

public ActionResult AddtoCard(int id)
        {
            List<Product> listcart;
            if (Session["menucart"]==null)
            {
                listcart = new List<Product>();
            }
            else
            {
                listcart = (List<Product>)Session["menucart"];
                    }
            listcart.Add(db.Products.Where(p => p.Product_Id == id).FirstOrDefault());
            Session["menucart"] = listcart;
            listcart[listcart.Count-1].Product_Quantity = 1;
            return RedirectToAction("PurchasedCart");
        }
        
        public ActionResult Plus(int RowNo)
        {
            List<Product> listcart =  (List<Product>)Session["menucart"];
            Session["menucart"] = listcart;
            listcart[RowNo].Product_Quantity+=4;
            return RedirectToAction("PurchasedCart");
        }
        public ActionResult Minus(int RowNo)
        {
            List<Product> listcart = (List<Product>)Session["menucart"];
            Session["menucart"] = listcart;
            listcart[RowNo].Product_Quantity-=3;
            return RedirectToAction("PurchasedCart");
        }
        public ActionResult remove(int RowNo)
        {
            List<Product> listcart = (List<Product>)Session["menucart"];            
            Session["menucart"] = listcart;
            listcart.RemoveAt(RowNo);
            return RedirectToAction("PurchasedCart");
        }
        //working with paypall
        public ActionResult buyno(Order od)
        {
            od.Order_Status = "Paid";
            od.Order_Type = "Purchased";
            od.Order_Delivery_Status = "deliver";
            od.Order_Date_Time = System.DateTime.Now;
            Session["Order"] = od;
            // payment by paypal
            //Response.Redirect("https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_xclick&business=cookbookand@gmail.com&qantity=1&currency=USD&return=https://localhost:44324/Home/OrderBooked&amount=" + double.Parse(Session["TotalSaleAmount"].ToString()) / 167);
            return RedirectToAction("PurchaseOrder");

        }
        // working for the order booked 
        public ActionResult PurchaseOrder()
        {
            Order od=(Order)Session["Order"];
  
            //data save in oder table
            db.Orders.Add(od);
            db.SaveChanges();
            //data save in order detail table
       List<Product> ca=(List<Product>)Session["menucart"];
            for (int i = 0; i < ca.Count; i++)
            {
                Order_Details order = new Order_Details();
                //<====Start===>
                // query for fetch max id from order table
                int orderid = db.Orders.Max(x => x.Order_Id);
                order.Order_Fid = orderid;
                //<====end====>
                order.Product_Fid = ca[i].Product_Id;
                order.OD_Quantity = ca[i].Product_Quantity;
                order.OD_Purchase_Price = ca[i].Product_Purchase_price;
                order.OD_Sale_Price = ca[i].Product_Sale_Price;

                db.Order_Details.Add(order);
                db.SaveChanges();
            }
            
            return View(); 
        }
    }
    
}
