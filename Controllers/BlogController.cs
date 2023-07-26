using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EvdeEczane.Models;
namespace EvdeEczane.Controllers
{
    public class BlogController : Controller
    {
        EVDEECZANEEntities3 db = new EVDEECZANEEntities3();
        // GET: Blog
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Bloglar()
        {
            var model = db.Bloglar.ToList();
            return View(model);
        }
        public ActionResult BlogEkle()
        {
            return View();
        }
        public ActionResult BlogDetay(int ID)
        {
            var model = db.Bloglar.Where(x => x.ID == ID).FirstOrDefault();
            return View(model);
        }
        public JsonResult BlogSil(int ID)
        {
            db.Bloglar.Remove(db.Bloglar.Where(x => x.ID == ID).FirstOrDefault());
            db.SaveChanges();
            return Json("Başarılı");
        }
        [HttpPost]
        public ActionResult BloggEkle(Bloglar p)
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
                        p.BlogResim = "/Resimler/Bloglar/" + fname;
                        fname = Path.Combine(Server.MapPath("~/Resimler/Bloglar/"), fname);
                        file.SaveAs(fname);

                    }
                    HttpPostedFileBase dosya = files[0];
                    string dosyaAdi = dosya.FileName;

                    db.Bloglar.Add(new Bloglar()
                    {
                        BlogAdi = p.BlogAdi,
                        BlogResim = p.BlogResim,
                        BlogAciklama = p.BlogAciklama,
                        BlogKisaAciklama = p.BlogKisaAciklama,
                        BlogTarih = DateTime.Now
                    });
                    try
                    {
                        db.SaveChanges();
                        //return Json("Başarılı");
                        return RedirectToAction("/Bloglar/");

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
                db.Bloglar.Add(new Bloglar()
                {
                    BlogAdi = p.BlogAdi,
                    BlogAciklama = p.BlogAciklama,
                    BlogKisaAciklama = p.BlogKisaAciklama,
                    BlogTarih = DateTime.Now
                });
                try
                {
                    db.SaveChanges();
                    //return Json("Başarılı");
                    return RedirectToAction("/Bloglar/");

                }
                catch (Exception e)
                {
                    return Json("Hata " + e.Message);

                    //return RedirectToAction("/Index");
                }
            }
        }
        [HttpPost]
        public ActionResult BlogGuncelle(Bloglar p)
        {
            if (Request.Files.Count <= 1 || Request.Files == null)
            {
                try
                {
                    var blog = db.Bloglar.Where(x => x.ID == p.ID).FirstOrDefault();
                    blog.BlogAdi = p.BlogAdi;
                    blog.BlogAciklama = p.BlogAciklama;
                    blog.BlogKisaAciklama = p.BlogKisaAciklama;
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        if (file.FileName == "")
                        {

                            string fname = blog.BlogResim;
                            //System.IO.File.Delete(Server.MapPath(fname+file.FileName));
                            //p.SiteLogo = "/Resimler/Logo/" + fname;
                            //fname = Path.Combine(Server.MapPath("~/Resimler/Logo/"), fname);
                            //file.SaveAs(fname);
                        }
                        else
                        {
                            string fname = file.FileName;
                            p.BlogResim = "/Resimler/Bloglar/" + fname;
                            fname = Path.Combine(Server.MapPath("~/Resimler/Bloglar/"), fname);
                            file.SaveAs(fname);
                            blog.BlogResim = p.BlogResim;
                        }

                    }




                    try
                    {
                        db.SaveChanges();
                        //return Json("Başarılı");
                        return RedirectToAction("/Bloglar/");

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
                var blog = db.Bloglar.Where(x => x.ID == p.ID).FirstOrDefault();
                blog.BlogAdi = p.BlogAdi;
                blog.BlogKisaAciklama = p.BlogKisaAciklama;
                blog.BlogAciklama = p.BlogAciklama;

                try
                {
                    db.SaveChanges();
                    //return Json("Başarılı");
                    return RedirectToAction("/Bloglar/");

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