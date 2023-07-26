using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EvdeEczane.Models;
namespace EvdeEczane.Controllers
{
    public class MenulerController : Controller
    {
        EVDEECZANEEntities3 db = new EVDEECZANEEntities3();
        // GET: Menuler
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Menuler()
        {
            var model = db.Menuler.Where(x => x.ParentID == null && x.AltMenuID == null).ToList();
            var altmenu = db.Menuler.Where(y => y.ParentID != null).ToList();
            ViewBag.Altmenu = altmenu;
            var kategoribazlimenu = db.Menuler.Where(y => y.ParentID == null && y.AltMenuID != null).ToList();
            ViewBag.KategoriBazliMenu = kategoribazlimenu;
            return View(model);
        }
        public ActionResult MenuDetay(int ID)
        {
            var model = db.Menuler.Where(x => x.ID == ID).FirstOrDefault();
            return View(model);
        }
        public ActionResult MenuEkle()
        {
            return View();
        }
        public JsonResult MenuSil(int ID)
        {
            db.Menuler.Remove(db.Menuler.Where(x => x.ID == ID).FirstOrDefault());
            db.SaveChanges();
            return Json("Başarılı");
        }
        [HttpPost]
        public ActionResult MenuuEkle(Menuler p)
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
                        p.MenuResim = "/Resimler/Menu/" + fname;
                        fname = Path.Combine(Server.MapPath("~/Resimler/Menu/"), fname);
                        file.SaveAs(fname);

                    }
                    HttpPostedFileBase dosya = files[0];
                    string dosyaAdi = dosya.FileName;

                    db.Menuler.Add(new Menuler()
                    {
                        MenuAdi = p.MenuAdi,
                        MenuAciklama = p.MenuAciklama,
                        MenuResim = p.MenuResim
                    });
                    try
                    {
                        db.SaveChanges();
                        //return Json("Başarılı");
                        return RedirectToAction("/Menuler/");

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
                db.Menuler.Add(new Menuler()
                {
                    MenuAdi = p.MenuAdi,
                    MenuAciklama = p.MenuAciklama
                });
                try
                {
                    db.SaveChanges();
                    //return Json("Başarılı");
                    return RedirectToAction("/Menuler/");

                }
                catch (Exception e)
                {
                    return Json("Hata " + e.Message);

                    //return RedirectToAction("/Index");
                }
            }
        }
        public ActionResult AltMenuEkle()
        {
            List<SelectListItem> Menu = (from i in db.Menuler.Where(x=> x.ParentID == null).ToList()
                                          select new SelectListItem
                                          {
                                              Text = i.MenuAdi,
                                              Value = i.ID.ToString()
                                          }).ToList();
            ViewBag.Menu = Menu;
            return View();
        }
        public ActionResult AltMenuEklee(int Menu, string AltMenuAciklamasi, string AltMenuAdi)
        {
            db.Menuler.Add(new Menuler()
            {
                MenuAdi = AltMenuAdi,
                MenuAciklama = AltMenuAciklamasi,
                ParentID = Menu,
                MenuResim = "/Resimler/Menu/AltMenu.png"
            });
            try
            {
                db.SaveChanges();
                return Json("Başarılı");

            }
            catch (Exception e)
            {
                return Json("Hata " + e.Message);
            }
        }
        [HttpPost]
        public ActionResult MenuGuncelle(Menuler p)
        {
            if (Request.Files.Count <= 1 || Request.Files == null)
            {
                try
                {
                    var menu = db.Menuler.Where(x => x.ID == p.ID).FirstOrDefault();
                    menu.MenuAdi = p.MenuAdi;
                    menu.MenuAciklama = p.MenuAciklama;
                    
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        if (file.FileName == "")
                        {

                            string fname = menu.MenuResim;
                            //System.IO.File.Delete(Server.MapPath(fname+file.FileName));
                            //p.SiteLogo = "/Resimler/Logo/" + fname;
                            //fname = Path.Combine(Server.MapPath("~/Resimler/Logo/"), fname);
                            //file.SaveAs(fname);
                        }
                        else
                        {
                            string fname = file.FileName;
                            p.MenuResim = "/Resimler/Menu/" + fname;
                            fname = Path.Combine(Server.MapPath("~/Resimler/Menu/"), fname);
                            file.SaveAs(fname);
                            menu.MenuResim = p.MenuResim;
                        }

                    }




                    try
                    {
                        db.SaveChanges();
                        //return Json("Başarılı");
                        return RedirectToAction("/Menuler/");

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
                var menu = db.Menuler.Where(x => x.ID == p.ID).FirstOrDefault();
                menu.MenuAdi = p.MenuAdi;
                menu.MenuAciklama = p.MenuAciklama;

                try
                {
                    db.SaveChanges();
                    //return Json("Başarılı");
                    return RedirectToAction("/Menuler/");

                }
                catch (Exception e)
                {
                    return Json("Hata " + e.Message);

                    //return RedirectToAction("/Index");
                }
            }
        }

        public ActionResult KategoriBazliMenuEkle()
        {
            List<SelectListItem> Menu = (from i in db.Menuler.Where(x => x.ParentID != null && x.AltMenuID == null).ToList()
                                         select new SelectListItem
                                         {
                                             Text = i.MenuAdi,
                                             Value = i.ID.ToString()
                                         }).ToList();
            ViewBag.Menu = Menu;
            return View();
        }
        public ActionResult KategoriBazliMenuEklee(int Menu, string AltMenuAciklamasi, string AltMenuAdi)
        {
            db.Menuler.Add(new Menuler()
            {
                MenuAdi = AltMenuAdi,
                MenuAciklama = AltMenuAciklamasi,
                AltMenuID = Menu,
                MenuResim = "/Resimler/Menu/AltMenu.png"
            });
            try
            {
                db.SaveChanges();
                return Json("Başarılı");

            }
            catch (Exception e)
            {
                return Json("Hata " + e.Message);
            }
        }
    }
}