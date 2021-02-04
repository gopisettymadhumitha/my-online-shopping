using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOSEntityLib
{
     public class Product
     {
        public int ProductID { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public float Rating { get; set; }
        public string Picture { get; set; }
        public string Discount { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
     }
}
