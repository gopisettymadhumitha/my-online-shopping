using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

using MOSEntityLib;//for entites

using MOSExeceptionsLib;//for exceptions


namespace MOSDataAccesslayer
{
    public class MOSDataAcesslayer : IMOSDataAcess
    {
        //configure connection object
        SqlConnection con;
        SqlCommand cmd;
       public MOSDataAcesslayer()
       {
            con = new SqlConnection();
            //object Configurationmanager = null;

            con.ConnectionString =ConfigurationManager.ConnectionStrings["sqlconstr"].ConnectionString;
       }

        /// <summary>
        ///  this method to delete items from cart items
        /// </summary>
        /// <param name="id"> using product id</param>
        public void DeleteFromCart(int id)
        {
            try
            {

                //configureing command for delete
                cmd = new SqlCommand();
                cmd.CommandText = "delete from Cart where ProductId=@id";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", id);
                cmd.CommandType = System.Data.CommandType.Text;
                //attach the connection with the command
                cmd.Connection = con;
                //open the connection
                con.Open();
                //execute the command
                int recordseffected = cmd.ExecuteNonQuery();
                //close the connection
                con.Close();
                if (recordseffected == 0)
                {
                    throw new Exception(" item is not in cart");
                }
            }
            catch (SqlException ex)
            {
                throw new MOSException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new MOSException(ex.Message);
            }
            finally
            {
                //close the connection
                con.Close();
            }


        } 
        /// <summary>
        ///  this method to get all categories in the table
        /// </summary>
        /// <returns>  returns categories name</returns>

        public List<Category> GetAllCategories()
        {

            List<Category> lstcategories = new List<Category>();
            try
            {
                //configure command for select statement
                cmd = new SqlCommand();
                cmd.CommandText = "select * from Category_table ";
                //attach the connection with the command
                cmd.Connection = con;
                //open connection
                con.Open();
                //execute the command
                SqlDataReader sdr = cmd.ExecuteReader();
                //read the records from data reader and add them to the collection
                while (sdr.Read())
                {
                    Category category = new Category
                    {
                        CategoryId = (int)sdr[0],
                        CategoryName = sdr[1].ToString(),
                    };

                    lstcategories.Add(category);
                }
                //close the data reader 
                sdr.Close();
            }
            catch (SqlException ex)
            {
                throw new MOSException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new MOSException(ex.Message);
            }
            finally
            {
                //close the connection
                con.Close();
            }
            //return  all the records using collection
            return lstcategories;
        }     
        /// <summary>
        ///  This method to get product details 
        /// </summary>
        /// <param name="id"> using id as a parameter</param>
        /// <returns>returns the details of particular product</returns>

        public Product GetProductDetailsById(int id)
        {
            Product product = new Product();
            try
            {
                //configure command
                cmd = new SqlCommand();
                cmd.CommandText = "select * from Product_table where  ProductId='"+id+"'";
                
                cmd.CommandType = CommandType.Text;
                //attach connection
                cmd.Connection = con;
                //open connection
                con.Open();
                //execute command
                SqlDataReader sdr = cmd.ExecuteReader();
                while(sdr.Read())
                {
                    //read records
                    product.ProductID = (int)sdr[8];
                    Category c = new Category
                    {
                        CategoryId = (int)sdr[0],
                    };
                    product.ProductName = sdr[1].ToString();
                    product.Price = (decimal)sdr[2];                    
                    product.Discount = sdr[4].ToString();                                       
                    product.Picture = sdr[5].ToString();
                    product.Content = sdr[6].ToString();
                    product.Description = sdr[7].ToString();                    
                }
                //close the data reader 
                sdr.Close();                

            }
            catch (MOSException ex)
            {
                throw ex;
            }
            catch (SqlException ex)
            {
                throw new MOSException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new MOSException(ex.Message);
            }
            finally
            {
                //close the connection
                con.Close();
            }
            //return  the record value
            return product;
        }

