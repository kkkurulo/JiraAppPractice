using JiraAppPractice.Data.Context;
using JiraAppPractice.Data.Models;
using JiraAppPractice.Services.Dtos;
using JiraAppPractice.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

/**
 To Do:
    Upgrade getting specific users tasks
 */
namespace JiraAppPractice.Api.Controllers
{
    [Authorize]
    [Route("api/item")]
    [ApiController]
    public class JiraItemController : ControllerBase
    {
        private readonly IJiraItemService _jiraItem;
        public JiraItemController(IJiraItemService jiraItem)
        {
            _jiraItem = jiraItem;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tasks>>> GetAsync()
        {
            var items = await _jiraItem.GetAsync();
            return Ok(items);
        }
        [HttpGet("{boardId}")]
        public async Task<ActionResult<IEnumerable<GettingTaskDto>>> GetAsync(int boardId)
        {
            var items = await _jiraItem.GetAsync(boardId);
            var finishItems = items.Select(x => new GettingTaskDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                CreatedAt = x.CreatedAt,
                BoardId = x.BoardId,
                AsigneeId = x.AsigneeId,
                StatusId = x.StatusId
            });
            return Ok(finishItems);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(CreateJiraItemDto item)
        {
            await _jiraItem.CreateAsync(item);
            return Ok();
        }
        [HttpPut("status")]
        public async Task<ActionResult> UpdateAsync(UpdateStatusDto dto)
        {
            await _jiraItem.UpdateStatusAsync(dto);
            return Ok();
        }
        [HttpPut("title-description")]
        public async Task<ActionResult> UpdateAsync(UpdateInfoTaskDto updateInfo)
        {
            await _jiraItem.UpdateInfoAsync(updateInfo);
            return Ok();
        }
        [HttpPut("asignee")]
        public async Task<ActionResult> UpdateAsync(UpdateAsigneeDto updateInfo)
        {
            await _jiraItem.UpdateAsigneeAsync(updateInfo);
            return Ok();
        }
        [HttpDelete("{taskId}")]
        public async Task<ActionResult> DeleteTaskAsync(int taskId)
        {
            await _jiraItem.DeleteAsync(taskId);
            return Ok();
        }
 
    }
}
