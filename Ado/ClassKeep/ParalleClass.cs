using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Concurrent;

namespace Ado.ClassKeep
{
    class ParalleClass
    {
        private Stopwatch stopWatch = new Stopwatch();

        public void Run1()
        {
            for (int i = 0; i < 10000; i++)
            {
                for (int j = 0; j < 60000; j++)
                {
                    int sum = 0;
                    sum += i;
                }
            }
            Console.WriteLine("第一个方法");
        }
        public void Run2()
        {
            for (int i = 0; i < 20000; i++)
            {
                for (int j = 0; j < 60000; j++)
                {
                    int sum = 0;
                    sum += i;
                }
            }
            Console.WriteLine("第二个方法");
        }

        public void ParallelInvokeMethod()
        {
            stopWatch.Start();
            Parallel.Invoke(Run1, Run2);
            stopWatch.Stop();
            Console.WriteLine("Parallel run " + stopWatch.ElapsedMilliseconds + " ms.");

            stopWatch.Restart();
            Run1();
            Run2();
            stopWatch.Stop();
            Console.WriteLine("Normal run " + stopWatch.ElapsedMilliseconds + " ms.");
        }

        public void ParallelFor()
        {
            ConcurrentBag<int> bag = new ConcurrentBag<int>();
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            Parallel.For(0, 1000, (i, state) =>
            {
                if (bag.Count == 300)
                {
                    state.Stop();
                    return;
                }
                bag.Add(i);
            });
            stopWatch.Stop();

            Console.WriteLine("Bag count is " + bag.Count + ", " + stopWatch.ElapsedMilliseconds);
        }

        #region 并行死锁问题
        public static void ParallelLock()
        {
            var t1 = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Task 1 Start running...");
                while (true)
                {
                    System.Threading.Thread.Sleep(1000);
                }
                Console.WriteLine("Task 1 Finished!");
            });
            var t2 = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Task 2 Start running...");
                System.Threading.Thread.Sleep(2000);
                Console.WriteLine("Task 2 Finished!");
            });
            Task.WaitAll(t1, t2);
        }
        #endregion

        #region 解决死锁问题
        /// <summary>
        /// 解决死锁问题设置时间
        /// </summary>
        public static void ParallelLockEnd()
        {
            Task[] tasks = new Task[2];
            tasks[0] = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Task 1 Start running...");
                while (true)
                {
                    System.Threading.Thread.Sleep(1000);
                }
                Console.WriteLine("Task 1 Finished!");
            });
            tasks[1] = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Task 2 Start running...");
                System.Threading.Thread.Sleep(2000);
                Console.WriteLine("Task 2 Finished!");
            });

            Task.WaitAll(tasks, 5000);
            for (int i = 0; i < tasks.Length; i++)
            {
                if (tasks[i].Status != TaskStatus.RanToCompletion)
                {
                    Console.WriteLine("Task {0} Error!", i + 1);
                }
            }
            Console.Read();
        }
        #endregion


        #region parallel用法解释
        //Paraller.For()方法类似于C#的for循环语句，也是多次执行一个任务。使用Paraller.For()方法，可以并行运行迭代，迭代的顺序没有定义。
        // Paraller.ForEach()方法遍历实现了IEnumerable的集合，其方法类似于 foreach的语句，但以异步方式遍历，这里也没有确定遍历顺序
        // Parallel.Invoke()方法，它提供了任务并行性模式。Paraller.Invoke()方法允许传递一个Action委托数组，在其中可以指定应运行的方法
        //Parallel.For()和Paraller.ForEach()方法在每次迭代中调用相同的代码，而Parallel.Invoke()方法允许同时调用不同的方法。Parallel.ForEach()用于数据并行性，Parallel.Invoke()用于任务并行性；
        #endregion
    }
}
