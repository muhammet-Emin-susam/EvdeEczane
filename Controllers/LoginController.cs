using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EvdeEczane.Models;
namespace EvdeEczane.Controllers
{
    public class LoginController : Controller
    {
        EVDEECZANEEntities3 db = new EVDEECZANEEntities3();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SiteLogin()
        {
            return View();
        }
        public ActionResult GirisYap(string KullaniciAdi , string Sifre)
        {
            if (KullaniciAdi != string.Empty && Sifre != string.Empty)
            {
                //string sifre = Sifre.MD5Olustur(yourPassword);
                var p = db.Admin.Where(x => x.AdminKullaniciAdi == KullaniciAdi && x.AdminSifre == Sifre).FirstOrDefault();
                if (p != null)
                {
                    Session["ID"] = p.ID;
                    Session["KullaniciAdi"] = p.AdminKullaniciAdi;
                    //return RedirectToAction("Index", "Admin");
                    return Json("Başarılı");
                }
                else
                {
                    return Json("Hata");
                }

            }
            else
            {
                return Json("Bos");
            }
        }
        public ActionResult SiteGirisYap(string KullaniciAdi , string Sifre)
        {
            if (KullaniciAdi != string.Empty && Sifre != string.Empty)
            {
                //string sifre = Sifre.MD5Olustur(yourPassword);
                var p = db.Kullanicilar.Where(x => x.KullaniciMail == KullaniciAdi && x.KullaniciSifre == Sifre).FirstOrDefault();
                if (p != null)
                {
                    Session["ID"] = p.ID;
                    Session["KullaniciAdi"] = p.KullaniciAdSoyad;
                    //return RedirectToAction("Index", "Admin");
                    return Json("Başarılı");
                }
                else
                {
                    return Json("Hata");
                }

            }
            else
            {
                return Json("Bos");
            }
        }
        public ActionResult KayitOl(string RegisterPassword, string RegisterEmail, string RegisterUser)
        {
            db.Kullanicilar.Add(new Kullanicilar()
            {
                KullaniciAdSoyad = RegisterUser,
                KullaniciMail = RegisterEmail,
                KullaniciSonGiris = DateTime.Now,
                KullaniciSifre = RegisterPassword
            });
            try
            {
                db.SaveChanges();
                ViewBag.Msg = "Kayıt Başarıyla Oluşturuldu.";
                return Json("Başarılı");

            }
            catch (Exception e)
            {
                return Json("Hata " + e.Message);
            }
        }
    }
}