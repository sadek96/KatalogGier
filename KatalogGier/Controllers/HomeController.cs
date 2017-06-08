using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using KatalogGier.Models;
using System.Globalization;

namespace KatalogGier.Controllers
{
    public class HomeController : Controller
    {
        public KatalogGierRepository Context = new KatalogGierRepository();


        public ActionResult Index(string text = null)
        {


            return View("Index", Context.SearchForGames(text));
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Skontaktuj się z nami";

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
        public ActionResult About(AddRecenzjeModel model,string id)
        {
            model.Gra = Context.GetGameById(new ObjectId(id));

            model.Recenzja.Data_wstawienia = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (model.Gra.Recenzje != null)
                model.Gra.Recenzje.Add(model.Recenzja);
            else
                model.Gra.Recenzje = new List<Recenzja>()
                {
                    model.Recenzja
                };

            Context.Update(model.Gra.ID, model.Gra);

            return RedirectToAction("About", model.Gra.ID);
        }
    }
}