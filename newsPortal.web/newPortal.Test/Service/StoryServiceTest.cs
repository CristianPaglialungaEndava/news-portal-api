
using Moq;
using newsPortal.Dtos;
using newsPortal.Services;
using newsPortal.Models;
using newsPortal.Repositories.Interfaces;

namespace newPortal.Test.Services
{
    [TestFixture]
    public class StoryServiceTest
    {
        [Test]
        public async Task GetStories_WithTitle_ReturnsFilteredStories()
        {
            // Arrange
            var newsServiceMock = new Mock<INewsService>();

            var storyIds = new List<int> { 1, 2, 3 };
            newsServiceMock.Setup(x => x.GetStoriesIdByType(It.IsAny<string>())).ReturnsAsync(storyIds);
            var story1 = new Story { Title = "TestTitle1" };
            var story2 = new Story { Title = "TestTitle2" };
            var story3 = new Story { Title = "OtherTitle" };
            newsServiceMock.Setup(x => x.Find(It.IsAny<int>())).ReturnsAsync((int id) =>
            {
                switch (id)
                {
                    case 1: return story1;
                    case 2: return story2;
                    case 3: return story3;
                    default: return null;
                }
            });

            var storyService = new StoryService(newsServiceMock.Object);

            var options = new GetStoriesRequestDto
            {
                Criteria = new SearchCriteria
                {
                    Title = "TestTitle"
                },
                Page = 1,
                PageSize = 10
            };


            // Act
            var result = await storyService.GetStories(options);

            // Assert
            Assert.AreEqual(2, result.countTotal);
            Assert.AreEqual(2, result.Results.Count());
            Assert.AreEqual("TestTitle1", result.Results.First().Title);
        }

        [Test]
        public async Task GetStories_WithoutTitle_ReturnsAllStories()
        {
            // Arrange
            var newsServiceMock = new Mock<INewsService>();
            var storyService = new StoryService(newsServiceMock.Object);

            var options = new GetStoriesRequestDto
            {
                Criteria = new SearchCriteria
                {
                    Title = null
                },
                Page = 1,
                PageSize = 10
            };

            var storyIds = new List<int> { 1, 2, 3 };
            newsServiceMock.Setup(x => x.GetStoriesIdByType(It.IsAny<string>())).ReturnsAsync(storyIds);

            var story1 = new Story { Title = "TestTitle1" };
            var story2 = new Story { Title = "TestTitle2" };
            var story3 = new Story { Title = "OtherTitle" };


            newsServiceMock.Setup(x => x.Find(It.IsAny<int>())).ReturnsAsync((int id) =>
            {
                return new Story { Title = $"TestTitle{id}" };
            });
            // Act
            var result = await storyService.GetStories(options);

            // Assert
            Assert.AreEqual(3, result.countTotal);
            Assert.AreEqual(3, result.Results.Count());
        }

        [Test]
        public async Task GetStories_ReturnsPaginatedStories()
        {
            // Arrange
            var newsServiceMock = new Mock<INewsService>();
            var storyService = new StoryService(newsServiceMock.Object);

            var options = new GetStoriesRequestDto
            {
                Criteria = new SearchCriteria
                {
                    Title = null
                },
                Page = 1,
                PageSize = 5
            };

            var storyIds = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            newsServiceMock.Setup(x => x.GetStoriesIdByType(It.IsAny<string>())).ReturnsAsync(storyIds);

            newsServiceMock.Setup(x => x.Find(It.IsAny<int>())).ReturnsAsync((int id) =>
            {
                return new Story { Title = $"TestTitle{id}" };
            });

            // Act
            var result = await storyService.GetStories(options);

            // Assert
            Assert.AreEqual(10, result.countTotal);
            Assert.AreEqual(5, result.Results.Count());
            Assert.AreEqual(2, result.NextPage);
            Assert.IsNull(result.PrevPage);
        }
    }
}

