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
    public class GenresController : Controller
    {
        readonly BookSellerDbContext database = new BookSellerDbContext();
        // GET: Authors
        public ActionResult Index()
        {
            return View(database.Genres.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        public PartialViewResult CreateGenre()
        {
            return PartialView("_CreateGenre");
        }
        [HttpPost]
        public PartialViewResult CreateGenre(Genre g)
        {
            Thread.Sleep(2000);
            if (ModelState.IsValid)
            {
                database.Genres.Add(g);
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
        public PartialViewResult EditGenre(int id)
        {
            var b = database.Genres.First(x => x.GenreID == id);
            return PartialView("_EditGenre", b);
        }
        [HttpPost]
        public PartialViewResult EditGenre(Genre g)
        {
            Thread.Sleep(2000);
            if (ModelState.IsValid)
            {
                database.Entry(g).State = EntityState.Modified;
                database.SaveChanges();
                return PartialView("_Success");
            }
            return PartialView("_Fail");
        }
        public ActionResult Delete(int id)
        {
            return View(database.Genres.First(x => x.GenreID == id));
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DoDelete(int GenreID)
        {
            var a = new Genre { GenreID = GenreID };
            database.Entry(a).State = EntityState.Deleted;
            database.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}