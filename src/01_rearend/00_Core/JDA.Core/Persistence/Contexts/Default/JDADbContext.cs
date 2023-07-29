using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.Persistence.Contexts.Default
{
    public partial class JDADbContext : JDABaseDbContext
    {
        public JDADbContext(DbContextOptions<JDADbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
    }
}
