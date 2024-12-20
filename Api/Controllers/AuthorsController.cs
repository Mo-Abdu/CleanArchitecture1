using Application.Commands.AuthorCommands;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/authors
        [HttpGet]
        public async Task<IActionResult> GetAuthors(CancellationToken cancellationToken)
        {
            var query = new GetAuthorsQuery();
            var authors = await _mediator.Send(query, cancellationToken);
            return Ok(authors);
        }

        // GET: api/authors/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById(int id, CancellationToken cancellationToken)
        {
            var query = new GetAuthorByIdQuery(id);
            var author = await _mediator.Send(query, cancellationToken);

            if (author == null)
                return NotFound($"Author with ID {id} not found.");

            return Ok(author);
        }

        // POST: api/authors
        [HttpPost]
        public async Task<IActionResult> AddAuthor([FromBody] AddAuthorCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result.ErrorMessage);

            return CreatedAtAction(nameof(GetAuthorById), new { id = result.Data.Id }, result.Data);
        }

        // PUT: api/authors/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] UpdateAuthorCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id)
                return BadRequest($"Mismatch between route ID ({id}) and command ID ({command.Id}).");

            var result = await _mediator.Send(command, cancellationToken);

            if (!result.IsSuccess)
                return NotFound(result.ErrorMessage);

            return Ok(result.Data);
        }

        // DELETE: api/authors/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id, CancellationToken cancellationToken)
        {
            var command = new DeleteAuthorCommand(id);
            var result = await _mediator.Send(command, cancellationToken);

            if (!result.IsSuccess)
                return NotFound(result.ErrorMessage);

            return NoContent();
        }
    }
}
