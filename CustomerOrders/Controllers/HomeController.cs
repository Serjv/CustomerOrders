using CustomerOrders.DAL;
using CustomerOrders.Models;
using CustomerOrders.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerOrders.Controllers
{
    public class HomeController : Controller
    {
        SampleContext db = new SampleContext();
        

        public ActionResult Index()
        {
            //var customers = db.Customers.OrderBy(q => q.Name).ToList();
            var result = from cust in db.Customers
                         join ol in db.OrderLists on cust.ID equals ol.CustomerID
                         join oi in db.OrderInfos on ol.ID equals oi.OrderListID
                         group oi by new { cust.ID, cust.Name, cust.Address, cust.Category, oi.Total } into g
                         orderby g.Key.ID
                         select new CustomerTotal
                         {
                             ID = g.Key.ID,
                             Name = g.Key.Name,
                             Address = g.Key.Address,
                             Category = g.Key.Category,
                             Summa = g.Sum(oi => oi.Total)
                         };

            return View(result);
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

        public ActionResult OrderList(int id)
        {
            var result = from cust in db.Customers
                         where cust.ID == id
                         join ol in db.OrderLists on cust.ID equals ol.CustomerID
                         join oi in db.OrderInfos on ol.ID equals oi.OrderListID
                         select new OrderTotal
                         {
                             ID = oi.ID,
                             Name = cust.Name,
                             Date = ol.Date,
                             OrderID = oi.OrderListID,
                             ProductID = oi.ProductID,
                             Price = oi.Price,
                             Amount = oi.Amount,
                             Total = oi.Total
                         };

            return View("OrderList", result);
        }

        public ActionResult Stat()
        {
            ViewBag.VIP = (from cust in db.Customers
                          where cust.Category == "VIP"
                          select cust.Category).Count();
            ViewBag.Top = (from cust in db.Customers
                           where cust.Category == "Top"
                           select cust.Category).Count();
            ViewBag.Mid = (from cust in db.Customers
                           where cust.Category == "Middle"
                           select cust.Category).Count();
            ViewBag.Std = (from cust in db.Customers
                           where cust.Category == "Std"
                           select cust.Category).Count();
            var stats = from cust in db.Customers
                                      join ol in db.OrderLists on cust.ID equals ol.CustomerID
                                      join oi in db.OrderInfos on ol.ID equals oi.OrderListID
                                      select new Stats
                                      {
                                          Category = cust.Category,
                                          Total = oi.Total
                                      };

            ViewBag.sum = stats.Where(x => x.Category == "VIP").Sum(x=> x.Total);
            ViewBag.sum2 = stats.Where(x => x.Category == "Top").Sum(x => x.Total);
            ViewBag.sum3 = stats.Where(x => x.Category == "Middle").Sum(x => x.Total);
            ViewBag.sum4 = stats.Where(x => x.Category == "Std").Sum(x => x.Total);

            return View("Stat");
        }


        public ActionResult Edit(int id)
        {
            var result = from cust in db.Customers
                         
                         join ol in db.OrderLists on cust.ID equals ol.CustomerID
                         join oi in db.OrderInfos on ol.ID equals oi.OrderListID
                         where oi.ID == id
                         select new OrderTotal
                         {
                             ID = oi.ID,
                             Name = cust.Name,
                             Date = ol.Date,
                             OrderID = oi.OrderListID,
                             ProductID = oi.ProductID,
                             Price = oi.Price,
                             Amount = oi.Amount,
                             Total = oi.Total
                         };
            var temp = result.ToList();

            return View("Edit", temp[0]);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Date,OrderID,ProductID,Price,Amount,Total ")] OrderTotal orderTotal)
        {

            OrderInfo orderinfo = db.OrderInfos.Find(orderTotal.ID);
            orderinfo.Price = orderTotal.Price;
            orderinfo.Amount = orderTotal.Amount;
            orderinfo.Total = orderTotal.Price * orderTotal.Amount;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

            public ActionResult Delete(int id)
        {
            var result = from cust in db.Customers

                         join ol in db.OrderLists on cust.ID equals ol.CustomerID
                         join oi in db.OrderInfos on ol.ID equals oi.OrderListID
                         where oi.ID == id
                         select new OrderTotal
                         {
                             ID = oi.ID,
                             Name = cust.Name,
                             Date = ol.Date,
                             OrderID = oi.OrderListID,
                             ProductID = oi.ProductID,
                             Price = oi.Price,
                             Amount = oi.Amount,
                             Total = oi.Total
                         };
            var temp = result.ToList();

            return View("Delete", temp[0]);

            
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            
            OrderInfo orderinfo = db.OrderInfos.Find(id);
            db.OrderInfos.Remove(orderinfo);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}