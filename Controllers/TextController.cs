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
    public class TextController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public TextController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public JsonResult Post(Text text)
        {
            string query = @"INSERT INTO text(name, size, author_id, date_add, boughtOnce, price) 
            VALUES (@name,@size,@author_id,@date_add,@boughtOnce,@price)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PhotoStockAppCon");
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    try
                    {
                        myCommand.Parameters.AddWithValue("@name", text.name);
                        myCommand.Parameters.AddWithValue("@size", text.size);
                        myCommand.Parameters.AddWithValue("@author_id", text.author_id);
                        myCommand.Parameters.AddWithValue("@date_add", text.date_add);
                        myCommand.Parameters.AddWithValue("@boughtOnce", text.boughtOnce);
                        myCommand.Parameters.AddWithValue("@price", text.price);
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myCon.Close();
                    }
                    catch (Exception ex)
                    {
                        return new JsonResult("Add error");
                    }

                }
            }

            return new JsonResult("Added successfully");
        }
    }
}
