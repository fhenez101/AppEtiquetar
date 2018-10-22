using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace App.Etiketarte.UI.Settings
{
    public static class Configuration
    {
        //Secure
        public static bool SecureActivation
        {
            get { return bool.Parse(ConfigurationManager.AppSettings.Get("SecureActivation")); }
        }

        //CustomSettings
        public static string MenuPath
        {
            get { return ConfigurationManager.AppSettings.Get("MenuPath"); }
        }

        public static string FaceBookFieldsPath
        {
            get { return ConfigurationManager.AppSettings.Get("FaceBookFieldsPath"); }
        }

        public static string WaterMarksPath
        {
            get { return ConfigurationManager.AppSettings.Get("WaterMarksPath"); }
        }

        public static string ArtesPath
        {
            get { return ConfigurationManager.AppSettings.Get("ArtesPath"); }
        }

        public static string WaterMaerkArtesImg
        {
            get { return ConfigurationManager.AppSettings.Get("WaterMaerkArtesImg"); }
        }

        public static string FormasPath
        {
            get { return ConfigurationManager.AppSettings.Get("FormasPath"); }
        }

        public static string LogosPath
        {
            get { return ConfigurationManager.AppSettings.Get("LogosPath"); }
        }

        //ImagesSubdomain
        public static string UrlForma
        {
            get { return ConfigurationManager.AppSettings.Get("UrlForma"); }
        }

        public static string UrlArte
        {
            get { return ConfigurationManager.AppSettings.Get("UrlArte"); }
        }

        public static string UrlWaterMark
        {
            get { return ConfigurationManager.AppSettings.Get("UrlWaterMark"); }
        }

        //SafeImage
        public static int ImageParts
        {
            get { return int.Parse(ConfigurationManager.AppSettings.Get("ImageParts")); }
        }

        public static int Angle
        {
            get { return int.Parse(ConfigurationManager.AppSettings.Get("Angle")); }
        }

        //CouponDiscount
        public static char[] CouponDiscountKey
        {
            get { return ConfigurationManager.AppSettings.Get("CouponDiscountKey").ToArray(); }
        }

        public static int CouponDiscountMaxSize
        {
            get { return int.Parse(ConfigurationManager.AppSettings.Get("CouponDiscountMaxSize")); }
        }

        //Email
        public static string EtiketasContact
        {
            get { return ConfigurationManager.AppSettings.Get("EtiketasContact"); }
        }

        public static string RecaptchaPrivateKey
        {
            get { return ConfigurationManager.AppSettings.Get("RecaptchaPrivateKey"); }
        }

        public static string RecaptchaPublicKey
        {
            get { return ConfigurationManager.AppSettings.Get("RecaptchaPublicKey"); }
        }

        public static string RecaptchaApiUrl
        {
            get { return ConfigurationManager.AppSettings.Get("RecaptchaApiUrl"); }
        }

        public static string RecaptchaVerify
        {
            get { return ConfigurationManager.AppSettings.Get("RecaptchaVerify"); }
        }

        public static string InfoMail
        {
            get { return ConfigurationManager.AppSettings.Get("InfoMail"); }
        }
    }
}