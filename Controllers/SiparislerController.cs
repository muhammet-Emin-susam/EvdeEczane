using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EvdeEczane.Models;
namespace EvdeEczane.Controllers
{
    public class SiparislerController : Controller
    {
        EVDEECZANEEntities3 db = new EVDEECZANEEntities3();
        // GET: Siparisler
        public ActionResult Index()
        {
            var ID = Convert.ToInt32(Session["ID"]);
            var model2 = db.Menuler.Where(x => x.ParentID == null && x.AltMenuID == null).ToList();
            ViewBag.Menuler = model2;
            var model = db.Siparisler.Where(x => x.KullaniciID == ID).ToList();
            return View(model);
        }
    }
}