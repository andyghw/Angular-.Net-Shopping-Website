using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Assignment5.MySqlConnector;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace Assignment5.Models
{
    public class UserService
    {
        [JsonIgnore]
        public AppDb Db { get; set; }

        public UserService(AppDb db)
        {
            Db = db;
        }

        public async Task<User> FindOneAsync(string email,string password)
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM users WHERE email = @email AND password = @password";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@email",
                DbType = DbType.String,
                Value = email,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@password",
                DbType = DbType.String,
                Value = password,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        private async Task<List<User>> ReadAllAsync(DbDataReader reader)
        {
            var users = new List<User>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var user = new User(Db)
                    {
                        Id= await reader.GetFieldValueAsync<int>(0),
                        Username = await reader.GetFieldValueAsync<string>(1),
                        Email = await reader.GetFieldValueAsync<string>(2),
                        Password = await reader.GetFieldValueAsync<string>(3)
                    };
                    users.Add(user);
                }
            }
            return users;
        }
    }
}
