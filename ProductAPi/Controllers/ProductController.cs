﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ProductAPi.Models;

namespace ProductAPi.Controllers
{
    [Route("products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        Connect conn = new Connect();
        [HttpGet]
        public List<Product> Get() 
        {
            List<Product> products = new List<Product>();
            string sql = "SELECT * FROM products";
            conn.Connection.Open();
            MySqlCommand cmd = new MySqlCommand(sql,conn.Connection);
            MySqlDataReader reader = cmd.ExecuteReader();

            reader.Read();

            do 
            {
                Product product = new Product();

                product.Id = reader.GetGuid(0);
                product.Name = reader.GetString(1);
                product.Price=reader.GetInt32(2);
                

                product.CreatedTime = reader.GetDateTime(3);
                products.Add(product);
            } 
            while (reader.Read());


            conn.Connection.Close();

            return products;
        }
    }
}
