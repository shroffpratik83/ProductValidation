using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProductValidation.Engine.Interface;
using ProductValidation.Entity;

namespace ProductValidation.Engine
{
    public class FileEngine : IFileEngine
    {
        public void WriteJsonData(List<Product> product, string path)
        {
            string outputJson = JsonConvert.SerializeObject(product, Formatting.Indented);
            using (var writer = new StreamWriter(path))
            {
                writer.WriteLine(outputJson);
            }
        }

        public void WriteCSVData(List<ProductOutput> items, string path)
        {
            Type itemType = typeof(ProductOutput);
            var props = itemType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            using (var writer = new StreamWriter(path))
            {
                writer.WriteLine(string.Join(", ", props.Select(p => p.Name)));

                foreach (var item in items)
                {
                    writer.WriteLine(string.Join(", ", props.Select(p => p.GetValue(item, null))));
                }
            }
        }
    }
}
