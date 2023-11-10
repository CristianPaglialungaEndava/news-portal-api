using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using newsPortal.Dtos;
using newsPortal.Services.Interfaces;

namespace newsPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoryController : ControllerBase
    {
        private readonly IStoryService _storyService;
        public StoryController(IStoryService storyService)
        {
            _storyService = storyService;
        }
        [HttpPost(Name = "story")]
        public async Task<ActionResult<GetStoriesResponseDto>> GetStories([FromBody] GetStoriesRequestDto requestParams)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _storyService.GetStories(requestParams);
            return Ok(response);
        }
    }
}
