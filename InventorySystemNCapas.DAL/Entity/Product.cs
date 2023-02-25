using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystemNCapas.DAL.Entity
{
    public class Product
    {
        public string Sku { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
    }
}
