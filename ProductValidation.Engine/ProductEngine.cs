using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProductValidation.Entity;
using ProductValidation.Engine.Extenstions;
using System.Reflection;
using System.IO;
using ProductValidation.Engine.Interface;

namespace ProductValidation.Engine
{
    public class ProductEngine : IProductEngine
    {
        
        public ProductResponse ValidateProducts(List<Product> productList)
        {
            var productResponse = new ProductResponse();

            productResponse.BadProducts = new List<Product>();

            var productsToRemove = productList.Where(x => string.IsNullOrEmpty(x.RegistrationId) || 
                                                string.IsNullOrEmpty(x.ContactTime) || 
                                                !x.ContactTime.IsNumeric()).ToList();

            if (productsToRemove.Any())
                productResponse.BadProducts.AddRange(productsToRemove);

            var duplicateRegIdRecords = productList.GroupBy(s => s.RegistrationId)
                             .Where(g => g.Count() > 1)
                             .Select(g => g.Key).ToList();

            foreach (var item in duplicateRegIdRecords)
            {
                var productListToRemove = productList.Where(x => x.RegistrationId == item).ToList();
                
                foreach (var product in productListToRemove)
	            {
                    if (!productResponse.BadProducts.Contains(product))
                        productResponse.BadProducts.Add(product);
	            }
                
            }

            productList = productList.Except(productResponse.BadProducts).ToList();

            productResponse.GoodProducts = _transformCsvOutput(productList).OrderBy(x => x.ContactTime).ThenBy(x => x.ProductName).ToList();

            return productResponse;
        }

        private static List<ProductOutput> _transformCsvOutput(List<Product> productList)
        {
            // Should have used AutoMapper, Mapper profile for transforming objects

            var productOutputList = new List<ProductOutput>();

            foreach (var product in productList)
            {
                productOutputList.Add(new ProductOutput()
                {
                    RegistrationId = product.RegistrationId,
                    ActiveIngredients = string.Join<string>(";", product.ActiveIngredients),
                    ContactTime = (int)Math.Round(Convert.ToDecimal(product.ContactTime)),
                    ProductName = product.ProductName,
                    VirusesKilled = string.Join<string>(";", product.VirusesKilled)
                });
            }
            return productOutputList;
        }

    }
}
