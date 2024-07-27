using JiraAppPractice.Data.Context;
using JiraAppPractice.Data.Models;
using JiraAppPractice.Services.Dtos;
using JiraAppPractice.Services.Exceptions;
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

    public async Task<List<Tasks>> GetAsync(int BoardId)
    {
        if (await _context.Boards.FindAsync(BoardId) is null)
        {
            throw new BoardNotFound();
        }
        return await _context.Tasks.Where(x => x.BoardId == BoardId).ToListAsync();

    }

    public async Task CreateAsync(CreateJiraItemDto item)
    {
        var itemTask = new Tasks
        {
            Title = item.Title,
            Description = item.Description,
            AsigneeId = _user.UserId,
            BoardId = 1,
            StatusId = (int)Statuses.ToDo
           
        };
        _context.Tasks.Add(itemTask);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateStatusAsync(int TaskId, UpdateStatusDto dto)
    {
        var item = await _context.Tasks.FindAsync(TaskId);
        if (item is null) { throw new ItemNotFound(); }
        if(item.AsigneeId != _user.UserId) { throw new NotExactUser(); }
        if (item.StatusId != (int)Statuses.Done && Math.Abs(item.StatusId - dto.StatusId) == 1)
        {
            item.StatusId = dto.StatusId;
        }
        else
        {
            throw new InvalidStatus();
        }
        await _context.SaveChangesAsync();
    }

    public async Task UpdateInfoAsync(int TaskId, UpdateInfoTaskDto dto)
    {
        var item = await _context.Tasks.FindAsync(TaskId);
        if (item is null) { throw new ItemNotFound(); }
        if (item.AsigneeId != _user.UserId) { throw new NotExactUser(); }

        if (dto.Title != "string")
        {
            item.Title = dto.Title;
        }
        if (dto.Description != "string")
        {
            item.Description = dto.Description;
        }

        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsigneeAsync(int TaskId, UpdateAsigneeDto dto)
    {
        var item = await _context.Tasks.FindAsync(TaskId);
        if (item != null) {

           if(await _context.Users.FindAsync(dto.AsigneeId) != null)
            {
                item.AsigneeId = dto.AsigneeId;
            }
            else
            {
                throw new UserNotFound();
            }

        }
        else
        {
            throw new ItemNotFound();
        }

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int TaskId)
    {
        var item = await _context.Tasks.FindAsync(TaskId);
        if (item is null) { throw new ItemNotFound(); }
        if (item.AsigneeId != _user.UserId) { throw new NotExactUser(); }

        _context.Tasks.Remove(item);
            await _context.SaveChangesAsync();
    }

}