        /// <summary>
        /// method to get the products related to  name
        /// </summary>
        /// <param name="name"> taking name as astring</param>
        /// <returns> returns product with that name</returns>

        public List<Product> SearchProductByName(string name)
        {
            List<Product> lstproduct = new List<Product>();
            try
            {
                //configure command for select statement
                cmd = new SqlCommand();
                cmd.CommandText = "select p.ProductId,p.Picture,p.ProductName,p.Price,p.Discount from Product_table as p join Category_table as c on p.CategoryId = c.CategoryId and c.CategoryName like '%" + name + "%'";
               
                cmd.CommandType = CommandType.Text;
                //attach connection
                cmd.Connection = con;
                //open connection
                con.Open();
                //execute command
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    Product product = new Product
                    {
                        ProductID = (int)sdr[0],
                        Picture = sdr[1].ToString(),
                        ProductName = sdr[2].ToString(),
                        Price = Convert.ToDecimal(sdr[3]),
                        Discount = sdr[4].ToString(),
                    };
                    lstproduct.Add(product);
                }
                //close the data reader 
                sdr.Close();

            }
            catch(SqlException ex)
            {
                throw new MOSException("some database error occured: " + ex.Message);
            }
            catch(Exception ex)
            {
                throw new MOSException("some database error occured: " + ex.Message);
            }
            finally
            {
                //close the connection
                con.Close();
            }
            //return  all the record  using collection
            return lstproduct; 

        }
        /// <summary>
        /// method to view the items in the cart 
        /// </summary>
        /// <param name="list">list as parameter</param>
        /// <returns>the list of item in the cart</returns>

        public List<Product> view_cart(string list)
        {
            List<Product> lstproduct = new List<Product>();
            try
            {
                //configure command for select statement
                cmd = new SqlCommand();
                cmd.CommandText = "select ProductId,Picture,ProductName,Price from Product_table where ProductId in ("+list+"0)";
                
                cmd.CommandType = CommandType.Text;
                //attach connection
                cmd.Connection = con;
                //open connection
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    Product product = new Product
                    {
                        ProductID = (int)sdr[0],
                        Picture = sdr[1].ToString(),
                        ProductName = sdr[2].ToString(),
                        Price = Convert.ToDecimal(sdr[3]),
                    };
                    lstproduct.Add(product);
                }
                //close the data reader 
                sdr.Close();

            }
            catch (SqlException ex)
            {
                throw new MOSException("some database error occured: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new MOSException("some database error occured: " + ex.Message);
            }
            finally
            {
                //close the connection
                con.Close();
            }
            //return  all record using collection
            return lstproduct;

        }
        /// <summary>
        ///  this method to add items to cart
        /// </summary>
        /// <param name="lstcart"> list as a parameter</param>
        public void AddToCart(List<Cart> lstcart)
        {
            Product product = new Product();
            try
            {
                //attach connection
                cmd.Connection = con;
                //open connection
                con.Open();
                foreach(var item in lstcart)
                {
                    //configure command for insert statement
                    cmd = new SqlCommand();
                    cmd.CommandText = "insert into Cart values(@pid,@pname,@pic,@price)";
                    //supply values to the parameters of the command
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@pid",item.ProductId);
                    cmd.Parameters.AddWithValue("@pname", item.ProductName);
                    cmd.Parameters.AddWithValue("@pic", item.Picture);
                    cmd.Parameters.AddWithValue("@price", item.Price);
                    //specify the type of command
                    cmd.CommandType = CommandType.Text;
                    //execute the command
                    int recordseffected = cmd.ExecuteNonQuery();
                    //close the connection
                    if (recordseffected == 0)
                    {
                        throw new Exception("No item is in cart");
                    }
                }            
                              

            }
            catch (MOSException ex)
            {
                throw ex;
            }
            catch (SqlException ex)
            {
                throw new MOSException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new MOSException(ex.Message);
            }
            finally
            {
                con.Close();
            }
            


        }
        /// <summary>
        /// this method is to store user details
        /// </summary>
        /// <param name="user"> with user as a parameter</param>
        public void AddUserDetails(UserDetails user)
        {
            
            try
            {
                cmd = new SqlCommand();

                cmd.CommandText = "insert into UserDetails values (@ui,@un,@up,@um)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ui", user.User_Id);
                cmd.Parameters.AddWithValue("@un", user.UserName);
                cmd.Parameters.AddWithValue("@up", user.User_Password);
                cmd.Parameters.AddWithValue("@um", user.User_mail);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();

            }
            catch (SqlException ex)
            {
                throw new MOSException("some database error occured: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new MOSException("some database error occured: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
            
        }

        //public void AddUserDetails(UserDetails user)
        //{
        //    try
        //    {
        //        cmd = new SqlCommand();

        //        cmd.CommandText = "insert into UserDetails values (@ui,@un,@up,@um)";
        //        cmd.Parameters.Clear();
        //        cmd.Parameters.AddWithValue("@ui", user.User_Id);
        //        cmd.Parameters.AddWithValue("@un", user.UserName);
        //        cmd.Parameters.AddWithValue("@up", user.User_Password);
        //        cmd.Parameters.AddWithValue("@um", user.User_mail);
        //        cmd.CommandType = CommandType.Text;
        //        cmd.Connection = con;
        //        con.Open();
        //        cmd.ExecuteNonQuery();

        //    }
        //    catch (SqlException ex)
        //    {
        //        throw new MOSException("some database error occured: " + ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new MOSException("some database error occured: " + ex.Message);
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }

