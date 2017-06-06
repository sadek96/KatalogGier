using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using KatalogGier.Models;

namespace KatalogGier.Controllers
{
    public class HomeController : Controller
    {
        protected static IMongoClient _client;
        protected static IMongoDatabase _database;


       
      //  string a = "~/Content/Images/Heroes.jpg";
        public ActionResult Index()
        {
            List<Gra> gry = new List<Gra> {
                new Gra { Tytul = "Heroes III", Zdjecie = null, Platforma = new string [] { "PC" }  },
                new Gra { Tytul = "Diablo II", Gatunek = new string [] {"HackAndSlash" } , Zdjecie  = "/Content/Images/Heroes.jpg"}
            };

            ViewBag.Gry = gry;

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

        public ActionResult Recenzje()
        {
            ViewBag.Message = "Your contact page.";

            return RedirectToAction("Index");
        }

    }
}