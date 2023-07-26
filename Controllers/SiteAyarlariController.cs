using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EvdeEczane.Models;
namespace EvdeEczane.Controllers
{
    public class SiteAyarlariController : Controller
    {
        EVDEECZANEEntities3 db = new EVDEECZANEEntities3();
        // GET: SiteAyarlari
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SEO()
        {
            var model = db.SEO.FirstOrDefault();
            return View(model);
        }
        public ActionResult SEOGuncelle(string Description, string Keyword,string Title, int ID)
        {
            var model = db.SEO.Where(x => x.ID == ID).FirstOrDefault();
            model.Title = Title;
            model.Keywords = Keyword;
            model.Description = Description;
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
        public ActionResult Hakkimizda()
        {
            var model = db.Bilgiler.FirstOrDefault();
            return View(model);
        }
        public ActionResult HakkimizdaGuncelle(string Hakkimizda)
        {
            var model = db.Bilgiler.FirstOrDefault();
            model.SiteHakkimizda = Hakkimizda;
            db.SaveChanges();
            return Json("Başarılı");
        }
        public ActionResult SiteBilgileri()
        {
            var model = db.Bilgiler.FirstOrDefault();
            return View(model);
        }
        //public ActionResult SiteBilgileriGuncelle(Bilgiler p)
        //{
        //    if (Request.Files.Count <= 1 || Request.Files == null)
        //    {
        //        try
        //        {
        //            var bilgiler = db.Bilgilers.FirstOrDefault();
        //            bilgiler.SiteEmail = p.SiteEmail;
        //            bilgiler.SiteHakkimizda = p.SiteHakkimizda;
        //            bilgiler.SiteInstagram = p.SiteInstagram;
        //            bilgiler.SiteTel = p.SiteTel;
        //            bilgiler.FirmaAdi = p.FirmaAdi;
        //            HttpFileCollectionBase files = Request.Files;
        //            for (int i = 0; i < files.Count; i++)
        //            {
        //                HttpPostedFileBase file = files[i];
        //                if (file.FileName == "")
        //                {
        //                    string fname = bilgiler.LogoAdi;
        //                    p.SiteLogo = "/Resimler/Logo/" + fname;
        //                    fname = Path.Combine(Server.MapPath("~/Resimler/Logo/"), fname);
        //                    file.SaveAs(fname);
        //                }
        //                else
        //                {
        //                    //string fname = file.FileName;
        //                    //p.SiteLogo = "/Resimler/Logo/" + fname;
        //                    //fname = Path.Combine(Server.MapPath("~/Resimler/Logo/"), fname);
        //                    //HttpPostedFileBase dosya = files[0];
        //                    //string dosyaAdi = dosya.FileName;
        //                    //bilgiler.LogoAdi = dosyaAdi;
        //                    //file.SaveAs(fname);
        //                    string fname = file.FileName;
        //                    p.SiteLogo = "/Resimler/Logo/" + fname;
        //                    fname = Path.Combine(Server.MapPath("~/Resimler/Logo/"), fname);
        //                    file.SaveAs(fname);

        //                }

        //            }

        //            bilgiler.SiteLogo= p.SiteLogo;

        //            try
        //            {
        //                db.SaveChanges();
        //                //return Json("Başarılı");
        //                return RedirectToAction("/SiteBilgileri/");

        //            }
        //            catch (Exception e)
        //            {
        //                return Json("Hata " + e.Message);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            return Json("Hata : " + ex.Message);
        //        }

        //    }
        //    else
        //    {
        //        var bilgiler = db.Bilgilers.FirstOrDefault();
        //        bilgiler.SiteEmail = p.SiteEmail;
        //        bilgiler.SiteHakkimizda = p.SiteHakkimizda;
        //        bilgiler.SiteInstagram = p.SiteInstagram;
        //        bilgiler.SiteTel = p.SiteTel;
        //        bilgiler.FirmaAdi = p.FirmaAdi;
        //        try
        //        {
        //            db.SaveChanges();
        //            //return Json("Başarılı");
        //            return RedirectToAction("/SiteBilgileri/");

        //        }
        //        catch (Exception e)
        //        {
        //            return Json("Hata " + e.Message);

        //            //return RedirectToAction("/Index");
        //        }
        //    }
        //}
        public ActionResult SiteBilgileriGuncelle(Bilgiler p)
        {
            if (Request.Files.Count <= 1 || Request.Files == null)
            {
                try
                {
                    var bilgi = db.Bilgiler.Where(x => x.ID == 1).FirstOrDefault();
                    bilgi.SiteEmail = p.SiteEmail;
                    bilgi.SiteInstagram = p.SiteInstagram;
                    bilgi.SiteTel = p.SiteTel;
                    bilgi.FirmaAdi = p.FirmaAdi;
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        if (file.FileName == "")
                        {

                            string fname = bilgi.SiteLogo;
                            //System.IO.File.Delete(Server.MapPath(fname+file.FileName));
                            //p.SiteLogo = "/Resimler/Logo/" + fname;
                            //fname = Path.Combine(Server.MapPath("~/Resimler/Logo/"), fname);
                            //file.SaveAs(fname);
                        }
                        else
                        {
                            string fname = file.FileName;
                            p.SiteLogo = "/Resimler/Logo/" + fname;
                            fname = Path.Combine(Server.MapPath("~/Resimler/Logo/"), fname);
                            file.SaveAs(fname);
                            bilgi.SiteLogo = p.SiteLogo;
                        }

                    }


                    

                    try
                    {
                        db.SaveChanges();
                        //return Json("Başarılı");
                        return RedirectToAction("/SiteBilgileri/");

                    }
                    catch (Exception e)
                    {
                        return Json("Hata " + e.Message);
                    }
                }
                catch (Exception ex)
                {
                    return Json("Hata : " + ex.Message);
                }

            }
            else
            {
                var bilgi = db.Bilgiler.Where(x => x.ID == 1).FirstOrDefault();
                bilgi.SiteEmail = p.SiteEmail;
                bilgi.SiteInstagram = p.SiteInstagram;
                bilgi.SiteTel = p.SiteTel;
                bilgi.FirmaAdi = p.FirmaAdi;

                try
                {
                    db.SaveChanges();
                    //return Json("Başarılı");
                    return RedirectToAction("/SiteBilgileri/");

                }
                catch (Exception e)
                {
                    return Json("Hata " + e.Message);

                    //return RedirectToAction("/Index");
                }
            }
        }
        public ActionResult Slider()
        {
            var model = db.Slider.ToList();
            return View(model);
        }
        public ActionResult SliderEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SliderEkle(Slider p)
        {
            if (Request.Files.Count <= 1)
            {
                try
                {
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname = file.FileName;
                        p.SliderResim = "/Resimler/Slider/" + fname;
                        fname = Path.Combine(Server.MapPath("~/Resimler/Slider/"), fname);
                        file.SaveAs(fname);

                    }
                    HttpPostedFileBase dosya = files[0];
                    string dosyaAdi = dosya.FileName;

                    db.Slider.Add(new Slider()
                    {
                        SliderAdi = p.SliderAdi,
                        SliderResim = p.SliderResim,
                        SliderURL = p.SliderURL
                    });
                    try
                    {
                        db.SaveChanges();
                        //return Json("Başarılı");
                        return RedirectToAction("/Slider/");

                    }
                    catch (Exception e)
                    {
                        return Json("Hata " + e.Message);
                    }
                }
                catch (Exception ex)
                {
                    return Json("Hata " + ex.Message);
                }

            }
            else
            {
                db.Slider.Add(new Slider()
                {
                    SliderAdi = p.SliderAdi
                });
                try
                {
                    db.SaveChanges();
                    //return Json("Başarılı");
                    return RedirectToAction("/Slider/");

                }
                catch (Exception e)
                {
                    return Json("Hata " + e.Message);

                    //return RedirectToAction("/Index");
                }
            }
        }
        public JsonResult SliderSil(int ID)
        {
            db.Slider.Remove(db.Slider.Where(x => x.ID == ID).FirstOrDefault());
            db.SaveChanges();
            return Json("Başarılı");
        }
    }
}