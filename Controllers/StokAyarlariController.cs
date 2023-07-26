using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EvdeEczane.Models;
namespace EvdeEczane.Controllers
{
    public class StokAyarlariController : Controller
    {
        EVDEECZANEEntities3 db = new EVDEECZANEEntities3();
        // GET: StokAyarlari
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Stoklarim()
        {
            var model = db.Stok.ToList();
            return View(model);
        }
        public ActionResult StokEkle()
        {
            List<SelectListItem> Marka = (from i in db.Marka.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = i.MarkaAdi,
                                                  Value = i.ID.ToString()
                                              }).ToList();
            ViewBag.Marka = Marka;
            List<SelectListItem> AltKategori = (from i in db.Menuler.Where(x => x.ParentID == null && x.AltMenuID != null ).ToList()
                                              select new SelectListItem
                                              {
                                                  Text = i.MenuAdi,
                                                  Value = i.ID.ToString()
                                              }).ToList();
            ViewBag.AltKategori = AltKategori;
            return View();
        }
        public ActionResult StokDetay(int ID)
        {
            List<SelectListItem> Marka = (from i in db.Marka.ToList()
                                          select new SelectListItem
                                          {
                                              Text = i.MarkaAdi,
                                              Value = i.ID.ToString()
                                          }).ToList();
            ViewBag.Marka = Marka;
            List<SelectListItem> AltKategori = (from i in db.Menuler.Where(x => x.ParentID == null && x.AltMenuID != null).ToList()
                                                select new SelectListItem
                                                {
                                                    Text = i.MenuAdi,
                                                    Value = i.ID.ToString()
                                                }).ToList();
            ViewBag.AltKategori = AltKategori;
            var model = db.Stok.Where(x => x.ID == ID).FirstOrDefault();
            return View(model);
        } 
        [HttpPost]
        public ActionResult UrunEkle(Stok p)
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
                        p.StokResim = "/Resimler/StokResim/" + fname;
                        fname = Path.Combine(Server.MapPath("~/Resimler/StokResim/"), fname);
                        file.SaveAs(fname);

                    }
                    HttpPostedFileBase dosya = files[0];
                    string dosyaAdi = dosya.FileName;
                    var menu = db.Menuler.Where(x => x.ID == p.StokKategoriID).FirstOrDefault();
                    var Parentmenu = db.Menuler.Where(x => x.ID == menu.AltMenuID).FirstOrDefault();
                    db.Stok.Add(new Stok()
                    {
                        StokAdi = p.StokAdi,
                        StokAciklama = p.StokAciklama,
                        StokBakiye = p.StokBakiye,
                        StokBarkod = p.StokBarkod,
                        StokFiyat = p.StokFiyat,
                        StokKategoriID = p.StokKategoriID,
                        StokKodu = p.StokKodu,
                        StokMarkaID = p.StokMarkaID,
                        StokResim = p.StokResim,
                        StokTarih = DateTime.Now,
                        isIndirim = p.isIndirim,
                        IndirimliFiyat = p.IndirimliFiyat,
                        isTrend = p.isTrend,
                        isYeni = p.isYeni,
                        StokResimAdi = dosyaAdi,
                        ParentKategoriID = menu.AltMenuID,
                        AnaKategoriID = Parentmenu.ParentID
                    }) ;
                    try
                    {
                        db.SaveChanges();
                        //return Json("Başarılı");
                        return RedirectToAction("/StokEkle/");

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
                var menu = db.Menuler.Where(x => x.ID == p.StokKategoriID).FirstOrDefault();
                var Parentmenu = db.Menuler.Where(x => x.ID == menu.AltMenuID).FirstOrDefault();
                db.Stok.Add(new Stok()
                {
                    StokAdi = p.StokAdi,
                    StokAciklama = p.StokAciklama,
                    StokBakiye = p.StokBakiye,
                    StokBarkod = p.StokBarkod,
                    StokFiyat = p.StokFiyat,
                    StokKategoriID = p.StokKategoriID,
                    StokKodu = p.StokKodu,
                    StokMarkaID = p.StokMarkaID,
                    StokTarih = DateTime.Now,
                    isIndirim = p.isIndirim,
                    IndirimliFiyat = p.IndirimliFiyat,
                    isTrend = p.isTrend,
                    isYeni = p.isYeni,
                    ParentKategoriID = menu.AltMenuID,
                    AnaKategoriID = Parentmenu.ParentID
                });
                try
                {
                    db.SaveChanges();
                    //return Json("Başarılı");
                    return RedirectToAction("/Index/");

                }
                catch (Exception e)
                {
                    return Json("Hata " + e.Message);

                    //return RedirectToAction("/Index");
                }
            }
        }
        public JsonResult StokSil(int ID)
        {
            db.Stok.Remove(db.Stok.Where(x => x.ID == ID).FirstOrDefault());
            db.SaveChanges();
            return Json("Başarılı");
        }
        [HttpPost]
        public ActionResult UrunGuncelle(Stok p)
        {
            if (Request.Files.Count <= 1 || Request.Files == null)
            {
                try
                {
                    var menu = db.Menuler.Where(x => x.ID == p.StokKategoriID).FirstOrDefault();
                    var Parentmenu = db.Menuler.Where(x => x.ID == menu.AltMenuID).FirstOrDefault();
                    var stok = db.Stok.Where(x => x.ID == p.ID).FirstOrDefault();
                    stok.IndirimliFiyat = p.IndirimliFiyat;
                    stok.isIndirim = p.isIndirim;
                    stok.isTrend = p.isTrend;
                    stok.isYeni = p.isYeni;
                    stok.StokMarkaID = p.StokMarkaID;
                    stok.StokAciklama = p.StokAciklama;
                    stok.StokAdi = p.StokAdi;
                    stok.StokBakiye = p.StokBakiye;
                    stok.StokBarkod = p.StokBarkod;
                    stok.StokFiyat = p.StokFiyat;
                    stok.StokKategoriID = p.StokKategoriID;
                    stok.StokKodu = p.StokKodu;
                    stok.ParentKategoriID = menu.AltMenuID;
                    stok.AnaKategoriID = Parentmenu.ParentID;
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        if (file.FileName == "")
                        {
                            string fname = stok.StokResimAdi;
                            //p.StokResim = "/Resimler/StokResim/" + fname;
                            //fname = Path.Combine(Server.MapPath("~/Resimler/StokResim/"), fname);
                            //file.SaveAs(fname);
                        }
                        else
                        {
                            string fname = file.FileName;
                            p.StokResim = "/Resimler/StokResim/" + fname;
                            fname = Path.Combine(Server.MapPath("~/Resimler/StokResim/"), fname);
                            file.SaveAs(fname);
                            stok.StokResim = p.StokResim;
                        }

                    }

                   
                    
                    
                    try
                    {
                        db.SaveChanges();
                        //return Json("Başarılı");
                        return RedirectToAction("/Stoklarim/");

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
                var menu = db.Menuler.Where(x => x.ID == p.StokKategoriID).FirstOrDefault();
                var Parentmenu = db.Menuler.Where(x => x.ParentID == menu.ID).FirstOrDefault();
                var stok = db.Stok.Where(x => x.ID == p.ID).FirstOrDefault();
                stok.IndirimliFiyat = p.IndirimliFiyat;
                stok.isIndirim = p.isIndirim;
                stok.isTrend = p.isTrend;
                stok.isYeni = p.isYeni;
                stok.StokMarkaID = p.StokMarkaID;
                stok.StokAciklama = p.StokAciklama;
                stok.StokAdi = p.StokAdi;
                stok.StokBakiye = p.StokBakiye;
                stok.StokBarkod = p.StokBarkod;
                stok.StokFiyat = p.StokFiyat;
                stok.StokKategoriID = p.StokKategoriID;
                stok.StokKodu = p.StokKodu;
                stok.ParentKategoriID = menu.AltMenuID;
                stok.AnaKategoriID = Parentmenu.ParentID;

                try
                {
                    db.SaveChanges();
                    //return Json("Başarılı");
                    return RedirectToAction("/Stoklarim/");

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