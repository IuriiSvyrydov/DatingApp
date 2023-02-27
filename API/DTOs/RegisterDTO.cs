using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.DTOs
{
    public class RegisterDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required] public string KnownAS { get; set; }
        [Required] public string Gender { get; set; }
        [JsonIgnore]
        public DateOnly? DateOfBirth { get; set; }
        [Required] public string City { get; set; }
        [Required] public string Country { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
