using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_PhotoStockPlatform.Models
{
    public class Photo
    {
        public int id { get; set; }

        public string name { get; set; }

        public string link { get; set; }
        public int size { get; set; }
        public int author_id { get; set; }
        public double price { get; set; }
        public int boughtOnce { get; set; }
        public string date_add { get; set; }

    }
}
