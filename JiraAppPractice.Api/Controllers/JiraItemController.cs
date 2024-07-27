using JiraAppPractice.Data.Context;
using JiraAppPractice.Data.Models;
using JiraAppPractice.Services.Dtos;
using JiraAppPractice.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

/**
 To Do:
    Upgrade getting specific users tasks
 */
namespace JiraAppPractice.Api.Controllers
{
    [Route("api/item-controller")]
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
        [HttpGet("{BoardId}")]
        public async Task<ActionResult<IEnumerable<GettingTaskDto>>> GetAsync(int BoardId)
        {
            var items = await _jiraItem.GetAsync(BoardId);
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
        [HttpPut("{TaskId}/status")]
        public async Task<ActionResult> UpdateAsync(int TaskId, UpdateStatusDto dto)
        {
            await _jiraItem.UpdateStatusAsync(TaskId, dto);
            return Ok();
        }
        [HttpPut("{TaskId}/name-description")]
        public async Task<ActionResult> UpdateAsync(int TaskId, UpdateInfoTaskDto updateInfo)
        {
            await _jiraItem.UpdateInfoAsync(TaskId, updateInfo);
            return Ok();
        }
        [HttpPut("{TaskId}/asignee")]
        public async Task<ActionResult> UpdateAsync(int TaskId, UpdateAsigneeDto updateInfo)
        {
            await _jiraItem.UpdateAsigneeAsync(TaskId, updateInfo);
            return Ok();
        }
        [HttpDelete("{TaskId}")]
        public async Task<ActionResult> DeleteTaskAsync(int TaskId)
        {
            await _jiraItem.DeleteAsync(TaskId);
            return Ok();
        }
 
    }
}
