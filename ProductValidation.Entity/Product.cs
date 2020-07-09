using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductValidation.Entity
{
    public class Product
    {
        public string RegistrationId { get; set; }

        public List<string> ActiveIngredients { get; set; }

        public string ProductName { get; set; }

        public List<string> VirusesKilled { get; set; }

        public string ContactTime { get; set; }

    }
}
