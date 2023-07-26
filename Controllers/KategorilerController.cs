using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EvdeEczane.Models;
namespace EvdeEczane.Controllers
{
    public class KategorilerController : Controller
    {
        EVDEECZANEEntities3 db = new EVDEECZANEEntities3();
        // GET: Kategoriler
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Kategori(int ID, string Indirimli, string Yeni, string Trend)
        {

            var model = db.Menuler.Where(x => x.ID == ID).FirstOrDefault();
            var urunler = db.Stok.Where(x => x.StokKategoriID == ID || x.AnaKategoriID == ID || x.ParentKategoriID == ID).ToList();
            if (Indirimli == "true")
            {
                var IndirimliUrunler = db.Stok.Where(x => x.StokKategoriID == ID && x.isIndirim == true).ToList();
                ViewBag.Urunler = IndirimliUrunler;
                var UrunSayisi = IndirimliUrunler.Count();
                ViewBag.StokSayisi = UrunSayisi;
            }
            else if (Yeni == "true")
            {
                var YeniUrunler = db.Stok.Where(x => x.StokKategoriID == ID && x.isYeni == true).ToList();
                ViewBag.Urunler = YeniUrunler;
                var UrunSayisi = YeniUrunler.Count();
                ViewBag.StokSayisi = UrunSayisi;
            }
            else if (Trend == "true")
            {
                var TrendUrunler = db.Stok.Where(x => x.StokKategoriID == ID && x.isTrend == true).ToList();
                ViewBag.Urunler = TrendUrunler;
                var UrunSayisi = TrendUrunler.Count();
                ViewBag.StokSayisi = UrunSayisi;
            }
            else
            {
                var UrunSayisi = urunler.Count();
                ViewBag.StokSayisi = UrunSayisi;
                ViewBag.Urunler = urunler;
            }
            var EncokSatanlar = db.Stok.Where(x => x.StokKategoriID == ID || x.AnaKategoriID == ID || x.ParentKategoriID == ID).ToList();
            ViewBag.encok = EncokSatanlar.Take(10);
            var marka = db.Marka.Where(x => x.KategoriID == ID).ToList();
            ViewBag.Marka = marka;

            return View(model);
        }
        public ActionResult Partial(string text, int kategoriID, string trend, string aralik)
        {
            var tru = true;
            var adanzye = db.Stok.Where(x => x.StokKategoriID == kategoriID || x.AnaKategoriID == kategoriID || x.ParentKategoriID == kategoriID).OrderBy(x => x.StokAdi).ToList();
            var artanfiyat = db.Stok.Where(x => x.StokKategoriID == kategoriID || x.AnaKategoriID == kategoriID || x.ParentKategoriID == kategoriID).OrderBy(x => x.StokFiyat).ToList();
            var urunler = db.Stok.Where(x => x.StokKategoriID == kategoriID || x.AnaKategoriID == kategoriID || x.ParentKategoriID == kategoriID).ToList();


            var BirElli = urunler.Where(x => x.StokFiyat < 51).ToList();
            var trendBirElli = BirElli.Where(x => x.isTrend == true).ToList();
            var YeniBirElli = BirElli.Where(x => x.isYeni == true).ToList();
            var IndirimliBirElli = BirElli.Where(x => x.isIndirim == true).ToList();

            var ElliYuz = urunler.Where(x => x.StokFiyat < 100 && x.StokFiyat > 51).ToList();
            var trendElliYuz = ElliYuz.Where(x => x.isTrend == true).ToList();
            var YeniElliYuz = ElliYuz.Where(x => x.isYeni == true).ToList();
            var IndirimliElliYuz = ElliYuz.Where(x => x.isIndirim == true).ToList();

            var YuzYuzelli = urunler.Where(x => x.StokFiyat < 151 && x.StokFiyat > 100).ToList();
            var trendyuzyuzelli = YuzYuzelli.Where(x => x.isTrend == true).ToList();
            var Yeniyuzyuzelli = YuzYuzelli.Where(x => x.isYeni == true).ToList();
            var Indirimliyuzyuzelli = YuzYuzelli.Where(x => x.isIndirim == true).ToList();

            var YuzEllIkiYuzElli = urunler.Where(x => x.StokFiyat < 201 && x.StokFiyat > 150).ToList();
            var trendYuzEllIkiYuz = YuzEllIkiYuzElli.Where(x => x.isTrend == true).ToList();
            var YeniYuzEllIkiYuz = YuzEllIkiYuzElli.Where(x => x.isYeni == true).ToList();
            var IndirimliYuzEllIkiYuz = YuzEllIkiYuzElli.Where(x => x.isIndirim == true).ToList();

            var IkiYuzElliDortYuz = urunler.Where(x => x.StokFiyat < 401 && x.StokFiyat > 250).ToList();
            var trendIkiYuzElliDortYuz = IkiYuzElliDortYuz.Where(x => x.isTrend == true).ToList();
            var YeniIkiYuzElliDortYuz = IkiYuzElliDortYuz.Where(x => x.isYeni == true).ToList();
            var IndirimliIkiYuzElliDortYuz = IkiYuzElliDortYuz.Where(x => x.isIndirim == true).ToList();

            var DortYuzIkibin = urunler.Where(x => x.StokFiyat < 2001 && x.StokFiyat > 400).ToList();
            var trendDortYuzIkibin = DortYuzIkibin.Where(x => x.isTrend == true).ToList();
            var YeniDortYuzIkibin = DortYuzIkibin.Where(x => x.isYeni == true).ToList();
            var IndirimliDortYuzIkibin = DortYuzIkibin.Where(x => x.isIndirim == true).ToList();

            var Ikibin = urunler.Where(x => x.StokFiyat > 2000).ToList();
            var trendIkibin = Ikibin.Where(x => x.isTrend == true).ToList();
            var YeniIkibin = Ikibin.Where(x => x.isYeni == true).ToList();
            var IndirimliIkibin = Ikibin.Where(x => x.isIndirim == true).ToList();

            var trendd = db.Stok.Where(x => x.isTrend == true).ToList();
            var trendurunler = trendd.Where(x => x.isTrend != null && x.AnaKategoriID == kategoriID || x.ParentKategoriID == kategoriID).ToList();
            var trendartanfiyat = trendurunler.OrderBy(x => x.StokFiyat).ToList();
            var trendadanzye = trendurunler.OrderBy(x => x.StokAdi).ToList();


            var yeni = db.Stok.Where(x => x.isYeni == true).ToList();
            var yeniurunler = yeni.Where(x => x.StokKategoriID == kategoriID || x.AnaKategoriID == kategoriID || x.ParentKategoriID == kategoriID).ToList();
            var yeniartanfiyat = yeniurunler.OrderBy(x => x.StokFiyat).ToList();
            var yeniadanzye = yeniurunler.OrderBy(x => x.StokAdi).ToList();


            //var indirimliurunler = db.Stok.Where(x => x.isIndirim == true && x.StokKategoriID == kategoriID || x.AnaKategoriID == kategoriID || x.ParentKategoriID == kategoriID).ToList();
            var indirimli = db.Stok.Where(x => x.isIndirim == true).ToList();
            var indirimliurunler = indirimli.Where(x => x.StokKategoriID == kategoriID || x.AnaKategoriID == kategoriID || x.ParentKategoriID == kategoriID).ToList();
            var indirimliartanfiyat = indirimliurunler.OrderBy(x => x.StokFiyat).ToList();
            var indirimliadanzye = indirimliurunler.OrderBy(x => x.StokAdi).ToList();
            var partial = db.Stok.Where(x => x.StokKategoriID == kategoriID).ToList();
            if (aralik == null)
            {

                if (trend == "trend")
                {
                    if (text == "a-z")
                    {
                        return PartialView("PartialStok", trendadanzye);
                    }
                    else if (text == "arfiyat")
                    {
                        return PartialView("PartialStok", trendartanfiyat);
                    }
                    else
                    {
                        return PartialView("PartialStok", trendurunler);
                    }
                }
                else if (trend == "yeni")
                {
                    if (text == "a-z")
                    {
                        return PartialView("PartialStok", yeniadanzye);
                    }
                    else if (text == "arfiyat")
                    {
                        return PartialView("PartialStok", yeniartanfiyat);
                    }
                    else
                    {
                        return PartialView("PartialStok", yeniurunler);
                    }
                }
                else if (trend == "indirimli")
                {
                    if (text == "a-z")
                    {
                        return PartialView("PartialStok", indirimliadanzye);
                    }
                    else if (text == "arfiyat")
                    {
                        return PartialView("PartialStok", indirimliartanfiyat);
                    }
                    else
                    {
                        return PartialView("PartialStok", indirimliurunler);
                    }
                }
                else
                {
                    if (text == "a-z")
                    {
                        return PartialView("PartialStok", adanzye);
                    }
                    else if (text == "arfiyat")
                    {
                        return PartialView("PartialStok", artanfiyat);
                    }
                    else
                    {
                        return PartialView("PartialStok", urunler);
                    }

                }
            }
            if (aralik == "1-50")
            {
                if (trend == "trend")
                {
                    return PartialView("PartialStok", trendBirElli);
                }
                else if (trend == "yeni")
                {
                    return PartialView("PartialStok", YeniBirElli);
                }
                else if (trend == "indirimli")
                {
                    return PartialView("PartialStok", IndirimliBirElli);
                }
                else
                {
                    return PartialView("PartialStok", BirElli);
                }

            }
            else if (aralik == "51-100")
            {
                if (trend == "trend")
                {
                    return PartialView("PartialStok", trendElliYuz);
                }
                else if (trend == "yeni")
                {
                    return PartialView("PartialStok", YeniElliYuz);
                }
                else if (trend == "indirimli")
                {
                    return PartialView("PartialStok", IndirimliElliYuz);
                }
                else
                {
                    return PartialView("PartialStok", ElliYuz);
                }
            }
            else if (aralik == "101-150")
            {
                if (trend == "trend")
                {
                    return PartialView("PartialStok", trendyuzyuzelli);
                }
                else if (trend == "yeni")
                {
                    return PartialView("PartialStok", Yeniyuzyuzelli);
                }
                else if (trend == "indirimli")
                {
                    return PartialView("PartialStok", Indirimliyuzyuzelli);
                }
                else
                {
                    return PartialView("PartialStok", YuzYuzelli);
                }
            }
            else if (aralik == "151-250")
            {
                if (trend == "trend")
                {
                    return PartialView("PartialStok", trendYuzEllIkiYuz);
                }
                else if (trend == "yeni")
                {
                    return PartialView("PartialStok", YeniYuzEllIkiYuz);
                }
                else if (trend == "indirimli")
                {
                    return PartialView("PartialStok", IndirimliYuzEllIkiYuz);
                }
                else
                {
                    return PartialView("PartialStok", YuzEllIkiYuzElli);
                }
            }
            else if (aralik == "251-400")
            {
                if (trend == "trend")
                {
                    return PartialView("PartialStok", trendIkiYuzElliDortYuz);
                }
                else if (trend == "yeni")
                {
                    return PartialView("PartialStok", YeniIkiYuzElliDortYuz);
                }
                else if (trend == "indirimli")
                {
                    return PartialView("PartialStok", IndirimliIkiYuzElliDortYuz);
                }
                else
                {
                    return PartialView("PartialStok", IkiYuzElliDortYuz);
                }
            }
            else if (aralik == "401-2000")
            {
                if (trend == "trend")
                {
                    return PartialView("PartialStok", trendDortYuzIkibin);
                }
                else if (trend == "yeni")
                {
                    return PartialView("PartialStok", YeniDortYuzIkibin);
                }
                else if (trend == "indirimli")
                {
                    return PartialView("PartialStok", IndirimliDortYuzIkibin);
                }
                else
                {
                    return PartialView("PartialStok", DortYuzIkibin);
                }
            }
            else if (aralik == "2000+")
            {
                if (trend == "trend")
                {
                    return PartialView("PartialStok", trendIkibin);
                }
                else if (trend == "yeni")
                {
                    return PartialView("PartialStok", YeniIkibin);
                }
                else if (trend == "indirimli")
                {
                    return PartialView("PartialStok", IndirimliIkibin);
                }
                else
                {
                    return PartialView("PartialStok", Ikibin);
                }
            }
            else
            {
                return PartialView("PartialStok", urunler);
            }


        }
        public ActionResult YeniUrunler()
        {
            var urunler = db.Stok.Where(x => x.isYeni == true).ToList();
            var UrunSayisi = urunler.Count();
            var marka = db.Marka.ToList();
            ViewBag.Marka = marka;
            ViewBag.Urunler = urunler;
            ViewBag.StokSayisi = UrunSayisi;
            return View();
        }
        public ActionResult CokSatanlar()
        {
            var urunler = db.Stok.Where(x => x.isTrend == true).ToList();
            ViewBag.CokSatanlar = urunler;
            var UrunSayisi = urunler.Count();
            ViewBag.StokSayisi = UrunSayisi;
            var model = db.Menuler.Where(x => x.ParentID == null && x.AltMenuID == null).ToList();
            ViewBag.Marka = model;
            var marka = db.Marka.ToList();
            ViewBag.Markalar = marka;
            return View();
        }
    }
}