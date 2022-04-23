using System.ComponentModel.DataAnnotations;
using CNaturalApi.Models;


namespace CNaturalApi.Models
{
    public class Product
    {
        
        [Required]
        public int Id { get; set; }
        [Required,MaxLength(50)]
        public string? Name { get; set; }
        /// <summary>
        /// How much are in stock
        /// </summary>
        [Range(0,double.MaxValue)]
        public int Count { get; set; }
        [MaxLength(100)]
        public string? Design { get; set; }
        /// <summary>
        /// This product's collection of sales
        /// </summary>
        public List<Sale>? Sales { get; set; }    // le cambie el tipo, lo hice una lista.(agregar migracion)
        public List<Investment>? Investments { get; set; }
        public bool IsDeleted { get; set; }
        //tengo que agregar la migracion para que se ponga la columna esta
    }
}
