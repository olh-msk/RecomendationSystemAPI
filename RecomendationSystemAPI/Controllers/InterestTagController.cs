using Microsoft.AspNetCore.Mvc;
using RecomendatinSystemAPI.Models;
using RecomendationSystemAPI.DTOs.InterestTags;
using RecomendationSystemAPI.Helpers;
using RecomendationSystemAPI.Services.Interfaces;

namespace RecomendationSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InterestTagController : ControllerBase
    {
        private readonly IInterestTagService _service;

        public InterestTagController(IInterestTagService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InterestTagDto>>> GetAll()
        {
            var tags = await _service.GetAllAsync();
            return Ok(tags.Select(DtoMapper.ToDto));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] string name)
        {
            await _service.AddTagAsync(new InterestTag { Name = name });
            return Ok();
        }
    }
}
