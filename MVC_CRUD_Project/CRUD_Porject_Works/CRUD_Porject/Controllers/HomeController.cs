using CRUD_Porject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD_Porject.Controllers
{
    public class HomeController : Controller
    {
        BookSellerDbContext db = new BookSellerDbContext();
        // GET: Home
        public ActionResult Index()
        {
            var data = db.Authors.ToList();
            return View();
        }
    }
}