using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeEF
{
    class SysPermissonInfor
    {
        public int id { get; set; }
        public int parentId { get; set; }
        public string menuName { get; set; }
        public string areaName { get; set; }
        public string controllerName { get; set; }
        public string actionName { get; set; }
        public string iconClassName { get; set; }
        public string remarkContent { get; set; }
        public Nullable<System.DateTime> addTime { get; set; }
        public Nullable<int> sortFlag { get; set; }
        public Nullable<int> delFlag { get; set; }
    }
}
