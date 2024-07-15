namespace JiraAppPractice.Data.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Tasks> Tasks { get; set; }

}