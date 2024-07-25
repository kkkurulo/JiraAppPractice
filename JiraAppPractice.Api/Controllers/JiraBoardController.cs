using JiraAppPractice.Data.Models;
using JiraAppPractice.Services.Dtos;
using JiraAppPractice.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JiraAppPractice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JiraBoardController : ControllerBase
    {
        private readonly IBoardService _board;
        public JiraBoardController(IBoardService board)
        {
            _board = board;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Boards>>> GetAsync()
        {
            var items = await _board.GetAsync();
            return Ok(items);
        }
        [HttpPost]
        public async Task<ActionResult> CreateAsync(CreateBoardDto item)
        {
            await _board.CreateBoardAsync(item);
            return Ok();
        }
    }
}
