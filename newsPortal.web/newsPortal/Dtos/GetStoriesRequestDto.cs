using newsPortal.Common;
using System.ComponentModel.DataAnnotations;

namespace newsPortal.Dtos
{
    public class GetStoriesRequestDto
    {
        public GetStoriesRequestDto()
        {
            Page = 1;
            PageSize = 10;
            Criteria = new SearchCriteria();
        }
        [Range(1, int.MaxValue, ErrorMessage = "Page must be greater than 0.")]
        public int Page{ get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Page Size must be greater than 0.")]
        public int PageSize { get; set; }
        public SearchCriteria Criteria { get; set; }
    }
    public class SearchCriteria
    {
        public string? Title { get; set; }
        [RegularExpression($"^(?i)({Constants.NewStory}|{Constants.TopStory}|{Constants.BestStory})$", ErrorMessage = $"Invalid SearchCriteria. Valid values are {Constants.NewStory},{Constants.TopStory},{Constants.BestStory}.")]
        public string? StoryType { get; set; }
    }
}
 