using MoviesDataBaseApp.Models;

namespace MoviesDataBaseApp.Interfaces
{
    public interface IMovieRepository
    {

        Task<IEnumerable<Movie>> GetAll();

        //Task<Movie> GetByIdAsync(int id);
        Task<Movie> GetByIdAsync(int id, string appid);


        //Task<Movie> GetByIdAsyncNoTracking(int id);
        Task<Movie> GetByIdAsyncNoTracking(int id, string appid);

        Task<IEnumerable<Movie>> GetMovieByGenre(string genre);

        bool Add(Movie movie);

        bool Update(Movie movie);

        bool Delete(Movie movie);

        bool Save();
    }
}
