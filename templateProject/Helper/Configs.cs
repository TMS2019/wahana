using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace templateProject.Helper
{
    public class Configs
    {
        ////Mail Service
        public static string Mail_smtp = GeneralFunctions.GetConfig("Mail_smtp");
        public static string Mail_Dummy = GeneralFunctions.GetConfig("Mail_Dummy");
        public static string Mail_port = GeneralFunctions.GetConfig("Mail_port");
        public static string Mail_from = GeneralFunctions.GetConfig("Mail_from");
        public static string Mail_Bcc = GeneralFunctions.GetConfig("Mail_Bcc");
        public static string Mail_Power = GeneralFunctions.GetConfig("Mail_Power");
        public static string Mail_fromDisplayName = GeneralFunctions.GetConfig("Mail_fromDisplayName");

        public static string session = GeneralFunctions.GetConfig("Session");
        public static string KeyEncrypt = GeneralFunctions.GetConfig("KeyEncrypt");
        public static string CookiesName = GeneralFunctions.GetConfig("CookiesName");
        public static string PassCookies = GeneralFunctions.GetConfig("PassCookies");
        public static string AppsCodeSSO = GeneralFunctions.GetConfig("AppsCodeSSO");

        public static string BypassSSL = GeneralFunctions.GetConfig("BypassSSL");
        
        public static string DefaultDomain = GeneralFunctions.GetConfig("DefaultDomain");
        public static string DirectoryPath = GeneralFunctions.GetConfig("DirectoryPath");

        public static int PerPage = Int32.Parse(GeneralFunctions.GetConfig("PerPage"));
        
    }
}