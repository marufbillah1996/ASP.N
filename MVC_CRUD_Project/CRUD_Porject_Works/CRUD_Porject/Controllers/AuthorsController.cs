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
    public class AuthorsController : Controller
    {
        readonly BookSellerDbContext database = new BookSellerDbContext();
        // GET: Authors
        public ActionResult Index()
        {
            return View(database.Authors.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        public PartialViewResult CreateAuthor()
        {
            return PartialView("_CreateAuthor");
        }
        [HttpPost]
        public PartialViewResult CreateAuthor(Author a)
        {
            Thread.Sleep(4000);
            if (ModelState.IsValid)
            {
                database.Authors.Add(a);
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
        public PartialViewResult EditAuthor(int id)
        {
            var b = database.Authors.First(x => x.AuthorID == id);
            return PartialView("_EditAuthor", b);
        }
        [HttpPost]
        public PartialViewResult EditAuthor(Author a)
        {
            Thread.Sleep(4000);
            if (ModelState.IsValid)
            {
                database.Entry(a).State = EntityState.Modified;
                database.SaveChanges();
                return PartialView("_Success");
            }
            return PartialView("_Fail");
        }
        public ActionResult Delete(int id)
        {
            return View(database.Authors.First(x => x.AuthorID == id));
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DoDelete(int AuthorId)
        {
            var a = new Author { AuthorID = AuthorId };
            database.Entry(a).State = EntityState.Deleted;
            database.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}