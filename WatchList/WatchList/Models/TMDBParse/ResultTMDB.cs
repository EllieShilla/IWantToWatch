namespace WatchList.Models.TMDBParse
{
    public class ResultTMDB
    {
        [Newtonsoft.Json.JsonProperty("results")]
        public TMDB_Model[] Results { get; set; } = new TMDB_Model[0];
        [Newtonsoft.Json.JsonProperty("total_pages")]
        public int PageCount { get; set; } = 0;
    }
}
