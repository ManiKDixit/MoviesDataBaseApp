using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesDatabase.Data;
using MoviesDataBaseApp.Interfaces;
using MoviesDataBaseApp.Models;

namespace MoviesDataBaseApp.Repository
{
    public class MovieRepository : IMovieRepository
    {

        private readonly ApplicationDbContext _context;

        public MovieRepository(ApplicationDbContext context)
        {
            _context = context; //Injecting the DB here
        }

        public bool Add(Movie movie)
        {
            _context.Add(movie);
            return Save();
        }

        public bool Delete(Movie movie)
        {
            _context.Remove(movie);
            return Save();
        }

        public async Task<IEnumerable<Movie>> GetAll()
        {
            return await _context.Movies.ToListAsync();
            
        }

        public async Task<Movie> GetByIdAsync(int id)
        {
            return await _context.Movies.Include(director => director.Director)
            .Include(genre => genre.Genre)
            .Include(award => award.Award)
            .Include(studios => studios.Studios).FirstOrDefaultAsync(i => i.Id == id);
                   }

        public async Task<Movie> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Movies.Include(director => director.Director)
            .Include(genre => genre.Genre)
            .Include(award => award.Award)
            .Include(studios => studios.Studios).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public Task<IEnumerable<Movie>> GetMovieByGenre(string genre)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Movie movie)
        {
            _context.Update(movie);
            return Save();
        }
    }
}
