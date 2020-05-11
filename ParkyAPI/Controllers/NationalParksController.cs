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
    [Route("api/[controller]")]
    [ApiController]
    public class NationalParksController : Controller
    {
        private readonly INationalParkRepository _nationalParkRepository;
        private readonly IMapper _mapper;

        public NationalParksController(INationalParkRepository nationalParkRepository,IMapper mapper)
        {
            this._nationalParkRepository = nationalParkRepository;
            this._mapper = mapper;
        }
        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var nps = await _nationalParkRepository.GetNationalParksAsync();
            if (nps==null)
            {
                return NotFound();
            }
            return Ok(nps);
        }

        // GET api/<controller>/5
        [HttpGet("{id}",Name = "GetNationalPark")]
        public async Task<IActionResult> Get(int id)
        {
            var nps = await _nationalParkRepository.GetNationalParkByIdAsync(id);
            if (nps == null)
            {
                return NotFound();
            }
            return Ok(nps);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]NationalParkDto nationalParkDto)
        {
            var np = _mapper.Map<NationalPark>(nationalParkDto);
            if (await _nationalParkRepository.CreateNationalParkAsync(np))
                return CreatedAtRoute("GetNationalPark", new { id = np.Id }, np);
            return StatusCode(StatusCodes.Status500InternalServerError, "Something wrong happen");

        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]NationalParkDto nationalParkDto)
        {
            if (!await _nationalParkRepository.ExistNationalParkByIdAsync(id))
            {
                return NotFound();
            }
            var np = await _nationalParkRepository.GetNationalParkByIdAsync(id);
            var newnp = _mapper.Map(nationalParkDto, np);
            if(await _nationalParkRepository.UpdateNationalParkAsync(newnp))
                return CreatedAtRoute("GetNationalPark", new { id = newnp.Id }, newnp);

            return StatusCode(StatusCodes.Status500InternalServerError, "Something wrong happen");
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await _nationalParkRepository.ExistNationalParkByIdAsync(id))
            {
                return NotFound();
            }
            var np = await _nationalParkRepository.GetNationalParkByIdAsync(id);
            if (await _nationalParkRepository.DeleteNationalParkAsync(np))
                return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
        }
    }
}
