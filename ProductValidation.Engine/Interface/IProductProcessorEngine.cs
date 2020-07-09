using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductValidation.Model;

namespace ProductValidation.Engine.Interface
{
    public interface IProductProcessorEngine
    {
        string ProcessProducts(ProductValidateModel productValidateModel);
    }
}
