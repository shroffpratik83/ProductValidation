using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductValidation.Entity
{
    public class ProductOutput
    {
        public string RegistrationId { get; set; }

        public string ActiveIngredients { get; set; }

        public string ProductName { get; set; }

        public string VirusesKilled { get; set; }

        public int ContactTime { get; set; }
    }
}
