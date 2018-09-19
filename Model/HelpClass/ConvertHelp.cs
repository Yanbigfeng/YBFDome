using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.HelpClass
{
    public class ConvertHelp
    {
        #region 1.时间转换成字符串

        private const string DateFormat = "yyyy-MM-dd HH:ss";

        //private const string DateFormat = "yyyy-MM-dd hh:mm:ss.S";
        //private const string DateFormat = "yyyy-MM-dd E HH:mm:ss";
        //private const string DateFormat = "yyyy-MM-dd EE hh:mm:ss";
        //private const string DateFormat = "yyyy-MM-dd EEE hh:mm:ss";

        /// <summary>
        /// 时间转换成字符串
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <returns>字符串</returns>
        public static string DateToString(DateTime dateTime)
        {
            return dateTime.ToString(DateFormat);
        }
        #endregion

        #region 2.将DateTime时间换成中文
        /// <summary>
        /// 将DateTime时间换成中文
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <returns>System.String.</returns>
        /// <example>
        /// 2012-12-21 12:12:21.012 → 1月前
        /// 2011-12-21 12:12:21.012 → 1年前
        /// </example>
        public static string ToChsStr(DateTime dateTime)
        {
            var ts = DateTime.Now - dateTime;
            if ((int)ts.TotalDays >= 365)
                return (int)ts.TotalDays / 365 + "年前";
            if ((int)ts.TotalDays >= 30 && ts.TotalDays <= 365)
                return (int)ts.TotalDays / 30 + "月前";
            if ((int)ts.TotalDays == 1)
                return "昨天";
            if ((int)ts.TotalDays == 2)
                return "前天";
            if ((int)ts.TotalDays >= 3 && ts.TotalDays <= 30)
                return (int)ts.TotalDays + "天前";
            if ((int)ts.TotalDays != 0) return dateTime.ToString("yyyy年MM月dd日");
            if ((int)ts.TotalHours != 0)
                return (int)ts.TotalHours + "小时前";
            if ((int)ts.TotalMinutes == 0)
                return "刚刚";
            return (int)ts.TotalMinutes + "分钟前";
        }

        #endregion

        #region 3.String to Boolean
        /// <summary>
        /// String to Boolean(字符串 转换成 布尔型)
        /// </summary>
        /// <param name="s">String</param>
        /// <param name="def">Default</param>
        /// <returns>Byte</returns>
        public static bool ToBoolean(string s, bool def = default(bool))
        {
            bool result;
            return Boolean.TryParse(s, out result) ? result : def;
        }
        #endregion

        #region 4.String to Char
        /// <summary>
        /// String to Char(字符串 转换成 无符号、数值、整数)
        /// </summary>
        /// <param name="s">String</param>
        /// <param name="def">Default</param>
        /// <returns>Byte</returns>
        public static char ToChar(string s, char def = default(char))
        {
            char result;
            return Char.TryParse(s, out result) ? result : def;
        }
        #endregion

        #region 5.String to Decimal
        /// <summary>
        /// String to Decimal(字符串 转换成 数值、十进制)
        /// </summary>
        /// <param name="s">String</param>
        /// <param name="def">Default</param>
        /// <returns>Byte</returns>
        public static decimal ToDecimal(string s, decimal def = default(decimal))
        {
            decimal result;
            return Decimal.TryParse(s, out result) ? result : def;
        }
        #endregion

        #region 6.String to Double
        /// <summary>
        /// String to Double(字符串 转换成 数值、浮点)
        /// </summary>
        /// <param name="s">String</param>
        /// <param name="def">Default</param>
        /// <returns>Byte</returns>
        public static double ToDouble(string s, double def = default(double))
        {
            double result;
            return Double.TryParse(s, out result) ? result : def;
        }
        #endregion

        #region 7.String to Single
        /// <summary>
        /// String to Single(字符串 转换成 数值、浮点)
        /// </summary>
        /// <param name="s">String</param>
        /// <param name="def">Default</param>
        /// <returns>Byte</returns>
        public static float ToSingle(string s, float def = default(float))
        {
            float result;
            return Single.TryParse(s, out result) ? result : def;
        }
        #endregion

        #region 8.String to Byte
        /// <summary>
        /// String to Byte(字符串 转换成 无符号、数值、整数)
        /// </summary>
        /// <param name="s">String</param>
        /// <param name="def">Default</param>
        /// <returns>Byte</returns>
        public static byte ToByte(string s, byte def = default(byte))
        {
            byte result;
            return Byte.TryParse(s, out result) ? result : def;
        }
        #endregion

        #region 9.String to SByte
        /// <summary>
        /// String to SByte(字符串 转换成 有符号、数值、整数)
        /// </summary>
        /// <param name="s">String</param>
        /// <param name="def">Default</param>
        /// <returns>Byte</returns>
        public static sbyte ToSByte(string s, sbyte def = default(sbyte))
        {
            sbyte result;
            return SByte.TryParse(s, out result) ? result : def;
        }
        #endregion

        #region 10.String to Int16
        /// <summary>
        /// String to Int16(字符串 转换成 有符号、数值、整数)
        /// </summary>
        /// <param name="s">String</param>
        /// <param name="def">Default</param>
        /// <returns>Byte</returns>
        public static short ToInt16(string s, short def = default(short))
        {
            short result;
            return Int16.TryParse(s, out result) ? result : def;
        }
        #endregion

        #region 11.String to UInt16
        /// <summary>
        /// String to UInt16(字符串 转换成 无符号、数值、整数)
        /// </summary>
        /// <param name="s">String</param>
        /// <param name="def">Default</param>
        /// <returns>Byte</returns>
        public static ushort ToUInt16(string s, ushort def = default(ushort))
        {
            ushort result;
            return UInt16.TryParse(s, out result) ? result : def;
        }
        #endregion

        #region 12.String to Int32
        /// <summary>
        /// String to Int32(字符串 转换成 有符号、数值、整数)
        /// </summary>
        /// <param name="s">String</param>
        /// <param name="def">Default</param>
        /// <returns>Byte</returns>
        public static int ToInt32(string s, int def = default(int))
        {
            int result;
            return Int32.TryParse(s, out result) ? result : def;
        }
        #endregion

        #region 13.String to UInt32
        /// <summary>
        /// String to UInt32(字符串 转换成 无符号、数值、整数)
        /// </summary>
        /// <param name="s">String</param>
        /// <param name="def">Default</param>
        /// <returns>Byte</returns>
        public static uint ToUInt32(string s, uint def = default(uint))
        {
            uint result;
            return UInt32.TryParse(s, out result) ? result : def;
        }
        #endregion

        #region 14.String to Int64
        /// <summary>
        /// String to Int64(字符串 转换成 有符号、数值、整数)
        /// </summary>
        /// <param name="s">String</param>
        /// <param name="def">Default</param>
        /// <returns>Byte</returns>
        public static long ToInt64(string s, long def = default(long))
        {
            long result;
            return Int64.TryParse(s, out result) ? result : def;
        }
        #endregion

        #region 15.String to UInt64
        /// <summary>
        /// String to UInt64(字符串 转换成 无符号、数值、整数)
        /// </summary>
        /// <param name="s">String</param>
        /// <param name="def">Default</param>
        /// <returns>Byte</returns>
        public static ulong ToUInt64(string s, ulong def = default(ulong))
        {
            ulong result;
            return UInt64.TryParse(s, out result) ? result : def;
        }
        #endregion

        #region 16.String to DateTime
        /// <summary>
        /// String to DateTime(字符串 转换成 时间)
        /// </summary>
        /// <param name="s">String</param>
        /// <param name="def">Default</param>
        /// <returns>Byte</returns>
        public static DateTime ToDateTime(string s, DateTime def = default(DateTime))
        {
            DateTime result;
            return DateTime.TryParse(s, out result) ? result : def;
        }
        #endregion

        #region 17.String to Guid
        /// <summary>
        /// String to Guid(字符串 转换成 Guid)
        /// </summary>
        /// <param name="s">String</param>
        /// <param name="def">Default</param>
        /// <returns>Byte</returns>
        public static Guid ToGuid(string s, Guid def = default(Guid))
        {
            Guid result;
            return Guid.TryParse(s, out result) ? result : def;
        }
        #endregion
    }
}
