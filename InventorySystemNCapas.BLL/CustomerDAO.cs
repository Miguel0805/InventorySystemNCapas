using InventorySystemNCapas.DAL.Repository;
using System.Data;

namespace InventorySystemNCapas.BLL
{
    public class CustomerDAO
    {
        private CustomerRepository repository;
        
        public CustomerDAO()
        {
            repository = new CustomerRepository();
        }

        public DataTable GetAll()
        {
            var customersTable = repository.GetAll();
            return customersTable;
        }
        


    }
}
