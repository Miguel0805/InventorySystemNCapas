using InventorySystemNCapas.DAL.Connection;
using InventorySystemNCapas.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystemNCapas.DALL.Repository
{
    public class SaleRepository
    {
        private ConnectionDB _connectionDB;
        private SqlDataReader _dataReader;
        private DataTable _table;

        public SaleRepository() 
        {
            _connectionDB = new ConnectionDB();
            _table = new DataTable();
        } 

        public bool Insert(Sale obj)
        {
            string query = "sp_insert_sale", errorMessage = "";

            using (var connection = _connectionDB.GetConnection)
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;

                string stringDetail = BuildStringSaleDetail(obj.SaleDetail);

                command.Parameters.AddWithValue("@userId", obj.UserId);
                command.Parameters.AddWithValue("@customerId", obj.CustomerId);
                command.Parameters.AddWithValue("@date", obj.Date);
                command.Parameters.AddWithValue("@total", obj.Total);
                command.Parameters.AddWithValue("@detail", stringDetail);

                try
                {
                    _dataReader = command.ExecuteReader();

                    while (_dataReader.Read())
                    {
                        errorMessage = (_dataReader.IsDBNull(5)) ? "" : _dataReader.GetString(5);
                    }
                    _dataReader.Close();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                throw new Exception(errorMessage);
            }

            return true;
        }

        public bool Update(int id, Sale obj)
        {
            string query = "sp_update_sale", errorMessage = "";

            using (var connection = _connectionDB.GetConnection)
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;

                string stringDetail = BuildStringSaleDetail(obj.SaleDetail);

                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@userId", obj.UserId);
                command.Parameters.AddWithValue("@customerId", obj.CustomerId);
                command.Parameters.AddWithValue("@date", obj.Date);
                command.Parameters.AddWithValue("@total", obj.Total);
                command.Parameters.AddWithValue("@detail", stringDetail);

                try
                {
                    _dataReader = command.ExecuteReader();

                    while (_dataReader.Read())
                    {
                        errorMessage = (_dataReader.IsDBNull(5)) ? "" : _dataReader.GetString(5) ;
                    }

                    _dataReader.Close();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                throw new Exception(errorMessage);
            }

            return true;
        }

        public bool Delete(int id)
        {
            string query = "sp_delete_sale", errorMessage = "";

            using (var connection = _connectionDB.GetConnection)
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@id", id);

                try
                {
                    _dataReader = command.ExecuteReader();

                    while (_dataReader.Read())
                    {
                        errorMessage = (_dataReader.IsDBNull(5)) ? "" : _dataReader.GetString(5);
                    }

                    _dataReader.Close();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                throw new Exception(errorMessage);
            }

            return true;
        }

        public DataTable GetAll()
        {
            string query = "SELECT s.id, u.username, c.name AS customer, s.date, s.total " +
                            "FROM sale s " +
                            "LEFT JOIN customer c ON(c.id = s.customer_id) " +
                            "LEFT JOIN users u ON(u.id = s.user_id);";

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

        public Sale GetById(int id)
        {
            Sale saleDTO = new Sale();
            string query = "SELECT s.id, s.user_id, u.username, s.customer_id, c.name AS customer, " +
                "s.date, s.total, sd.product_sku, p.name, sd.price, sd.units, sd.discount, sd.subtotal " +
                "FROM sale s " +
                "LEFT JOIN sale_detail sd ON(sd.sale_id = s.id) " +
                "LEFT JOIN customer c ON(c.id = s.customer_id) " +
                "LEFT JOIN users u ON(u.id = s.user_id) " +
                "LEFT JOIN product p ON(p.sku = sd.product_sku) " +
                "WHERE s.id = @id;";

            using (var connection = _connectionDB.GetConnection)
            {
                SqlCommand command = new SqlCommand(query, connection);
                bool headerFill = false;
                command.Parameters.AddWithValue("@id", id);

                try
                {
                    _dataReader = command.ExecuteReader();

                    if (!_dataReader.HasRows)
                    {
                        return null;
                    }

                    List<SaleDetail> saleDetail = new List<SaleDetail>();

                    while (_dataReader.Read())
                    {
                        if (headerFill == false)
                        {
                            saleDTO.Id = _dataReader.GetInt32(0);
                            saleDTO.UserId = _dataReader.GetInt32(1);
                            saleDTO.Username = _dataReader.GetString(2);
                            saleDTO.CustomerId = _dataReader.GetInt32(3);
                            saleDTO.CustomerName = _dataReader.GetString(4);
                            saleDTO.Date = _dataReader.GetDateTime(5);
                            saleDTO.Total = _dataReader.GetDecimal(6);

                            headerFill = true;
                        }

                        var detail = new SaleDetail()
                        {
                            ProductSku = _dataReader.GetString(7),
                            ProductName = _dataReader.GetString(8),
                            Price = _dataReader.GetDecimal(9),
                            Units = _dataReader.GetInt32(10),
                            Discount = _dataReader.GetDecimal(11),
                            Subtotal = _dataReader.GetDecimal(12)
                        };

                        saleDetail.Add(detail);
                    }

                    saleDTO.SaleDetail = saleDetail;
                    _dataReader.Close();
                    connection.Close();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return saleDTO;
        }

        public string BuildStringSaleDetail(List<SaleDetail> detail)
        {
            string detailIntoString = "";

            for (int i = 0; i < detail.Count; i++)
            {
                detailIntoString += $"{detail[i].ProductSku},{detail[i].Price}," +
                    $"{detail[i].Units},{detail[i].Discount},{detail[i].Subtotal}";

                detailIntoString += ((detail.Count() > 1) && (i < detail.Count() - 1)) ? "|" : "";
            }

            return detailIntoString;
        }
    }
}
