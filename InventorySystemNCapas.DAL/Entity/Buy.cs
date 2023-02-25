using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystemNCapas.DAL.Entity
{
    public class Buy
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SupplierId { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }

        public List<BuyDetail> SaleDetail { get; set; }
    }
}
