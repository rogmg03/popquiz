using PopQuiz.Data.Models;
using PopQuiz.Data.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopQuiz.Core.Business
{
    public interface IDirectorBusiness
    {
        /// <summary>
        /// Deletes the director associated with the director id.
        /// </summary>
        /// <param name="id">The director id.</param>
        /// <returns>True if deletion was successful, false otherwise.</returns>

        Task<bool> DeleteDirectorAsync(int id);
        /// <summary>
        /// Gets directors. If id is provided, returns only that director; otherwise returns all directors.
        /// </summary>
        /// <param name="id">Optional director id.</param>
        /// <returns>A collection of directors.</returns>
        Task<IEnumerable<Director>> GetDirectors(int? id);

        /// <summary>
        /// Saves a director (creates or updates).
        /// </summary>
        /// <param name="director">The director to save.</param>
        /// <returns>True if save was successful, false otherwise.</returns>
        Task<bool> SaveDirectorAsync(Director director);
    }

    public class DirectorBusiness(IRepositoryDirector repositoryDirector) : IDirectorBusiness
    {
        /// <inheritdoc />
        public async Task<bool> SaveDirectorAsync(Director director)
        {
            return await repositoryDirector.UpdateAsync(director);
        }
        /// <inheritdoc />
        public async Task<bool> DeleteDirectorAsync(int id)
        {
            var director = await repositoryDirector.FindAsync(id);
            if (director == null) return false;
            return await repositoryDirector.DeleteAsync(director);
        }
        /// <inheritdoc />
        public async Task<IEnumerable<Director>> GetDirectors(int? id)
        {
            return id == null
                ? await repositoryDirector.ReadAsync()
                : [await repositoryDirector.FindAsync((int)id)];
        }
    }
}


