using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WebjetMovies.Enums;

namespace WebjetMovies.Models
{
    public class Movie
    {
        [Required]
        public string title { get; set; }
        [Required]
        public string id { get; set; }
        public string year { get; set; }
        public string type { get; set; }
        public string rating { get; set; }
        public string price { get; set; }
        public ProviderType provider { get; set; }
    }
}
