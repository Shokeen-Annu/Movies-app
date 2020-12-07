using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebjetMovies.Models;
using WebjetMovies.Services;

namespace WebjetMovies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ILogger<MoviesController> _logger;
        private readonly WebjetService webjetService;
        private readonly IConfiguration _config;
        public MoviesController(ILogger<MoviesController> logger, IConfiguration config)
        {
            _logger = logger;
            webjetService = new WebjetService(logger, config);
            _config = config;
        }
        
        [HttpGet]
        public async Task<List<Movie>> Get()
        {
            List<Movie> finalMovies = await prepareList();
            return finalMovies;
        }
        private async Task<List<Movie>> prepareList()
        {
            List<Movie> cinemaWorldMovies = await webjetService.getCinemaWorldMovies();
            List<Movie> filmWorldMovies = await webjetService.getFilmWorldMovies();

            IEnumerable<Movie> allMovies = cinemaWorldMovies.Concat(filmWorldMovies);
            var distinctMovies = allMovies.Select(m => m.title).Distinct();
            List<Movie> finalList = new List<Movie>();

            try
            {
                //Select movie with minimum price
                foreach (var movie in distinctMovies)
                {
                    List<Movie> selectedMovieDetails = allMovies.Where(m => m.title == movie).Select(m => m).ToList();
                    if (selectedMovieDetails.Count > 1)
                    {
                        decimal minPrice = decimal.MaxValue;
                        Movie minPriceMovie = null;
                        foreach (var item in selectedMovieDetails)
                        {
                            decimal itemPrice = Convert.ToDecimal(item.price);
                            if (itemPrice < minPrice)
                            {
                                minPrice = itemPrice;
                                minPriceMovie = item;
                            }
                        }

                        if (minPriceMovie != null)
                        {
                            finalList.Add(minPriceMovie);
                        }

                    }
                    else if (selectedMovieDetails.Count == 1)
                    {
                        finalList.Add(selectedMovieDetails[0]);
                    }
                }
            }
            catch(Exception ex)
            {
                _logger.LogInformation("An error occurred: ", ex);
                return new List<Movie>();
            }

            return finalList;
        }
    }
}
