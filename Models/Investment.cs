using CNaturalApi.Models;
using System.ComponentModel.DataAnnotations;

namespace CNaturalApi.Models
{
    public class Investment
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public double Price { get; set; }
        /// <summary>
        /// The date when it started the task(fecha de encargo)
        /// </summary>
        [Required]
        public DateTime TaskDate { get; set; }
        /// <summary>
        /// The date when it arrived the task
        /// </summary>
        [Required]
        public DateTime ArrivalDate { get; set; }
        /// <summary>
        /// The product where it going to investmenting
        /// </summary>
        public Product? Product { get; set; }
        public Accountancy? Accountancy { get; set; }
        public bool IsArrived { get; set; }
    }
}
