using Shard.Database;
using Shard.Startup;

namespace Shard
{
    internal class Program
    {
        private const string ConfigPath = @"./config.json";

        [STAThread]
        static void Main(string[] args)
        {
            // create config if doesn't exist and exit
            if (!File.Exists(ConfigPath))
            {
                ConfigManager.Create(ConfigPath);
                Config cfg = ConfigManager.Parse(ConfigPath);
                var context = new DatabaseContext();
                context.Database.EnsureCreated();
                CacheBuilder.BuildCache(context, cfg.SongsFolder);
                Environment.Exit(0);
            }

            Config config = ConfigManager.Parse(ConfigPath);

            var db = new DatabaseContext();
            foreach (var bm in db.Beatmaps.Where(b => b.Difficulty == ""))
            {
                Console.WriteLine("{0} - {1} ({2}) [{3}]", bm.Artist, bm.Title, bm.Mapper, bm.Difficulty);
            }
        }
    }
}