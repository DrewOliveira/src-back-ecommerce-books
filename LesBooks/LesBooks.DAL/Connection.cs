using System.Data.SqlClient;

namespace LesBooks.DAL
{
    public abstract class Connection
    {
        protected SqlDataReader reader;
        private SqlConnection conn;
        protected SqlCommand cmd;

        string connectionString = "Data Source=DESKTOP-MVIAPPB\\SQLEXPRESS;Initial Catalog=LesBooks2;Integrated Security=True";
        public Connection()
        {
            conn = new SqlConnection(connectionString);
            cmd = conn.CreateCommand();
            cmd.Connection = conn;
        }

        protected void OpenConnection()
        {
            if(conn.State == System.Data.ConnectionState.Closed)
                conn.Open();
            cmd.Parameters.Clear();
        }
        protected void CloseConnection()
        {
            if(reader != null && !reader.IsClosed)
                reader.Close();
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
            cmd.Parameters.Clear();
        }
    }
}