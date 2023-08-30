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
