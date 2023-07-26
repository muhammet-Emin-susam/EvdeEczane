using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EvdeEczane.Models;

namespace EvdeEczane.Controllers
{
    public class HakkimizdaController : Controller
    {
        EVDEECZANEEntities3 db = new EVDEECZANEEntities3();
        // GET: Hakkimizda
        public ActionResult Index()
        {
            var model = db.Bilgiler.FirstOrDefault();
            return View(model);
        }
    }
}