using JiraAppPractice.Data.Models;
using JiraAppPractice.Services.Dtos;

namespace JiraAppPractice.Services.Interfaces;

public interface IJiraItemService
{
    Task<List<Tasks>> GetAsync();
    Task CreateAsync(CreateJiraItemDto item);
    Task UpdateAsync(int id, int status);

    Task DeleteAsync(int id);
}
