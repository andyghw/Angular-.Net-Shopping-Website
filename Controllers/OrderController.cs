using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment5.Models;
using Assignment5.Models.Services;
using Assignment5.MySqlConnector;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Assignment5.Controllers
{
    [Route("api/[Controller]")]
    public class OrderController : Controller
    {
        private readonly OrderService OS;
        private readonly AppDb Db;

        public OrderController(OrderService os,AppDb db)
        {
            OS = os;
            Db = db;
        }

        [Route("GetOrder/{userId}")]
        public async Task<List<Order>> GetOrder(string userId)
        {
            using (Db)
            {
                await Db.Connection.OpenAsync();
                return await OS.GetOrder(userId);
            }
        }

        [Route("GetOrderDetail/{orderId}")]
        public async Task<List<Item>> GetOrderDetail(string orderId)
        {
            using (Db)
            {
                await Db.Connection.OpenAsync();
                return await OS.GetOrderitemsById(int.Parse(orderId));
            }
        }

        [Route("GetMostRecentOrder/{userId}")]
        public async Task<List<Item>> GetMostRecentOrder(string userId)
        {
            using (Db)
            {
                await Db.Connection.OpenAsync();
                var orderId = await OS.GetMostRecentOrderId(userId);
                return await OS.GetOrderitemsById(orderId);
            }
        }

        [HttpPost]
        [Route("AddOrder")]
        public async Task AddOrder([FromBody]List<Item> orderItems)
        {
            using (Db)
            {
                await Db.Connection.OpenAsync();
                User user = new User(Db)
                {
                    Id = 1,
                    Username = "andyghw",
                    Password = "19950116",
                    Email = "guo.hanw@husky.neu.edu"
                };
                await OS.AddOrder(orderItems, user);
            }
        }

        [HttpDelete]
        [Route("DeleteOrder/{orderId}")]
        public async Task DeleteOrder(int orderId)
        {
            using (Db)
            {
                await Db.Connection.OpenAsync();
                User user = new User
                {
                    Id = 1,
                    Username = "andyghw",
                    Password = "19950116",
                    Email = "guo.hanw@husky.neu.edu"
                };
                await OS.DeleteOrder(orderId);
            }
        }

        [HttpPut]
        [Route("UpdateOrder/{orderId}")]
        public async Task UpdateOrder(int orderId)
        {
            using (Db)
            {
                await Db.Connection.OpenAsync();
                await OS.UpdateOrder(orderId);
            }
        }
    }
}