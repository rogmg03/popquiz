using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace PAWQuiz.Business.BusinessLogic
{
    public interface IMovieBusiness
    {
        Task<List<Movie>> GetMoviesAsync();
        Task<bool> PurchaseMovieAsync(List<(int movieId, int quantity)> items, User user);
    }
    public class MovieBusiness(IRepositoryMovie repositoryMovie) : IMovieBusiness
    {
        public async Task<List<Movie>> GetMoviesAsync()
        {
            return await repositoryMovie.GetAsync();
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
                var movie = await repositoryMovie.FindByIdAsync(item.movieId);
                if (movie == null)
                {
                    return false;
                }
                if (item.quantity <= 0)
                {
                    return false;
                }
                //restriccion de edad
                if (movie.IsAdult)
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