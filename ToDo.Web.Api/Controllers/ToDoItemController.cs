using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDo.Application.ToDoItems.Commands.CompleteItem;
using ToDo.Application.ToDoItems.Commands.CreateItem;
using ToDo.Application.ToDoItems.Commands.DeleteItem;
using ToDo.Application.ToDoItems.Commands.UpdateItem;
using ToDo.Application.ToDoItems.Queries.GetItem;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDo.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoItemController : ControllerBase
    {
        private readonly ISender _sender;

        public ToDoItemController(ISender sender)
        {
            _sender = sender;
        }

        // GET: api/<ToDoItems>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var query = new GetAllToDoItemQuery();
            var result = await _sender.Send(query);
            return Ok(result.Value);
        }

        // GET api/<ToDoItems>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var query = new GetToDoItemByIdQuery(id);
            var result = await _sender.Send(query);
            return Ok(result.Value);
        }

        // POST api/<ToDoItems>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] string title)
        {
            var command = new CreateToDoItemCommand(title);
            var result = await _sender.Send(command);
            return Ok(result.Value);
        }

        // PUT api/<ToDoItems>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTitleAsync(int id, [FromBody] string title)
        {
            var command = new UpdateTaskCommand(id, title);
            var result = await _sender.Send(command);
            return Ok(result.Value);
        }

        // PUT api/<ToDoItems>/5
        [HttpPut("Complete/{id}")]
        public async Task<IActionResult> PutCompleteAsync(int id)
        {
            var command = new CompleteToDoItemCommand(id);
            var result = await _sender.Send(command);
            return Ok(result.Value);
        }

        // DELETE api/<ToDoItems>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var command = new DeleteTaskCommand(id);
            var result = await _sender.Send(command);
            return Ok(result.Value);
        }
    }
}
