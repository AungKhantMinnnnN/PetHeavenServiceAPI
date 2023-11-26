using System.ComponentModel.DataAnnotations;

namespace PetHeavenService.Objects
{
    public class Pet
    {
        [Key]
        public Guid PetId { get; set; }
        [Required]
        public string PetName { get; set; }
        public Guid PetTypeId { get; set; }
        public virtual PetType? PetType { get; set; } = null;
        public string PetDescription { get; set; } = string.Empty;
        public Guid PetImageId { get; set; } = Guid.Empty;
        public virtual PetImage? PetImage { get; set; } = null;
        public bool isAvailableForAdoption { get; set; } 
    }
}
