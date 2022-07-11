namespace WatchList.Models.TMDBParse
{
    public class GenreTMDB
    {
        [Newtonsoft.Json.JsonProperty("id")]
        public int GenresId { get; set; }

        [Newtonsoft.Json.JsonProperty("name")]
        public string GenresName { get; set; } = "";
    }
}
