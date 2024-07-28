
namespace JiraAppPractice.Services.Dtos;

public class CreateJiraItemDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int BoardId {  get; set; }
}
