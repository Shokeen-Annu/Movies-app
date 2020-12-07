using System;
using Xunit;
using WebjetMovies.Services;
using WebjetMovies.Models;
using System.Collections.Generic;

namespace WebjetService.Tests
{
    public class WebjetServiceTest
    {
        [Fact]
        public async void cinemaWorldShouldReturnEmptyListWhenNoMoviesPassed()
        {
            var webjetService = new WebjetMovies.Services.WebjetService();
            var mockMovies = new List<Movie>();
            List<Movie> result = await webjetService.getCinemaWorldMoviesDetails(mockMovies);
            Assert.False(result.Count != 0, "Cinema world movie details are wrongly fetched when no movies are provided as input");
        }

        [Fact]
        public async void cinemaWorldShouldReturnMovieDetailsWhenMoviesAreAvailable()
        {
            var webjetService = new WebjetMovies.Services.WebjetService();
            var mockMovies = new List<Movie>();
            mockMovies.Add(new Movie
            {
                title= "Star Wars: Episode IV - A New Hope",
                year= "1977",
                id= "cw0076759",
                type= "movie"
            });
            mockMovies.Add(new Movie
            {
                title = "Star Wars: Episode VI - Return of the Jedi",
                year = "1983",
                id = "cw0086190",
                type = "movie"
            });
            List<Movie> result = await webjetService.getCinemaWorldMoviesDetails(mockMovies);
            Assert.True(result.Count == 2, "Cinema world movie details are not available when movies are provided as input");
            Assert.False(result[0].id != "cw0076759", "Details of \"Star Wars: Episode IV - A New Hope\" from cinema world are not fetched successfully");
            Assert.False(result[1].id != "cw0086190", "Details of \"Star Wars: Episode VI - Return of the Jedi\" from cinema world are not fetched successfully");
        }

        [Fact]
        public async void filmWorldShouldReturnEmptyListWhenNoMoviesPassed()
        {
            var webjetService = new WebjetMovies.Services.WebjetService();
            var mockMovies = new List<Movie>();
            List<Movie> result = await webjetService.getFilmWorldMoviesDetails(mockMovies);
            Assert.False(result.Count != 0, "Film world movie details are wrongly fetched when no movies are provided as input");
        }

        [Fact]
        public async void filmWorldShouldReturnMovieDetailsWhenMoviesAreAvailable()
        {
            var webjetService = new WebjetMovies.Services.WebjetService();
            var mockMovies = new List<Movie>();
            mockMovies.Add(new Movie
            {
                title = "Star Wars: Episode IV - A New Hope",
                year = "1977",
                id = "fw0076759",
                type = "movie"
            });
            mockMovies.Add(new Movie
            {
                title = "Star Wars: Episode VI - Return of the Jedi",
                year = "1983",
                id = "fw0086190",
                type = "movie"
            });
            List<Movie> result = await webjetService.getFilmWorldMoviesDetails(mockMovies);
            Assert.True(result.Count == 2, "Film world movie details are not available when movies are provided as input");
            Assert.False(result[0].id != "fw0076759", "Details of \"Star Wars: Episode IV - A New Hope\" from film world are not fetched successfully");
            Assert.False(result[1].id != "fw0086190", "Details of \"Star Wars: Episode VI - Return of the Jedi\" from film world are not fetched successfully");
        }
    }
}
