using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace AdoHelp.Common
{
    /// <summary>
    /// 反射属性相关封装
    /// </summary>
    class ForeachClass
    {
        /// <summary>
        /// C#反射遍历对象属性
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="model">对象</param>
        public static void ForeachClassProperties<T>(T model)
        {
            Type t = model.GetType();
            PropertyInfo[] PropertyList = t.GetProperties();
            foreach (PropertyInfo item in PropertyList)
            {
                string name = item.Name;
                object value = item.GetValue(model, null);
            }
        }
        /// <summary>
        /// C#同类属性名相同就行值赋值
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="model">接受对象</param>
        /// <param name="modeld">赋值对象</param>
        public static void ForeachClassAss<T>(T accModel, T assModel)
        {
            //接受
            Type ta = assModel.GetType();
            PropertyInfo[] PropertyListA = ta.GetProperties();
            //赋值
            Type tb = assModel.GetType();
            PropertyInfo[] PropertyListB = tb.GetProperties();
            foreach (PropertyInfo item in PropertyListB)
            {
                string nameB = item.Name;
                object value = item.GetValue(assModel, null);
                //循环查询相同属性
                foreach (PropertyInfo itemA in PropertyListA)
                {
                    string nameA = itemA.Name;
                    if (nameA == nameB)
                    {
                        if (value != null)
                        {
                            itemA.SetValue(accModel, value, null);
                        }
                    }
                }
            }
        }

    }
    public class aClass
    {
        public string name { set; get; }
        public int? age { set; get; }
        public DateTime? addTime { set; get; }
    }
}
