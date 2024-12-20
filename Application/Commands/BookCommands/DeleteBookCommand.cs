using MediatR;
using Models;
namespace Application.Commands.BookCommands
{
    public class DeleteBookCommand : IRequest<OperationResult<bool>>
    {
        public int Id { get; set; } // BookId

        public DeleteBookCommand(int id)
        {
            Id = id; 
        }
    }
}
