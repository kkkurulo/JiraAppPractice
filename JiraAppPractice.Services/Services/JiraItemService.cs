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

    public async Task<List<Tasks>> GetAsync(int boardId)
    {
        if (await _context.Boards.FindAsync(boardId) is null)
        {
            throw new BoardNotFound();
        }
        return await _context.Tasks.Where(x => x.BoardId == boardId).ToListAsync();

    }

    public async Task CreateAsync(CreateJiraItemDto item)
    {
        if(await _context.Boards.FindAsync(item.BoardId) is null)
        {
            throw new BoardNotFound();
        }
        var itemTask = new Tasks
        {
            Title = item.Title,
            Description = item.Description,
            AsigneeId = _user.UserId,
            BoardId = item.BoardId,
            StatusId = (int)Status.ToDo
           
        };
        _context.Tasks.Add(itemTask);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateStatusAsync(UpdateStatusDto dto)
    {
        var item = await _context.Tasks.FindAsync(dto.TaskId);
        if (item is null) { throw new ItemNotFound(); }
        if(item.AsigneeId != _user.UserId) { throw new NotExactUser(); }

        if (item.StatusId == (int)Status.ToDo && dto.StatusId == (int)Status.Done)
        {
            throw new InvalidStatus();
        }
        else if (item.StatusId == (int)Status.Done)
        {
            throw new InvalidStatus();
        }
        item.StatusId = dto.StatusId;
        await _context.SaveChangesAsync();
    }

    public async Task UpdateInfoAsync(UpdateInfoTaskDto dto)
    {
        var item = await _context.Tasks.FindAsync(dto.TaskId);
        if (item is null) { throw new ItemNotFound(); }
        if (item.AsigneeId != _user.UserId) { throw new NotExactUser(); }
        item.Title = dto.Title;
        item.Description = dto.Description;

        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsigneeAsync(UpdateAsigneeDto dto)
    {
        var item = await _context.Tasks.FindAsync(dto.TaskId);
        var user = await _context.Users.FindAsync(dto.AsigneeId);
        if (item is null) { throw new ItemNotFound(); }
        if (user is null) { throw new UserNotFound(); }
        item.AsigneeId = dto.AsigneeId;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int taskId)
    {
        var item = await _context.Tasks.FindAsync(taskId);
        if (item is null) { throw new ItemNotFound(); }
        if (item.AsigneeId != _user.UserId) { throw new NotExactUser(); }

        _context.Tasks.Remove(item);
        await _context.SaveChangesAsync();
    }

}
