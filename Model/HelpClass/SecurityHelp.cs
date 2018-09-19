using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Web.Security;
using System.IO;

namespace Model.HelpClass
{
    class SecurityHelp
    {
        #region 1.MD5加密
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str">要加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string Md5(string str)
        {
            var md5 = MD5.Create();

            // 计算字符串的散列值
            var bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            var sbd = new StringBuilder();
            foreach (var item in bytes)
            {
                sbd.Append(item.ToString("x2"));
            }
            return sbd.ToString();
        }
        #endregion

        #region 2.基于MD5的自定义加密字符串方法（非MD5,不可逆）
        /// <summary>
        /// 基于MD5的自定义加密字符串方法(不可逆)（非MD5）
        /// <param name="str">要加密的字符串</param>
        /// <param name="key">加密密钥</param>
        /// <returns>加密后的字符串</returns>
        public static string Encrypt(string str, string key = "iceStone")
        {
            var md5 = MD5.Create();

            // 计算字符串的散列值
            var bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            var eKey = new StringBuilder();
            foreach (var item in bytes)
            {
                eKey.Append(item.ToString("x"));
            }

            // 字符串散列值+密钥再次计算散列值
            bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(key + eKey));
            var pwd = new StringBuilder();
            foreach (var item in bytes)
            {
                pwd.Append(item.ToString("x"));
            }

            return pwd.ToString();
        }
        #endregion

        #region 3.加密一个字符串(可逆，非固定)
        /// <summary>
        /// 加密一个字符串(可逆，非固定)
        /// </summary>
        /// <param name="str">要加密的字符串</param>
        /// <param name="key">加密密钥</param>
        /// <returns>加密后的字符串</returns>
        public static string EncryptStr(string str, string key = "iceStone")
        {
            var des = DES.Create();
            // var timestamp = DateTime.Now.ToString("HHmmssfff");
            var inputBytes = Encoding.UTF8.GetBytes(MixUp(str));
            var keyBytes = Encoding.UTF8.GetBytes(key);
            SHA1 ha = new SHA1Managed();
            var hb = ha.ComputeHash(keyBytes);
            var sKey = new byte[8];
            var sIv = new byte[8];
            for (var i = 0; i < 8; i++)
                sKey[i] = hb[i];
            for (var i = 8; i < 16; i++)
                sIv[i - 8] = hb[i];
            des.Key = sKey;
            des.IV = sIv;
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputBytes, 0, inputBytes.Length);
                    cs.FlushFinalBlock();
                    var ret = new StringBuilder();
                    foreach (var b in ms.ToArray())
                    {
                        ret.AppendFormat("{0:X2}", b);
                    }

                    return ret.ToString();
                }
            }
        }
        #endregion

        #region 4.解密一个字符串
        /// <summary>
        /// 解密一个字符串
        /// <param name="str">要解密的字符串</param>
        /// <param name="key">加密密钥</param>
        /// <returns>解密后的字符串</returns>
        public static string DecryptStr(string str, string key = "iceStone")
        {
            var des = DES.Create();
            var inputBytes = new byte[str.Length / 2];
            for (var x = 0; x < str.Length / 2; x++)
            {
                inputBytes[x] = (byte)System.Convert.ToInt32(str.Substring(x * 2, 2), 16);
            }
            var keyByteArray = Encoding.UTF8.GetBytes(key);
            var ha = new SHA1Managed();
            var hb = ha.ComputeHash(keyByteArray);
            var sKey = new byte[8];
            var sIv = new byte[8];
            for (var i = 0; i < 8; i++)
                sKey[i] = hb[i];
            for (var i = 8; i < 16; i++)
                sIv[i - 8] = hb[i];
            des.Key = sKey;
            des.IV = sIv;
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputBytes, 0, inputBytes.Length);
                    cs.FlushFinalBlock();
                    return ClearUp(Encoding.UTF8.GetString(ms.ToArray()));
                }
            }
        }
        #endregion

        #region 5.用随机字符串简单混淆

        private const int TimestampLength = 36;

        /// <summary>
        /// 用随机字符串简单混淆
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <returns>混淆后字符串</returns>
        public static string MixUp(string str)
        {

            var timestamp = Guid.NewGuid().ToString();
            var count = str.Length + TimestampLength;
            var sbd = new StringBuilder(count);
            int j = 0;
            int k = 0;
            for (int i = 0; i < count; i++)
            {
                if (j < TimestampLength && k < str.Length)
                {
                    if (i % 2 == 0)
                    {
                        sbd.Append(str[k]);
                        k++;
                    }
                    else
                    {
                        sbd.Append(timestamp[j]);
                        j++;
                    }
                }
                else if (j >= TimestampLength)
                {
                    sbd.Append(str[k]);
                    k++;
                }
                else if (k >= str.Length)
                {
                    break;

                    // sbd.Append(timestamp[j]);
                    // j++;
                }
            }

            return sbd.ToString();
        }
        #endregion

        #region 6.简单反混淆
        /// <summary>
        /// 简单反混淆
        /// </summary>
        /// <param name="str">混淆后字符串</param>
        /// <returns>原来字符串</returns>
        public static string ClearUp(string str)
        {
            var sbd = new StringBuilder();
            int j = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (i % 2 == 0)
                {
                    sbd.Append(str[i]);
                }
                else
                {
                    j++;
                }

                if (j > TimestampLength)
                {
                    sbd.Append(str.Substring(i));
                    break;
                }
            }

            return sbd.ToString();
        }
        #endregion

        #region 7.票据加密
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <returns>加密字符串</returns>
        public static string Encrypt(string userInfo)
        {
            //第一：将用户数据存入票据对象
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, "十二", DateTime.Now, DateTime.Now.AddMinutes(10), true, userInfo);

            //第二:将票据对象加密成字符串
            string security = FormsAuthentication.Encrypt(ticket);
            return security;
        }
        #endregion

        #region 8.票据解密
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="encryptStr">加密字符串</param>
        /// <returns>用户数据</returns>
        public static string DeEncrypt(string encryptStr)
        {

            //将加密字符串解释成票据对象
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(encryptStr);
            return ticket.UserData;
        }
        #endregion
    }
}
