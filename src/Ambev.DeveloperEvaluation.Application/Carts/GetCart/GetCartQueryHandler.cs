using Ambev.DeveloperEvaluation.Application.Carts.Shared.Results;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart
{
    /// <summary>
    /// Handler for executing <see cref="GetCartQuery"/>, retrieving a cart by its ID.
    /// </summary>
    public class GetCartQueryHandler : IRequestHandler<GetCartQuery, CartResult>
    {
        private readonly ICartRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of <see cref="GetCartQueryHandler"/>.
        /// </summary>
        public GetCartQueryHandler(ICartRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the query to retrieve a cart by ID.
        /// </summary>
        public async Task<CartResult> Handle(GetCartQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id)
                         ?? throw new KeyNotFoundException($"Cart with ID {request.Id} not found.");

            return _mapper.Map<CartResult>(entity);
        }
    }
}
