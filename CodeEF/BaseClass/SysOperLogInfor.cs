using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeEF
{
    class SysOperLogInfor
    {
        public string id { get; set; }
        public int userId { get; set; }
        public string userName { get; set; }
        public string operMessage { get; set; }
        public Nullable<System.DateTime> operTime { get; set; }
        public Nullable<int> delFlag { get; set; }
    }
}
