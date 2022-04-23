using CNaturalApi.Models;
using System.ComponentModel.DataAnnotations;

namespace CNaturalApi.Models
{

    public class Sale
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int Count { get; set; }
        /// <summary>
        /// I don't think I must edit the date.
        /// </summary>
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public double Price { get; set; }
        /// <summary>
        /// The product that will be sell in this sale. 
        /// </summary>
        [Required]
        public Product Product { get; set; }
        public Accountancy? Accountancy { get; set; }
        public Buyer Buyer { get; set; }

    }
}
