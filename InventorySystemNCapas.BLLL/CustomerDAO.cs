using InventorySystemNCapas.DAL.Repository;
using InventorySystemNCapas.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace InventorySystemNCapas.BLL
{
    public class CustomerDAO
    {
        private CustomerRepository _customerRepository;
        
        public CustomerDAO()
        {
            _customerRepository = new CustomerRepository();
        }

        public DataTable GetAll()
        {
            return _customerRepository.GetAll();
        }
        
        public bool Insert(Customer obj)
        {
            if (RegisterExist(obj))
            {
                throw new Exception($"The customer name: {obj.Name} is already registered.");
            }
            return _customerRepository.Insert(obj);
        }

        public bool Update(int id, Customer obj)
        {
            return _customerRepository.Update(id, obj);
        }

        public bool Delete(int id)
        {
            return _customerRepository.Delete(id);
        }

        public List<Customer> GetCustomers()
        {
            return _customerRepository.GetCustomers().ToList();
        }
        public Customer GetById(int id)
        {
            return _customerRepository.GetById(id);
        }



        #region logic methods
        public bool RegisterExist(Customer obj)
        {
            var queryable = _customerRepository.GetCustomers();
            return queryable.Any(x => x.Name == obj.Name);
        }
        #endregion
    }
}
