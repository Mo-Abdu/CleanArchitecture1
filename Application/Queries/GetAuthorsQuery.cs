using MediatR;
using Models;
namespace Application.Queries
{
    public class GetAuthorsQuery : IRequest<OperationResult<List<Author>>> 
    {
  
    }
}
