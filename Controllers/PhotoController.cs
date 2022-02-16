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
    public class PhotoController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public PhotoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]

        public JsonResult Get()
        {
            string query = @"select author.id as 'author ID', author.nickname as 'Nickname',
            photo.id as 'Photo id', photo.name as 'Photo name', photo.link as 'Photo link', photo.date_add as 'Photo date add',
            photo.size as 'Photo size', photo.price as 'Price' from author join photo on photo.author_id = author.id ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PhotoStockAppCon");
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }


    }
}
