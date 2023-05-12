using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp3.Models;
using System.Data.SqlClient;

namespace WpfApp3.Services
{
    public class ProductService
    {
        public int ID { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        private readonly string _connectionString;

        public ProductService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Product> GetAllProducts()
        {
            var products = new List<Product>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("SELECT * FROM Product", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            ID = reader.GetInt32(0),
                            Category = reader.GetString(1),
                            Name = reader.GetString(2),
                            Price = reader.GetDecimal(3),
                            Quantity = reader.GetInt32(4)
                        });
                    }
                }
            }

            return products;
        }

        public void AddProduct(Product product)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("INSERT INTO Product (Category, Name, Price, Quantity) VALUES (@Category, @Name, @Price, @Quantity)", connection))
                {
                    command.Parameters.AddWithValue("@Category", product.Category);
                    command.Parameters.AddWithValue("@Name", product.Name);
                    command.Parameters.AddWithValue("@Price", product.Price);
                    command.Parameters.AddWithValue("@Quantity", product.Quantity);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateProduct(ProductService product)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("UPDATE Product SET Category = @Category, Name = @Name, Price = @Price, Quantity = @Quantity WHERE ID = @ID", connection))
                {
                    command.Parameters.AddWithValue("@ID", product.ID);
                    command.Parameters.AddWithValue("@Category", product.Category);
                    command.Parameters.AddWithValue("@Name", product.Name);
                    command.Parameters.AddWithValue("@Price", product.Price);
                    command.Parameters.AddWithValue("@Quantity", product.Quantity);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteProduct(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("DELETE FROM Product WHERE ID = @ID", connection))
                {
                    command.Parameters.AddWithValue("@ID", id);

                    command.ExecuteNonQuery();
                }
            }
        }

        private List<ProductService> _cart = new List<ProductService>();

        public void AddToCart(ProductService product, int quantity)
        {
            // Add the product to the cart
            for (int i = 0; i < quantity; i++)
            {
                _cart.Add(product);
            }
        }

        public List<ProductService> GetCartItems()
        {
            // Return the list of products in the cart
            return _cart;
        }

        public void RemoveFromCart(ProductService product)
        {
            // Remove the product from the cart
            _cart.Remove(product);
        }

        public void ClearCart()
        {
            // Clear all items from the cart
            _cart.Clear();
        }
    }

}
