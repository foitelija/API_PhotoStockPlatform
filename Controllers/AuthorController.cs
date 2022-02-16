using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using API_PhotoStockPlatform.Models;

namespace API_PhotoStockPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthorController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        public JsonResult Get()
        {
            string query = @"SELECT * FROM author";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PhotoStockAppCon");
            SqlDataReader myReader;

            using(SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using(SqlCommand myCommand = new SqlCommand(query,myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }    
            }
            return new JsonResult(table);
        }

        [HttpPost]

        public JsonResult Post(Author author)
        {
            string query = @"INSERT INTO author (name, nickname, age, date_reg) VALUES (@name,@nickname,@age,@date_reg)";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PhotoStockAppCon");
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@name", author.name);
                    myCommand.Parameters.AddWithValue("@nickname", author.nickname);
                    myCommand.Parameters.AddWithValue("@age", author.age);
                    myCommand.Parameters.AddWithValue("@date_reg", author.date_reg);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added successfully");
        }
    }
}
