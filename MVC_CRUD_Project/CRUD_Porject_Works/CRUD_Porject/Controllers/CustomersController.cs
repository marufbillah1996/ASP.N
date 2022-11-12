using CRUD_Porject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace CRUD_Porject.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        readonly BookSellerDbContext database = new BookSellerDbContext();
        // GET: Authors
        public ActionResult Index()
        {
            return View(database.Customers.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        public PartialViewResult CreateCustomer()
        {
            return PartialView("_CreateCustomer");
        }
        [HttpPost]
        public PartialViewResult CreateCustomer(Customer p)
        {
            Thread.Sleep(2000);
            if (ModelState.IsValid)
            {
                database.Customers.Add(p);
                database.SaveChanges();
                return PartialView("_Success");
            }
            return PartialView("_fail");
        }
        public ActionResult Edit(int id)
        {
            ViewBag.Id = id;
            return View();
        }
        public PartialViewResult EditCustomer(int id)
        {
            var p = database.Customers.First(x => x.CustomerID== id);
            return PartialView("_EditCustomer", p);
        }
        [HttpPost]
        public PartialViewResult EditCustomer(Customer p)
        {
            Thread.Sleep(2000);
            if (ModelState.IsValid)
            {
                database.Entry(p).State = EntityState.Modified;
                database.SaveChanges();
                return PartialView("_Success");
            }
            return PartialView("_Fail");
        }
        public ActionResult Delete(int id)
        {
            return View(database.Customers.First(x => x.CustomerID == id));
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DoDelete(int CustomerID)
        {
            var a = new Customer { CustomerID = CustomerID };
            database.Entry(a).State = EntityState.Deleted;
            database.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}