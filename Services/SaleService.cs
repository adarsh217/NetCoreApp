using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp3.Models;

namespace WpfApp3.Services
{
    public class SaleService
    {
        //private string connectionString = "Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;";
        private readonly string _connectionString;

        public SaleService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Sale> GetAllSales()
        {
            var sales = new List<Sale>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("SELECT * FROM Sales", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        sales.Add(new Sale
                        {
                            ID = reader.GetInt32(0),
                            Date = reader.GetDateTime(1),
                            Total = reader.GetDecimal(2)
                        });
                    }
                }
            }

            return sales;
        }

        public void AddSale(Sale sale)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("INSERT INTO Sales (Date, Total) VALUES (@Date, @Total)", connection))
                {
                    command.Parameters.AddWithValue("@Date", sale.Date);
                    command.Parameters.AddWithValue("@Total", sale.Total);

                    command.ExecuteNonQuery();
                }
            }
        }
        public void UpdateSale(Sale sale)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("UPDATE Sales SET Date = @Date, Total = @Total WHERE ID = @ID", connection))
                {
                    command.Parameters.AddWithValue("@ID", sale.ID);
                    command.Parameters.AddWithValue("@Date", sale.Date);
                    command.Parameters.AddWithValue("@Total", sale.Total);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteSale(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("DELETE FROM Sales WHERE ID = @ID", connection))
                {
                    command.Parameters.AddWithValue("@ID", id);

                    command.ExecuteNonQuery();
                }
            }
        }
        public void ProcessSale(List<Product> cartItems)
        {
            // For simplicity, this just prints the sale
            // In a real application, you would need to update the database and handle payment processing
            foreach (var item in cartItems)
            {
                Console.WriteLine($"Sold {item.Quantity} of {item.Name}");
            }
        }
        public List<Sale> GenerateReport(DateTime startDate, DateTime endDate)
        {
            // Generate and return the report
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Sales WHERE Date >= @startDate AND Date <= @endDate", connection))
                {
                    command.Parameters.AddWithValue("@startDate", startDate);
                    command.Parameters.AddWithValue("@endDate", endDate);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<Sale> sales = new List<Sale>();
                        while (reader.Read())
                        {
                            sales.Add(new Sale
                            {
                                ID = reader.GetInt32(0),
                                Date = reader.GetDateTime(1),
                                Total = reader.GetDecimal(2)
                            });
                        }

                        return sales;
                    }
                }
            }
        }

        internal Sale ProcessSale(List<ProductService> productServices)
        {
            throw new NotImplementedException();
        }
    }

}
