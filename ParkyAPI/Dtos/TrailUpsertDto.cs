using ParkyAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkyAPI.Dtos
{
    public class TrailUpsertDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public double Distance { get; set; }
        public DiffiicultyType Diffiiculty { get; set; }
        [Required]
        public int NationalParkId { get; set; }
    }
}
