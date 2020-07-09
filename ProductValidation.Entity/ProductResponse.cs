using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductValidation.Entity
{
    public class ProductResponse
    {
        public List<ProductOutput> GoodProducts { get; set; }
        public List<Product> BadProducts { get; set; }
    }
}
