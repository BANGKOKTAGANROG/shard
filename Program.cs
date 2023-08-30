using Shard.Startup;
using Shard.Database;

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
                { 
                    Console.WriteLine("Would you like to perform database cache now? (Y/n)\n> ");
                    var input = Console.ReadLine();

                    if (string.IsNullOrEmpty(input) | input.ToLower() == "y" )
                    {
                        using (var context = new DatabaseContext())
                        {
                            context.Database.EnsureCreated();
                            CacheBuilder.BuildCache(context, cfg.SongsFolder);
                        }
                    }
                    else
                    {
                        Environment.Exit(0);
                    }
                }
            }

            Config config = ConfigManager.Parse(ConfigPath);

            var db = new DatabaseContext();
            foreach (var bm in db.Beatmaps.Where(b => b.Difficulty == "Kyouaku"))
            {
                Console.WriteLine("{0} - {1} ({2}) [{3}]", bm.Artist, bm.Title, bm.Mapper, bm.Difficulty);
            }
        }
    }
}