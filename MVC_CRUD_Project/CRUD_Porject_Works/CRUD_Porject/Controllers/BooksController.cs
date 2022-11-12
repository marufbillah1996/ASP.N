using CRUD_Porject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using CRUD_Porject.ViewModel;
using System.IO;

namespace CRUD_Porject.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly BookSellerDbContext db = new BookSellerDbContext();
        // GET: Books
        public ActionResult Index()
        {
            var data = db.Books
                .Include(x=>x.BookAuthors.Select(y => y.Author))
                .ToList();

            return View(data);
        }
        public ActionResult Create()
        {
            ViewBag.Publishers = db.Publishers.ToList();
            ViewBag.Genres = db.Genres.ToList();
            var data = new BookInputModel();
            data.Authors.Add(new AuthorViewModel());
            return View(data);
        }
        [HttpPost]
        public ActionResult Create(BookInputModel model, int[] AuthorID)
        {
            if (ModelState.IsValid)
            {
                var ba = new Book
                {
                    BookName = model.Title,
                    Price = model.Price,
                    PublishDate = model.PublishDate,
                    Available = model.Available,
                    GenreID = model.GenreID,
                    PublisherID = model.PublisherID,
                   
                };

               foreach(var i in AuthorID)
                {
                    ba.BookAuthors.Add(new BookAuthor { AuthorID = i });
                }
                if (model.Picture.ContentLength > 0)
                {
                    string ext = Path.GetExtension(model.Picture.FileName);
                    string fileName = Guid.NewGuid() + ext;
                    model.Picture.SaveAs(Path.Combine(Server.MapPath("~/Uploads"), fileName));
                    ba.Picture = fileName;
                }
                db.Books.Add(ba);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           
            ViewBag.Genres = db.Genres.ToList();

            return View(model);
        }
        public JsonResult GetAddress(int id)
        {
            var t = db.Authors.FirstOrDefault(x => x.AuthorID == id);
            return Json(t.AuthorAddress);
        }
        public ActionResult CreateNewField(BookInputModel data)
        {
            ViewBag.Authors = db.Authors.ToList();
            
            return PartialView();
        }
    }
}