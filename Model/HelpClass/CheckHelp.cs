using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Model
{
  public  class CheckHelp
    {

        #region 检验身份证是否合法
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdCard">身份证号</param>
        /// <returns>ok表示合法，error表示非法</returns>
        public static string CheckIdCard(string IdCard)
        {
            //校验码验证
            //（1）十七位数字本体码加权求和公式
            //S = Sum(Ai * Wi), i = 0, ... , 16 ，先对前17位数字的权求和
            //Ai:表示第i位置上的身份证号码数字值
            //Wi:表示第i位置上的加权因子
            //Wi: 7 9 10 5 8 4 2 1 6 3 7 9 10 5 8 4 2 
            //（2）计算模
            //Y = mod(S, 11)
            //（3）通过模得到对应的校验码
            //Y: 0 1 2 3 4 5 6 7 8 9 10
            //校验码: 1 0 X 9 8 7 6 5 4 3 2

            //1.先对身份证前18位进行拆分
            string[] Num18 = new string[18];
            for (int i = 0; i < 18; i++)
            {
                Num18[i] = IdCard.Substring(i, 1);
            }
            //2.加权因子
            string[] jqNum17 = new string[17] { "7", "9", "10", "5", "8", "4", "2", "1", "6", "3", "7", "9", "10", "5", "8", "4", "2" };
            //3.加权求和
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum = sum + int.Parse(Num18[i]) * int.Parse(jqNum17[i]);
            }
            //4.计算模
            int Y = sum % 11;
            //5.判断是否合法
            //5.1.声明数组用来存放 键值
            string[] key = new string[11] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
            string[] value = new string[11] { "1", "0", "X", "9", "8", "7", "6", "5", "4", "3", "2" };
            Hashtable ht = new Hashtable();
            for (int i = 0; i < 11; i++)
            {
                ht.Add(key[i], value[i]);
            }
            //5.2判断是否合法
            if (ht[Y.ToString()].Equals(Num18[17]))
            {
                //表示合法
                return "ok";
            }
            return "error";
        }

        #endregion
    }
}
