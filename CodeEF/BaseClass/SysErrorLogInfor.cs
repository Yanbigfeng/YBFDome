using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeEF
{
    class SysErrorLogInfor
    {
        public string id { get; set; }
        public int userId { get; set; }
        public string userName { get; set; }
        public string logLevel { get; set; }
        public string logMessage { get; set; }
        public Nullable<System.DateTime> addTime { get; set; }
        public Nullable<int> delFlag { get; set; }
    }
}
