using Microsoft.Extensions.Caching.Memory;
using newsPortal.Models;
using newsPortal.Repositories;
using newsPortal.Repositories.Interfaces;

namespace newsPortal.Infrastructure
{
    public class CachingHackNewService : INewsService
    {
        private readonly HackNewService _newsService;
        private readonly IMemoryCache _cache;
        public CachingHackNewService(HackNewService newsService, IMemoryCache cache)
        {
            _newsService = newsService; 
            _cache = cache; 
        }
        public Task<Story> Find(int id)
        {
            string key = $"story-{id}";

            return _cache.GetOrCreateAsync(
            key,
            entry => {
                entry.SetAbsoluteExpiration(
                    TimeSpan.FromMinutes(5));

                return _newsService.Find(id);
            });
        }

        public Task<IEnumerable<int>> GetStoriesIdByType(string storyType)
        {
            string key = $"{storyType}story-Ids";

            return _cache.GetOrCreateAsync(
            key,
            entry => {
                entry.SetAbsoluteExpiration(
                    TimeSpan.FromMinutes(5));

                return _newsService.GetStoriesIdByType(storyType);
            });
        }
    }
}
