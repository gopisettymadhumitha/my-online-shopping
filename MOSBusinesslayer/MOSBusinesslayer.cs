using MOSEntityLib;//for entites
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOSDataAccesslayer;//for dataaccess
using MOSExeceptionsLib;//for exceptions handling


namespace MOSBusinesslayer
{
    public class MOSBusinesslayer : IMOSBusiness
    {
        /// <summary>
        /// to call method searchproduct by name from dataacccess layer
        /// </summary>
        /// <param name="name">string as a parameter</param>
        /// <returns> returns products list</returns>

        public List<Product> SearchProductByName(string name)
        {
            try
            {
                MOSDataAcesslayer dal = new MOSDataAcesslayer();
                var lstproduct = dal.SearchProductByName(name);
                return lstproduct;
            }
            catch(Exception ex)
            {
                throw new MOSException(ex.Message);
            }

        }
        /// <summary>
        /// to call method get product deatils from data access layer
        /// </summary>
        /// <param name="id">with int as parameter</param>
        /// <returns>returns tha product details</returns>

        public Product GetProductDetailsById(int id)
        {
            try
            {
                MOSDataAcesslayer dal = new MOSDataAcesslayer();
                Product product = dal.GetProductDetailsById(id);
                return product;
            }
            catch (Exception ex)
            {
                throw new MOSException(ex.Message);
            }



        }
        /// <summary>
        /// to call method get all categories from dal layer
        /// </summary>
        /// <returns> returns the categories</returns>
        public List<Category> GetAllCategories()
        {
            try
            {
                MOSDataAcesslayer dal = new MOSDataAcesslayer();
                var lstproduct = dal.GetAllCategories();
                return lstproduct;
            }
            catch (Exception ex)
            {
                throw new MOSException(ex.Message);
            }

        }
        /// <summary>
        /// to call method addto cart from data access layer
        /// </summary>
        /// <param name="cart"> using cart list</param>
        public void AddToCart(List<Cart> cart)
        {
            try
            {
                MOSDataAcesslayer dal = new MOSDataAcesslayer();
                dal.AddToCart(cart);
            }
            catch (Exception ex)
            {
                throw new MOSException(ex.Message);
            }
        }
        /// <summary>
        /// to call method deletefromcart from data access layer 
        /// </summary>
        /// <param name="id"> using int as parameter</param>

        public void DeleteFromCart(int id)
        {
            try
            {
                MOSDataAcesslayer dal = new MOSDataAcesslayer();
                dal.DeleteFromCart(id);
            }
            catch (Exception ex)
            {
                throw new MOSException(ex.Message);
            }

        }
        //public List<Product> GetAllProducts()
        //{
        //    try
        //    {
        //        MOSDataAcesslayer dal = new MOSDataAcesslayer();
        //        var lstproducts = dal.GetAllProducts();
        //        return lstproducts;
        //    }
        //    catch (MOSException ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public List<Category> GetAllCategories()
        //{           

        //        MOSDataAcesslayer dal = new MOSDataAcesslayer();
        //        var lstcategories = dal.GetAllCategories();
        //        return lstcategories;          

        //}

        //public Product SearchProductByCategoryname(string name)
        //{
        //    MOSDataAcesslayer dal = new MOSDataAcesslayer();
        //    var product = dal.SearchProductByCategoryname(name);
        //    return product;
        //}
        //public List<Product> SearchProductByCategoryname(string name)
        //{
        //    MOSDataAcesslayer dal = new MOSDataAcesslayer();
        //    var lstproduct = dal.SearchProductByName(name);
        //    return lstproduct;

        //}

        //public Product DisplayProductDetails(string name)
        //{
        //    MOSDataAcesslayer dal = new MOSDataAcesslayer();
        //    Product p = dal.DisplayProductDetails(name);
        //    return p;
        //}
        /// <summary>
        /// to call method Add user details from data access layer
        /// </summary>
        /// <param name="user"> with user as a parameter</param>
        public void AddUserDetails(UserDetails user)
        {
            MOSDataAcesslayer dal = new MOSDataAcesslayer();
            dal.AddUserDetails(user);

        }
    }
}
