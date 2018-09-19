using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace CodeEF
{
    class BaseDbContext : DbContext
    {
        public BaseDbContext()
            : base("name=DataModelContext")//web.config中connectionstring的名字
        {
        }
        public DbSet<SysButtonInfor> SysButtonInfors { get; set; }
        public DbSet<SysErrorLogInfor> SysErrorLogInfors { get; set; }
        public DbSet<SysLoginLogInfor> SysLoginLogInfors { get; set; }
        public DbSet<SysOperLogInfor> SysOperLogInfors { get; set; }
        public DbSet<SysPermissonInfor> SysPermissonInfors { get; set; }
        public DbSet<SysRoleButtonInfor> SysRoleButtonInfors { get; set; }
        public DbSet<SysRoleInfor> SysRoleInfors { get; set; }
        public DbSet<SysRolePerInfor> SysRolePerInfors { get; set; }
        public DbSet<SysUserInfor> SysUserInfors { get; set; }
        public DbSet<SysUserRoleInfor> SysUserRoleInfors { get; set; }
        public DbSet<TestInfor> TestInfors { get; set; }
    }
}
