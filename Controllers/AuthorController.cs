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
            string query = @"select author.id as 'Author ID',author.name as 'Author Name', 
            author.nickname as 'Author NickName', author.age as 'Author Age', author.date_reg as 'Author reg date',
            photo.id as 'Photo ID', photo.name as 'Photo name', photo.link as 'Photo link', photo.size as 'Photo size',
            photo.date_add as 'Photo add date', photo.price as 'Photo price', photo.boughtOnce as 'Bought' ,
            text.id as 'Text ID', text.name as 'Text name', text.size as 'Text size', text.date_add as 'Text add date',
            text.price as 'Text price', text.boughtOnce as 'Bought'
            from author join photo on photo.author_id = author.id join text on text.author_id = author.id";

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
