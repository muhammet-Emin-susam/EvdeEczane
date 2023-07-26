using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using EvdeEczane.Models;
namespace EvdeEczane.Controllers
{
    public class HomeController : Controller
    {
        EVDEECZANEEntities3 db = new EVDEECZANEEntities3();
        public ActionResult Index()
        {
            var model = db.Slider.ToList();
            ViewBag.Slider = model;
            var model2 = db.Menuler.Where(x => x.ParentID == null && x.AltMenuID == null).ToList();
            ViewBag.Kategoriler = model2;
            var model3 = db.Stok.Where(x => x.isTrend == true).ToList();
            ViewBag.Trend = model3;
            var model4 = db.Stok.Where(x => x.StokBakiye < 4).ToList();
            ViewBag.CokSatanlar = model4;
            var model5 = db.Stok.Where(x => x.isYeni == true).ToList();
            ViewBag.Yeni = model5;
            var model6 = db.Stok.Where(x => x.isIndirim == true).ToList();
            ViewBag.Indirim = model6;
            var model7 = db.Bloglar.ToList();
            ViewBag.Bloglar = model7;
            var model8 = db.Marka.ToList();
            ViewBag.Markalar = model8;
            return View();
        }
        public ActionResult PartialViewSample()
        {
            using (EVDEECZANEEntities3 db = new EVDEECZANEEntities3())
            {
                List<Marka> marka = new List<Marka>();
            }

            var partial = db.Marka;

            return PartialView("getMarka", partial);
        }
        public ActionResult getMenu()
        {
            var partial = db.Menuler.ToList();

            return PartialView("getMenuler", partial);
        }
        public ActionResult Partial(string text, int kategoriID, string trend, string aralik)
        {
            var tru = true;
            var adanzye = db.Stok.Where(x => x.StokMarkaID == kategoriID).OrderBy(x => x.StokAdi).ToList();
            var artanfiyat = db.Stok.Where(x => x.StokMarkaID == kategoriID).OrderBy(x => x.StokFiyat).ToList();
            var urunler = db.Stok.Where(x => x.StokMarkaID == kategoriID).ToList();


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
            var trendurunler = trendd.Where(x => x.StokMarkaID == kategoriID).ToList();
            var trendartanfiyat = trendurunler.OrderBy(x => x.StokFiyat).ToList();
            var trendadanzye = trendurunler.OrderBy(x => x.StokAdi).ToList();


            var yeni = db.Stok.Where(x => x.isYeni == true).ToList();
            var yeniurunler = yeni.Where(x => x.StokMarkaID == kategoriID).ToList();
            var yeniartanfiyat = yeniurunler.OrderBy(x => x.StokFiyat).ToList();
            var yeniadanzye = yeniurunler.OrderBy(x => x.StokAdi).ToList();


            //var indirimliurunler = db.Stok.Where(x => x.isIndirim == true && x.StokKategoriID == kategoriID || x.AnaKategoriID == kategoriID || x.ParentKategoriID == kategoriID).ToList();
            var indirimli = db.Stok.Where(x => x.isIndirim == true).ToList();
            var indirimliurunler = indirimli.Where(x => x.StokMarkaID == kategoriID).ToList();
            var indirimliartanfiyat = indirimliurunler.OrderBy(x => x.StokFiyat).ToList();
            var indirimliadanzye = indirimliurunler.OrderBy(x => x.StokAdi).ToList();
            var partial = db.Stok.Where(x => x.StokKategoriID == kategoriID).ToList();
            if (aralik == null)
            {

                if (trend == "trend")
                {
                    if (text == "a-z")
                    {
                        return PartialView("StokPartial", trendadanzye);
                    }
                    else if (text == "arfiyat")
                    {
                        return PartialView("StokPartial", trendartanfiyat);
                    }
                    else
                    {
                        return PartialView("StokPartial", trendurunler);
                    }
                }
                else if (trend == "yeni")
                {
                    if (text == "a-z")
                    {
                        return PartialView("StokPartial", yeniadanzye);
                    }
                    else if (text == "arfiyat")
                    {
                        return PartialView("StokPartial", yeniartanfiyat);
                    }
                    else
                    {
                        return PartialView("StokPartial", yeniurunler);
                    }
                }
                else if (trend == "indirimli")
                {
                    if (text == "a-z")
                    {
                        return PartialView("StokPartial", indirimliadanzye);
                    }
                    else if (text == "arfiyat")
                    {
                        return PartialView("StokPartial", indirimliartanfiyat);
                    }
                    else
                    {
                        return PartialView("StokPartial", indirimliurunler);
                    }
                }
                else
                {
                    if (text == "a-z")
                    {
                        return PartialView("StokPartial", adanzye);
                    }
                    else if (text == "arfiyat")
                    {
                        return PartialView("StokPartial", artanfiyat);
                    }
                    else
                    {
                        return PartialView("StokPartial", urunler);
                    }

                }
            }
            if (aralik == "1-50")
            {
                if (trend == "trend")
                {
                    return PartialView("StokPartial", trendBirElli);
                }
                else if (trend == "yeni")
                {
                    return PartialView("StokPartial", YeniBirElli);
                }
                else if (trend == "indirimli")
                {
                    return PartialView("StokPartial", IndirimliBirElli);
                }
                else
                {
                    return PartialView("StokPartial", BirElli);
                }

            }
            else if (aralik == "51-100")
            {
                if (trend == "trend")
                {
                    return PartialView("StokPartial", trendElliYuz);
                }
                else if (trend == "yeni")
                {
                    return PartialView("StokPartial", YeniElliYuz);
                }
                else if (trend == "indirimli")
                {
                    return PartialView("StokPartial", IndirimliElliYuz);
                }
                else
                {
                    return PartialView("StokPartial", ElliYuz);
                }
            }
            else if (aralik == "101-150")
            {
                if (trend == "trend")
                {
                    return PartialView("StokPartial", trendyuzyuzelli);
                }
                else if (trend == "yeni")
                {
                    return PartialView("StokPartial", Yeniyuzyuzelli);
                }
                else if (trend == "indirimli")
                {
                    return PartialView("StokPartial", Indirimliyuzyuzelli);
                }
                else
                {
                    return PartialView("StokPartial", YuzYuzelli);
                }
            }
            else if (aralik == "151-250")
            {
                if (trend == "trend")
                {
                    return PartialView("StokPartial", trendYuzEllIkiYuz);
                }
                else if (trend == "yeni")
                {
                    return PartialView("StokPartial", YeniYuzEllIkiYuz);
                }
                else if (trend == "indirimli")
                {
                    return PartialView("StokPartial", IndirimliYuzEllIkiYuz);
                }
                else
                {
                    return PartialView("StokPartial", YuzEllIkiYuzElli);
                }
            }
            else if (aralik == "251-400")
            {
                if (trend == "trend")
                {
                    return PartialView("StokPartial", trendIkiYuzElliDortYuz);
                }
                else if (trend == "yeni")
                {
                    return PartialView("StokPartial", YeniIkiYuzElliDortYuz);
                }
                else if (trend == "indirimli")
                {
                    return PartialView("StokPartial", IndirimliIkiYuzElliDortYuz);
                }
                else
                {
                    return PartialView("StokPartial", IkiYuzElliDortYuz);
                }
            }
            else if (aralik == "401-2000")
            {
                if (trend == "trend")
                {
                    return PartialView("StokPartial", trendDortYuzIkibin);
                }
                else if (trend == "yeni")
                {
                    return PartialView("StokPartial", YeniDortYuzIkibin);
                }
                else if (trend == "indirimli")
                {
                    return PartialView("StokPartial", IndirimliDortYuzIkibin);
                }
                else
                {
                    return PartialView("StokPartial", DortYuzIkibin);
                }
            }
            else if (aralik == "2000+")
            {
                if (trend == "trend")
                {
                    return PartialView("StokPartial", trendIkibin);
                }
                else if (trend == "yeni")
                {
                    return PartialView("StokPartial", YeniIkibin);
                }
                else if (trend == "indirimli")
                {
                    return PartialView("StokPartial", IndirimliIkibin);
                }
                else
                {
                    return PartialView("StokPartial", Ikibin);
                }
            }
            else
            {
                return PartialView("StokPartial", urunler);
            }


        }
        public ActionResult KargoTakip()
        {
            var model = db.Menuler.Where(x => x.ParentID == null && x.AltMenuID == null).ToList();
            return View(model);
        }
        public ActionResult Bloglar()
        {
            var model = db.Bloglar.ToList();
            return View(model);
        }
        public ActionResult BlogDetay(int ID)
        {
            var model = db.Bloglar.Where(x => x.ID == ID).FirstOrDefault();
            return View(model);
        }
        public ActionResult SikcaSorulanSorular()
        {
            var yazi = "<h1><span style='font-size:12pt;color:#ef001b;font-weight:700'>Sıkça Sorulan Sorular</span></h1><p></p><p><span style='color:#ef001b;font-weight:700'>1- Evdeeczane internet sitesi üzerinden satılan ürünler orijinal midir?</span></p><p>Evet. Site üzerinden satışa sunulan her bir ürün orijinaldir. Ürünler, üretici ve ithalatçı firmalardan temin edilerek Evdeeczane güvencesi ile kullanıcılara sunulmaktadır.</p><p>Ürün Orijinallik Sorgulaması Nasıl Yapılır Detaylı Anlatım Sayfamız:</p><p><a href='https://www.evdeeczane.com/sayfa/orijinallik-sorgulamasi' target='_blank' title='Orijinallik Sorgulama'>https://www.evdeeczane.com/sayfa/orijinallik-sorgulamasi</a><br></p><p></p><p><span style='color:#ef001b;font-weight:700'>2- Kargo takibini nasıl gerçekleştirebilirim?</span></p><p>Siparişini verdiğiniz ürününüzün kargo takibini gerçekleştirmek için 'siparişlerim” kısmında yer alan alana giriş yaparak kargo takibinizi gerçekleştirebilirsiniz. Sorun yaşadığınız takdirde canlı destek veren ekiplerimiz ile anlık bir şekilde iletişime geçebilirsiniz.</p><p></p><p><span style='color:#ef001b;font-weight:700'>3- Siparişini verdiğim bir ürün elime ne zaman ulaşır?</span></p><p>Siparişinizi oluşturduktan sonra, sipariş operasyon ekibimiz tarafından sıraya alınarak paketleme aşamasına geçmektedir. Kargo çıkışları mümkün olan en kısa sürede sağlanır. Kampanya ve resmi tatil dönemlerinde kısa süreli gecikmeler yaşanabilmekle birlikte, siparişlerin kargo çıkışı sağlanmasının ardından ortalama teslimat süresi 3 iş günüdür. Havale ödemeli siparişlerde siparişiniz ödemenizi gerçekleştirdikten sonra işleme alınır.</p><p></p><p><span style='color:#ef001b;font-weight:700'>4- Alışveriş yaparken verdiğim kredi kartı bilgilerim güvende mi?</span></p><p>Kredi kartı bilgileriniz, alışverişinizi tamamlandıktan sonra site içerisinde sizlerin izni dışında hiçbir şekilde saklanmamaktadır. Ek olarak kredi kartı bilgileriniz ve şifreleriniz 128 bit SSL (Secure Sockets Layer) protokolü ile korunmaktadır.</p><p></p><p><span style='color:#ef001b;font-weight:700'>5- Sipariş içeriğimi bana ulaşana kadar kimler görebiliyor?</span></p><p>Siparişinizi ürünü paketleyen ekip arkadaşlarımız dışında size ulaşana kadar kimse göremiyor. Fatura ve özel bilgileriniz ürünlerinizin saklandığı kutular içerisinde muhafaza ediliyor.</p><p></p><p><span style='color:#ef001b;font-weight:700'>6- İnternet sitesi veya siparişim ile ilgili bir sorun yaşadığımda nasıl iletişime geçebilirim?</span></p><p>İnternet sitesi veya sipariş bölümünde yaşadığınız her türlü sorun için 'Canlı Destek” ekibi ile iletişime geçebilirsiniz. Canlı destek sekmesi internet sitesinin sağ alt tarafında belirmektedir. Canlı destek haricinde sosyal medya, telefon ve e-posta yoluyla da kolay bir şekilde iletişime geçmeniz mümkündür.</p><p></p><p><span style='color:#ef001b;font-weight:700'>7- Diğer orijinal ürünlere göre fiyatlarınız neden daha uygun?</span></p><p>Toplu bir şekilde satın alım yapılarak temin edilen ürünler, kullanıcının ekonomik şartlarını zorlamayacak şekilde, iki tarafın çıkarları esas alınarak fiyatlandırılmaktadır.</p><p></p><p><span style='color:#ef001b;font-weight:700'>8- Ürünlerin son kullanım tarihi nedir ?</span></p><p>Tüm ürünlerimiz son kullanım tarihi garantilidir, 6 aydan yakın tarihli ürünlerin gönderimi yapılmamaktadır.</p><p></p><p><span style='color:#ef001b;font-weight:700'>9- Ortalama kargo teslim süresi ne kadardır?</span></p><p>Kargo teslim süreleri 3 gün ile 5 gün arasında gerçekleşmektedir. Çalışma saatleri içerisinde verdiğiniz siparişlerin kargo teslimi daha kısa bir süre içerisinde gerçekleşir.</p><p></p><p><span style='color:#ef001b;font-weight:700'>10- Siparişini verdiğim ürünü hangi kargo firması getirecek?</span></p><p>Siparişleriniz tercihinize göre Sendeo Kargo ya da Yurtiçi Kargo firmaları ile sizlere ulaştırılmaktadır. Yaşadığınız herhangi bir sorun ile, alakalı tarafla iletişime geçebilirsiniz.</p><p></p><p><span style='color:#ef001b;font-weight:700'>11- Siparişimi nasıl iptal/iade edebilirim?</span></p><p>Siparişiniz, elinize ulaşıp ambalajı açıldıktan sonra belirli hijyen kuralları çerçevesinde iptal edilememektedir. Ambalajı açılmayan paketlerde ise, teslim aldıktan sonraki 15 günlük bir süre içerisinde e-posta yolu ile iletişime geçerek iade sürecini başlatabilirsiniz. İptal sürecini ise ürün kargoya verilmeden önce 'siparişlerim” kısmından gerçekleştirebilirsiniz.</p><p></p><p><span style='color:#ef001b;font-weight:700'>12- Ürün bedelinin iadesi ne kadar sürede yapılır ?</span></p><p>İade ettiğiniz ürünle ilgili tüm işlemler, ürününüz depomuza ulaştığı andan itibaren başlar ve 7 iş günü içerisinde sonuçlandırılır. İade tutarının kartınıza yansıma süresi, bankanıza göre değişiklik göstermektedir.</p>";
            ViewBag.SikcaSorulanSorular = yazi;
            return View();
        }
        public ActionResult Markalarimiz()
        {
            var model = db.Marka.ToList();
            ViewBag.Markalar = model;
            var model2 = db.Menuler.Where(x => x.ParentID == null && x.AltMenuID == null).ToList();
            ViewBag.Kategoriler = model2;
            return View();
        }
        public ActionResult MarkaDetay(int ID)
        {
            var marka = db.Marka.Where(x => x.ID == ID).FirstOrDefault();
            var model = db.Stok.Where(x => x.StokMarkaID == ID).ToList();
            var UrunSayisi = model.Count();
            var model2 = db.Menuler.Where(x => x.ParentID == null && x.AltMenuID == null).ToList();
            ViewBag.Kategoriler = model2;
            ViewBag.StokSayisi = UrunSayisi;
            ViewBag.Urunler = model;
            return View(marka);
        }
        public ActionResult Sayfalar()
        {
            return View();
        }
        public ActionResult MusteriHizmetleri()
        {
            var model = db.Menuler.Where(x => x.ParentID == null && x.AltMenuID == null).ToList();
            return View(model);
        }
        public ActionResult Iletisim()
        {
            var html = "<div class='contentbox-body'><div class='list-group list-group-flush contact-us'><div class='list-group-item'><div class='row'><div class='col-12 col-lg-3'>Firma Adı</div><div class='col-12 col-lg-4'>Evdeeczane.com</div></div></div><div class='list-group-item'><div class='row'><div class='col-12 col-lg-3'>Firma Resmi Adı</div><div class='col-12 col-lg-4'>AYAZ VE ORTAKLARI LTD ŞTİ</div></div></div><div class='list-group-item'><div class='row'><div class='col-12 col-lg-3'>Yetkili Kişi</div><div class='col-12 col-lg-4'>TOLGAHAN AYAZ</div></div></div><div class='list-group-item'><div class='row'><div class='col-12 col-lg-3'>Telefon 1</div><div class='col-12 col-lg-4'><a href='tel:+908503052571'>08503052571</a></div></div></div><div class='list-group-item'><div class='row'><div class='col-12 col-lg-3'>Telefon 2</div><div class='col-12 col-lg-4'><a href='tel:+905426429277'>05426429277</a></div></div></div><div class='list-group-item'><div class='row'><div class='col-12 col-lg-3'>Fax</div><div class='col-12 col-lg-4'>0232 8327570</div></div></div><div class='list-group-item'><div class='row'><div class='col-12 col-lg-3'>E-mail</div><div class='col-12 col-lg-4'><a href='mailto:info@evdeeczane.com'>info@evdeeczane.com</a></div></div></div><div class='list-group-item'><div class='row'><div class='col-12 col-lg-3'>Adres</div><div class='col-12 col-lg-4'>UĞUR MUMCU MAH. 1347 SOK. NO:12 Bodrum E MENEMEN İZMİR</div></div></div><div class='list-group-item'><div class='row'><div class='col-12 col-lg-3'>Ülke</div><div class='col-12 col-lg-4'>Türkiye</div></div></div><div class='list-group-item'><div class='row'><div class='col-12 col-lg-3'>Şehir</div><div class='col-12 col-lg-4'>İzmir</div></div></div><div class='list-group-item'><div class='row'><div class='col-12 col-lg-3'>Semt</div><div class='col-12 col-lg-4'>Menemen</div></div></div><div class='list-group-item'><div class='row'><div class='col-12 col-lg-3'>Vergi No</div><div class='col-12 col-lg-4'>1080045459</div></div></div><div class='list-group-item'><div class='row'><div class='col-12 col-lg-3'>Vergi Dairesi</div><div class='col-12 col-lg-4'>MENEMEN</div></div></div><div class='list-group-item'><div class='row'><div class='col-12 col-lg-3'>Kep Adresi</div><div class='col-12 col-lg-4'>T.han@hs01.kep.tr</div></div></div><div class='list-group-item'><div class='row'><div class='col-12 col-lg-3'>Mersis No</div><div class='col-12 col-lg-4'>0108004545900017</div></div></div><div class='list-group-item'><div class='row'><div class='col-12 col-lg-3'>Havale Bildirim Formu</div><div class='col-12 col-lg-7'><a href='/hesabim/havale-bildirim'>Havale Bildirim Formu</a></div></div></div><div class='list-group-item'><div class='row'><div class='col-12 col-lg-3'>Arıza / İade Başvurusu</div><div class='col-12 col-lg-7'><a href='/iade-ve-iptal-formu'>Arıza / İade Başvurusu</a></div></div></div><div class='list-group-item'><div class='row'><div class='col-12 col-lg-3'>İletişim Formu</div><div class='col-12 col-lg-4'><a href='/iletisim-formu'>İletişim Formu</a></div></div></div></div></div>";
            ViewBag.Html = html;
            return View();
        }

    }
}