using System.ComponentModel.DataAnnotations;

namespace AdminCheckWeb.Models
{
    public class Login
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? username { get; set; }

        [Required]
        public string? password { get; set; }

        [Required]
        public string? UserType { get; set; }

        public List<UserDetails>?  Details { get; set; }
        
    }
}
