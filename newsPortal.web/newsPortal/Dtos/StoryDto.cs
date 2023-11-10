using newsPortal.Models;

namespace newsPortal.Dtos
{
    public class StoryDto
    {
        public StoryDto(Story story)
        {
            Title = story.Title;
            Url = story.Url;
            Id = story.Id;
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Url { get; set; }
    }
}
