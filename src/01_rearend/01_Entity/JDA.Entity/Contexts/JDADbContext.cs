using JDA.Core.Persistence.Contexts;
using JDA.Core.Persistence.Entities;
using JDA.Core.Persistence.Maps;
using JDA.Core.Utilities;
using JDA.Entity.Entities.Sys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Entity.Contexts
{
    public partial class JDADbContext : JDABaseDbContext<JDADbContext>
    {
        public JDADbContext(DbContextOptions<JDADbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
    }
}
