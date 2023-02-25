using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystemNCapas.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public decimal Total { get; set; }

        public List<SaleDetail> SaleDetail { get; set; }
    }
}
