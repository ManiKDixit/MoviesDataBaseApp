using Microsoft.EntityFrameworkCore;
using MoviesDatabase.Data;
using MoviesDataBaseApp.Interfaces;
using MoviesDataBaseApp.Models;

namespace MoviesDataBaseApp.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardRepository(IHttpContextAccessor httpContextAccessor , ApplicationDbContext context)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;

        }
        public async Task<List<Movie>> GetAllUserMovies()
        {
            var curUser =  _httpContextAccessor.HttpContext?.User.GetUserId();
            var usermovies = _context.Movies.Where(r => r.AppUser.Id == curUser);
            return usermovies.ToList();
        }



        public async Task<AppUser> GetUserById(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<AppUser> GetByIdNoTracking(string id)
        {
            return await _context.Users.Where(u => u.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public bool Update(AppUser user)
        {
            _context.Users.Update(user);
            return Save();
        }


        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
