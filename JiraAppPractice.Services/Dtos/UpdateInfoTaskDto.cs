namespace JiraAppPractice.Services.Dtos;

public class UpdateInfoTaskDto
{
    public int TaskId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
}
