using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductValidation.Entity;

namespace ProductValidation.Engine.Interface
{
    public interface IFileEngine
    {
        void WriteJsonData(List<Product> product, string path);

        void WriteCSVData(List<ProductOutput> items, string path);
    }
}
