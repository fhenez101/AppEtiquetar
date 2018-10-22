using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Etiketarte.UI.Settings
{
    public static class ExtensionMethods
    {
        public static string ToCapitalize(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException("ARGH!");
            }
              
            return input.First().ToString().ToUpper() + input.Substring(1);
        }
    }
}