using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOSEntityLib;

namespace MOSDataAccesslayer
{
    interface IMOSDataAcess
    {
        List<Product> SearchProductByName(string name);       
        
        Product GetProductDetailsById(int id);
       
        List<Category> GetAllCategories();
        void AddToCart(List<Cart> cart);
        List<Product> view_cart(string list);
        void DeleteFromCart( int id);

        
    }
}
