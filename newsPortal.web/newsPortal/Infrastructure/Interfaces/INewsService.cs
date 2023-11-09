using newsPortal.Models;
using System;

namespace newsPortal.Repositories.Interfaces
{
    public interface INewsService
    {
        public Task<Story?> Find(int id);
        public Task<IEnumerable<int>?> GetNewStoriesId();
        public Task<IEnumerable<int>?> GetTopStoriesId();
    }
}
