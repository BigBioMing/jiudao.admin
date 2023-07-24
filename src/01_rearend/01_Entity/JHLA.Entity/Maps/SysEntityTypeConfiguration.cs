using JHLA.Core.Persistence.Maps;
using JHLA.Entity.Entities.Sys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHLA.Entity.Maps
{
    public partial class SysUserEntityTypeConfiguration : SuperMapping<SysUser>
    {
        public override void Configure(EntityTypeBuilder<SysUser> builder)
        {
            builder.ToTable("sys_user");
        }
    }
}
