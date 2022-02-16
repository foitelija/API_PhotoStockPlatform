using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_PhotoStockPlatform.Models
{
    public class Author
    {
        public int id { get; set; }
        public string name { get; set; }
        public string nickname { get; set; }
        public int age { get; set; }
        public string date_reg { get; set; }
    }
}
