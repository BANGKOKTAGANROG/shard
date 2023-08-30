using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Xml;

namespace Shard.Startup
{
    public class ConfigManager
    {
        private static string GetFolderPath()
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    return fbd.SelectedPath;
                }
            }

            return string.Empty;
        }

        public static void Create(string path)
        {
            string songsFolder;
            bool revalidateCacheOnStartup, defaultComboTag;

            Console.WriteLine("Creating Shard config... Press Enter to use defaults.");

            // asking for songs folder
            Console.WriteLine(@"Select your Songs folder. Default: %LocalAppData%\osu!\Songs");
            Console.WriteLine("Writing anything opens dialog window. Do you agree with default? (Y/n)");
            while (true)
            {
                Console.Write("> ");
                if (string.IsNullOrEmpty(Console.ReadLine()))
                {
                    songsFolder = @"%LocalAppData%\osu!\Songs";
                    if (Directory.Exists(Environment.ExpandEnvironmentVariables(songsFolder)))
                    {
                        break;
                    }
                }
                
                songsFolder = GetFolderPath();
                Console.WriteLine($"Your choice: {songsFolder}. Is that correct folder? (Y/n)");

                while (true)
                {
                    Console.Write("> ");
                    var input = Console.ReadLine();
                    if (string.IsNullOrEmpty(input) | input == "y" | input == "Y")
                    {
                        break;
                    }
                    else
                    {
                        songsFolder = GetFolderPath();
                        Console.WriteLine($"Your choice: {songsFolder}. Is that correct folder? (Y/n)");
                    }
                };

                break;
            }

            // asking for cache revalidation
            Console.WriteLine("Do you want Shard to revalidate cache on each run? Default: yes");
            Console.WriteLine("Do you agree with default? (Y/n)");
            while (true)
            {
                Console.Write("> ");
                var input = Console.ReadLine();

                if (string.IsNullOrEmpty(input) | input == "y" | input == "Y")
                {
                    revalidateCacheOnStartup = true;
                }
                else
                {
                    revalidateCacheOnStartup = false;
                }

                break;
            }

            // asking if combotag should be default
            Console.WriteLine("Do you want Combo-Tag to be default mode? Default: no");
            Console.WriteLine("Do you agree with default? (y/N)");
            while (true)
            {
                Console.Write("> ");
                var input = Console.ReadLine();

                if (string.IsNullOrEmpty(input) | input == "n" | input == "N")
                {
                    defaultComboTag = false;
                }
                else
                {
                    defaultComboTag = true;
                }

                break;
            }

            Write(new Config(songsFolder, revalidateCacheOnStartup, defaultComboTag), path);
        }

        public static void Write(Config config, string path)
        {
            string json = JsonConvert.SerializeObject(config, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(path, json);
        }

        public static Config Parse(string path)
        {
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<Config>(json, new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore
            })!;
        }
    }
}
