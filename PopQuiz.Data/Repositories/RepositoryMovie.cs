using Microsoft.EntityFrameworkCore;

using PopQuiz.Data.Models;

namespace PopQuiz.Data.Repositories;

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
    /// <summary>
    /// Search movies by title.
    /// </summary>
    /// <param name="title"></param>
    /// <returns></returns>
    Task<IEnumerable<Movie>> SearchByTitleAsync(string title);
    /// <summary>
    /// Search movies by title including related Genre and Director details.
    /// </summary>
    /// <param name="title"></param>
    /// <returns></returns>
    Task<IEnumerable<Movie>> SearchByTitleWithDetailsAsync(string title);


}
public class RepositoryMovie : RepositoryBase<Movie>, IRepositoryMovie
{
    public RepositoryMovie(HallOfFameContext context) : base(context)
    {
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Movie>> SearchByTitleAsync(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            return new List<Movie>();
        
        return await DbContext.Movies
            .Where(m => m.Title.Contains(title))
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Movie>> SearchByTitleWithDetailsAsync(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            return new List<Movie>();

        return await DbContext.Movies
            .Include(m => m.Genre)
            .Include(m => m.Director)
            .Where(m => m.Title.Contains(title))
            .ToListAsync();
    }

    


}
