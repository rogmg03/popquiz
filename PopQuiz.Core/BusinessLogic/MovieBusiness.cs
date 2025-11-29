using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PopQuiz.Data.Models;
using PopQuiz.Data.Repositories;

namespace PopQuiz.Core.BusinessLogic
{
    public interface IMovieBusiness
    {
        Task<IEnumerable<Movie>> GetMoviesAsync(int? id);
        Task<bool> PurchaseMovieAsync(List<(int movieId, int quantity)> items, User user);
    }
    public class MovieBusiness(IRepositoryMovie repositoryMovie) : IMovieBusiness
    {
        public async Task<IEnumerable<Movie>> GetMoviesAsync(int? id)
        {

            return id == null
            ? await repositoryMovie.ReadAsync()
            : [await repositoryMovie.FindAsync((int)id)];

        }
        public async Task<bool> PurchaseMovieAsync(List<(int movieId, int quantity)> items, User user)
        {
            if (items == null || items.Count == 0)
            {
                throw new ArgumentException("No items to purchase");
            }
            // validar si existen las peliculas y cantidades validas
            foreach (var item in items)
            {
                var movie = await repositoryMovie.FindAsync(item.movieId);
                if (movie == null)
                {
                    return false;
                }
                if (item.quantity <= 0)
                {
                    return false;
                }
                //restriccion de edad
                if ((bool)movie.IsAdult)
                {
                    int age = CalculateAge(user.DateBirth);
                    if (age < 18)
                    {
                        return false;
                    }
                }
            }
            //limite de compra
            if (items.Count > 10)
            {
                return false;
            }
            return true;
        }

        //metodo para calcular la edad
        private int CalculateAge(DateTime birthdate)
        {
            var today = DateTime.Today;
            int age = today.Year - birthdate.Year;

            if (birthdate.Date > today.AddYears(-age))
                age--;

            return age;
        }
    }
}
