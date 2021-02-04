using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using MOSEntityLib;
using MOSExeceptionsLib;


namespace MYOnlineShopping.Controllers
{
    public class MOSController : Controller
    {
        /// <summary>
        /// action to view home page to search products
        /// </summary>
        /// <returns>takes to products page</returns>
        
        public ActionResult Index()
        {

            try
            {
                MOSDataAccesslayer.MOSDataAcesslayer obj = new MOSDataAccesslayer.MOSDataAcesslayer();
                var lstcategories = obj.GetAllCategories();
                return View(lstcategories);
            }
            catch(Exception ex)
            {
                throw new MOSException(ex.Message);
            }

        }
        /// <summary>
        /// action to searchthe produts with name
        /// </summary>
        /// <param name="name"> taking product name as a parameter</param>
        /// <returns> returns the products of that name</returns>
        public ActionResult SearchProductByName(string name)
        {
            try
            {
                MOSDataAccesslayer.MOSDataAcesslayer obj = new MOSDataAccesslayer.MOSDataAcesslayer();
                var lstproduct = obj.SearchProductByName(name);
                return View(lstproduct);
            }
            catch(Exception ex)
            {
                throw new MOSException(ex.Message);
            }
        }
        /// <summary>
        /// action to get product details page
        /// </summary>
        /// <param name="id"> by taking product id as a parameter</param>
        /// <returns> returns the details page</returns>
        public ActionResult GetProductDetailsById(int id)
        {
            try
            {
                MOSDataAccesslayer.MOSDataAcesslayer obj = new MOSDataAccesslayer.MOSDataAcesslayer();
                var product = obj.GetProductDetailsById(id);
                return View(product);
            }
            catch(Exception ex)
            {
                throw new MOSException(ex.Message);
            }
        }

        /// <summary>
        /// action to add cart details
        /// </summary>
        /// <param name="cart"></param>
        /// <returns> returns the view </returns>
        public ActionResult AddToCart(List<Cart> cart)
        {
            MOSDataAccesslayer.MOSDataAcesslayer obj = new MOSDataAccesslayer.MOSDataAcesslayer();
            return View();
        }
        /// <summary>
        /// action to to store cart details by using sessions
        /// </summary>
        /// <param name="id"> takes int as parameter</param>
        /// <param name="name"> takes string i.e product name as aparameter</param>
        /// <returns></returns>

        public ActionResult AddToCarts(int id, string name)
        {
            Session["cart_values"] = Session["cart_values"] + id.ToString()+",";         
            return RedirectToAction("Index");
        }
        /// <summary>
        /// action to view the items in the cart 
        /// </summary>
        /// <returns>returns items when there are items else displayes the message </returns>
        public ActionResult viewCart()
        {            
            var items = Session["cart_values"];
            if (items == null)
            {
                ViewBag.Message = "No products to display";
                items = "0";
            }
            else { ViewBag.Message = ""; }
            MOSDataAccesslayer.MOSDataAcesslayer obj = new MOSDataAccesslayer.MOSDataAcesslayer();
            var lstproduct = obj.view_cart(items.ToString());
            return View(lstproduct);
        }
        /// <summary>
        ///  action to delete items from cart
        /// </summary>
        /// <param name="id"> by takeing product id as a parameter</param>
        /// <returns>deletes the item</returns>
        public ActionResult DeleteFromCart(int  id)
        {
           
            string items = Session["cart_values"].ToString();
            string[] selections = items.Split(new char[] { ',' });
            var list = new List<string>(selections);
            list.Remove(id.ToString());
            list.RemoveAll(s => string.IsNullOrWhiteSpace(s));
            Session["cart_values"] = "";
            for ( int i=0; i<list.Count;i++)
            {
                Session["cart_values"] = Session["cart_values"] + list[i] + ",";
            }

            return RedirectToAction("viewCart");
                
        }
        /// <summary>
        /// action to show order placing
        /// </summary>
        /// <returns> returns the message</returns>
        
        public ActionResult OrderNow()
        {
            return View();
        }
        public ActionResult AddUserDetails(UserDetails user)
        {

            MOSDataAccesslayer.MOSDataAcesslayer obj = new MOSDataAccesslayer.MOSDataAcesslayer();            
            return View();
        }
        //public ActionResult SearchProductByCategoryname(string name)
        //{

        //Uri uri = new Uri("http://localhost:51563/api/");
        //using (var client = new HttpClient())
        //{
        //    client.BaseAddress = uri;
        //var result = client.GetStringAsync("MYOnlineShopping").Result;
        //var lstcategories = JsonConvert.DeserializeObject<List<Category>>(result);                
        //}
        //    MOSDataAccesslayer.MOSDataAcesslayer obj = new MOSDataAccesslayer.MOSDataAcesslayer();
        //    var lstproduct = obj.SearchProductByCategoryname(name);
        //    return View(lstproduct);

        //}
    }
}