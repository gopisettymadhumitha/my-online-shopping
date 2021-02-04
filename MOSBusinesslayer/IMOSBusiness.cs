using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOSEntityLib;


namespace MOSBusinesslayer
{
    interface IMOSBusiness
    {
        List<Product> SearchProductByName(string name);
       
        
        Product GetProductDetailsById(int id);      
        List<Category> GetAllCategories();
        void AddToCart(List<Cart> cart);

        void DeleteFromCart(int id);

        void AddUserDetails(UserDetails user);        
        //List<Product> SearchProductByCategoryname(string name);
        //List<Product> GetAllProducts();
        //Product DisplayProductDetails(string name);
    }
}
