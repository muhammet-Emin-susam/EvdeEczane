//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EvdeEczane.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Yorumlar
    {
        public int ID { get; set; }
        public string YorumAdi { get; set; }
        public string YorumAciklama { get; set; }
        public Nullable<int> KullaniciID { get; set; }
        public Nullable<int> StokID { get; set; }
        public Nullable<System.DateTime> YorumTarihi { get; set; }
    
        public virtual Kullanicilar Kullanicilar { get; set; }
        public virtual Stok Stok { get; set; }
    }
}