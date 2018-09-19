using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ado.ClassKeep;

namespace Ado.MethodKeep
{
    class method2
    {
        #region 距离计算
        private const decimal EARTH_RADIUS = 6378.137M;
        /// <summary>
        /// 转化为弧度(rad) 
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private static decimal rad(decimal d)
        {
            return d * Convert.ToDecimal(Math.PI / 180.0);
        }

        /// <summary>    
        /// 计算坐标点的距离    
        /// </summary>    
        /// <param name="begin">开始的经度纬度</param>    
        /// <param name="end">结束的经度纬度</param>    
        /// <returns>距离(千米)</returns>    
        public static decimal GetDistance(decimal beginLat, decimal beginLng, decimal endLat, decimal endLng)
        {
            decimal radBeginLat = rad(beginLat);
            decimal radEndLat = rad(endLat);

            decimal radLatInterval = radBeginLat - radEndLat;
            decimal radLngInterval = rad(beginLng) - rad(endLng);

            decimal dis = Convert.ToDecimal(2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin((double)radLatInterval / 2), 2) + Math.Cos((double)radBeginLat) * Math.Cos((double)radEndLat) * Math.Pow(Math.Sin((double)radLngInterval / 2), 2))));
            dis = dis * EARTH_RADIUS;
            decimal distance = Convert.ToDecimal(Math.Round((decimal)dis * 10000) / 10000);

            return distance;
        }
        #endregion

        #region 3.命名规则转换
        public void Camel()
        {
            Console.WriteLine("请输入字符串内容！！！！");
            var str = Console.ReadLine();
            Console.WriteLine("请选中命名规则1：camel2：pascal");
            var judge = Console.ReadLine();
            string[] strs = str.Split(' ');
            string result = "";

            for (int i = 0; i < strs.Length; i++)
            {
                if (i == 0)
                {
                    result = strs[i].Substring(0, 1).ToLower() + strs[i].Substring(1);
                }
                else
                {
                    result += strs[i].Substring(0, 1).ToUpper() + strs[i].Substring(1);
                }

            }
            Console.WriteLine(result);
        }

        #endregion

        #region 时间是否本周
        public void isWeek()
        {
            //需要计算的周时间
            var dateOld = Convert.ToDateTime("2017-1-8");
            //时间距离周末还有几天
            int distance = Convert.ToInt32(dateOld.DayOfWeek);
            //周末日期
            var weekDate = dateOld.AddDays(distance == 0 ? 0 : 7 - distance);
            //当前日期
            var dateNew = DateTime.Now.Date;
            //判断当前日期是否在计算时间的那周内
            if (dateNew > weekDate)
            {
                Console.WriteLine("不是本周");
            }
            else
            {
                Console.WriteLine("在本周");
            }
        }
        #endregion

        #region 子类重写父类之后调用父类方法
        public void BaseCall()
        {
            b ss = new b();
            ss.aa();
            ((a)ss).aa();
        }
        #endregion

        #region 乘阶
        public static decimal jicheng(decimal num)
        {
            if (num == 0)
            {
                return 0;
            }
            if (num == 1)
            {
                return 1;
            }
            else
            {
                return num * jicheng(num - 1);
            }
        }
        #endregion

        #region 验证数字
        private static bool isNub(string i)
        {
            foreach (char c in i)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;

        }
        #endregion

    }
}
