using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EvdeEczane.Models;
namespace EvdeEczane.Controllers
{
    public class MarkaController : Controller
    {
        EVDEECZANEEntities3 db = new EVDEECZANEEntities3();

        // GET: Marka
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Markalar()
        {
            var model = db.Marka.ToList();
            return View(model);
        }
        public ActionResult MarkaEkle()
        {
            return View();
        }
        public ActionResult MarkaDetay(int ID)
        {
            var model = db.Marka.Where(x => x.ID == ID).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public ActionResult MarkaaEkle(Marka p)
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
                        p.MarkaResim = "/Resimler/Marka/" + fname;
                        fname = Path.Combine(Server.MapPath("~/Resimler/Marka/"), fname);
                        file.SaveAs(fname);

                    }
                    HttpPostedFileBase dosya = files[0];
                    string dosyaAdi = dosya.FileName;

                    db.Marka.Add(new Marka()
                    {
                        MarkaAdi = p.MarkaAdi,
                        MarkaAciklama = p.MarkaAciklama,
                        MarkaResim = p.MarkaResim
                    });
                    try
                    {
                        db.SaveChanges();
                        //return Json("Başarılı");
                        return RedirectToAction("/Markalar/");

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
                db.Marka.Add(new Marka()
                {
                    MarkaAdi = p.MarkaAdi,
                    MarkaAciklama = p.MarkaAciklama
                });
                try
                {
                    db.SaveChanges();
                    //return Json("Başarılı");
                    return RedirectToAction("/Markalar/");

                }
                catch (Exception e)
                {
                    return Json("Hata " + e.Message);

                    //return RedirectToAction("/Index");
                }
            }
        }
        public JsonResult MarkaSil(int ID)
        {
            db.Marka.Remove(db.Marka.Where(x => x.ID == ID).FirstOrDefault());
            db.SaveChanges();
            return Json("Başarılı");
        }
        [HttpPost]
        public ActionResult MarkaGuncelle(Marka p)
        {
            if (Request.Files.Count <= 1 || Request.Files == null)
            {
                try
                {
                    var marka = db.Marka.Where(x => x.ID == p.ID).FirstOrDefault();
                    marka.MarkaAdi = p.MarkaAdi;
                    marka.MarkaAciklama = p.MarkaAciklama;
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        if (file.FileName == "")
                        {

                            string fname = marka.MarkaResim;
                            //System.IO.File.Delete(Server.MapPath(fname+file.FileName));
                            //p.SiteLogo = "/Resimler/Logo/" + fname;
                            //fname = Path.Combine(Server.MapPath("~/Resimler/Logo/"), fname);
                            //file.SaveAs(fname);
                        }
                        else
                        {
                            string fname = file.FileName;
                            p.MarkaResim = "/Resimler/Marka/" + fname;
                            fname = Path.Combine(Server.MapPath("~/Resimler/Marka/"), fname);
                            file.SaveAs(fname);
                            marka.MarkaResim= p.MarkaResim;
                        }

                    }




                    try
                    {
                        db.SaveChanges();
                        //return Json("Başarılı");
                        return RedirectToAction("/Markalar/");

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
                var marka = db.Marka.Where(x => x.ID == p.ID).FirstOrDefault();
                marka.MarkaAdi = p.MarkaAdi;
                marka.MarkaAciklama = p.MarkaAciklama;

                try
                {
                    db.SaveChanges();
                    //return Json("Başarılı");
                    return RedirectToAction("/Markalar/");

                }
                catch (Exception e)
                {
                    return Json("Hata " + e.Message);

                    //return RedirectToAction("/Index");
                }
            }
        }
    }
}