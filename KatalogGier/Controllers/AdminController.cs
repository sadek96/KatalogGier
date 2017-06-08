using KatalogGier.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KatalogGier.Controllers
{
    public class AdminController : Controller
    {

        public KatalogGierRepository Context = new KatalogGierRepository();

        // GET: Admin
        public ActionResult Index()
        {
            return View(Context.GetAllGames());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Gra gra)
        {
            Context.Add(gra);

            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {

            return View(Context.GetGameById(new ObjectId(id)));
        }

        [HttpPost]
        public ActionResult Edit(Gra g)
        {
            return RedirectToAction("Index");
        }

        
        public ActionResult Delete(string id)
        {
            Context.Delete(new ObjectId(id));

            return RedirectToAction("Index");
        }

        public ActionResult AddGatunek(string id,string text)
        {
            Gra g = Context.GetGameById(new ObjectId(id));

            if (g.Gatunek != null) { 
                string[] gatunki = g.Gatunek;

                string[] array = { text };

                g.Gatunek = gatunki.Concat(array).ToArray();
            }
            else
            {
                g.Gatunek = new string[]
                {
                    text
                };
            }
                

            Context.Update(new ObjectId(id), g);
            
            return RedirectToAction("Index");
        }

        public ActionResult DeleteGatunek(string id,string text)
        {
            Gra g = Context.GetGameById(new ObjectId(id));

            string[] gatunki = g.Gatunek;

            for(int i = 0; i < gatunki.Length; i++)
            {
                if (gatunki[i] == text)
                {
                    gatunki = gatunki.Except(new string[] { text }).ToArray();
                }
            }
            g.Gatunek = gatunki;

            Context.Update(new ObjectId(id), g);

            return RedirectToAction("Index");
        }

        public ActionResult DeletePlatforma(string id, string text)
        {
            Gra g = Context.GetGameById(new ObjectId(id));

            string[] gatunki = g.Platforma;

            for (int i = 0; i < gatunki.Length; i++)
            {
                if (gatunki[i] == text)
                {
                    gatunki = gatunki.Except(new string[] { text }).ToArray();
                }
            }

            g.Platforma = gatunki;

            Context.Update(new ObjectId(id), g);

            return RedirectToAction("Index");
        }

        public ActionResult AddPlatforma(string id, string text)
        {
            Gra g = Context.GetGameById(new ObjectId(id));

            if(g.Platforma!= null)
            { 
                string[] gatunki = g.Platforma;

                string[] array = { text };

                g.Platforma = gatunki.Concat(array).ToArray();
            }
            else
            {
                g.Platforma = new string[]
                {
                    text
                };
            }

            Context.Update(new ObjectId(id), g);

            return RedirectToAction("Index");
        }
    }
}