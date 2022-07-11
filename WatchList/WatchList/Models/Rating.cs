namespace WatchList.Models
{
	public class Rating
	{
		public int IdTMDB { get; set; }
		public MediType type { get; set; }
		public int rate { get; set; }
	}

	public enum MediType
	{
		Movie,
		Series
	}
}
