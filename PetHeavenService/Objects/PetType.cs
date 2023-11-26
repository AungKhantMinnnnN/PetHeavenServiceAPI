using System.ComponentModel.DataAnnotations;

namespace PetHeavenService.Objects
{
    public class PetType
    {
        [Key]
        public Guid PetTypeId { get; set; }
        public string PetTypeName { get; set; } = string.Empty;
    }
}
