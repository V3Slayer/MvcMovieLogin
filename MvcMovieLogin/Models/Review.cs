using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovieLogin.Models
{
    public class Review
    {

        //DatabaseGeneratedAttribute.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        public int ID { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Name { get; set; }

        [StringLength(100000, MinimumLength = 10)]
        [Required]
        public string MovieReview { get; set; }

        public int MovieID { get; set; }
    }
}
