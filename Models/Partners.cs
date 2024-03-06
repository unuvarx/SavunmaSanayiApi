using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class Partners
    {


        [Key]

        public int partnerId { get; set; }


        [Required]

        public byte[] ImageData { get; set; }

    }
}
