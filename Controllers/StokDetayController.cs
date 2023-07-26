using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EvdeEczane.Models;

namespace EvdeEczane.Controllers
{
    public class StokDetayController : Controller
    {
        EVDEECZANEEntities3 db = new EVDEECZANEEntities3();
        // GET: StokDetay
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult StokDetay(int ID)
        {
            var model = db.Stok.Where(x => x.ID == ID).FirstOrDefault();
            return View(model);
        }
    }
}