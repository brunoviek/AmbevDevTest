using Ambev.DeveloperEvaluation.Application.Users.Results;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUsers
{
    /// <summary>
    /// Handler for listing users with pagination and optional ordering.
    /// </summary>
    public class ListUsersCommandHandler : IRequestHandler<ListUsersCommand, IEnumerable<GetUserResult>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ListUsersCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetUserResult>> Handle(ListUsersCommand request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync(
                page: request.Page,
                size: request.Size,
                order: request.Order,
                cancellationToken: cancellationToken);

            return _mapper.Map<IEnumerable<GetUserResult>>(users);
        }
    }
}
