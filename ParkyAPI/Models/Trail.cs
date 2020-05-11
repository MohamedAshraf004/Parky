using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkyAPI.Models
{
    public class Trail
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Distance { get; set; }
        public DiffiicultyType Diffiiculty { get; set; }
        [ForeignKey("NationalPark"), Required]
        public int NationalParkId { get; set; }
        public NationalPark NationalPark { get; set; }

    }
}
