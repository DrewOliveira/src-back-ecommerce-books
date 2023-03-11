using System.Data.SqlClient;

namespace LesBooks.DAL
{
    public abstract class Connection
    {
        protected SqlDataReader reader;
        private SqlConnection conn;
        protected SqlCommand cmd;

        string connectionString = "Data Source=NB-SIN-CM471N3\\SQLEXPRESS;Initial Catalog=LesBooks;Integrated Security=True";
        public Connection()
        {
            conn = new SqlConnection(connectionString);
            cmd = conn.CreateCommand();
        }

        protected void OpenConnection()
        {
            if(conn.State == System.Data.ConnectionState.Closed)
                conn.Open();
            cmd.Parameters.Clear();
        }
        protected void CloseConnection()
        {
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
        }
    }
}