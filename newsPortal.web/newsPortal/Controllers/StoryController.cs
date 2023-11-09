using Microsoft.AspNetCore.Http;
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
        [HttpGet(Name = "Story")]
        public async Task<GetStoriesResponseDto> Get(int page = 1)
        {
            var response = await _storyService.getStories(page, 5);
            return response;
        }
    }
}
