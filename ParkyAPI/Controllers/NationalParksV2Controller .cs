using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkyAPI.Dtos;
using ParkyAPI.Models;
using ParkyAPI.Repositories.IRepositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ParkyAPI.Controllers
{
    /// <summary>
    /// National Park Controller
    /// </summary>
    //[Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/nationalparks")]
    [ApiVersion("2.0")]
    [ApiController]
    //[ApiExplorerSettings(GroupName = "ParkOpenApiSpecNP")]
    public class NationalParksV2Controller : Controller
    {
        private readonly INationalParkRepository _nationalParkRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// National Park Constructer
        /// </summary>
        /// <param name="nationalParkRepository"></param>
        /// <param name="mapper"></param>
        public NationalParksV2Controller(INationalParkRepository nationalParkRepository,IMapper mapper)
        {
            this._nationalParkRepository = nationalParkRepository;
            this._mapper = mapper;
        }
        /// <summary>
        /// get list of NationalPark
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type =typeof(IEnumerable<NationalPark>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Get()
        {
            var nps = await _nationalParkRepository.GetNationalParksAsync();
            if (nps==null)
            {
                return NotFound();
            }
            return Ok(nps.FirstOrDefault());
        }
           
    }
}
