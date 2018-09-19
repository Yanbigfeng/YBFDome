using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace CodeEF
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new BaseDbContext())
            {
                Console.Write("警告：！！！开始运行程序");
                while (true)
                {
                    // Create and save a new TestInfor 
                    Console.Write("请输入操作命令：1.删除  2.修改 3.添加 4.退出 ");
                    var nub = Console.ReadLine();
                    if (nub == "1")
                    {
                        Console.Write("你选择删除，请输入id号 ");
                        string id =Console.ReadLine();
                        List<TestInfor> listDels = db.Set<TestInfor>().Where(u => u.id == id).ToList();
                        listDels.ForEach(d =>
                        {
                            db.Set<TestInfor>().Attach(d);
                            db.Set<TestInfor>().Remove(d);
                            db.SaveChanges();
                            Console.Write("删除完毕！ ");
                        });
                    }
                    else if (nub == "2")
                    {
                        Console.Write("你选择修改，请输入id号 ");
                        string id =Console.ReadLine();
                        Console.Write("你选择修改，请输入内容 ");
                        var title = Console.ReadLine();
                        TestInfor TestInfor = new TestInfor();
                        TestInfor.id = id;
                        TestInfor.title = title;
                        db.Entry(TestInfor).State = EntityState.Modified;
                        db.SaveChanges();
                        Console.Write("修改成功！ ");
                    }
                    else if (nub == "3")
                    {
                        Console.Write("你选择添加，请输入标题  ");
                        var title = Console.ReadLine();
                        Console.Write("你选择添加，请输入内容  ");
                        var content = Console.ReadLine();
                        var TestInfor = new TestInfor();
                        TestInfor.id = Guid.NewGuid().ToString("N");
                        TestInfor.title = title;
                        TestInfor.content = content;
                        db.TestInfors.Add(TestInfor);
                        db.SaveChanges();
                        Console.Write("添加成功！ ");
                    }
                    else if (nub == "4")
                    {
                        break;
                    }

                    // Display all TestInfors from the database 
                    var query = from b in db.TestInfors
                                orderby b.title
                                select b;

                    Console.WriteLine("开始打印所有数据：请等待。。。。。");
                    foreach (var item in query)
                    {
                        Console.WriteLine(item.title);
                    }


                }
            }
        }
    }
}
