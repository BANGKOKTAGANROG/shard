namespace Shard.Startup
{
    public class Config
    {
        private string songsFolder;
        public string SongsFolder
        {
            get
            {
                return Environment.ExpandEnvironmentVariables(songsFolder);
            }
            set
            {
                songsFolder = value;
            }
        }
        public bool RevalidateCacheOnStartup { get; set; }
        public bool DefaultComboTag { get; set; }

        public Config(string _songsFolder, bool revalidateCacheOnStartup, bool defaultComboTag)
        {
            songsFolder = _songsFolder;
            RevalidateCacheOnStartup = revalidateCacheOnStartup;
            DefaultComboTag = defaultComboTag;
        }
    }
}