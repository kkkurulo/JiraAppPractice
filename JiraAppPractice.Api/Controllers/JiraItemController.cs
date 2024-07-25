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
    [Route("api/[controller]")]
    [ApiController]
    public class JiraItemController : ControllerBase
    {
        private readonly IJiraItemService _jiraItem;
        public JiraItemController(IJiraItemService jiraItem)
        {
            _jiraItem = jiraItem;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tasks>>> GetAsync() {
            var items = await _jiraItem.GetAsync();
            return Ok(items);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(CreateJiraItemDto item)
        {
            await _jiraItem.CreateAsync(item);
            return Ok();
        }
        [HttpPut("{id}/status/{status}")]
        public async Task<ActionResult> UpdateStatusAsync(int id, int status)
        {
            await _jiraItem.UpdateAsync(id, status);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTaskAsync(int id)
        {
            await _jiraItem.DeleteAsync(id);
            return Ok();
        }

    }
}
