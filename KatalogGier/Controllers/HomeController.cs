﻿using System;
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
       

        public ActionResult Index()
        {
            var gry = new List<Gra> { new Gra { Tytul = "Heroes III" } };

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


    }
}