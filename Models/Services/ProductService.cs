using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Assignment5.MySqlConnector;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace Assignment5.Models
{
    public class ProductService
    {
        [JsonIgnore]
        public readonly AppDb Db;

        public ProductService(AppDb db)
        {
            Db = db;
        }

        public async Task<List<Product>> SearchByKeyword(string str)
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            string[] keywords = str.Split(" ");
            StringBuilder sb = new StringBuilder(@"SELECT name,price,url,text,type,img,products.id FROM products INNER JOIN (select * from imgs where img in (select min(img) from imgs group by productId)) i ON products.id=i.productId WHERE ");
            for (int i = 0; i < keywords.Length; i++)
            {
                if (i == keywords.Length - 1)
                {
                    sb.Append(@"products.name LIKE @keyword" + i);
                }
                else
                {
                    sb.Append(@"products.name LIKE @keyword" + i + " AND ");
                }
            }
            var s = sb.ToString();
            Debug.WriteLine(s);
            cmd.CommandText = @s;
            for (int i = 0; i < keywords.Length; i++)
            {
                cmd.Parameters.AddWithValue("@keyword" + i, "%" + keywords[i] + "%");
            }
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result;
        }

        public async Task<List<Product>> GetLastThree()
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT name,price,url,text,type,img,products.id FROM products INNER JOIN (select * from imgs where img in (select min(img) from imgs group by productId)) i ON products.id=i.productId LIMIT 3";
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result;
        }

        public async Task<List<Product>> FindByType(string type)
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT name,price,url,text,type,img,products.id FROM products INNER JOIN (select * from imgs where img in (select min(img) from imgs group by productId)) i ON products.id=i.productId WHERE type=@type";
            cmd.Parameters.AddWithValue("@type",type);
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result;
        }

        public async Task<Product> FindById(string id)
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT name,price,url,text,type,img,products.id FROM products INNER JOIN (select * from imgs where img in (select min(img) from imgs group by productId)) i ON products.id=i.productId WHERE products.id=@id";
            cmd.Parameters.AddWithValue("@id", id);
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result[0] == null?null:result[0];
        }

        private async Task<List<Product>> ReadAllAsync(DbDataReader reader)
        {
            var products = new List<Product>();
            List<string> names = new List<string>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var Name = await reader.GetFieldValueAsync<string>(0);
                    if (names.Contains(Name))
                    {
                        foreach(var product in products)
                        {
                            if (product.name.Equals(Name))
                            {
                                product.imgs.Add(await reader.GetFieldValueAsync<string>(5));
                                break;
                            }
                        }
                    }
                    else
                    {
                        var product = new Product()
                        {
                            name = Name,
                            price = await reader.GetFieldValueAsync<double>(1),
                            url = await reader.GetFieldValueAsync<string>(2),
                            text = await reader.GetFieldValueAsync<string>(3),
                            type = await reader.GetFieldValueAsync<string>(4),
                            imgs = new List<string>(),
                            id= await reader.GetFieldValueAsync<int>(6)
                        };
                        product.imgs.Add(await reader.GetFieldValueAsync<string>(5));
                        products.Add(product);
                    }
                }
            }
            return products;
        }
    }
}
