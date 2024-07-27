using JiraAppPractice.Data.Models;
using JiraAppPractice.Services.Dtos;

namespace JiraAppPractice.Services.Interfaces;

public interface IJiraItemService
{
    Task<List<Tasks>> GetAsync();
    Task <List<Tasks>>GetAsync(int id);
    Task CreateAsync(CreateJiraItemDto item);
    Task UpdateStatusAsync(int TaskId, UpdateStatusDto dto);
    Task UpdateInfoAsync(int TaskId, UpdateInfoTaskDto dto);
    Task UpdateAsigneeAsync(int TaskId, UpdateAsigneeDto dto);
    Task DeleteAsync(int TaskId);


}
