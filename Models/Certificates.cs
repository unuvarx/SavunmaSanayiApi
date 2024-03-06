using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class Certificates
    {

        [Key]

        public int certificateId { get; set; }


        [Required]

         public string certificateName { get; set; }

        [Required]


        public byte[] ImageData { get; set; }

    }
}
