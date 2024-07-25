using JiraAppPractice.Data.Models;
using JiraAppPractice.Services.Dtos;

namespace JiraAppPractice.Services.Interfaces;

public interface IBoardService
{
    Task<List<Boards>> GetAsync();
    Task CreateBoardAsync(CreateBoardDto dto);
}
