using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace AdoHelp.MethodKeep
{
    /// <summary>
    /// 实现相同数据库模型但是不同地址切换
    /// </summary>
    class EntityConnectionString
    {
        #region 1.EF切换连接地址方法
        /// <summary>
        /// 得到Entity的连接字符串
        /// </summary>
        /// <param name="edmxFullName">Edmx的包括命名空间的全名称</param>
        /// <param name="server">服务器地址或名称</param>
        /// <param name="dadaBase">数据库</param>
        /// <param name="usr">用户</param>
        /// <param name="pswd">密码</param>
        /// <param name="appName">程序名称</param>
        /// <returns>Entity连接字符串</returns>
        public static string GetEntityConnectionString(string edmxFullName, string server, string dadaBase, string usr, string pswd, string appName)
        {
            System.Data.EntityClient.EntityConnectionStringBuilder entityConnectionStringBuilder = new System.Data.EntityClient.EntityConnectionStringBuilder();
            entityConnectionStringBuilder.Metadata = "res://*/" + edmxFullName + ".csdl|res://*/" + edmxFullName + ".ssdl|res://*/" + edmxFullName + ".msl";
            entityConnectionStringBuilder.Provider = "System.Data.SqlClient";

            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
            sqlConnectionStringBuilder.DataSource = server;
            sqlConnectionStringBuilder.InitialCatalog = dadaBase;
            sqlConnectionStringBuilder.IntegratedSecurity = true;
            sqlConnectionStringBuilder.UserID = usr;
            sqlConnectionStringBuilder.Password = pswd;
            sqlConnectionStringBuilder.MultipleActiveResultSets = true;
            sqlConnectionStringBuilder.ApplicationName = appName;
            entityConnectionStringBuilder.ProviderConnectionString = sqlConnectionStringBuilder.ToString();

            return entityConnectionStringBuilder.ConnectionString;
        } 
        #endregion
    }
}
