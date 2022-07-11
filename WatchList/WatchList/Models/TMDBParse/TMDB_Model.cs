namespace WatchList.Models.TMDBParse
{
    public class TMDB_Model
    {
        [Newtonsoft.Json.JsonProperty("id")]
        public int Id { get; set; }
        [Newtonsoft.Json.JsonProperty("title")]
        public string Title { get; set; } = "";

        [Newtonsoft.Json.JsonProperty("name")]
        public string Name { get; set; } = "";

        [Newtonsoft.Json.JsonProperty("overview")]
        public string Overview { get; set; } = "";
        [Newtonsoft.Json.JsonProperty("release_date")]
        public string ReleaseDate { get; set; } = "";
        [Newtonsoft.Json.JsonProperty("poster_path")]
        public string PosterPath { get; set; } = "";
        [Newtonsoft.Json.JsonProperty("vote_average")]
        public float Vote { get; set; } = 0;
        [Newtonsoft.Json.JsonProperty("genre_ids")]
        public int[] GenresId { get; set; } = { 0, 0 };
        [Newtonsoft.Json.JsonProperty("genres")]
        public GenreTMDB[] GenreTMDB { get; set; }
        public List<string> GenresInText { get; set; } = new List<string> { };

        [Newtonsoft.Json.JsonProperty("last_episode_to_air")]
        public DetailTVInfo Detail { get; set; }

        [Newtonsoft.Json.JsonProperty("runtime")]
        public int Runtime { get; set; } = 0;

        [Newtonsoft.Json.JsonProperty("tagline")]
        public string Tagline { get; set; } 

        [Newtonsoft.Json.JsonProperty("status")]
        public string Status { get; set; }


        [Newtonsoft.Json.JsonProperty("number_of_seasons")]
        public int NumberOfSeasons { get; set; }

        [Newtonsoft.Json.JsonProperty("number_of_episodes")]
        public int NumberOf_Episodes { get; set; }

        [Newtonsoft.Json.JsonProperty("seasons")]
        public SeasonsDetail[] Seasons { get; set; }

        [Newtonsoft.Json.JsonProperty("media_type")]
        public string mediaType { get; set; }
    }
    public class DetailTVInfo
    {
        [Newtonsoft.Json.JsonProperty("episode_number")]
        public int EpisodeNumber { get; set; }
    }

    public class SeasonsDetail
    {
        [Newtonsoft.Json.JsonProperty("episode_count")]
        public int EpisodeCount { get; set; }

        [Newtonsoft.Json.JsonProperty("id")]
        public int Id { get; set; }

        [Newtonsoft.Json.JsonProperty("season_number")]
        public int SeasonNumber { get; set; }

        [Newtonsoft.Json.JsonProperty("overview")]
        public string Overview { get; set; }

        [Newtonsoft.Json.JsonProperty("poster_path")]
        public string PosterPath { get; set; }
    }
}
