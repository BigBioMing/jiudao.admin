using JDA.Core.Persistence.Maps;
using JDA.Entity.Entities.Sys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Entity.Maps
{
    public partial class SysUserEntityTypeConfiguration : SuperMapping<SysUser>
    {
        public override void Configure(EntityTypeBuilder<SysUser> builder)
        {
            builder.ToTable("sys_user");
        }
    }
    public partial class SysRoleEntityTypeConfiguration : SuperMapping<SysRole>
    {
        public override void Configure(EntityTypeBuilder<SysRole> builder)
        {
            builder.ToTable("sys_role");
        }
    }
    public partial class SysRouteResourceEntityTypeConfiguration : SuperMapping<SysRouteResource>
    {
        public override void Configure(EntityTypeBuilder<SysRouteResource> builder)
        {
            builder.ToTable("sys_route_resource");
        }
    }
    public partial class SysActionResourceEntityTypeConfiguration : SuperMapping<SysActionResource>
    {
        public override void Configure(EntityTypeBuilder<SysActionResource> builder)
        {
            builder.ToTable("sys_action_resource");
        }
    }
    public partial class SysRoleRouteResourceEntityTypeConfiguration : SuperMapping<SysRoleRouteResource>
    {
        public override void Configure(EntityTypeBuilder<SysRoleRouteResource> builder)
        {
            builder.ToTable("sys_role_route_resource");
        }
    }
    public partial class SysRoleActionResourceEntityTypeConfiguration : SuperMapping<SysRoleActionResource>
    {
        public override void Configure(EntityTypeBuilder<SysRoleActionResource> builder)
        {
            builder.ToTable("sys_role_action_resource");
        }
    }
    public partial class SysOrganizationEntityTypeConfiguration : SuperMapping<SysOrganization>
    {
        public override void Configure(EntityTypeBuilder<SysOrganization> builder)
        {
            builder.ToTable("sys_organization");
        }
    }
    public partial class SysUserRoleEntityTypeConfiguration : SuperMapping<SysUserRole>
    {
        public override void Configure(EntityTypeBuilder<SysUserRole> builder)
        {
            builder.ToTable("sys_user_role");
        }
    }
    public partial class SysUserOrganizationEntityTypeConfiguration : SuperMapping<SysUserOrganization>
    {
        public override void Configure(EntityTypeBuilder<SysUserOrganization> builder)
        {
            builder.ToTable("sys_user_organization");
        }
    }
    public partial class SysDictionaryDefineEntityTypeConfiguration : SuperMapping<SysDictionaryDefine>
    {
        public override void Configure(EntityTypeBuilder<SysDictionaryDefine> builder)
        {
            builder.ToTable("sys_dictionary_define");
        }
    }
    public partial class SysDictionaryDataEntityTypeConfiguration : SuperMapping<SysDictionaryData>
    {
        public override void Configure(EntityTypeBuilder<SysDictionaryData> builder)
        {
            builder.ToTable("sys_dictionary_data");
        }
    }
    public partial class SysLogEntityTypeConfiguration : SuperMapping<SysLog>
    {
        public override void Configure(EntityTypeBuilder<SysLog> builder)
        {
            builder.ToTable("sys_log");
        }
    }
}
