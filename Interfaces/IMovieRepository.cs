using MoviesDataBaseApp.Models;

namespace MoviesDataBaseApp.Interfaces
{
    public interface IMovieRepository
    {

        Task<IEnumerable<Movie>> GetAll();

        Task<Movie> GetByIdAsync(int id);

        Task<Movie> GetByIdAsyncNoTracking(int id);

        Task<IEnumerable<Movie>> GetMovieByGenre(string genre);

        bool Add(Movie movie);

        bool Update(Movie movie);

        bool Delete(Movie movie);

        bool Save();
    }
}
