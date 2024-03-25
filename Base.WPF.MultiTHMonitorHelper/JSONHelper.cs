using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Helper
{
    public class JSONHelper
    {
        /// <summary>
        /// 使用Newton方式实体对象转换成JSON字符串
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="x">对象</param>
        /// <returns>字符串</returns>
        public static string EntityToJSON<T>(T x)
        {
            string result = string.Empty;

            try
            {
                result = Newtonsoft.Json.JsonConvert.SerializeObject(x);
            }
            catch (Exception)
            {
                result = string.Empty;
            }
            return result;

        }

        /// <summary>
        /// 使用Newton方式JSON字符串转换成实体类
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">字符串</param>
        /// <returns>对象</returns>
        public static T JSONToEntity<T>(string json)
        {
            T t = default(T);
            try
            {
                t = (T)JsonConvert.DeserializeObject(json, typeof(T));
            }
            catch (Exception)
            {
                t = default(T);
            }

            return t;
        }
    }
}
