using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystemNCapas.Models.DTOs
{
    public class BuyDetailDTO
    {
        public int Id { get; set; }
        public int BuyId { get; set; }
        public string ProductSku { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }  
        public int Units { get; set; }
        public decimal Discount { get; set; }
        public decimal Subtotal { get; set; }

    }
}
