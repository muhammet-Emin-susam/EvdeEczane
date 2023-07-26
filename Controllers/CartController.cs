using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EvdeEczane.Models;
namespace EvdeEczane.Controllers
{
    public class CartController : Controller
    {
        EVDEECZANEEntities3 db = new EVDEECZANEEntities3();
        // GET: Cart
        public ActionResult Index()
        {
            return View(GetCart());
        }
        public JsonResult AddCart(int ID)
        {
            var product = db.Stok.Where(i => i.ID == ID).FirstOrDefault();

            if (product != null)
            {
                GetCart().AddProduct(product, 1);
            }

            return Json("Başarılı");
        }
        public PartialViewResult Summary()
        {
            return PartialView(GetCart());
        }
        public JsonResult RemoveCart(int ID)
        {
            var product = db.Stok.Where(i => i.ID == ID).FirstOrDefault();

            if (product != null)
            {
                GetCart().DeleteProduct(product);
            }
            return Json("Başarılı");
        }
        public Cart GetCart()
        {
            var cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }

            return cart;
        }
        public ActionResult SatinAl(int UserID)
        {
            Cart crt = new Cart();
            var cart = GetCart();
            List<Cartline> _cardLines = new List<Cartline>();
            //var order = new Order();
            //db.Orders.Add(new Orders()
            //{
            //    UserID = UserID,
            //    AddressID = AddressID,
            //    AddedDate = DateTime.Now,
            //    Status = "SA",
            //});
            var order = new Siparisler()
            {
                KullaniciID = UserID,
                SiparisTarihi = DateTime.Now,
                ToplamTutar = cart.Cartlines.Sum(x => x.Product.StokFiyat * x.Quantity)
            };
            db.Siparisler.Add(order);
            db.SaveChanges();
            //SaveOrder(crt);
            foreach (var pr in cart.Cartlines)
            {
                db.SiparisDetay.Add(new SiparisDetay()
                {
                    Adet = pr.Quantity,
                    Fiyat = pr.Product.StokFiyat,
                    UrunID = pr.Product.ID,
                    SiparisID = order.ID
                });
                var _product = db.Stok.Where(x => x.ID == pr.Product.ID).FirstOrDefault();
                _product.StokBakiye = _product.StokBakiye - pr.Quantity;
            }


            try
            {
                db.SaveChanges();
                ViewBag.Msg = "Kayıt Başarıyla Oluşturuldu.";
                cart.Clear();
                return Json("Başarılı");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}