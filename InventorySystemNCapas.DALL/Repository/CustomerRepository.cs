using InventorySystemNCapas.DAL.Connection;
using InventorySystemNCapas.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices.ComTypes;

namespace InventorySystemNCapas.DAL.Repository
{
    public class CustomerRepository
    {
        private ConnectionDB _connectionDB = new ConnectionDB();
        private SqlDataReader _dataReader;
        private DataTable _table;

        public CustomerRepository()
        {
            _table = new DataTable();
        }

        public bool Insert(Customer obj)
        {
            int rowsAffected = 0;
            string query = "INSERT INTO customer(name, address, email, phone)" +
                            "VALUES(@name, @address, @email, @phone)";

            using (var connection = _connectionDB.GetConnection)
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@name", obj.Name);
                command.Parameters.AddWithValue("@address", obj.Address);
                command.Parameters.AddWithValue("@email", obj.Email);
                command.Parameters.AddWithValue("@phone", obj.Phone);

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

        public bool Update(int id, Customer obj)
        {
            int rowsAffected = 0;
            string query = "UPDATE customer SET name = @name, address = @address," +
                            "email = @email, phone = @phone " +
                            "WHERE id = @id;";

            using (var connection = _connectionDB.GetConnection)
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@name", obj.Name);
                command.Parameters.AddWithValue("@address", obj.Address);
                command.Parameters.AddWithValue("@email", obj.Email);
                command.Parameters.AddWithValue("@phone", obj.Phone);
                command.Parameters.AddWithValue("@id", id);

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

        public bool Delete(int id)
        {
            int rowsAffected = 0;
            string query = "DELETE FROM customer WHERE id = @id;";

            using (var connection = _connectionDB.GetConnection)
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

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
            string query = "SELECT id, name, address, email, phone FROM customer";

            using (var connection = _connectionDB.GetConnection)
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

        public IEnumerable<Customer> GetCustomers()
        {
            IEnumerable<Customer> customers;
            string query = "SELECT id, name, address, email, phone " +
                            "FROM customer;";

            using (var connection = _connectionDB.GetConnection)
            {
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    _dataReader = command.ExecuteReader();
                    var list = new List<Customer>();

                    while (_dataReader.Read())
                    {
                        var customer = new Customer();

                        customer.Id = _dataReader.GetInt32(0);
                        customer.Name = _dataReader.GetString(1);
                        customer.Address = _dataReader.GetString(2);
                        customer.Email = _dataReader.GetString(3);
                        customer.Phone = _dataReader.GetString(4);

                        list.Add(customer);
                    }

                    customers = list;
                    _dataReader.Close();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return customers;
        }

        public Customer GetById(int id)
        {
            Customer customer;
            string query = "SELECT id, name, address, email, phone " +
                            "FROM customer " +
                            "WHERE id = @id";

            using (var connection = _connectionDB.GetConnection)
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                try
                {
                    _dataReader = command.ExecuteReader();
                    customer = new Customer();

                    while (_dataReader.Read())
                    {
                        customer.Id = _dataReader.GetInt32(0);
                        customer.Name = _dataReader.GetString(1);
                        customer.Address = _dataReader.GetString(2);
                        customer.Email = _dataReader.GetString(3);
                        customer.Phone = _dataReader.GetString(4);
                    }
                    
                    _dataReader.Close();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return customer;
            }
        }
    }
}
