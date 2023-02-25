using InventorySystemNCapas.DALL.Repository;
using InventorySystemNCapas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace InventorySystemNCapas.BLLL
{
    public class ProductDAO
    {
        private ProductRepository _productRepository;
        private DataTable _table;

        public ProductDAO()
        {
            _productRepository= new ProductRepository();
            _table = new DataTable();
        }

        public bool Insert(Product obj)
        {
            var productBySku = _productRepository.GetBySku(obj.Sku);

            if (productBySku != null)
            {
                throw new Exception("There is already a product register with the same SKU.");
            }

            return _productRepository.Insert(obj);
        }

        public bool Update(string sku, Product obj)
        {
            return _productRepository.Update(sku, obj);
        }

        public bool Delete(string sku)
        {
            return _productRepository.Delete(sku);
        }

        public DataTable GetAll()
        {
            return _productRepository.GetAll();
        }

        public Product GetBySku(string sku)
        {
            return _productRepository.GetBySku(sku);
        }

        public List<Product> GetProducts()
        {
            return _productRepository.GetProducts().ToList();
        }
    }
}
