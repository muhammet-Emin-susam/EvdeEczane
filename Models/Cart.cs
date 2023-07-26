using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EvdeEczane.Models;

namespace EvdeEczane.Models
{
    public class Cart
    {
        private List<Cartline> _cardLines = new List<Cartline>();
        public List<Cartline> Cartlines
        {
            get { return _cardLines; }
        }
        public void AddProduct(Stok product, int Quantity)
        {
            var line = _cardLines.Where(i => i.Product.ID == product.ID).FirstOrDefault();

            if (line == null)
            {
                _cardLines.Add(new Cartline() { Product = product, Quantity = Quantity });
            }
            else
            {
                line.Quantity += Quantity;
            }
        }
        public void DeleteProduct(Stok product)
        {
            _cardLines.RemoveAll(i => i.Product.ID == product.ID);
        }
        public double Total()
        {
            return (double)_cardLines.Sum(x => x.Product.StokFiyat * x.Quantity);
        }
        public void Clear()
        {
            _cardLines.Clear(); ;
        }

    }
    public class Cartline
    {
        public Stok Product { get; set; }
        public int Quantity { get; set; }
    }
}