        //}


        //public List<Product> AddToCart(int id)
        //{
        //    List<Product> lstproduct = new List<Product>();
        //    try
        //    {
        //        cmd = new SqlCommand();

        //        cmd.CommandText = cmd.CommandText = "select p.ProductId,p.Picture,p.ProductName,p.Price from Product_table as p join Category_table as c on p.CategoryId = c.CategoryId and c.CategoryName like '%" + id + "%'";
        //        cmd.CommandType = CommandType.Text;
        //        cmd.Connection = con;
        //        con.Open();
        //        SqlDataReader sdr = cmd.ExecuteReader();
        //        while (sdr.Read())
        //        {
        //            Product product = new Product
        //            {
        //                ProductID = (int)sdr[0],
        //                Picture = sdr[1].ToString(),
        //                ProductName = sdr[2].ToString(),
        //                Price = Convert.ToDecimal(sdr[3]),
        //            };
        //            lstproduct.Add(product);
        //        }
        //        sdr.Close();              

        //    }
        //    catch (SqlException ex)
        //    {
        //        throw new MOSException("some database error occured: " + ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new MOSException("some database error occured: " + ex.Message);
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //    return lstproduct;

        //}

        //public List<Product> GetAllProducts()
        //{
        //   List<Product> lstproducts = new List<Product>();
        //   try
        //   {
        //     cmd = new SqlCommand();
        //     cmd.CommandText = "select * from Product_table ";
        //     cmd.Connection = con;
        //     con.Open();
        //     SqlDataReader sdr = cmd.ExecuteReader();
        //       while (sdr.Read())
        //       {
        //          Product product = new Product
        //          {
        //             ProductID = (int)sdr[0],
        //             Category = new Category
        //             {
        //                 CategoryId = (int)sdr[1]
        //             },
        //             ProductName = sdr[2].ToString(),
        //             Price = (decimal)sdr[3],
        //             Rating = (float)sdr[4],
        //             Discount = sdr[5].ToString(),
        //             Description = sdr[6].ToString(),
        //             Picture = sdr[7].ToString(),


        //          };
        //          string Content = sdr[8].ToString();
        //          string[] cols = Content.Split(',');
        //          foreach (var col in cols)
        //          {
        //             string colName, colValue;
        //             string[] colNameValue = col.Split(':');
        //             colName = colNameValue[0];
        //             colValue = colNameValue[1];
        //             Console.WriteLine(colName + ":" + colValue);
        //          }

