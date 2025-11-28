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
        Task<bool> GetUserWithCredentials(string email, string password);

    }

    public class UserBusiness(IRepositoryUser repositoryUser) : IUserBusiness
    {
        public async Task<IEnumerable<User>> GetUsers(int? id)
        {
            return id == null
                ? await repositoryUser.ReadAsync()
                : [await repositoryUser.FindAsync((int)id)];
        }
        public async Task<bool> GetUserWithCredentials(string email, string password)
        {
            var users = await repositoryUser.ReadAsync();
            var user = users.FirstOrDefault(u => u.Email == email && u.Password == password);
            return user != null;
        }
    }
}
