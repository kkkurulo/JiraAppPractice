using JiraAppPractice.Data.Context;
using JiraAppPractice.Data.Models;
using JiraAppPractice.Services.Dtos;
using JiraAppPractice.Services.Exceptions;
using JiraAppPractice.Services.Interfaces;
using JiraAppPractice.Services.Services;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace JiraAppPractice.Service.Test;

public class JiraItemServiceTests
{
    private const int UserId = 1;

    [Fact]
    public async Task UpdateStatusAsync_WithNotFoundItem_ThrowsItemNotFound()
    {
        var userService = new Mock<ICurrentUserService>();
        userService.Setup(x=>x.UserId).Returns(UserId);
        
        var dbContext = GetDbContext();
        var service = new JiraItemService(dbContext, userService.Object);

        var param = new UpdateStatusDto {};
        Func<Task> act = async () => await service.UpdateStatusAsync(param);

        await Assert.ThrowsAsync<ItemNotFound>(act);
    }
    [Fact]
    public async Task UpdateStatusAsync_WithDifferentOwner_ThrowsNotExactUser()
    {
        var userService = new Mock<ICurrentUserService>();
        userService.Setup(x => x.UserId).Returns(UserId);

        var dbContext = GetDbContext();
        var item = new Tasks
        {
            Title = "test",
            Description = "test",
            AsigneeId = 2,
            Id = 1
        };
        dbContext.Tasks.Add(item);
        await dbContext.SaveChangesAsync();
        dbContext.ChangeTracker.Clear();

        var service = new JiraItemService(dbContext, userService.Object);
        var param = new UpdateStatusDto { TaskId = 1 };
        Func<Task> act = async () => await service.UpdateStatusAsync(param);

        await Assert.ThrowsAsync<NotExactUser>(act);
    }

    [Fact]
    public async Task UpdateStatusAsync_WithInvalidStatus_ThrowInvalidStatus()
    {
        var userService = new Mock<ICurrentUserService>();
        userService.Setup(x => x.UserId).Returns(UserId); 

        var dbContext = GetDbContext();
        var item = new Tasks
        {
            Title = "test",
            Description = "test",
            StatusId = 1,
            AsigneeId = UserId,
            Id = 1
        };
        dbContext.Tasks.Add(item);
        await dbContext.SaveChangesAsync();
        dbContext.ChangeTracker.Clear();

        var service = new JiraItemService(dbContext, userService.Object);
        var param = new UpdateStatusDto { TaskId = 1, StatusId = 3 };
        Func<Task> act = async () => await service.UpdateStatusAsync(param);

        await Assert.ThrowsAsync<InvalidStatus>(act);
    }

     
    [Fact]
    public async Task UpdateInfoAsync_WithNotFoundItem_ThrowsItemNotFound()
    {
        var userService = new Mock<ICurrentUserService>();
        userService.Setup(x=>x.UserId).Returns(UserId);
        
        var dbContext = GetDbContext();
        var service = new JiraItemService(dbContext, userService.Object);

        var param = new UpdateInfoTaskDto {};
        Func<Task> act = async () => await service.UpdateInfoAsync(param);

        await Assert.ThrowsAsync<ItemNotFound>(act);
    }
    [Fact]
    public async Task UpdateInfoAsync_WithDifferentOwner_ThrowsNotExactUser()
    {
        var userService = new Mock<ICurrentUserService>();
        userService.Setup(x => x.UserId).Returns(UserId); 

        var dbContext = GetDbContext();
        var item = new Tasks
        {
            Title = "test",
            Description = "test",
            AsigneeId = 2,
            Id = 1
        };
        dbContext.Tasks.Add(item);
        await dbContext.SaveChangesAsync();
        dbContext.ChangeTracker.Clear();

        var service = new JiraItemService(dbContext, userService.Object);
        var param = new UpdateInfoTaskDto { TaskId = 1 };
        Func<Task> act = async () => await service.UpdateInfoAsync(param);

        await Assert.ThrowsAsync<NotExactUser>(act); 
    }


    [Fact]
    public async Task UpdateAsigneeAsync_WithNotFoundItem_ThrowsItemNotFound()
    {
        var userService = new Mock<ICurrentUserService>();
        userService.Setup(x => x.UserId).Returns(UserId);

        var dbContext = GetDbContext();
        var service = new JiraItemService(dbContext, userService.Object);
        var item = new Tasks
        {
            Title = "test",
            Description = "test",
            AsigneeId = 2,
            Id = 1
        };
        var param = new UpdateAsigneeDto { TaskId = 1, AsigneeId = UserId };
        Func<Task> act = async () => await service.UpdateAsigneeAsync(param);

        await Assert.ThrowsAsync<ItemNotFound>(act);
    }

    [Fact]
    public async Task UpdateAsigneeAsync_WithNotFoundUser_ThrowsUserNotFound()
    {
        var userService = new Mock<ICurrentUserService>();
        userService.Setup(x => x.UserId).Returns(UserId);

        var dbContext = GetDbContext();

        var task = new Tasks {
            Id = 1,
            AsigneeId = 1,
            Description = "Test",
            Title = "test"
        };

        dbContext.Tasks.Add(task);
        await dbContext.SaveChangesAsync();

        var service = new JiraItemService(dbContext, userService.Object);

        var param = new UpdateAsigneeDto { TaskId = 1, AsigneeId = 3 };
        Func<Task> act = async () => await service.UpdateAsigneeAsync(param);

        await Assert.ThrowsAsync<UserNotFound>(act);
    }

    private JiraContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<JiraContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new JiraContext(options);
    }
}
