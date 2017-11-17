using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMovieLogin.Models
{
    public class MovieReviewModel
    {
        public MovieGenreViewModel MGenre { get; set; }
        public MovieReview MReview { get; set; }
    }
}
