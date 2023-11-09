using newsPortal.Dtos;
using newsPortal.Models;
using newsPortal.Repositories.Interfaces;
using newsPortal.Services.Interfaces;

namespace newsPortal.Services
{
    public class StoryService : IStoryService
    {
        private readonly INewsService _storyRepository;
        public StoryService(INewsService storyRepository)
        {
            _storyRepository = storyRepository;
        }
        public async Task<GetStoriesResponseDto> getStories(int pageNumber, int pageSize)
        {
            var storyIdsCollection = await _storyRepository.GetNewStoriesId();
            var paginatedStoryIdCollection = storyIdsCollection.Skip(getStartIndexByPage(pageNumber, pageSize, storyIdsCollection.Count())).Take(getItemsToTakeByPage(pageNumber, pageSize, storyIdsCollection.Count()));
            var getStoryTasks = paginatedStoryIdCollection.Select(storyId => _storyRepository.Find(storyId));
            var storiesCollection = await Task.WhenAll(getStoryTasks);
            return new GetStoriesResponseDto()
            {
                countTotal = storyIdsCollection.Count(),
                PrevPage = pageNumber == 1 ? 1: pageNumber -1,
                NextPage = isLastPage(pageNumber, pageSize, storyIdsCollection.Count())? pageNumber : pageNumber + 1,
                Results = storiesCollection
            };
        }

        private int getStartIndexByPage(int pageNumber, int pageSize,int totalItems)
        {
            var startIndex = (pageNumber - 1) * pageSize - 1;
            if (startIndex > totalItems -1)
            {
                throw new ArgumentException("PageNumber is out of list range");
            }
            return startIndex;

        }
        private int getItemsToTakeByPage(int pageNumber, int pageSize, int totalItems)
        {
            var startIndex = getStartIndexByPage(pageNumber, pageSize, totalItems);
            var itemsToTake = pageSize;
            if ((startIndex + itemsToTake) > totalItems)
            {
                itemsToTake = totalItems - startIndex;
            }
            return itemsToTake;
        }

        private bool isLastPage(int pageNumber, int pageSize, int totalItems)
        {
            var startIndex = getStartIndexByPage(pageNumber, pageSize, totalItems);
            return (startIndex + pageSize) > totalItems;
        }
    }
}
