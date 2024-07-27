namespace JiraAppPractice.Services.Dtos;

public class GettingTaskDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public int BoardId { get; set; }
    public int StatusId { get; set; }
    public int AsigneeId { get; set; }
}
