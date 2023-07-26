using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EvdeEczane.Models;
namespace EvdeEczane.Controllers
{
    public class AramaController : Controller
    {
        EVDEECZANEEntities3 db = new EVDEECZANEEntities3();
        // GET: Arama
        public ActionResult Index(string p)
        {
            var liste = from d in db.Stok select d;
            if (!string.IsNullOrEmpty(p))
            {
                liste = liste.Where(x => x.StokAdi.Contains(p));
            }
            var urun = liste.ToList();
            var model = db.Menuler.Where(x => x.ParentID == null && x.AltMenuID == null).ToList();
            ViewBag.Kategoriler = model;
            ViewBag.Urunler = liste.ToList();
            ViewBag.StokSayisi = urun.Count();
            return View();
        }
    }
}