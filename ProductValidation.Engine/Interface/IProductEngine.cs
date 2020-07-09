using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductValidation.Entity;

namespace ProductValidation.Engine.Interface
{
    public interface IProductEngine
    {
        ProductResponse ValidateProducts(List<Product> productList);
    }
}
