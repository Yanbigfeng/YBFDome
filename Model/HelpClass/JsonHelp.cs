using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace Model.HelpClass
{
   public class JsonHelp
    {
        #region  把对象转换成JSON格式
        //js序列化器
        static JavaScriptSerializer jss = new JavaScriptSerializer();
        /// <summary>
        /// 把对象转换成JSON格式
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>json格式数据</returns>
        public static string ObjToJson(object obj)
        {

            return jss.Serialize(obj);
        }
        #endregion

        #region 把一个字符串进行MD5加密
        public static string Md5(string str)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(str, FormsAuthPasswordFormat.MD5.ToString());
        }
        #endregion
    }
}
