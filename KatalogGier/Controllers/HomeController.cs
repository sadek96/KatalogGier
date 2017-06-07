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
        public KatalogGierRepository Context = new KatalogGierRepository();


        //  string a = "~/Content/Images/Heroes.jpg";
        public ActionResult Index()
        {

            return View("Index", Context.GetAllGames());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult About(string Id)
        {
            ViewBag.Message = "Recenzje Gry";
            if (Id == null||Id == "")
            {
                return View(new AddRecenzjeModel() {
            
                    Gra = null,
                    Recenzja = new Recenzja()
                });
            }
            return View(new AddRecenzjeModel()
            {
                Gra = Context.GetGameById(new ObjectId(Id)),
                Recenzja = new Recenzja()
            });

        }

        [HttpPost]
        public ActionResult About(AddRecenzjeModel model)
        {
            model.Gra.Recenzje.Add(model.Recenzja);

            Context.Update(model.Gra.ID, model.Gra);

            return RedirectToAction("About", model.Gra.ID);
        }
    }
}