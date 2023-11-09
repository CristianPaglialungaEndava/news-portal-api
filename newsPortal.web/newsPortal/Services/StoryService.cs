using newsPortal.Common;
using newsPortal.Dtos;
using newsPortal.Extensions;
using newsPortal.Models;
using newsPortal.Repositories.Interfaces;
using newsPortal.Services.Interfaces;

namespace newsPortal.Services
{
    public class StoryService : IStoryService
    {
        private readonly INewsService _newService;
        public StoryService(INewsService newService)
        {
            _newService = newService;
        }
        public async Task<GetStoriesResponseDto> GetStories(GetStoriesRequestDto options)
        {
            var getStoryIdsTask = options.Criteria.StoryType is null ? GetAllStoriesId() : _newService.GetStoriesIdByType(options.Criteria.StoryType);
            IEnumerable<int> storyIdsCollection = await getStoryIdsTask;

            if (options.Criteria.Title is not null)
            {
                return await getAndfilteredStories(options, storyIdsCollection);
            }
            else
            {
                return await getAllStories(options, storyIdsCollection);
            }
        }
        private async Task<GetStoriesResponseDto> getAndfilteredStories(GetStoriesRequestDto options,IEnumerable<int> StoryIds)
        {
            var getStoryTasks = StoryIds.Select(_newService.Find);
            var storiesCollection = await Task.WhenAll(getStoryTasks);
            var filteredCollection = storiesCollection.Where(story => story.Title.ToLower().Contains(options.Criteria.Title.ToLower()));
            var paginatedfilteredCollection = filteredCollection.GetPaginatedItems(options.Page, options.PageSize);
            return new GetStoriesResponseDto()
            {
                countTotal = filteredCollection.Count(),
                PrevPage = options.Page == 1 ? null : options.Page - 1,
                NextPage = filteredCollection.IsLastPage(options.Page, options.PageSize) ? null : options.Page + 1,
                Results = paginatedfilteredCollection
            };
        }
        
        private async Task<GetStoriesResponseDto> getAllStories(GetStoriesRequestDto options, IEnumerable<int> StoryIds)
        {
            var paginatedStoryIdCollection = StoryIds.GetPaginatedItems(options.Page, options.PageSize);
            var getStoryTasks = paginatedStoryIdCollection.Select(_newService.Find);
            var storiesCollection = await Task.WhenAll(getStoryTasks);
            return new GetStoriesResponseDto()
            {
                countTotal = StoryIds.Count(),
                PrevPage = options.Page == 1 ? null : options.Page - 1,
                NextPage = StoryIds.IsLastPage(options.Page, options.PageSize) ? null : options.Page + 1,
                Results = storiesCollection
            };
        }

        public async Task<IEnumerable<int>> GetAllStoriesId()
        {
            string[] allStoryTypes = { Constants.NewStory, Constants.TopStory, Constants.BestStory };
            var getStoriesTasks = allStoryTypes.Select(_newService.GetStoriesIdByType);
            IEnumerable<IEnumerable<int>> response = await Task.WhenAll(getStoriesTasks);
            IEnumerable<int> storyIdList = response.SelectMany(id => id);
            return storyIdList;
        }
    }
}
