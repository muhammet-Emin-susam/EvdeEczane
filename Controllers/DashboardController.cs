using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EvdeEczane.Models;
namespace EvdeEczane.Controllers
{
    public class DashboardController : Controller
    {
        EVDEECZANEEntities3 db = new EVDEECZANEEntities3();
        // GET: Dashboard
        public ActionResult Index()
        {
            ViewBag.KullaniciSayisi = db.Kullanicilar.Count();
            ViewBag.StokSayisi = db.Stok.Count();
            ViewBag.BlogSayisi = db.Bloglar.Count();
            ViewBag.KampanyaSayisi = db.Kampanyalar.Count();
            ViewBag.MarkaSayisi = db.Marka.Count();
            return View();
        }
    }
}