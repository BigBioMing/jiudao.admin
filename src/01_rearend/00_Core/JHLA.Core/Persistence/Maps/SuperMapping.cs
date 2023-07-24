﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHLA.Core.Persistence.Maps
{
    public abstract class SuperMapping<TSuperEntity> : IEntityTypeConfiguration<TSuperEntity> where TSuperEntity : class
    {
        public abstract void Configure(EntityTypeBuilder<TSuperEntity> builder);
    }
}
