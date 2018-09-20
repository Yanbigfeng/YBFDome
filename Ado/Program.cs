using System;
using ModelSoure.DataHelp;
using System.Data;
using System.Data.SqlClient;
using ModelSoure.DataSoure;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using AdoHelp.Concurrency;

namespace AdoHelp
{
    class Program
    {
        static void Main(string[] args)
        {


            try
            {
                Stopwatch stopWatch = new Stopwatch();
                TaskTest.QueueMian();
                Console.ReadLine();
                TaskTest.QueueC();
                Console.ReadLine();
            }
            catch (Exception e)
            {

                throw;
            }
        }
        

    }
}
