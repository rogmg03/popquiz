using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PopQuiz.Data.Models;
using PopQuiz.Data.Repositories;

namespace PopQuiz.Core.BusinessLogic
{
    public interface IGenreBusiness
    {
        /// <summary>
        /// Deletes the genre associated with the genre id.
        /// </summary>
        /// <param name="id">The genre id.</param>
        /// <returns>True if deletion was successful, false otherwise.</returns>
        Task<bool> DeleteGenreAsync(int id);
        /// <summary>
        /// Gets genres. If id is provided, returns only that genre; otherwise returns all genres.
        /// </summary>
        /// <param name="id">Optional genre id.</param>
        /// <returns>A collection of genres.</returns>
        Task<IEnumerable<Genre>> GetGenres(int? id);
        /// <summary>
        /// Saves a genre (creates or updates).
        /// </summary>
        /// <param name="genre">The genre to save.</param>
        /// <returns>True if save was successful, false otherwise.</returns>
        Task<bool> SaveGenreAsync(Genre genre);
    }

    public class GenreBusiness(IRepositoryGenre repositoryGenre) : IGenreBusiness
    {
        /// <inheritdoc />
        public async Task<bool> SaveGenreAsync(Genre genre)
        {
            return await repositoryGenre.UpdateAsync(genre);
        }

        /// <inheritdoc />

        public async Task<bool> DeleteGenreAsync(int id)

        {
            var genre = await repositoryGenre.FindAsync(id);
            if (genre == null) return false;
            return await repositoryGenre.DeleteAsync(genre);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Genre>> GetGenres(int? id)
        {
            return id == null
                ? await repositoryGenre.ReadAsync()
                : [await repositoryGenre.FindAsync((int)id)];
        }
    }
}
