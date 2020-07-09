using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProductValidation.Engine;
using ProductValidation.Engine.Interface;
using ProductValidation.Model;

namespace ProductValidation.Web.Controllers
{
    public class ProductController : Controller
    {
        IProductProcessorEngine productProcessorEngine = new ProductProcessorEngine();

        public ActionResult ProductValidation()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ProductValidation(ProductValidateModel productValidateModel)
        { 
            if (ModelState.IsValid)
            {
                var productValidateMessage = productProcessorEngine.ProcessProducts(productValidateModel);
                ViewBag.Message = productValidateMessage;
                return View(productValidateModel);
            }
            return View(productValidateModel);
        }

    }
}
