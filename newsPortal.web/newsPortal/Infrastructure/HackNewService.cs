using newsPortal.Models;
using newsPortal.Repositories.Interfaces;

namespace newsPortal.Repositories
{
    public class HackNewService : INewsService
    {
        private readonly IHttpClientFactory _factory;
        public HackNewService(IHttpClientFactory factory)
        {
            _factory = factory;
        }
        public async Task<Story?> Find(int id)
        {
            var httpClient = _factory.CreateClient("HackNews");
            Story response = await httpClient.GetFromJsonAsync<Story>($"item/{id}.json?print=pretty");
            return response;
        }

        public async Task<IEnumerable<int>?> GetNewStoriesId()
        {
            var httpClient = _factory.CreateClient("HackNews");
            var response = await httpClient.GetFromJsonAsync<List<int>>($"newstories.json?print=pretty");
            return response;
        }

        public async Task<IEnumerable<int>?> GetTopStoriesId()
        {
            var httpClient = _factory.CreateClient("HackNews");
            var response = await httpClient.GetFromJsonAsync<List<int>>($"topstories.json?print=pretty");
            return response;
        }
    }
}
