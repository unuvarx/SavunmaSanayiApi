using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class Communication
    {
        [Key]
        public int communicationId { get; set; }

        [Required]

        public string adress { get; set; }

        [Required]
        public string title { get; set; }

        [Required]

        public string phone { get; set; }

        [Required]

        public string email { get; set; }


        [Required]

        public string activeTimes { get; set; }


        [Required]


        public string coordinateLant { get; set; }

        [Required]

        public string coordinateLng { get; set; }
    }
}
