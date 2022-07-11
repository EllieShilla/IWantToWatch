namespace WatchList.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public int idTMDB { get; set; }
        public int idUser { get; set; }
        public bool isWatched { get; set; }
        public float raiting { get; set; }
    }
}
