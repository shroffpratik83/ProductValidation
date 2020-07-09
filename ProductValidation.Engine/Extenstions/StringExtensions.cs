using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductValidation.Engine.Extenstions
{
    public static class StringExtensions
    {
        public static bool IsNumeric(this string text)
        {
            double value;
            return double.TryParse(text, out value);
        }
    }
}
