using Newtonsoft.Json;
using System.Net;
using WatchList.Models.TMDBParse;

namespace WatchList.Models
{
    public class ToTMDB
    {
        string apiKey = "93e8cd020039ccea55dc74ad9d498b0a";

        public TMDB_Model GetMoreAboutMovieById(int id)
        {
            HttpWebRequest apiRequest = WebRequest.Create("https://api.themoviedb.org/3/movie/" + id + "?api_key=" + apiKey + "&language=en-US") as HttpWebRequest;

            TMDB_Model dataMovie;
            using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                dataMovie = JsonConvert.DeserializeObject<TMDB_Model>(reader.ReadToEnd());
            }

            return dataMovie;
        }


        public Tuple<TMDB_Model[], int> CallTopMovieAPI(int pageNum)
        {
            HttpWebRequest apiRequest = WebRequest.Create("https://api.themoviedb.org/3/movie/top_rated?api_key=" + apiKey + "&language=en-US&page=" + pageNum) as HttpWebRequest;

            ResultTMDB datalist;
            using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                datalist = JsonConvert.DeserializeObject<ResultTMDB>(reader.ReadToEnd());
            }

            return Tuple.Create(datalist.Results, datalist.PageCount);
        }

        public Tuple<TMDB_Model[], int> CallTopTVAPI(int pageNum)
        {
            HttpWebRequest apiRequest = WebRequest.Create("https://api.themoviedb.org/3/tv/top_rated?api_key=" + apiKey + "&language=en-US&page=" + pageNum) as HttpWebRequest;

            ResultTMDB datalist;
            using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                datalist = JsonConvert.DeserializeObject<ResultTMDB>(reader.ReadToEnd());
            }

            return Tuple.Create(datalist.Results, datalist.PageCount);
        }

        public TMDB_Model GetMoreAboutTVById(int id)
        {
            HttpWebRequest apiRequest = WebRequest.Create("https://api.themoviedb.org/3/tv/" + id + "?api_key=" + apiKey + "&language=en-US") as HttpWebRequest;

            TMDB_Model dataTV;
            using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                dataTV = JsonConvert.DeserializeObject<TMDB_Model>(reader.ReadToEnd());
            }

            return dataTV;
        }

        public Tuple<ResultTMDB, int> Search(string query, int pageNum)
        {
            HttpWebRequest apiRequest = WebRequest.Create("https://api.themoviedb.org/3/search/multi?api_key=" + apiKey + "&language=en-US&query=" + query + "&page=" + pageNum + "&include_adult=false") as HttpWebRequest;

            string buff;
            using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                buff = reader.ReadToEnd();
            }

            ResultTMDB resultTMDB = JsonConvert.DeserializeObject<ResultTMDB>(buff);

            return Tuple.Create(resultTMDB, resultTMDB.PageCount);
        }

        public TMDB_Images GetImagesFromMovie(int id)
        {
            HttpWebRequest apiRequest = WebRequest.Create("https://api.themoviedb.org/3/movie/" + id + "/images?api_key=" + apiKey + "") as HttpWebRequest;

            string buff;
            using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                buff = reader.ReadToEnd();
            }

            TMDB_Images images = JsonConvert.DeserializeObject<TMDB_Images>(buff);
            return images;
        }

        public TMDB_Images GetVideosFromMovie(int id)
        {
            HttpWebRequest apiRequest = WebRequest.Create("https://api.themoviedb.org/3/movie/" + id + "/videos?api_key=" + apiKey + "&language=en-US") as HttpWebRequest;

            string buff;
            using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                buff = reader.ReadToEnd();
            }

            TMDB_Images video = JsonConvert.DeserializeObject<TMDB_Images>(buff);
            return video;
        }

        public TMDB_Model[] SimilarMovie(int id)
        {
            HttpWebRequest apiRequest = WebRequest.Create("https://api.themoviedb.org/3/movie/" + id + "/similar?api_key=" + apiKey + "&language=en-US") as HttpWebRequest;

            ResultTMDB datalist;
            using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                datalist = JsonConvert.DeserializeObject<ResultTMDB>(reader.ReadToEnd());
            }

            return datalist.Results;
        }

        public TMDB_Images GetImagesFromTV(int id)
        {
            HttpWebRequest apiRequest = WebRequest.Create("https://api.themoviedb.org/3/tv/" + id + "/images?api_key=" + apiKey + "") as HttpWebRequest;

            string buff;
            using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                buff = reader.ReadToEnd();
            }

            TMDB_Images images = JsonConvert.DeserializeObject<TMDB_Images>(buff);
            return images;
        }

        public TMDB_Images GetVideosFromTV(int id)
        {
            HttpWebRequest apiRequest = WebRequest.Create("https://api.themoviedb.org/3/tv/" + id + "/videos?api_key=" + apiKey + "&language=en-US") as HttpWebRequest;

            string buff;
            using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                buff = reader.ReadToEnd();
            }

            TMDB_Images video = JsonConvert.DeserializeObject<TMDB_Images>(buff);
            return video;
        }

        public TMDB_Model[] SimilarTV(int id)
        {
            HttpWebRequest apiRequest = WebRequest.Create("https://api.themoviedb.org/3/tv/" + id + "/similar?api_key=" + apiKey + "&language=en-US") as HttpWebRequest;

            ResultTMDB datalist;
            using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                datalist = JsonConvert.DeserializeObject<ResultTMDB>(reader.ReadToEnd());
            }

            return datalist.Results;
        }
    }
}

