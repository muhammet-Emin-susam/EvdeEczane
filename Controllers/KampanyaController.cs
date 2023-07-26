using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EvdeEczane.Models;
namespace EvdeEczane.Controllers
{
    public class KampanyaController : Controller
    {
        EVDEECZANEEntities3 db = new EVDEECZANEEntities3();
        // GET: Kampanya
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Kampanyalar()
        {
            var model = db.Kampanyalar.ToList();
            return View(model);
        }
        public JsonResult KampanyaSil(int ID)
        {
            db.Kampanyalar.Remove(db.Kampanyalar.Where(x => x.ID == ID).FirstOrDefault());
            db.SaveChanges();
            return Json("Başarılı");
        }
        public ActionResult KampanyaDetay(int ID)
        {
            var model = db.Kampanyalar.Where(x => x.ID == ID).FirstOrDefault();
            return View(model);
        }
        public ActionResult KampanyaEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult KampanyaEkle(Kampanyalar p)
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
                        p.KampanyaResim = "/Resimler/Kampanyalar/" + fname;
                        fname = Path.Combine(Server.MapPath("~/Resimler/Kampanyalar/"), fname);
                        file.SaveAs(fname);

                    }
                    HttpPostedFileBase dosya = files[0];
                    string dosyaAdi = dosya.FileName;

                    db.Kampanyalar.Add(new Kampanyalar()
                    {
                        KampanyaAdi = p.KampanyaAdi,
                        KampanyaAciklama = p.KampanyaAciklama,
                        KampanyaResim = p.KampanyaResim,
                        KampanyaURL = p.KampanyaURL
                    });
                    try
                    {
                        db.SaveChanges();
                        //return Json("Başarılı");
                        return RedirectToAction("/Kampanyalar/");

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
                db.Kampanyalar.Add(new Kampanyalar()
                {
                    KampanyaAdi = p.KampanyaAdi,
                    KampanyaAciklama = p.KampanyaAciklama,
                    KampanyaURL = p.KampanyaURL
                });
                try
                {
                    db.SaveChanges();
                    //return Json("Başarılı");
                    return RedirectToAction("/Kampanyalar/");

                }
                catch (Exception e)
                {
                    return Json("Hata " + e.Message);

                    //return RedirectToAction("/Index");
                }
            }
        }
        public ActionResult KampanyaGuncelle(Kampanyalar p)
        {
            if (Request.Files.Count <= 1 || Request.Files == null)
            {
                try
                {
                    var kampanya = db.Kampanyalar.Where(x => x.ID == p.ID).FirstOrDefault();
                    kampanya.KampanyaAdi = p.KampanyaAdi;
                    kampanya.KampanyaAciklama = p.KampanyaAciklama;
                    kampanya.KampanyaURL = p.KampanyaURL;

                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        if (file.FileName == "")
                        {

                            string fname = kampanya.KampanyaResim;
                            //System.IO.File.Delete(Server.MapPath(fname+file.FileName));
                            //p.SiteLogo = "/Resimler/Logo/" + fname;
                            //fname = Path.Combine(Server.MapPath("~/Resimler/Logo/"), fname);
                            //file.SaveAs(fname);
                        }
                        else
                        {
                            string fname = file.FileName;
                            p.KampanyaResim = "/Resimler/Kampanyalar/" + fname;
                            fname = Path.Combine(Server.MapPath("~/Resimler/Kampanyalar/"), fname);
                            file.SaveAs(fname);
                            kampanya.KampanyaResim = p.KampanyaResim;
                        }

                    }




                    try
                    {
                        db.SaveChanges();
                        //return Json("Başarılı");
                        return RedirectToAction("/Kampanyalar/");

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
                var kampanya = db.Kampanyalar.Where(x => x.ID == p.ID).FirstOrDefault();
                kampanya.KampanyaAdi = p.KampanyaAdi;
                kampanya.KampanyaAciklama = p.KampanyaAciklama;
                kampanya.KampanyaURL = p.KampanyaURL;

                try
                {
                    db.SaveChanges();
                    //return Json("Başarılı");
                    return RedirectToAction("/Kampanyalar/");

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