using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProductValidation.Engine.Interface;
using ProductValidation.Entity;
using ProductValidation.Model;

namespace ProductValidation.Engine
{
    public class ProductProcessorEngine : IProductProcessorEngine
    {
        IProductEngine productEngine = new ProductEngine();
        IFileEngine fileEngine = new FileEngine();


        public string ProcessProducts(ProductValidateModel productValidateModel)
        {
            string Message = string.Empty;
                
            try
            {
                if (File.Exists(productValidateModel.FilePath))
                {
                    string productJson = File.ReadAllText(productValidateModel.FilePath);

                    List<Product> productList = JsonConvert.DeserializeObject<List<Product>>(productJson);
                    var result = productEngine.ValidateProducts(productList);

                    if (result.GoodProducts.Any() || result.BadProducts.Any())
                    {
                        string fileOutPutPath = productValidateModel.FilePath.Substring(0, productValidateModel.FilePath.LastIndexOf('\\'));

                        var csvFilePath = string.Format("{0}\\GoodData.csv", fileOutPutPath);
                        fileEngine.WriteCSVData(result.GoodProducts,
                            csvFilePath);

                        var jsonFilePath = string.Format("{0}\\BadData.json", fileOutPutPath);
                        fileEngine.WriteJsonData(result.BadProducts, jsonFilePath);

                        Message = string.Format("GoodData.csv and BadData.json Files Processed SuccessFully at location {0}", fileOutPutPath);

                    }
                    else
                    {
                        Message = "No records processed";
                    }

                }
                else
                {
                    Message = "File location does not exist..Please try again";
                }

            }
            catch (Exception ex)
            {
                // Should have used log4Net for logging
                Message = string.Format("Exception {0} occured during execution of File..Please contact administator", ex.Message);
            }
            return Message;
        }

    }
}
