namespace WatchList.Models
{
    public class Series
    {
        public int Id { get; set; }
        public int idTMDB { get; set; }
        public int idUser { get; set; }
        public bool isWatched { get; set; }
        public float raiting { get; set; }

        public int lastWatchedSeries { get; set; }
        public int currentSeason { get; set; }

    }
}
