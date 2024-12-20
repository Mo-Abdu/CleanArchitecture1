using MediatR;
using Models;
namespace Application.Commands.AuthorCommands
{
    public class DeleteAuthorCommand : IRequest<OperationResult<bool>>
    {
        public int Id { get; set; }

        public DeleteAuthorCommand(int id)
        {
            Id = id;
        }
    }
}
