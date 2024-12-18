﻿using Microsoft.EntityFrameworkCore;

namespace ToDo.Data
{
    public sealed class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }
}
