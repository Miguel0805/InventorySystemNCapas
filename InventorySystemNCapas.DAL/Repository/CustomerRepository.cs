using InventorySystemNCapas.DAL.Connection;
using InventorySystemNCapas.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystemNCapas.DAL.Repository
{
    public class CustomerRepository
    {
        private ConnectionDB _connectionDB;
        private SqlDataReader _dataReader;
        private DataTable _table;

        public CustomerRepository()
        {
            _connectionDB = new ConnectionDB();
            _table = new DataTable();
        }

        public bool Insert(Customer obj)
        {
            int rowsAffected = 0;
            string query = "INSERT INTO customer(name, address, email, phone)" +
                            "VALUES(@name, @address, @email, @phone)";

            using (var connection = _connectionDB.GetConnection())
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
                            "email = @email, phone = @phone" +
                            "WHERE id = @id;";

            using (var connection = _connectionDB.GetConnection())
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

            using (var connection = _connectionDB.GetConnection())
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

            using (var connection = _connectionDB.GetConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    _dataReader = command.ExecuteReader();
                    _table.Load(_dataReader);

                    connection.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return _table;
        }
    }
}
