using Microsoft.EntityFrameworkCore;

namespace CoL.DB.Entities
{
    [Owned]
    public class BadgeUrls
    {
        public string Small { get; set; }
        public string Large { get; set; }
        public string Medium { get; set; }
    }
}