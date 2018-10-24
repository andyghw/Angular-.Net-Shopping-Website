using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment5.Models;
using Assignment5.MySqlConnector;
using Microsoft.AspNetCore.Mvc;

namespace Assignment5.Controllers
{
    [Route("api/[Controller]")]
    public class UserController : Controller
    {
        private readonly UserService US;
        private readonly AppDb Db;

        //Autowired
        public UserController(UserService us, AppDb db)
        {
            US = us;
            Db = db;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            using(Db)
            {
                await Db.Connection.OpenAsync();
                var result = await US.FindOneAsync(email, password);
                if (result == null)
                {
                    return new NotFoundResult();
                }
                return new OkObjectResult(result);
            }
        }

        [HttpPost]
        [Route("Register")]
        public async Task Register(string email, string username, string password)
        {
            using(Db)
            {
                await Db.Connection.OpenAsync();
                var user = new User(Db)
                {
                    Email = email,
                    Username = username,
                    Password = password
                };
                await user.InsertAsync();
            }
        }

        [HttpPut]
        [Route("UpdateAccount")]
        public async Task UpdateAccount(string email, string username, string password)
        {
            using (Db)
            {
                await Db.Connection.OpenAsync();
                var user = new User(Db)
                {
                    Email = email,
                    Username = username,
                    Password = password
                };
                await user.UpdateAsync();
            }
        }
    }
}