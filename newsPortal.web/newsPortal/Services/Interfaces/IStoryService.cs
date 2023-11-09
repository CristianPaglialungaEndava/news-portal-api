using newsPortal.Dtos;
using newsPortal.Models;

namespace newsPortal.Services.Interfaces
{
    public interface IStoryService
    {
        public Task<GetStoriesResponseDto> GetStories(GetStoriesRequestDto options);

    }
}
