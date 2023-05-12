using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp3.Services;
namespace WpfApp3.Models
{
    public class Sale
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
        public List<ProductService> Products { get; set; } = new List<ProductService>();
    }

}
