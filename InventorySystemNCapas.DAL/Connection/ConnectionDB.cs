using System.Data;
using System.Data.SqlClient;

namespace InventorySystemNCapas.DAL.Connection
{
    public class ConnectionDB
    {
        private SqlConnection Connection;
        private readonly string ConnectionString = "Data Source=DESKTOP\\SQLEXPRESS;Initial Catalog=note_control;User=sa;Password=contra1234";

        public ConnectionDB()
        {
            Connection = new SqlConnection(ConnectionString);
        }

        public SqlConnection GetConnection()
        {
            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }

            return Connection;
        }

        public SqlConnection CloseConnection()
        {
            if (Connection.State == ConnectionState.Open)
            {
                Connection.Close();
            }

            return Connection;
        }
    }
}
