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
                    try 
                    {
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myCon.Close();
                    }
                    catch(Exception ex)
                    {
                        return new JsonResult("Upload error, check database connection");
                    }

                }
            }
            return new JsonResult(table);
        }
        [HttpPost("byId")]
        public JsonResult Post(Photo search)
        {
            string query = @"select author.id as 'author ID', author.nickname as 'Nickname',
            photo.id as 'Photo id', photo.name as 'Photo name', photo.link as 'Photo link', photo.date_add as 'Photo date add',
            photo.size as 'Photo size', photo.price as 'Price' from author join photo on photo.author_id = author.id where photo.id = @id";

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
                        myCommand.Parameters.AddWithValue("@id", search.id);
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myCon.Close();
                    }
                    catch (Exception ex)
                    {
                        return new JsonResult("Upload error, check database connection");
                    }

                }
            }
            return new JsonResult(table);

        }

        [HttpPut]
        public JsonResult Put(Photo photo)
        {

            string query = @"update photo set photo.link =@link, 
            photo.size=@size, photo.author_id=@author_id, photo.price=@price, 
            photo.boughtOnce=@boughtOnce, photo.date_add=@date_add, 
            photo.name=@name where photo.id=@id";

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
                        myCommand.Parameters.AddWithValue("@id", photo.id);
                        myCommand.Parameters.AddWithValue("@link", photo.link);
                        myCommand.Parameters.AddWithValue("@size", photo.size);
                        myCommand.Parameters.AddWithValue("@author_id", photo.author_id);
                        myCommand.Parameters.AddWithValue("@price", photo.price);
                        myCommand.Parameters.AddWithValue("@boughtOnce", photo.boughtOnce);
                        myCommand.Parameters.AddWithValue("@name", photo.name);
                        myCommand.Parameters.AddWithValue("@date_add", photo.date_add);

                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myCon.Close();
                    }
                    catch(Exception ex)
                    {
                        return new JsonResult("Update error");
                    }
                    
                }
            }
            return new JsonResult("Update succesfully");
        }
    }
}
