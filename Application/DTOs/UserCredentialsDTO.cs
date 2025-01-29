using System.ComponentModel.DataAnnotations;

namespace Consorcio_Api.Application.DTOs
{
    public class UserCredentialsDTO
    {
        [EmailAddress]
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}
