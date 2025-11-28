using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PopQuiz.Data.Models;
using PopQuiz.Data.Repositories;

namespace PopQuiz.Core.BusinessLogic
{
    public interface IUserBusiness
    {
        Task<IEnumerable<User>> GetUsers(int? id);

        Task<bool> SaveUserAsync(User user);

        Task<bool> DeleteUserAsync(int id);

    }

    public class UserBusiness(IRepositoryUser repositoryUser) : IUserBusiness
    {
        public async Task<IEnumerable<User>> GetUsers(int? id)
        {
            return id == null
                ? await repositoryUser.ReadAsync()
                : [await repositoryUser.FindAsync((int)id)];
        }

        /// <inheritdoc />
        public async Task<bool> SaveUserAsync(User user)
        {
            return await repositoryUser.UpdateAsync(user);
        }

        /// <inheritdoc />
        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await repositoryUser.FindAsync(id);
            if (user == null) return false;
            return await repositoryUser.DeleteAsync(user);
        }

        
    }
}
