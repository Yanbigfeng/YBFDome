using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeEF
{
    class SysLoginLogInfor
    {
        public string id { get; set; }
        public int userId { get; set; }
        public string userName { get; set; }
        public string loginIp { get; set; }
        public string loginCity { get; set; }
        public Nullable<System.DateTime> loginTime { get; set; }
        public Nullable<int> delFlag { get; set; }
    }
}
