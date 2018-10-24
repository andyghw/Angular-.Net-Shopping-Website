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
    public class ProductController : Controller
    {
        private readonly ProductService PS;
        private readonly AppDb Db;

        public ProductController(ProductService ps,AppDb db)
        {
            PS = ps;
            Db = db;
        }

        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> SearchProducts(string keywords)
        {
            using(Db)
            {
                await Db.Connection.OpenAsync();
                var result = await PS.SearchByKeyword(keywords);
                if (result == null)
                {
                    return new NotFoundResult();
                }
                return new OkObjectResult(result);
            }
        }

        [HttpGet]
        [Route("FindLastThree")]
        public async Task<IActionResult> FindLastThree()
        {
            using (Db)
            {
                await Db.Connection.OpenAsync();
                var result = await PS.GetLastThree();
                if (result == null)
                {
                    return new NotFoundResult();
                }
                return new OkObjectResult(result);
            }
        }

        [HttpGet]
        [Route("FindByType/{type}")]
        public async Task<IActionResult> FindByType(string type)
        {
            using (Db)
            {
                await Db.Connection.OpenAsync();
                var result = await PS.FindByType(type);
                if (result == null)
                {
                    return new NotFoundResult();
                }
                return new OkObjectResult(result);
            }
        }

        [HttpGet]
        [Route("FindById/{id}")]
        public async Task<IActionResult> FindById(string id)
        {
            using (Db)
            {
                await Db.Connection.OpenAsync();
                var result = await PS.FindById(id);
                if (result == null)
                {
                    return new NotFoundResult();
                }
                return new OkObjectResult(result);
            }
        }
    }
}