using JHLA.Core.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHLA.Entity.Entities.Sys
{
    /// <summary>
    /// 系统用户表
    /// </summary>
    public partial class SysUser : SuperEntity
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 性别（关联字段表）
        /// </summary>
        public long Gender { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 密码盐
        /// </summary>
        public string PasswordSalt { get; set; }
        /// <summary>
        /// 是否启用（关联字段表）
        /// </summary>
        public long Enabled { get; set; }
    }
}
