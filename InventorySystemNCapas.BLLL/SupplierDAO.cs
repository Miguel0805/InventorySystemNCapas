using InventorySystemNCapas.DALL.Repository;
using InventorySystemNCapas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace InventorySystemNCapas.BLLL
{
    public class SupplierDAO
    {
        private SupplierRepository _supplierRepository;
        private DataTable _table;

        public SupplierDAO()
        {
            _supplierRepository = new SupplierRepository();
            _table = new DataTable();
        }

        public bool Insert(Supplier obj)
        {
            if (RegisterExist(obj))
            {
                throw new Exception($"The supplier name: {obj.Name} is already registered.");
            }

            return _supplierRepository.Insert(obj);
        }

        public bool Update(int id, Supplier obj)
        {
            return _supplierRepository.Update(id, obj);

        }

        public bool Delete(int id)
        {
            return _supplierRepository.Delete(id);
        }

        public DataTable GetAll()
        {
            return _supplierRepository.GetAll();
        }

        public Supplier GetById(int id)
        {
            return _supplierRepository.GetById(id);
        }

        public List<Supplier> GetSuppliers()
        {
            return _supplierRepository.GetSuppliers().ToList();
        }

        #region logic methods
        public bool RegisterExist(Supplier obj)
        {
            var queryable = _supplierRepository.GetSuppliers();

            return queryable.Any(x => x.Name == obj.Name);
        }

        #endregion

    }
}
