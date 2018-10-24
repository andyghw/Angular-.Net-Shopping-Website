using System.Collections.Generic;
using System.Threading.Tasks;
using Assignment5.MySqlConnector;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace Assignment5.Models
{
    public class User
    {
        public int Id{ get;set;}
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public List<Order> Orders { get; set; }
        public List<Item> Cart { get; set; }

        [JsonIgnore]
        public AppDb Db { get; set; }

        public User() {
            Orders = new List<Order>();
            Cart = new List<Item>();
        }
        public User(AppDb db=null)
        {
            Db = db;
            Orders = new List<Order>();
            Cart = new List<Item>();
        }

        public async Task InsertAsync()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO users (username, password,email) VALUES (@username, @password, @email);";
            BindUserParams(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task UpdateAsync()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE users SET username=@username,password = @password, email = @email WHERE email = @email;";
            BindUserParams(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        private void BindUserParams(MySqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@username", Username);
            cmd.Parameters.AddWithValue("@password", Password);
            cmd.Parameters.AddWithValue("@email",Email);
        }
    }
}
