using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MOSEntityLib;
using MOSExeceptionsLib;


namespace MOSWebAPI.Controllers
{
    public class MOSController : ApiController
    {

        [Route("api/MYOnlineShopping/SearchProductByName/{name}")]
        public List<Product> SearchProductByName(string name)
        {
            try
            {
                MOSBusinesslayer.MOSBusinesslayer bll = new MOSBusinesslayer.MOSBusinesslayer();
                var lstproducts = bll.SearchProductByName(name);
                return lstproducts;
            }
            catch(Exception ex)
            {
                throw new MOSException(ex.Message);
            }
        }
        
        [Route("api/MYOnlineShopping/GetProductDetailsById/{id}")]
        public Product GetProductDetailsById(int id)
        {
            try
            {
                MOSBusinesslayer.MOSBusinesslayer bll = new MOSBusinesslayer.MOSBusinesslayer();
                var product = bll.GetProductDetailsById(id);
                return product;
            }
            catch(Exception ex)
            {
                throw new MOSException(ex.Message);
            }
        }
        [Route("api/MYOnlineShopping/GetAllCategories")]
        public List<Category> GetAllCategories()
        {
            try
            {
                MOSBusinesslayer.MOSBusinesslayer bll = new MOSBusinesslayer.MOSBusinesslayer();
                var lstcategories = bll.GetAllCategories();
                return lstcategories;
            }
            catch(Exception ex)
            {
                throw new MOSException(ex.Message);
            }
        }
        [Route("api/MYOnlineShopping/DeleteFromCart")]
        public void DeleteFromCart(int id)
        {
            try
            {
                MOSBusinesslayer.MOSBusinesslayer bll = new MOSBusinesslayer.MOSBusinesslayer();
                bll.DeleteFromCart(id);
            }
            catch(Exception ex)
            {
                throw new MOSException(ex.Message);
            }
        }
        [Route("api/MYOnlineShopping/AddToCart")]
        public void AddToCart(List<Cart> cart)
        {
            try
            {
                MOSBusinesslayer.MOSBusinesslayer bll = new MOSBusinesslayer.MOSBusinesslayer();
                bll.AddToCart(cart);
            }
            catch(Exception ex)
            {
                throw new MOSException(ex.Message);
            }
        }
        //[Route("api/MYOnlineShopping/ViewCart")]
        ////public List<Product> Viewcart(string name)
        ////{
        ////    MOSBusinesslayer.MOSBusinesslayer bll = new MOSBusinesslayer.MOSBusinesslayer();
        ////    var lstproducts = bll.Viewcart(name);
        ////    return lstproducts;
        ////} 

        //[Route("api/MYOnlineShopping/SearchProductByCategoryName/{name}")]
        //public List<Product> SearchProductByCategoryName(string name)
        //{
        //   MOSBusinesslayer.MOSBusinesslayer bll = new MOSBusinesslayer.MOSBusinesslayer();
        //       var lstproducts = bll.SearchProductByCategoryname(name);
        //         return lstproducts;
        //}


    }
}
