using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeEF
{
    class SysUserInfor
    {
        public int id { get; set; }
        public string userAccount { get; set; }
        public string userPwd { get; set; }
        public string userRealName { get; set; }
        public Nullable<int> userSex { get; set; }
        public string userPhone { get; set; }
        public string userRemark { get; set; }
        public Nullable<System.DateTime> addTime { get; set; }
        public Nullable<int> delFlag { get; set; }
    }
}
