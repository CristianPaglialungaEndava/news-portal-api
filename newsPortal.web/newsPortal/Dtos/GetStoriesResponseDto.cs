using newsPortal.Models;

namespace newsPortal.Dtos
{
    public class GetStoriesResponseDto
    {
        public int countTotal { get; set; }
        public int? PrevPage { get; set; }
        public int? NextPage { get; set; }
        public IEnumerable<StoryDto> Results { get; set; }
    }
}
