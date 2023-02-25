using InventorySystemNCapas.DAL.Connection;
using InventorySystemNCapas.Models;
using InventorySystemNCapas.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace InventorySystemNCapas.DALL.Repository
{
    public class BuyRepository
    {
        private ConnectionDB _connectionDB;
        private SqlDataReader _dataReader;
        private DataTable _table;

        public BuyRepository()
        {
            _connectionDB = new ConnectionDB();
            _table = new DataTable();
        }

        public bool Insert(Buy obj)
        {
            string query = "sp_insert_buy", errorMessage = "";

            using (var connection = _connectionDB.GetConnection)
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;

                string stringDetail = BuildStringBuyDetail(obj.BuyDetail);

                command.Parameters.AddWithValue("@supplierId", obj.SupplierId);
                command.Parameters.AddWithValue("@userId", obj.UserId);
                command.Parameters.AddWithValue("@date", obj.Date);
                command.Parameters.AddWithValue("@total", obj.Total);
                command.Parameters.AddWithValue("@detail", stringDetail);

                try
                {
                    _dataReader = command.ExecuteReader();
                    //Console.WriteLine($"{_dataReader["ErrorNumber"]}, {_dataReader["ErrorMessage"]}");

                    while (_dataReader.Read())
                    {
                        errorMessage = (!_dataReader.IsDBNull(5)) ? _dataReader.GetString(5) : "";
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

        public bool Update(int id, Buy obj)
        {
            string query = "sp_update_buy", errorMessage = "";

            using (var connection = _connectionDB.GetConnection)
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;

                string stringDetail = BuildStringBuyDetail(obj.BuyDetail);

                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@supplierId", obj.SupplierId);
                command.Parameters.AddWithValue("@userId", obj.UserId);
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

        public bool Delete(int id)
        {
            string query = "sp_delete_buy", errorMessage = "";

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
                throw new Exception($"{errorMessage}");
            }

            return true;
        }

        public DataTable GetAll()
        {
            string query = "SELECT b.id, u.username, s.name AS supplier, b.date, b.total\r\n\tFROM buy b\r\n\tLEFT JOIN supplier s ON(s.id = b.supplier_id)\r\n\tLEFT JOIN users u ON(u.id = b.user_id);";

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

        public Buy GetById(int id)
        {
            Buy buyDTO = new Buy();
            string query = "SELECT b.id, b.user_id, u.username, b.supplier_id, s.name AS supplier, " +
                "b.date, b.total, bd.product_sku, p.name, bd.price, bd.units, bd.discount, bd.subtotal " +
                "FROM buy b " +
                "LEFT JOIN buy_detail bd ON(bd.buy_id = b.id) " +
                "LEFT JOIN supplier s ON(s.id = b.supplier_id) " +
                "LEFT JOIN users u ON(u.id = b.user_id) " +
                "LEFT JOIN product p ON(p.sku = bd.product_sku) " +
                "WHERE b.id = @id;";

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

                    List<BuyDetail> buyDetail = new List<BuyDetail>();

                    while (_dataReader.Read())
                    {
                        if (headerFill == false)
                        {
                            buyDTO.Id = _dataReader.GetInt32(0);
                            buyDTO.UserId = _dataReader.GetInt32(1);
                            buyDTO.Username = _dataReader.GetString(2);
                            buyDTO.SupplierId = _dataReader.GetInt32(3);
                            buyDTO.SupplierName = _dataReader.GetString(4);
                            buyDTO.Date = _dataReader.GetDateTime(5);
                            buyDTO.Total = _dataReader.GetDecimal(6);

                            headerFill = true;
                        }

                        var detail = new BuyDetail()
                        {
                            ProductSku = _dataReader.GetString(7),
                            ProductName = _dataReader.GetString(8),
                            Price = _dataReader.GetDecimal(9),
                            Units = _dataReader.GetInt32(10),
                            Discount = _dataReader.GetDecimal(11),
                            Subtotal = _dataReader.GetDecimal(12)
                        };

                        buyDetail.Add(detail);
                    }

                    buyDTO.BuyDetail = buyDetail;
                    _dataReader.Close();
                    connection.Close();
                    
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return buyDTO;
        }

        public string BuildStringBuyDetail(List<BuyDetail> detail)
        {
            string detailIntoString = "";

            for (int i=0; i<detail.Count; i++)
            {
                detailIntoString += $"{detail[i].ProductSku},{detail[i].Price}," +
                    $"{detail[i].Units},{detail[i].Discount},{detail[i].Subtotal}";

                detailIntoString += ((detail.Count() > 1) && (i < detail.Count() - 1)) ? "|" : "";
            }

            return detailIntoString;
        }
    }
}
