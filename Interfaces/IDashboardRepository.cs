using MoviesDataBaseApp.Models;

namespace MoviesDataBaseApp.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<Movie>> GetAllUserMovies();
        Task<AppUser> GetUserById(string id);
        Task<AppUser> GetByIdNoTracking(string id);
        bool Update(AppUser user);
        bool Save();
    }
}
