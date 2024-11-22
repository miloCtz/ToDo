using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDo.Application.ToDoItems.Commands.CompleteItem;
using ToDo.Application.ToDoItems.Commands.CreateItem;
using ToDo.Application.ToDoItems.Commands.DeleteItem;
using ToDo.Application.ToDoItems.Commands.UpdateItem;
using ToDo.Application.ToDoItems.Queries.GetItem;


namespace ToDo.Web.Api.Controllers
{
    [Route("api/[controller]")]
    public class ToDoItemController : ApiController
    {
        public ToDoItemController(ISender sender) : base(sender)
        {
        }

        // GET: api/<ToDoItems>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var query = new GetAllToDoItemQuery();
            var result = await _sender.Send(query);

            return result.Match<IActionResult>(
                value => Ok(value!),
                error => NotFound(error!));
        }

        // GET api/<ToDoItems>/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var query = new GetToDoItemByIdQuery(id);
            var result = await _sender.Send(query);

            return result.Match<IActionResult>(
                value => Ok(value!),
                error => NotFound(error!));
        }

        // POST api/<ToDoItems>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] string title)
        {
            var command = new CreateToDoItemCommand(title);
            var result = await _sender.Send(command);

            return result.Match<IActionResult>(
               value => Ok(value),
               error => HandleFailure(error!));
        }

        // PUT api/<ToDoItems>/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutTitleAsync(int id, [FromBody] string title)
        {
            var command = new UpdateTaskCommand(id, title);
            var result = await _sender.Send(command);

            return result.Match<IActionResult>(
              value => NoContent(),
              error => HandleFailure(error!));
        }

        // PUT api/<ToDoItems>/5
        [HttpPut("Complete/{id:int}")]
        public async Task<IActionResult> PutCompleteAsync(int id)
        {
            var command = new CompleteToDoItemCommand(id);
            var result = await _sender.Send(command);

            return result.Match<IActionResult>(
             value => NoContent(),
             error => HandleFailure(error!));
        }

        // DELETE api/<ToDoItems>/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var command = new DeleteTaskCommand(id);
            var result = await _sender.Send(command);

            return result.Match<IActionResult>(
             value => NoContent(),
             error => HandleFailure(error!));
        }
    }
}
