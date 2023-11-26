using System.ComponentModel.DataAnnotations;

namespace PetHeavenService.Objects
{
	public class User
	{
		[Key]
		public Guid UserId { get; set; }
		[Required]
		public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string mobileNumber { get; set; }
        public Guid PetId { get; set; } = Guid.Empty;
        public Pet? Pet { get; set; } = null;
    }
}

