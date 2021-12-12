using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

namespace CoL.DB.Entities
{
    [Owned]
    public class BadgeUrls
    {
        [Required]
        public string Small { get; set; }

        [Required]
        public string Large { get; set; }

        [Required]
        public string Medium { get; set; }
    }
}