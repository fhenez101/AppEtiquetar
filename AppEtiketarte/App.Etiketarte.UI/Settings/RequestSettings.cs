using App.Etiketarte.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Etiketarte.UI.Settings
{
    public class RequestSettings
    {
        Controller controller = null;

        public RequestSettings(Controller c)
        {
            controller = c;
        }

        public string Message
        {
            get { return (string)controller.TempData["Message"]; }
            set { controller.TempData["Message"] = value; }
        }
    }
}