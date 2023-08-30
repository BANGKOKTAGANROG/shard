using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Shard.Database
{
    public class DatabaseContext : DbContext
    {
        private const string ConnectionString = @"Data Source=./shard.db";

        public DbSet<Beatmap> Beatmaps { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(ConnectionString);
        }
    }
}
