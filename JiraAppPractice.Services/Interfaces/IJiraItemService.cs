using JiraAppPractice.Data.Models;
using JiraAppPractice.Services.Dtos;

namespace JiraAppPractice.Services.Interfaces;

public interface IJiraItemService
{
    Task<List<Tasks>> GetAsync();
    Task <List<Tasks>>GetAsync(int boardId);
    Task CreateAsync(CreateJiraItemDto item);
    Task UpdateStatusAsync(UpdateStatusDto dto);
    Task UpdateInfoAsync(UpdateInfoTaskDto dto);
    Task UpdateAsigneeAsync(UpdateAsigneeDto dto);
    Task DeleteAsync(int taskId);


}
