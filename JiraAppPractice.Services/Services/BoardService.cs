using JiraAppPractice.Data.Context;
using JiraAppPractice.Data.Models;
using JiraAppPractice.Services.Dtos;
using JiraAppPractice.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JiraAppPractice.Services.Services;

public class BoardService : IBoardService
{
    private readonly JiraContext _context;

    public BoardService(JiraContext context)
    {
        _context = context;
    }

    public async Task CreateBoardAsync(CreateBoardDto boardDto)
    {
        var item = new Boards
        {
            Name = boardDto.Name
        };
        _context.Boards.Add(item);
        await _context.SaveChangesAsync();
    }

    public  async Task<List<Boards>> GetAsync()
    {
        return await _context.Boards.ToListAsync();
    }
}
