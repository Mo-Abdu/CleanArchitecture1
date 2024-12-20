﻿using MediatR;
using Models;
namespace Application.Queries
{
    public class GetBooksQuery : IRequest<OperationResult<List<Book>>>
    {
      
    }
}
