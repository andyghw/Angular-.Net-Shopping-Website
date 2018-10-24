using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Assignment5.MySqlConnector;
using MySql.Data.MySqlClient;


namespace Assignment5.Models.Services
{
    public class OrderService
    {
        private AppDb db;

        public AppDb GetDb()
        {
            return db;
        }

        public void SetDb(AppDb value)
        {
            db = value;
        }

        public OrderService(AppDb db = null)
        {
            SetDb(db);
        }

        public async Task<List<Order>> GetOrder(string userId)
        {
            MySqlConnection conn = GetDb().Connection;
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM orders WHERE userId=@userId ORDER BY created_at DESC";
            cmd.Parameters.AddWithValue("@userId", userId);
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result;
        }

        public async Task<int> GetMostRecentOrderId(string userId)
        {
            MySqlConnection conn = GetDb().Connection;
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT MAX(id) FROM orders WHERE userId=@userId";
            cmd.Parameters.AddWithValue("@userId", userId);
            var orderId = 0;
            using (var reader = cmd.ExecuteReader())
            {
                while (await reader.ReadAsync())
                {
                    orderId = reader.GetFieldValue<int>(0);
                }
                return orderId;
            }
        }


        public async Task<List<Item>> GetOrderitemsById(int orderId)
        {
            MySqlConnection conn = GetDb().Connection;
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM orderItems WHERE orderId=@orderId";
            cmd.Parameters.AddWithValue("@orderId", orderId);
            var result = await ReadAllItemsAsync(await cmd.ExecuteReaderAsync());
            return result;
        }

        public async Task AddOrder(List<Item> orderItems, User user)
        {
            Order order = new Order(orderItems);
            MySqlConnection conn = GetDb().Connection;
            var cmd = conn.CreateCommand() as MySqlCommand;
            //Transaction
            using (IDbTransaction tran = conn.BeginTransaction())
            {
                try
                {
                    // your code
                    cmd.CommandText = @"INSERT INTO orders (userId,status,created_at) VALUES (@userId,@status,@created_at)";
                    cmd.Parameters.AddWithValue("@userId", user.Id);
                    cmd.Parameters.AddWithValue("@status", "Unpaid");
                    cmd.Parameters.AddWithValue("@created_at", order.Created_at);
                    await cmd.ExecuteNonQueryAsync();
                    long orderId = cmd.LastInsertedId;
                    int i = 0;
                    foreach (var item in orderItems)
                    {
                        cmd.CommandText = @"INSERT INTO orderItems (orderId,title,num,price,img) VALUES (@orderId" + i + ",@title" + i + ",@num" + i + ",@price" + i + ",@img" + i + ")";
                        cmd.Parameters.AddWithValue("@orderId" + i, orderId);
                        cmd.Parameters.AddWithValue("@title" + i, item.Title);
                        cmd.Parameters.AddWithValue("@num" + i, item.Num);
                        cmd.Parameters.AddWithValue("@price" + i, item.Price);
                        cmd.Parameters.AddWithValue("@img" + i, item.Img);
                        await cmd.ExecuteNonQueryAsync();
                        i++;
                    }
                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        public async Task UpdateOrder(int orderId)
        {
            MySqlConnection conn = GetDb().Connection;
            var cmd = conn.CreateCommand() as MySqlCommand;
            //Transaction
            using (IDbTransaction tran = conn.BeginTransaction())
            {
                try
                {
                    // your code
                    cmd.CommandText = @"UPDATE orders SET status=@status WHERE id=@id";
                    cmd.Parameters.AddWithValue("@id", orderId);
                    cmd.Parameters.AddWithValue("@status", "Paid");
                    await cmd.ExecuteNonQueryAsync();
                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        public async Task DeleteOrder(int orderId)
        {
            MySqlConnection conn = GetDb().Connection;
            var cmd = conn.CreateCommand() as MySqlCommand;
            //Transaction
            using (IDbTransaction tran = conn.BeginTransaction())
            {
                try
                {
                    // your code
                    cmd.CommandText = @"DELETE FROM orders WHERE id=@orderId";
                    cmd.Parameters.AddWithValue("@orderId", orderId);
                    await cmd.ExecuteNonQueryAsync();
                    cmd.CommandText = @"DELETE FROM orderitems WHERE orderId=@orderId2";
                    cmd.Parameters.AddWithValue("@orderId2", orderId);
                    await cmd.ExecuteNonQueryAsync();
                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        private async Task<List<Order>> ReadAllAsync(DbDataReader reader)
        {
            var orders = new List<Order>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var order = new Order()
                    {
                        Id = await reader.GetFieldValueAsync<int>(0),
                        UserId = await reader.GetFieldValueAsync<int>(1),
                        Status = await reader.GetFieldValueAsync<string>(2),
                        Created_at = await reader.GetFieldValueAsync<DateTime>(3),
                    };
                    orders.Add(order);
                }
            }
            return orders;
        }

        private async Task<List<Item>> ReadAllItemsAsync(DbDataReader reader)
        {
            var items = new List<Item>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var item = new Item()
                    {
                        Id = await reader.GetFieldValueAsync<int>(0),
                        OrderId = await reader.GetFieldValueAsync<int>(1),
                        Title = await reader.GetFieldValueAsync<string>(2),
                        Num = await reader.GetFieldValueAsync<int>(3),
                        Price = await reader.GetFieldValueAsync<double>(4),
                        Img = await reader.GetFieldValueAsync<string>(5)
                    };
                    items.Add(item);
                }
            }
            return items;
        }
    }
}
