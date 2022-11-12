using CRUD_Porject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using CRUD_Porject.ViewModel;

namespace CRUD_Porject.Controllers
{
    [Authorize]
    public class SalesController : Controller
    {
        private readonly BookSellerDbContext db = new BookSellerDbContext();
        // GET: Sales
        public ActionResult Index()
        {
            var data = db.Customers
                .Include(x => x.Sales.Select(y => y.Book))
                .ToList();

            return View(data);
        }
        public ActionResult Create()
        {
            ViewBag.Customers = db.Customers.ToList();
            var data = new CustomerSaleInputModel();
            data.CustomerBooks.Add(new CustomerBookViewModel());
            return View(data);
        }
        [HttpPost]
        public ActionResult Create(CustomerSaleInputModel model, int[] BookID)
        {
            if (ModelState.IsValid)
            {
                var c = new Customer
                {
                    CustomersName = model.CustomersName,
                
                };

                foreach (var i in BookID)
                {
                    c.Sales.Add(new SaleDetail { BookID = i });
                }
                
                db.Customers.Add(c);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Customers = db.Customers.ToList();
            return View(model);
        }
        public ActionResult CreateNewField(CustomerSaleInputModel data)
        {
            ViewBag.Books = db.Books.ToList();

            return PartialView();
        }
    }
}