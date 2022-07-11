using WatchList.Models;

namespace WatchList.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DataContext>();

                context.Database.EnsureCreated();

                if (!context.Genres.Any())
                {
                    context.Genres.AddRange(new List<Genre>(){

                        new Genre()
                        {
                            genre_name="Adventure",
                            idTMDB=12
                        },
                        new Genre()
                        {
                            genre_name="Action",
                            idTMDB=28
                        },
                        new Genre()
                        {
                            genre_name="Animation",
                            idTMDB=16
                        }
                        ,
                        new Genre()
                        {
                            genre_name="Comedy",
                            idTMDB=35
                        }
                        ,
                        new Genre()
                        {
                            genre_name="Crime",
                            idTMDB=80
                        }
                        ,
                        new Genre()
                        {
                            genre_name="Documentary",
                            idTMDB=99
                        }
                        ,
                        new Genre()
                        {
                            genre_name="Drama",
                            idTMDB=18
                        }
                        ,
                        new Genre()
                        {
                            genre_name="Family",
                            idTMDB=10751
                        }
                        ,
                        new Genre()
                        {
                            genre_name="Fantasy",
                            idTMDB=14
                        }
                        ,
                        new Genre()
                        {
                            genre_name="History",
                            idTMDB=36
                        }
                        ,
                        new Genre()
                        {
                            genre_name="Horror",
                            idTMDB=27
                        }
                        ,
                        new Genre()
                        {
                            genre_name="Music",
                            idTMDB=10402
                        }
                        ,
                        new Genre()
                        {
                            genre_name="Mystery",
                            idTMDB=9648
                        }
                                                ,
                        new Genre()
                        {
                            genre_name="Romance",
                            idTMDB=10749
                        }
                        ,
                        new Genre()
                        {
                            genre_name="Science Fiction",
                            idTMDB=878
                        }
                                                ,
                        new Genre()
                        {
                            genre_name="TV Movie",
                            idTMDB=10770
                        }
                        ,
                        new Genre()
                        {
                            genre_name="Thriller",
                            idTMDB=53
                        }
                        ,
                        new Genre()
                        {
                            genre_name="War",
                            idTMDB=10752
                        }
                        ,
                        new Genre()
                        {
                            genre_name="Western",
                            idTMDB=37
                        },
                        new Genre()
                        {
                            genre_name="Action & Adventure",
                            idTMDB=10759
                        },
                        new Genre()
                        {
                            genre_name="Kids",
                            idTMDB=10762
                        },
                        new Genre()
                        {
                            genre_name="News",
                            idTMDB=10763
                        },
                        new Genre()
                        {
                            genre_name="Reality",
                            idTMDB=10764
                        },
                        new Genre()
                        {
                            genre_name="Sci-Fi & Fantasy",
                            idTMDB=10765
                        },
                        new Genre()
                        {
                            genre_name="Soap",
                            idTMDB=10766
                        },
                        new Genre()
                        {
                            genre_name="Talk",
                            idTMDB=10767
                        },
                        new Genre()
                        {
                            genre_name="War & Politics",
                            idTMDB=10768
                        }
                    });
                }

                context.SaveChanges();

            }
        }
    }
}
