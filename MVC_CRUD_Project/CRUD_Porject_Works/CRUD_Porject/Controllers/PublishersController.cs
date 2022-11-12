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
    public class PublishersController : Controller
    {
        readonly BookSellerDbContext database = new BookSellerDbContext();
        // GET: Authors
        public ActionResult Index()
        {
            return View(database.Publishers.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        public PartialViewResult CreatePublisher()
        {
            return PartialView("_CreatePublisher");
        }
        [HttpPost]
        public PartialViewResult CreatePublisher(Publisher p)
        {
            Thread.Sleep(2000);
            if (ModelState.IsValid)
            {
                database.Publishers.Add(p);
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
        public PartialViewResult EditPublisher(int id)
        {
            var p = database.Publishers.First(x => x.PublisherID == id);
            return PartialView("_EditPublisher", p);
        }
        [HttpPost]
        public PartialViewResult EditPublisher(Publisher p)
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
            return View(database.Publishers.First(x => x.PublisherID == id));
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DoDelete(int PublisherID)
        {
            var a = new Publisher { PublisherID = PublisherID };
            database.Entry(a).State = EntityState.Deleted;
            database.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}