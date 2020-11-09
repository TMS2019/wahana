using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Data;
using System.ComponentModel;

using templateProject.Model;
using templateProject.Repository.Common;
using System.Text;

namespace templateProject.Helper
{
    public static class GeneralFunctions
    {
        public static string GetCurrentURL(bool isIncludeHttp = false)
        {
            string url = string.Empty;
            if (isIncludeHttp == false)
            {
                url = HttpContext.Current.Request.Url.AbsolutePath;
            }
            else
            {
                url = HttpContext.Current.Request.Url.AbsoluteUri;
            }
            return url;
        }

        public static string GetConfig(string key, bool isConString = false)
        {
            string result = string.Empty;

            try
            {
                if (isConString == false)
                {
                    result = ConfigurationManager.AppSettings[key].ToString();
                }
                else
                {
                    result = ConfigurationManager.ConnectionStrings[key].ToString();
                }
            }
            catch (Exception)
            {
                //FunctionsQ.WriteToLog(ex.Message);
                result = "";
            }

            return result;
        }

        public static bool IsSessionNotNull(string key)
        {
            if (HttpContext.Current.Session[key] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void SetSession(string key, Object obj)
        {
            HttpContext.Current.Session[key] = obj;
        }

        public static void SetSession(string key, string st)
        {
            HttpContext.Current.Session[key] = st;
        }

        public static Object GetSession(string key)
        {
            if (IsSessionNotNull(key))
            {
                return HttpContext.Current.Session[key];
            }
            else
            {
                return null;
            }
        }

        public static string GetSessionString(string key)
        {
            if (IsSessionNotNull(key))
            {
                return HttpContext.Current.Session[key].ToString();
            }
            else
            {
                return null;
            }
        }

        public static void DestroySession(string key)
        {
            HttpContext.Current.Session[key] = null;
        }

        public static List<string> GetPropertyDifferences<T>(this T obj1, T obj2)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            List<string> changes = new List<string>();

            foreach (PropertyInfo pi in properties)
            {
                object value1 = typeof(T).GetProperty(pi.Name).GetValue(obj1, null);
                object value2 = typeof(T).GetProperty(pi.Name).GetValue(obj2, null);

                if (value1 is DateTime && value2 is DateTime)
                {
                    DateTime from = (DateTime)value1;
                    DateTime to = (DateTime)value2;
                    if (from.Date != to.Date)
                    {
                        changes.Add(string.Format("Property {0} changed from {1} to {2}", pi.Name, value1, value2));
                    }

                    //if (!DateTime.Equals(from, to))
                    //{
                    //    changes.Add(string.Format("Property {0} changed from {1} to {2}", pi.Name, value1, value2));
                    //}
                }
                else if (value1 != value2 && (value1 == null || !value1.Equals(value2)))
                {
                    changes.Add(string.Format("Property {0} changed from {1} to {2}", pi.Name, value1, value2));
                }
            }
            return changes;
        }

        public static DateTime JsonDatetimeDeserialize(string jsonDatetime)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            DateTime dt = js.Deserialize<DateTime>(jsonDatetime);

            return dt;
        }

        public static string SplitByUpperCase(string str = "")
        {
            str = string.IsNullOrEmpty(str) ? "" : str;
            string[] s = Regex.Split(str, @"(?<!^)(?=[A-Z])");
            return string.Join(" ", s);
        }

        public static List<MMenuModel> GetMenuByListGroup(int userid)
        {
            UnitOfWork uow = new UnitOfWork();
            return uow.MenuRepository.GetMenuByGroupList(userid);
        }

        public static int getDayFromTwoDate(DateTime start, DateTime end)
        {
            return Convert.ToInt32(Math.Floor((end.Date - start.Date).TotalDays));
        }

        public static string GetExceptionMessage(Exception ex)
        {
            string output = "";
            try
            {
                if (ex.InnerException == null)
                {
                    output = string.Concat(ex.Message, System.Environment.NewLine, ex.StackTrace);
                }
                else
                {
                    // Retira a última mensagem da pilha que já foi retornada na recursividade anterior
                    // (senão a última exceção - que não tem InnerException - vai cair no último else, retornando a mesma mensagem já retornada na passagem anterior)
                    if (ex.InnerException.InnerException == null)
                        output = ex.InnerException.Message;
                    else
                        output = string.Concat(string.Concat(ex.InnerException.Message, System.Environment.NewLine, ex.StackTrace), System.Environment.NewLine, GetExceptionMessage(ex.InnerException));
                }
            }
            catch (Exception exc)
            {
                output = "Failed get inner exception." + exc.Message;
            }

            return output;
        }

        public static string RemoveLineEndingsWith(string value, string replace)
        {
            if (String.IsNullOrEmpty(value))
            {
                return value;
            }
            string lineSeparator = ((char)0x2028).ToString();
            string paragraphSeparator = ((char)0x2029).ToString();

            return value.Replace("\r\n", replace).Replace("\n", replace).Replace("\r", replace).Replace(lineSeparator, " ").Replace(paragraphSeparator, " ");
        }

        public static DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }

        public static string ConvertToBase64(string input)
        {
            string output = "";
            var byteArray = Encoding.ASCII.GetBytes(input);
            output = Convert.ToBase64String(byteArray);

            return output;
        }

        public static string ConvertFromBase64(string input)
        {
            string decodeauthToken = Encoding.UTF8.GetString(Convert.FromBase64String(input));
            return decodeauthToken;
        }
    }
}