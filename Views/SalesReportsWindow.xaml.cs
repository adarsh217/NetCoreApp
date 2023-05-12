using System;
using System.Collections.Generic;
using System.Windows;
using WpfApp3.Models;
using WpfApp3.Services;

namespace WpfApp3
{
    public partial class SalesReportsWindow : Window
    {
        private SaleService saleService;

        public SalesReportsWindow()
        {
            InitializeComponent();
            saleService = new SaleService("Server = localhost\\SQLEXPRESS; Database = master; Trusted_Connection = True;");
        }

        private void GenerateReportButton_Click(object sender, RoutedEventArgs e)
        {
            // Get date range from DatePickers
            DateTime startDate = StartDatePicker.SelectedDate.Value;
            DateTime endDate = EndDatePicker.SelectedDate.Value;

            // Generate report for the selected date range and bind it to ReportDataGrid
            List<Sale> report = saleService.GenerateReport(startDate, endDate);
            ReportDataGrid.ItemsSource = report;
        }
    }
}
