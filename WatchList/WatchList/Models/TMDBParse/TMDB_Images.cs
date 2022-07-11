namespace WatchList.Models.TMDBParse
{
    public class TMDB_Images
    {
        [Newtonsoft.Json.JsonProperty("backdrops")]
        public Images_TMDB[] backdrops { get; set; }
		[Newtonsoft.Json.JsonProperty("results")]
		public Video_TMDB[] results { get; set; }
	}

    public class Images_TMDB
	{
        [Newtonsoft.Json.JsonProperty("file_path")]
        public string imageLink { get; set; }
    }

    public class Video_TMDB
    {
        [Newtonsoft.Json.JsonProperty("key")]
        public string videoLink { get; set; } = "";

        [Newtonsoft.Json.JsonProperty("type")]
        public string type { get; set; } = "";
    }
}
