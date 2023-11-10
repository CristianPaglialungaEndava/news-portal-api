using Microsoft.AspNetCore.Mvc;
using Moq;
using newsPortal.Common;
using newsPortal.Controllers;
using newsPortal.Dtos;
using newsPortal.Models;
using newsPortal.Services.Interfaces;

namespace newPortal.Test.Controllers
{
    [TestFixture]
    public class StoryControllerTests
    {
        [Test]
        public async Task Get_ReturnsOkObjectResult()
        {
            // Arrange
            var storyServiceMock = new Mock<IStoryService>();
            var mStoryResponse = new GetStoriesResponseDto()
            {
                countTotal = 1,
                NextPage = 2,
                PrevPage = null,
                Results = new List<StoryDto>()
            };
            storyServiceMock.Setup(x => x.GetStories(It.IsAny<GetStoriesRequestDto>())).ReturnsAsync(mStoryResponse);
            var controller = new StoryController(storyServiceMock.Object);

            var requestDto = new GetStoriesRequestDto
            {
                Page = 0,
                PageSize = 10,
                Criteria = new SearchCriteria
                {
                    Title = "Sample Title",
                    StoryType = Constants.TopStory
                }
            };

            // Act
            var result = await controller.GetStories(requestDto);

            // Assert
            Assert.NotNull(result);
        }
    }
}