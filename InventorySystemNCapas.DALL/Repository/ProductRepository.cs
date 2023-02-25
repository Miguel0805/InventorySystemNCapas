using InventorySystemNCapas.DAL.Connection;
using InventorySystemNCapas.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace InventorySystemNCapas.DALL.Repository
{
    public class ProductRepository
    {
        private ConnectionDB _connnectionDB;
        private SqlDataReader _dataReader;
        private DataTable _table;

        public ProductRepository() 
        {
            _connnectionDB = new ConnectionDB();
            _table = new DataTable();
        }

        public bool Insert(Product obj)
        {
            int rowsAffected = 0;
            string query = "INSERT INTO product(sku, name, description, price, stock) " +
                            "VALUES(@sku, @name, @description, @stock)";

            using (var connection = _connnectionDB.GetConnection)
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@sku", obj.Sku);
                command.Parameters.AddWithValue("@name", obj.Name);
                command.Parameters.AddWithValue("@description", obj.Description);
                command.Parameters.AddWithValue("@price", obj.Price);
                command.Parameters.AddWithValue("@stock", obj.Stock);

                try
                {
                    rowsAffected = command.ExecuteNonQuery();

                    connection.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return rowsAffected > 0;
        }

        public bool Update(string sku, Product obj)
        {
            int rowsAffected = 0;
            string query = "UPDATE product SET sku=@sku, name=@name, description=@description, price=@price " +
                "WHERE sku=@currentSku";

            using (var connection = _connnectionDB.GetConnection)
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@sku", obj.Sku);
                command.Parameters.AddWithValue("@name", obj.Name);
                command.Parameters.AddWithValue("@description", obj.Description);
                command.Parameters.AddWithValue("@price", obj.Price);
                command.Parameters.AddWithValue("@currentSku", sku);

                try
                {
                    rowsAffected = command.ExecuteNonQuery();

                    connection.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return rowsAffected > 0;
        }

        public bool Delete(string sku)
        {
            int rowsAffected = 0;
            string query = "DELETE FROM product WHERE sku=@sku";

            using (var connection = _connnectionDB.GetConnection)
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@sku", sku);

                try
                {
                    rowsAffected = command.ExecuteNonQuery();
                    
                    connection.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return rowsAffected > 0;
        }

        public DataTable GetAll()
        {
            string query = "SELECT sku, name, description, price, stock FROM product";

            using (var connection = _connnectionDB.GetConnection)
            {
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    _dataReader = command.ExecuteReader();
                    _table.Load(_dataReader);

                    _dataReader.Close();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return _table;
        }

        public Product GetBySku(string sku)
        {
            Product product;
            string query = "SELECT sku, name, description, price, stock " +
                "FROM product " +
                "WHERE sku = @sku";

            using (var connection = _connnectionDB.GetConnection)
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@sku", sku);

                try
                {
                    _dataReader = command.ExecuteReader();

                    product = (_dataReader.HasRows) ? new Product() : null;

                    while (_dataReader.Read())
                    {
                        product.Sku = _dataReader.GetString(0);
                        product.Name = _dataReader.GetString(1);
                        product.Description = _dataReader.GetString(2);
                        product.Price = _dataReader.GetDecimal(3);
                        product.Stock = _dataReader.GetInt32(4);
                    }

                    _dataReader.Close();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return product;
        }

        public IEnumerable<Product> GetProducts()
        {
            IEnumerable<Product> products;
            string query = "SELECT sku, name, description, price, stock " +
                            "FROM product;";

            using (var connection = _connnectionDB.GetConnection)
            {
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    _dataReader = command.ExecuteReader();
                    var list = new List<Product>();

                    while (_dataReader.Read())
                    {
                        var product = new Product();

                        product.Sku = _dataReader.GetString(0);
                        product.Name = _dataReader.GetString(1);
                        product.Description = _dataReader.GetString(2);
                        product.Price = _dataReader.GetDecimal(3);
                        product.Stock = _dataReader.GetInt32(4);

                        list.Add(product);
                    }

                    products = list;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return products;
        }
    }
}
