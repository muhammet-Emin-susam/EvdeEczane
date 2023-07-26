using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EvdeEczane.Models;
namespace EvdeEczane.Controllers
{
    public class KullaniciAyarlariController : Controller
    {
        EVDEECZANEEntities3 db = new EVDEECZANEEntities3();
        // GET: KullaniciAyarlari
        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Kullanicilar()
        {
            var model = db.Kullanicilar.ToList();
            return View(model);
        }
        public JsonResult KullaniciSil(int ID)
        {
            db.Kullanicilar.Remove(db.Kullanicilar.Where(x => x.ID == ID).FirstOrDefault());
            db.SaveChanges();
            return Json("Başarılı");
        }
        public ActionResult AdminAyarlari()
        {
            var model = db.Admin.FirstOrDefault();
            return View(model);
        }
        public ActionResult AdminGuncelle(string KullaniciAdi,string KullaniciSifre, int ID)
        {
            var model = db.Admin.Where(x => x.ID == ID).FirstOrDefault();
            model.AdminKullaniciAdi = KullaniciAdi;
            model.AdminSifre = KullaniciSifre;
           
            try
            {
                db.SaveChanges();
                return Json("Başarılı");
            }
            catch (Exception)
            {
                return Json("Hata");
            }
        }
    }
}