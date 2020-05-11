using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkyAPI.Dtos
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class NationalParkDto
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Name { get; set; }
        [Required]
        public string State { get; set; }
        public byte[] Picture { get; set; }
        public DateTime Created { get; set; }
        public DateTime Established { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

}
