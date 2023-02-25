using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystemNCapas.Models.DTOs
{
    public class BuyDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public DateTime Date { get; set; }

        public decimal Total { get; set; }

        public IEnumerable<BuyDetailDTO> BuyDetail { get; set; }
    }
}
