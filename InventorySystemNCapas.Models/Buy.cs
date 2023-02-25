using System;
using System.Collections.Generic;

namespace InventorySystemNCapas.Models
{
    public class Buy
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }

        public List<BuyDetail> BuyDetail { get; set; }
    }
}
