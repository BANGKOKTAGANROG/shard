using System.Text.RegularExpressions;

namespace Shard.Database
{
    public class CacheBuilder
    {
        public static void BuildCache(DatabaseContext context, string path)
        {
            Console.WriteLine("Creating beatmap cache...");
            context.Database.EnsureCreated();
            List<string> files = Directory.GetFiles(path, "*.osu", SearchOption.AllDirectories).ToList();

            foreach (string filename in files)
            {
                // declare map properties
                string artist, title, mapper, difficulty;

                using (var sr = new StreamReader(filename))
                {
                    // get osu file format version
                    int FileVersion = int.Parse(Regex.Match(sr.ReadLine()!, @"(?<=osu file format v)\d+").Value);

                    // read until metadata block is found
                    while (true)
                    {
                        var line = sr.ReadLine();
                        if (line == "[Metadata]")
                        {
                            break;
                        }
                    }

                    string pattern = @"^[^:]+:(.+)$";

                    if (FileVersion >= 10) // TitleUnicode and ArtistUnicode were introduced in v10, so this check is necessary
                    {
                        title = Regex.Match(sr.ReadLine()!, pattern).Groups[1].Value;
                        sr.ReadLine(); // skip TitleUnicode
                        artist = Regex.Match(sr.ReadLine()!, pattern).Groups[1].Value;
                        sr.ReadLine(); // skip ArtistUnicode
                        mapper = Regex.Match(sr.ReadLine()!, pattern).Groups[1].Value;
                        difficulty = Regex.Match(sr.ReadLine()!, pattern).Groups[1].Value;
                    }
                    else
                    {
                        title = Regex.Match(sr.ReadLine()!, pattern).Groups[1].Value;
                        artist = Regex.Match(sr.ReadLine()!, pattern).Groups[1].Value;
                        mapper = Regex.Match(sr.ReadLine()!, pattern).Groups[1].Value;
                        difficulty = Regex.Match(sr.ReadLine()!, pattern).Groups[1].Value;
                    }
                }

                Beatmap beatmap = new(artist, title, mapper, difficulty, filename);

                context.Beatmaps.Add(beatmap);
            }

            // save changes cuz yeah
            context.SaveChanges();
        }
    }
}
