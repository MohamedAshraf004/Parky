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
    //[ApiExplorerSettings(GroupName = "ParkOpenApiSpecTrails")]
    [Route("api/v{version:apiVersion}/trails")]
    [ApiController]
    public class TrailsController : Controller
    {
        private readonly ITrailRepository _trailRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// National Park Constructer
        /// </summary>
        /// <param name="trailRepository"></param>
        /// <param name="mapper"></param>
        public TrailsController(ITrailRepository trailRepository, IMapper mapper)
        {
            this._trailRepository = trailRepository;
            this._mapper = mapper;
        }
        /// <summary>
        /// get list of NationalPark
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type =typeof(IEnumerable<Trail>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Get()
        {
            var trails = await _trailRepository.GetTrailsAsync();
            if (trails==null)
            {
                return NotFound();
            }
            return Ok(trails);
        }

        /// <summary>
        /// get Trail by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}",Name = "GetTrail")]
        public async Task<IActionResult> Get(int id)
        {
            var trail = await _trailRepository.GetTrailByIdAsync(id);
            if (trail == null)
            {
                return NotFound();
            }
            return Ok(trail);
        }
        [HttpGet("GetTrailsInNationalPark/{id}")]
        public async Task<IActionResult> GetTrailsInNationalPark(int id)
        {
            var trails = await _trailRepository.GetTrailsByNationalParkIdAsync(id);
            var result = _mapper.Map<IEnumerable<TrailDto>>(trails);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        
        // POST api/<controller>
        [ProducesDefaultResponseType]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TrailUpsertDto trailUpsertDto)
        {
            var trail = _mapper.Map<Trail>(trailUpsertDto);
            if (await _trailRepository.CreateTrailAsync(trail))
                return CreatedAtRoute("GetTrail", new { version = HttpContext.GetRequestedApiVersion().ToString(), id = trail.Id }, trail);
            return StatusCode(StatusCodes.Status500InternalServerError, "Something wrong happen");

        }
/// <summary>
/// 
/// </summary>
/// <param name="id"></param>
/// <param name="trailUpsertDto"></param>
/// <returns></returns>
        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]TrailUpsertDto trailUpsertDto)
        {
            if (!await _trailRepository.ExistTrailByIdAsync(id))
            {
                return NotFound();
            }
            var trail = await _trailRepository.GetTrailByIdAsync(id);
            var newTrail = _mapper.Map(trailUpsertDto, trail);
            if(await _trailRepository.UpdateTrailAsync(newTrail))
                return NoContent();
            //return CreatedAtRoute("GetTrail", new { version = HttpContext.GetRequestedApiVersion().ToString(), id = newTrail.Id }, newTrail);
            return StatusCode(StatusCodes.Status500InternalServerError, "Something wrong happen");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await _trailRepository.ExistTrailByIdAsync(id))
            {
                return NotFound();
            }
            var trail = await _trailRepository.GetTrailByIdAsync(id);
            if (await _trailRepository.DeleteTrailAsync(trail))
                return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
        }
    }
}