        //          lstproducts.Add(product);
        //       }
        //       sdr.Close();
        //   }
        //   catch (SqlException ex)
        //   {
        //      throw new MOSException(ex.Message);
        //   }
        //   catch (Exception ex)
        //   {
        //     throw new MOSException(ex.Message);
        //   }
        //   finally
        //   {
        //    con.Close();
        //   }
        //     return lstproducts;
        //}
        //public void AddUserDetails(UserDetails user)
        //{
        //    try
        //    {
        //        cmd = new SqlCommand();

        //        cmd.CommandText = "insert into UserDetails values (@ui,@un,@up,@um)";
        //        cmd.Parameters.Clear();
        //        cmd.Parameters.AddWithValue("@ui",user.User_Id );
        //        cmd.Parameters.AddWithValue("@un", user.UserName);
        //        cmd.Parameters.AddWithValue("@up", user.User_Password);
        //        cmd.Parameters.AddWithValue("@um", user.User_mail);
        //        cmd.CommandType = CommandType.Text;
        //        cmd.Connection = con;
        //        con.Open();
        //        cmd.ExecuteNonQuery();

        //    }
        //    catch(SqlException ex)
        //    {
        //        throw new MOSException("some database error occured: " + ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new MOSException("some database error occured: " + ex.Message);
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}

        ////public Product DisplayProductDetails(string name)
        //{
        //    Product p = new Product();
        //    try
        //    {
        //        cmd = new SqlCommand();
        //        cmd.CommandText = "select * from Product_table where ProductName="+name;                          

        //        cmd.Connection = con;
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        con.Open();
        //        SqlDataReader sdr = cmd.ExecuteReader();
        //        if (sdr.Read())
        //        {
        //             p = new Product();
        //            p.ProductID = (int)sdr[0];
        //             Category c = new Category
        //             {
        //                 CategoryId = (int)sdr[1],
        //             };
        //             p.ProductName = sdr[2].ToString();
        //             p.Price = (decimal)sdr[3];
        //             p.Rating = (float)sdr[4];
        //             p.Discount = sdr[5].ToString();
        //             p.Description = sdr[6].ToString();
        //             p.Picture = sdr[7].ToString();
        //             p.Content = sdr[8].ToString();

        //        }
        //        else
        //        {
        //            throw new Exception("movie doesnot exists");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //    return p;



        //}


        //public List<Product> SearchProductByCategoryname(string name)
        //{
        //    List<Product> lstproduct = new List<Product>();
        //    try
        //    {
        //        cmd = new SqlCommand();
        //        cmd.CommandText = "select p.ProductId,p.Picture,p.ProductName,p.Price from Product_table as p join " +
        //            "Category_table as c on p.CategoryId=c.CategoryId and c.CategoryName like @cn";
        //        cmd.Parameters.Clear();
        //        cmd.Parameters.AddWithValue("@cn", "%" + name + "%");
        //        cmd.CommandType = CommandType.Text;
        //        cmd.Connection = con;
        //        con.Open();
        //        SqlDataReader sdr = cmd.ExecuteReader();
        //        while (sdr.Read())
        //        {
        //            Product product = new Product
        //            {
        //                ProductID = (int)sdr[0],
        //                Picture = sdr[1].ToString(),
        //                ProductName = sdr[2].ToString(),
        //                Price = Convert.ToDecimal(sdr[3]),
        //                Discount=sdr[4].ToString(),
        //                Rating=(float)sdr[5],
        //            };
        //            lstproduct.Add(product);
        //        }
        //        sdr.Close();

        //    }
        //    catch (SqlException ex)
        //    {
        //        throw new MOSException("some database error occured: " + ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new MOSException("some database error occured: " + ex.Message);
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //    return lstproduct;

        //}


    }
}
