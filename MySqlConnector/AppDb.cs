using System;
using MySql.Data.MySqlClient;

namespace Assignment5.MySqlConnector
{
    public class AppDb : IDisposable
    {
        public MySqlConnection Connection;

        public AppDb()
        {
            Connection = new MySqlConnection("host=127.0.0.1;port=3306;user id=root;password=19950116;database=shoppingwebsite;");
        }

        public AppDb(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
        }

        public void Dispose()
        {
            Connection.Close();
        }
    }
}
