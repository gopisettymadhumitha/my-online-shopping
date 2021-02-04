using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOSEntityLib
{
     public class Category
     {
             
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        
        public List<Product> products { get; set; }

     }
}
