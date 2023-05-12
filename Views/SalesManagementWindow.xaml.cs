using System.Windows;
using WpfApp3.Models;
using WpfApp3.Services;

namespace WpfApp3
{
    public partial class SalesManagementWindow : Window
    {
        private ProductService productService;
        private SaleService saleService;

        public SalesManagementWindow()
        {
            InitializeComponent();
            productService = new ProductService("Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;");
            saleService = new SaleService("Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;");

            // Load products into the DataGrid
            CartDataGrid.ItemsSource = productService.GetAllProducts();
        }

        private void AddToCartButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected product
            ProductService selectedProduct = CartDataGrid.SelectedItem as ProductService;

            if (selectedProduct == null)
            {
                MessageBox.Show("Please select a product to add to the cart.");
                return;
            }

            // Assuming you have a TextBox named QuantityTextBox for entering the quantity
            if (!int.TryParse(QuantityTextBox.Text, out int quantity))
            {
                MessageBox.Show("Please enter a valid quantity.");
                return;
            }

            // Add the product to the cart
            productService.AddToCart(selectedProduct, quantity);

            // Update the cart DataGrid
            CartDataGrid.ItemsSource = productService.GetCartItems();
        }

        private void RemoveFromCartButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected product
            ProductService selectedProduct = CartDataGrid.SelectedItem as ProductService;

            if (selectedProduct == null)
            {
                MessageBox.Show("Please select a product to remove from the cart.");
                return;
            }

            // Remove the product from the cart
            productService.RemoveFromCart(selectedProduct);

            // Update the cart DataGrid
            CartDataGrid.ItemsSource = productService.GetCartItems();
        }

        private void ProcessSaleButton_Click(object sender, RoutedEventArgs e)
        {
            // Process the sale
            Sale newSale = saleService.ProcessSale(productService.GetCartItems());

            if (newSale != null)
            {
                MessageBox.Show($"Sale processed successfully. Sale ID: {newSale.ID}, Total: {newSale.Total}");

                // Clear the cart
                productService.ClearCart();

                // Update the cart DataGrid
                CartDataGrid.ItemsSource = productService.GetCartItems();
            }
            else
            {
                MessageBox.Show("There was a problem processing the sale.");
            }
        }
    }
}
