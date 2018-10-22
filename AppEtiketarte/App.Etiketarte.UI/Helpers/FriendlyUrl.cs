using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace App.Etiketarte.UI.Helpers
{
    public static class FriendlyUrl
    {
        public static string Slug(this HtmlHelper helper, object id, string name)
        {
            return GenerateSlug(id, name);
        }

        private static string GenerateSlug(object id, string name)
        {
            string str = RemoveAccent($"{id.ToString()}-{name}");       
            str = Regex.Replace(str, @"[^a-z0-9\s-]", string.Empty); 
            str = Regex.Replace(str, @"\s+", " ")?.Trim();
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45)?.Trim();
            str = Regex.Replace(str, @"\s", "-");

            return str;
        }

        private static string RemoveAccent(string text)
        {
            return Encoding.ASCII.GetString(Encoding.GetEncoding("Cyrillic").GetBytes(text))?.ToLower();
        }
    }
}