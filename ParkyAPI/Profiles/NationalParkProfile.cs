using AutoMapper;
using ParkyAPI.Dtos;
using ParkyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkyAPI.Profiles
{
    public class NationalParkProfile:Profile
    {
        public NationalParkProfile()
        {
            this.CreateMap<NationalPark, NationalParkDto>().ReverseMap();
            this.CreateMap<Trail, TrailDto>().ReverseMap();
            this.CreateMap<Trail, TrailUpsertDto>().ReverseMap();
        }
    }
}
