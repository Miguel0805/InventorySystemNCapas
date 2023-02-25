using InventorySystemNCapas.DAL.Connection;
using InventorySystemNCapas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace InventorySystemNCapas.DALL.Repository
{
    public class SupplierRepository
    {
        private ConnectionDB _connectionDB;
        private DataTable _table;
        private SqlDataReader _dataReader;

        public SupplierRepository()
        {
            _connectionDB = new ConnectionDB();
            _table = new DataTable();
        }

        public bool Insert(Supplier obj)
        {
            int rowsAffected = 0;
            string query = "INSERT INTO supplier(name, address, email, phone)" +
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

        public bool Update(int id, Supplier obj)
        {
            int rowsAffected = 0;
            string query = "UPDATE supplier SET name=@name, address=@address, email=@email," +
                "phone=@phone WHERE id = @id";

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
            string query = "DELETE FROM supplier WHERE id=@id";

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
            string query = "SELECT id, name, address, email, phone " +
                            "FROM supplier";

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

        public Supplier GetById(int id)
        {
            Supplier supplier;
            string query = "SELECT id, name, address, email, phone " +
                            "FROM supplier " +
                            "WHERE id = @id;";

            using (var connection = _connectionDB.GetConnection)
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                try
                {
                    _dataReader = command.ExecuteReader();
                    supplier = new Supplier();

                    while (_dataReader.Read())
                    {
                        supplier.Id = _dataReader.GetInt32(0);
                        supplier.Name = _dataReader.GetString(1);
                        supplier.Address = _dataReader.GetString(2);
                        supplier.Email = _dataReader.GetString(3);
                        supplier.Phone = _dataReader.GetString(4);
                    }

                    _dataReader.Close();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

            return supplier;
        }

        public DataTable GetByQuery(string query, Dictionary<string, string> parameters)
        {
            using (var connection = _connectionDB.GetConnection)
            {
                SqlCommand command = new SqlCommand(query, connection);

                foreach (var item in parameters)
                {
                    command.Parameters.AddWithValue($"{item.Key}", $"{item.Value}");
                }

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

        public IEnumerable<Supplier> GetSuppliers()
        {
            IEnumerable<Supplier> suppliers;
            string query = "SELECT id, name, address, email, phone " +
                "FROM supplier;";

            using (var connection = _connectionDB.GetConnection)
            {
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    _dataReader = command.ExecuteReader();
                    List<Supplier> list = new List<Supplier>();

                    while (_dataReader.Read())
                    {
                        Supplier supplier = new Supplier();

                        supplier.Id = _dataReader.GetInt32(0);
                        supplier.Name = _dataReader.GetString(1);
                        supplier.Address = _dataReader.GetString(2);
                        supplier.Email = _dataReader.GetString(3);
                        supplier.Phone = _dataReader.GetString(4);

                        list.Add(supplier);
                    }

                    suppliers = list;

                    _dataReader.Close();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return suppliers;
        }

        
    }
}
