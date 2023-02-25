using InventorySystemNCapas.DALL.Repository;
using InventorySystemNCapas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystemNCapas.BLLL
{
    public class SaleDAO
    {
        private SaleRepository _saleRepository;
        private DataTable _table;

        public SaleDAO()
        {
            _saleRepository = new SaleRepository();
        }

        public bool Insert(Sale obj)
        {
            return _saleRepository.Insert(obj);
        }

        public bool Update(int id, Sale obj)
        {
            return _saleRepository.Update(id, obj);
        }

        public bool Delete(int id)
        {
            return _saleRepository.Delete(id);
        }

        public DataTable GetAll()
        {
            return _saleRepository.GetAll();
        }

        public Sale GetById(int id)
        {
            return _saleRepository.GetById(id);
        }
    }
}
