using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EvdeEczane.Models
{
    public class isStatic
    {
        public static List<Marka> getMarka()
        {
            using (EVDEECZANEEntities3 db = new EVDEECZANEEntities3())
            {
                return db.Marka.ToList();
            }
        }
    }
}