namespace JiraAppPractice.Data.Models;

public class Tasks
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public int BoardId { get; set; }
    public int StatusId { get; set; }
    public string AsigneeId { get; set; }
    public User User { get; set; }
    public Boards Board { get; set; }
    public Statuses Statuses { get; set; }

}
