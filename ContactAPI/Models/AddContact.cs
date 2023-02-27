using System.ComponentModel.DataAnnotations;

namespace ContactAPI.Models
{
    public class AddContact
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public long Phone { get; set; }
        public string Address { get; set; }
    }
}
