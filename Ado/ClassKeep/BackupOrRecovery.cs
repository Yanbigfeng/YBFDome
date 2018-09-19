using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace AdoHelp.ClassKeep
{
    class BackupOrRecovery
    {

        #region 调用示例
        static void Run()
        {
            //获取参数设置
            string dataName = System.Configuration.ConfigurationSettings.AppSettings["dataName"]; //数据库名称
            string backPath = System.Configuration.ConfigurationSettings.AppSettings["backPath"]; //数据库名称

            while (true)
            {
                Console.WriteLine("请输入命令：1：备份，2：恢复 100:退出");
                int command = 0;
                try
                {
                    command = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("请输入正确指令");
                }
                if (command == 1)
                {
                    Console.WriteLine("正在备份请稍后...");
                    //备份语句
                    string cmdText = @"backup database " + dataName + "  to disk='" + backPath + "'";
                    BakReductSql(cmdText, true, null);
                }
                else if (command == 2)
                {
                    Console.WriteLine("正在恢复请稍后...");
                    //恢复语句
                    string cmdText = @"restore database " + dataName + "  from disk='" + backPath + "'  With Replace";
                    BakReductSql(cmdText, false, dataName);
                }
                else if (command == 100)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("请输入正确指令");
                }
            }
        }
        #endregion


        #region 1.备份方法
        /// <summary>
        /// 备份方法
        /// </summary>
        /// <param name="dataName">数据库名称</param>
        /// <param name="path">备份文件路径</param>
        static void Backup(string dataName, string path)
        {
            //备份语句
            string cmdText = @"backup database "+ dataName + " to disk='"+ path + "'";
            BakReductSql(cmdText, true,"");

        }
        #endregion

        #region 2.恢复方法
        /// <summary>
        /// 恢复方法
        /// </summary>
        /// <param name="dataName">数据库名称</param>
        /// <param name="path">备份文件路径</param>
        static void Recovery(string dataName, string backPath)
        {
            //恢复语句
            string cmdText = @"restore database " + dataName + " from disk='"+ backPath + "'  With Replace";
            BakReductSql(cmdText, false, dataName);
        }


        /// <summary>
        /// 恢复方法[恢复到指定位置]
        /// </summary>
        /// <param name="dataName">数据库名称</param>
        /// <param name="path">备份文件路径</param>
        static void Recovery2(string dataName, string backPath)
        {
            //恢复语句
            string cmdText = @"restore database " + dataName + " from disk='" + backPath + "'  With Replace";
            BakReductSql(cmdText, false, dataName);
        }
        #endregion

        #region 3.备份恢复数据库sql执行语句

        /// <summary>
        /// 对数据库的备份和恢复操作，Sql语句实现
        /// </summary>
        /// <param name="cmdText">实现备份或恢复的Sql语句</param>
        /// <param name="isBak">该操作是否为备份操作，是为true否，为false</param>
        /// <param name="dataName">恢复时该字段为数据库名称，备份为空</param>
        static void BakReductSql(string cmdText, bool isBak, string dataName)
        {
            string SqlConnectionStr = System.Configuration.ConfigurationSettings.AppSettings["SqlConnectionStr"];
            SqlCommand cmdBakRst = new SqlCommand();
            SqlConnection conn = new SqlConnection(SqlConnectionStr);
            try
            {
                conn.Open();
                cmdBakRst.Connection = conn;
                cmdBakRst.CommandType = CommandType.Text;
                if (!isBak)     //如果是恢复操作
                {
                    string setOffline = "Alter database" + dataName + " Set Offline With rollback immediate ";
                    string setOnline = " Alter database " + dataName + " Set Online With Rollback immediate";
                    cmdBakRst.CommandText = setOffline + cmdText + setOnline;
                }
                else
                {
                    cmdBakRst.CommandText = cmdText;
                }
                cmdBakRst.ExecuteNonQuery();
                if (!isBak)
                {
                    Console.WriteLine("恭喜你，数据成功恢复为所选文档的状态！", "系统消息");
                }
                else
                {
                    Console.WriteLine("恭喜，你已经成功备份当前数据！", "系统消息");
                }
            }
            catch (SqlException sexc)
            {
                Console.WriteLine("失败，可能是对数据库操作失败，原因：" + sexc, "数据库错误消息");
            }
            catch (Exception ex)
            {
                Console.WriteLine("对不起，操作失败，可能原因：" + ex, "系统消息");
            }
            finally
            {
                cmdBakRst.Dispose();
                conn.Close();
                conn.Dispose();
            }
        }
        #endregion
    }
}
