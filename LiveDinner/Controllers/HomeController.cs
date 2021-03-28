using LiveDinner.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;


namespace LiveDinner.Controllers
{
    public class HomeController : Controller
    {
        Model1 db = new Model1();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult Contact(Contact_Us c)
        {
            c.Date_time = System.DateTime.Now;
            db.Contact_Us.Add(c);
            db.SaveChanges();

            return View();
        } 
        
        public ActionResult Menu(int? id)
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
        public ActionResult ManageAccount(int? id)
        {
            Account account = new Account();
            return View(db.Accounts.ToList());
        }
        public ActionResult IndexAdmin()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //login for the Admin
        public ActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminLogin(Account a)
        {
            Account d = db.Accounts.Where(x => x.Account_Email == a.Account_Email && x.Account_Password == a.Account_Password).FirstOrDefault();
            
            if (d!= null)
            {
                if (d.Account_Type=="Customer")
                {
                    Session["cus"] = d;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    Session["Admin"] = d;
                    return RedirectToAction("/IndexAdmin/Home");
                }
                
            }
            else
            {
                ViewBag.message = "invalid user name or password";
                return View();
            }

        } 
        public ActionResult Logout()
        {
            Session["Admin"] = null;
            return RedirectToAction("AdminLogin");
        }
        // Start custpmer logout
        public ActionResult CustomerLogout()
        {
            Session["cus"] = null;
            return RedirectToAction("Index");
        }
        //End custpmer logout

        // Start Signup for customer
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(Account account)
        {
            account.Account_Type = "Customer";
          
                db.Accounts.Add(account);
                db.SaveChanges();
                return RedirectToAction("AdminLogin");
                
        }

        //working for the order
        public ActionResult Customer()
        {
           
            return View();
        }

        //login for customer

       
// add to cart working

 public ActionResult menucart()
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
            Boolean isproductExist = false;
            foreach (var item in listcart)
            {
                if (id==item.Product_Id)
                {
                    isproductExist = true;
                    item.Product_Quantity++;
                }
            }
            if (isproductExist==false)
            {
                listcart.Add(db.Products.Where(p => p.Product_Id == id).FirstOrDefault());
                listcart[listcart.Count - 1].Product_Quantity = 1;
            }    
            //session srta 
            Session["menucart"] = listcart; 
            //session end
            return RedirectToAction("menucart");
        }
        
        public ActionResult Plus(int RowNo)
        {
            List<Product> listcart =  (List<Product>)Session["menucart"];
            int P_id = listcart[RowNo].Product_Id;
            int? available = db.Order_Details.Where(x => x.Product_Fid == P_id).Sum(x => x.OD_Quantity);
            if (available>listcart[RowNo].Product_Quantity)
            {
                listcart[RowNo].Product_Quantity++;
            }
            
            Session["menucart"] = listcart;
            return RedirectToAction("menucart");
        }
        public ActionResult Minus(int RowNo)
        {
            List<Product> listcart = (List<Product>)Session["menucart"];
            listcart[RowNo].Product_Quantity--;
            if (listcart[RowNo].Product_Quantity==0)
            {
                listcart.RemoveAt(RowNo);
            }
            Session["menucart"] = listcart;
            return RedirectToAction("menucart");
        }
        public ActionResult remove(int RowNo)
        {
            List<Product> listcart = (List<Product>)Session["menucart"];            
            Session["menucart"] = listcart;
            listcart.RemoveAt(RowNo);
            return RedirectToAction("menucart");
        }
        //working with paypall
        public ActionResult payno(Order od)
        {
            od.Order_Type = "Sale";
            od.Order_Delivery_Status = "deliver";
            od.Status = "Active";
            od.Order_Date_Time = System.DateTime.Now;
            // session start
            Session["Order"] = od;
            //session end
            if (Session["cus"]!=null)
            {
                Account c = (Account)Session["cus"];
                od.Account_Fid = c.Account_Id;
            }

            if (od.Order_Status == "Cash on deliver")
            {
                return RedirectToAction("Orderbooked");
            }
            else
            {
                // payment by paypal
                //Response.Redirect("https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_xclick&business=cookbookand@gmail.com&qantity=1&currency=USD&return=https://localhost:44324/Home/OrderBooked&amount=" + double.Parse(Session["TotalSaleAmount"].ToString()) / 167);
            }

            return View();

        }
        // working for the order booked 
        public ActionResult Orderbooked()
        {
            Order od=(Order)Session["Order"];
            // Send email to customer

            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("noumanuskt002@gmail.com");
                mail.To.Add(od.Order_Email.ToString());
                mail.Subject = "LiveDiner Order Confirmation.";
                mail.Body = "<b>Live & Diner</b><br/><p>Thanks For Your Order... we will devliver as soon as possible</p>";
                mail.IsBodyHtml = true;
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("LiveDinner", "AdminATH");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
            }
            catch
            {
            }
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
                order.OD_Quantity = ca[i].Product_Quantity* -1;
                order.OD_Purchase_Price = ca[i].Product_Purchase_price;
                order.OD_Sale_Price = ca[i].Product_Sale_Price;

                db.Order_Details.Add(order);
                db.SaveChanges();
            }
            
            return View(); 
        }
        public ActionResult CloseOrder()
        {
            Session["menucart"] = null;
            Session["Order"]= null;
            return RedirectToAction("Index");
        }

        public ActionResult EditCustomer(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCustomer( Account account)
        {
           
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");           
        }
        public ActionResult OrderHistory()
        {
            return View();
        }
        public ActionResult invoice(int id, String Purchase)
        {

            ViewBag.id = id;
            ViewBag.Purchase = Purchase;


            return View();
        }
        public ActionResult OrderCancel(int? id)
        {
            Order o = db.Orders.Where(x => x.Order_Id == id).FirstOrDefault();
            o.Status = "cancel";
            db.Entry(o).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("OrderHistory");
        }
        public ActionResult OrderActivate(int? id)
        {
            Order o = db.Orders.Where(x => x.Order_Id == id).FirstOrDefault();
            o.Status = "Active";
            db.Entry(o).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("OrderHistory");
        }
        public ActionResult CancelledOrder()
        {
            return View();
        }
    }
    }
    

