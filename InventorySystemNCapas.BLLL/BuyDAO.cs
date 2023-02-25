using InventorySystemNCapas.DALL.Repository;
using InventorySystemNCapas.Models;
using InventorySystemNCapas.Models.DTOs;
using System.Data;
using System.Data.SqlClient;

namespace InventorySystemNCapas.BLLL
{
    public class BuyDAO
    {
        private BuyRepository _buyRepository;
        private SqlDataReader _dataReader;
        private DataTable _table;
        
        public BuyDAO()
        {
            _buyRepository = new BuyRepository();
            _table = new DataTable();
        }

        public bool Insert(Buy obj)
        {
            return _buyRepository.Insert(obj);
        }

        public bool Update(int id, Buy obj)
        {
            return _buyRepository.Update(id, obj);
        }

        public bool Delete(int id)
        {
            return _buyRepository.Delete(id);
        }

        public DataTable GetAll()
        {
            return _buyRepository.GetAll();
        }

        public Buy GetById(int id)
        {
            return _buyRepository.GetById(id);
        }
    }
}
