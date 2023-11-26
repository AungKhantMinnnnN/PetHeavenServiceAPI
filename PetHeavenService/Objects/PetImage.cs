using System.ComponentModel.DataAnnotations;

namespace PetHeavenService.Objects
{
    public class PetImage
    {
        [Key]
        public Guid PetImageId { get; set; }
        public string PetImageBase64 { get; set; } = string.Empty;
    }
}
