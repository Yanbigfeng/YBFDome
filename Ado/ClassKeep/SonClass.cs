using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ado.ClassKeep
{
    #region 子类重写父类之后调用父类（类）
    public abstract class a
    {
        public a()
        {
            Console.WriteLine("a");
        }
        public virtual void Func()
        {
            Console.WriteLine("a.fun");
        }
        public void aa()
        {
            Console.WriteLine("父类方法");
        }
    }

    public class b : a
    {

        public b()
        {
            Console.WriteLine("b");
        }
        public override void Func()
        {
            Console.WriteLine("b.fun");
        }
        public new void aa()
        {
            Console.WriteLine("子类方法");
        }
        public void cccc()
        {
            base.aa();
        }

    }
    public class c : b
    {
        public new void aa()
        {
            ((a)this).aa();
            Console.WriteLine("我是cccccccc");
        }
    }
    #endregion
}
