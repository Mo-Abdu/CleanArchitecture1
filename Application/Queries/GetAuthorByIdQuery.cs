﻿using MediatR;
using Models;
namespace Application.Queries
{
    public class GetAuthorByIdQuery : IRequest<OperationResult<Author>>
    {
        public int Id { get; }

        public GetAuthorByIdQuery(int id)
        {
            Id = id;
        }
    }
}
