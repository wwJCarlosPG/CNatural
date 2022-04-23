using System.ComponentModel.DataAnnotations;

namespace CNaturalApi.Models
{
    public class Buyer
    {
        [Required]
        public int Id { get; set; }
        [Required,Phone]           
        public string? Mobile { get; set; }
        [MaxLength(50)]
        public string? Name { get; set; }
        [MaxLength(100),Required]
        public string? Address { get; set; }
        public List<Sale>? Sales { get; set; }
        public bool IsDeleted  { get; set; } = false;
    }
}
