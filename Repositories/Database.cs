using read_write_files.Interfaces;
using read_write_files.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace read_write_files.Repositories
{
    internal class Database : Product, IProduct
    {

        private readonly string stringConexao = "Data Source=PACIFICO-HP\\SQLEXPRESS;Initial Catalog=Catalog;User id=sa;pwd=pipoca*11;";
        public void Create(Product newProduct)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string query = "INSERT INTO Products VALUES(@IdProduct,@Name,@Description,@Price)";


                using(SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();

                    cmd.Parameters.AddWithValue("@IdProduct", newProduct.IdProduct);
                    cmd.Parameters.AddWithValue("@Name", newProduct.Name);
                    cmd.Parameters.AddWithValue("@Description", newProduct.Description);
                    cmd.Parameters.AddWithValue("Price", newProduct.Price);

                    cmd.ExecuteNonQuery();
                }
            }

        }

        public void Delete(string idProduct)
        {
            using(SqlConnection con =  new SqlConnection(stringConexao))
            {
                string queryString = "DELETE From Products Where idProduct = @IdProduct";

                using(SqlCommand cmd = new SqlCommand(queryString, con))
                {
                    con.Open();

                    cmd.Parameters.AddWithValue("IdProduct", idProduct);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Product> ReadAll()
        {
            List<Product> listProducts = new List<Product>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string query = "SELECT * From Products";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Product product = new()
                        {
                            IdProduct = rdr[0].ToString(),
                            Name = rdr["name"].ToString(),
                            Description = rdr[2].ToString(),
                            Price = Convert.ToDecimal(rdr[3])
                        };
                        listProducts.Add(product);
                    }
                }
            }
            return listProducts;
        }

        public void Update(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
