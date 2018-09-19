using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoHelp.ClassKeep
{
    class AsyncClass
    {
        #region 异步操作
        /// <summary>
        /// 调用开始
        /// </summary>
        static async void AsyncMethod()
        {
            Console.WriteLine("开始异步代码");
            //第一种
            await MyMethod();
            //第二种
            await Task.Run(() =>
            {

                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine("异步执行" + i.ToString() + "..");
                    Thread.Sleep(1000); //模拟耗时操作
                }
            });
            Console.WriteLine("异步代码执行完毕");
        }

        static async Task MyMethod()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("异步执行" + i.ToString() + "..");
                await Task.Delay(1000); //模拟耗时操作
            }
        }
        #endregion
    }
}
