using System.Windows;
using System.Collections.ObjectModel;
using WpfApp3.Models;
using WpfApp3.Services;
using System.Windows.Controls;

namespace WpfApp3.Views
{
    public partial class InventoryManagementWindow : Window
    {
        private ProductService _productService;
        private SaleService _saleService;

        public InventoryManagementWindow()
        {
            InitializeComponent();
            _productService = new ProductService("Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;");
            _saleService = new SaleService("Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;");

            LoadProducts();
        }

        private void LoadProducts()
        {
            var products = _productService.GetAllProducts();
            ProductDataGrid.ItemsSource = products;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // get data from textboxes
            var product = new Product
            {
                Category = CategoryTextBox.Text,
                Name = NameTextBox.Text,
                Price = decimal.Parse(PriceTextBox.Text),
                Quantity = int.Parse(QuantityTextBox.Text)
            };

            // save to database
            _productService.AddProduct(product);

            // reload data grid
            LoadProducts();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            // get selected product
            var product = (ProductService)ProductDataGrid.SelectedItem;

            // update product
            product.Category = CategoryTextBox.Text;
            product.Name = NameTextBox.Text;
            product.Price = decimal.Parse(PriceTextBox.Text);
            product.Quantity = int.Parse(QuantityTextBox.Text);

            // update in database
            _productService.UpdateProduct(product);

            // reload data grid
            LoadProducts();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // get selected product
            var product = (Product)ProductDataGrid.SelectedItem;

            // delete from database
            _productService.DeleteProduct(product.ID);

            // reload data grid
            LoadProducts();
        }

        private void ProductDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // get selected product
            var product = (Product)ProductDataGrid.SelectedItem;

            // display in textboxes
            CategoryTextBox.Text = product.Category;
            NameTextBox.Text = product.Name;
            PriceTextBox.Text = product.Price.ToString();
            QuantityTextBox.Text = product.Quantity.ToString();
        }
    }

}
