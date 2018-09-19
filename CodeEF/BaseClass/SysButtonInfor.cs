using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeEF
{
    class SysButtonInfor
    {
        public int id { get; set; }
        public int perId { get; set; }
        public string buttonName { get; set; }
        public string iconClassName { get; set; }
        public string methodName { get; set; }
        public Nullable<System.DateTime> addTime { get; set; }
        public Nullable<int> delFlag { get; set; }
    }
}
