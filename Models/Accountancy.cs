using System.ComponentModel.DataAnnotations;

namespace CNaturalApi.Models
{
    public class Accountancy
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        /// <summary>
        /// this is together the Investment class, when it make a investment then grow up this propierty.
        /// </summary>
        [Required,Range(0,double.MaxValue)]
        public double InvestedMoney { get; set; }
        /// <summary>
        /// I supose this is together sales.
        /// </summary>
        public double EarnedMoney { get; set; }
        public List<Investment>? Investments { get; set; }
        public List<Sale>? Sales { get; set; }
    }
    //Esto es algo que se resetea al mes y no es algo que cambie directamente el cliente
}
