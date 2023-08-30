namespace Shard.Database
{
    public class Beatmap
    {
        public Beatmap(string artist, string title, string mapper, string difficulty, string filePath)
        {
            Artist = artist;
            Title = title;
            Mapper = mapper;
            Difficulty = difficulty;
            FilePath = filePath;
        }

        // per db entry
        public int ID { get; set; }

        // from filename
        public string Artist { get; set; }
        public string Title { get; set; }
        public string Mapper { get; set; }
        public string Difficulty { get; set; }

        // filepath
        public string FilePath { get; set; }
    }
}
