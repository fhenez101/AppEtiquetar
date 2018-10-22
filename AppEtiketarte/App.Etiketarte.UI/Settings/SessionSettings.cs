using App.Etiketarte.UI.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace App.Etiketarte.UI.Settings
{
    public class SessionSettings<TModel>
    {
        public static string Message
        {
            get { return (string)HttpContext.Current.Session["Message"]; }
            set { HttpContext.Current.Session["Message"] = value; }
        }

        public static TModel FacebookModel
        {
            get { return (TModel)HttpContext.Current.Session["FacebookModel"]; }
            set { HttpContext.Current.Session["FacebookModel"] = value; }
        }

        public static TModel OldModel
        {
            get { return (TModel)HttpContext.Current.Session["OldModel"]; }
            set { HttpContext.Current.Session["OldModel"] = value; }
        }

        public static void Remove(string key)
        {
            HttpContext.Current.Session.Remove(key);
        }
    }
}