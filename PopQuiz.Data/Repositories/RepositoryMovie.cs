using Microsoft.EntityFrameworkCore;
using PAW.Data.Models;
using PopQuiz.Data.Models;

namespace PAW.Data.Repositories;

public interface IRepositoryMovie
{
    Task<bool> UpsertAsync(Movie entity, bool isUpdating);
    Task<bool> CreateAsync(Movie entity);
    Task<bool> DeleteAsync(Movie entity);
    Task<IEnumerable<Movie>> ReadAsync();
    Task<Movie> FindAsync(int id);
    Task<bool> UpdateAsync(Movie entity);
    Task<bool> UpdateManyAsync(IEnumerable<Movie> entities);
    Task<bool> ExistsAsync(Movie entity);
    Task<IEnumerable<Movie>> SearchByTitleAsync(string title);
}
public class RepositoryMovie : RepositoryBase<Movie>, IRepositoryMovie
{
    public RepositoryMovie(HallOfFameContext context) : base(context)
    {
    }
    public async Task<IEnumerable<Movie>> SearchByTitleAsync(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            return new List<Movie>();
        
        return await DbContext.Movies
            .Where(m => m.Title.Contains(title))
            .ToListAsync();
    }

}
