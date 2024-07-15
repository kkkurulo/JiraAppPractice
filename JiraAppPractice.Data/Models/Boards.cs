namespace JiraAppPractice.Data.Models;

public class Boards
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<Tasks> Tasks { get; set; }

}