using ParkyAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkyAPI.Dtos
{
    public class TrailDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public double Distance { get; set; }
        public DiffiicultyType Diffiiculty { get; set; }

        [ForeignKey("NationalPark"), Required]
        public int NationalParkId { get; set; }
        public NationalParkDto NationalPark { get; set; }
    }
}
