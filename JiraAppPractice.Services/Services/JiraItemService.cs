using JiraAppPractice.Data.Context;
using JiraAppPractice.Data.Models;
using JiraAppPractice.Services.Dtos;
using JiraAppPractice.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JiraAppPractice.Services.Services;

public class JiraItemService : IJiraItemService
{
    private readonly JiraContext _context;
    private readonly ICurrentUserService _user;

    public JiraItemService(JiraContext context, ICurrentUserService user)
    {
        _context = context;
        _user = user;
    }


    public async Task<List<Tasks>> GetAsync()
    {
        return await _context.Tasks
            .Where(x => x.AsigneeId == _user.UserId)
            .ToListAsync();
    }

    public async Task CreateAsync(CreateJiraItemDto item)
    {
        var itemTask = new Tasks
        {
            Title = item.Title,
            Description = item.Description,
            AsigneeId = _user.UserId,
            BoardId = 1,
            StatusId = 1
           
        };
        _context.Tasks.Add(itemTask);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, int status)
    {
        var item = await _context.Tasks.FindAsync(id);
        if (item is null) {   }
        if(item.AsigneeId != _user.UserId) { }

        item.StatusId = status;
        await _context.SaveChangesAsync();
         
    }

    public async Task DeleteAsync(int id)
    {
        var item = await _context.Tasks.FindAsync(id);
        
            _context.Tasks.Remove(item);
            await _context.SaveChangesAsync();
        
    }
}
