using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPITemplate.Business.Models
{
    public class ProductLine
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public decimal Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
