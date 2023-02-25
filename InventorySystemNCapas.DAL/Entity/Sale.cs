using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystemNCapas.DAL.Entity
{
    public class Sale
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CustomerId { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }

        public List<SaleDetail> SaleDetail { get; set; }
    }
}
