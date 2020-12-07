using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebjetMovies.Controllers;
using WebjetMovies.Enums;
using WebjetMovies.Models;

namespace WebjetMovies.Services
{
    public class WebjetService
    {
        private const string BASE_ADDRESS = "http://webjetapitest.azurewebsites.net";
        private readonly ILogger _logger;
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly string ACCESS_TOKEN;

        public WebjetService(ILogger<MoviesController> logger, IConfiguration config) : this(config)
        {
            _logger = logger;
            
        }

        public WebjetService(IConfiguration config)
        {
            ACCESS_TOKEN = config.GetSection("WebjetAccessToken").Value;
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(BASE_ADDRESS),
                
                DefaultRequestHeaders =
                {
                    {"x-access-token", ACCESS_TOKEN }
                },
                Timeout = TimeSpan.FromSeconds(2)
            };
            _httpClient.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue
            {
                MaxAge = TimeSpan.FromSeconds(300), // 5 minutes
            };
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
        }
        public async Task<List<Movie>> getCinemaWorldMovies()
        {
            List<Movie> movieList;

            try
            {
                using (var response = await _httpClient.GetAsync("api/cinemaworld/movies"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        CinemaWorld output = JsonSerializer.Deserialize<CinemaWorld>(apiResponse, _jsonOptions);
                        movieList = await getCinemaWorldMoviesDetails(output.movies);
                    }
                    else
                    {
                        movieList = new List<Movie>();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("An error occurred: ", ex.Message);
                return new List<Movie>();
            }
            return movieList;
        }

        public async Task<List<Movie>> getCinemaWorldMoviesDetails(List<Movie> movies)
        {
            List<Movie> output = new List<Movie>();
            try
            {
                if (movies != null && movies.Count > 0)
                {
                    foreach (var movie in movies)
                    {
                        string id = movie.id;
                        if (!string.IsNullOrWhiteSpace(id))
                        {
                            try
                            {
                                using (var response = await _httpClient.GetAsync("api/cinemaworld/movie/" + id))
                                {
                                    if (response.IsSuccessStatusCode)
                                    {
                                        string apiResponse = await response.Content.ReadAsStringAsync();
                                        Movie movieDetails = JsonSerializer.Deserialize<Movie>(apiResponse, _jsonOptions);
                                        movieDetails.provider = ProviderType.cinemaWorld;
                                        output.Add(movieDetails);
                                    }
                                    else { continue; }

                                }
                            }
                            catch (Exception ex)
                            {
                                _logger.LogInformation("An error occurred: ", ex.Message);
                                continue;
                            }
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("An error occurred: ", ex.Message);
                return output;
            }
            return output;
        }

        public async Task<List<Movie>> getFilmWorldMovies()
        {
            List<Movie> movieList;
            try
            {
                using (var response = await _httpClient.GetAsync("api/filmworld/movies"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        FilmWorld output = JsonSerializer.Deserialize<FilmWorld>(apiResponse, _jsonOptions);
                        movieList = await getFilmWorldMoviesDetails(output.movies);
                    }
                    else
                    {
                        movieList = new List<Movie>();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("An error occurred: ", ex.Message);
                return new List<Movie>();
            }
            return movieList;
        }
        public async Task<List<Movie>> getFilmWorldMoviesDetails(List<Movie> movies)
        {
            List<Movie> output = new List<Movie>();
            try
            {
                if (movies != null && movies.Count > 0)
                {
                    foreach (var movie in movies)
                    {
                        string id = movie.id;
                        if (!string.IsNullOrWhiteSpace(id))
                        {
                            try
                            {
                                using (var response = await _httpClient.GetAsync("api/filmworld/movie/" + id))
                                {
                                    if (response.IsSuccessStatusCode)
                                    {
                                        string apiResponse = await response.Content.ReadAsStringAsync();
                                        Movie movieDetails = JsonSerializer.Deserialize<Movie>(apiResponse, _jsonOptions);
                                        movieDetails.provider = ProviderType.filmWorld;
                                        output.Add(movieDetails);
                                    }
                                    else { continue; }

                                }
                            }
                            catch (Exception ex)
                            {
                                _logger.LogInformation("An error occurred: ", ex.Message);
                                continue;
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("An error occurred: ", ex.Message);
                return output;
            }
            return output;
        }

    }
}
