using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace InventorySystemNCapas.DAL.Connection
{
    public class ConnectionDB
    {
        public SqlConnection GetConnection
        {
            get
            {
                var sqlConnection = new SqlConnection();
                if (sqlConnection == null) return null;

                sqlConnection.ConnectionString = "Data Source=DESKTOP\\SQLEXPRESS;Initial Catalog=note_control;User=sa;Password=contra1234";
                sqlConnection.Open();

                return sqlConnection;
            }
        }
    }
}
