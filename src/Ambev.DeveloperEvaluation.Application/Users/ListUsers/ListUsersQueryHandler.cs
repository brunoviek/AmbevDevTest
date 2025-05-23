﻿using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Common.Pagination;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using System.Linq.Dynamic.Core;
using Ambev.DeveloperEvaluation.Application.Users.Shared.Results;
using AutoMapper.QueryableExtensions;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUsers
{
    /// <summary>
    /// Handles listing users with pagination and optional ordering,
    /// applying validation rules and mapping to result DTOs.
    /// </summary>
    public class ListUsersQueryHandler : IRequestHandler<ListUsersQuery, PaginatedList<UserResult>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of <see cref="ListUsersQueryHandler"/>.
        /// </summary>
        public ListUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Processes the ListUsersCommand by validating ordering,
        /// applying sorting, performing pagination, and mapping entities to results.
        /// </summary>
        /// <param name="request">The command containing page, size, and order parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// A <see cref="PaginatedList{GetUserResult}"/> containing users mapped to GetUserResult.
        /// </returns>
        public async Task<PaginatedList<UserResult>> Handle(
            ListUsersQuery request,
            CancellationToken cancellationToken)
        {
            var allowedProperties = typeof(UserResult).GetPropertyNames();

            var query = _userRepository.QueryAll()
                .ApplyDynamicFilters(request.Filters, allowedProperties);

            if (!string.IsNullOrWhiteSpace(request.Order))
                query = query.OrderBy(
                    OrderValidator.ValidateUserOrderFields(request.Order));

            return await PaginatedList<UserResult>.CreateAsync(
                query.ProjectTo<UserResult>(_mapper.ConfigurationProvider),
                request.Page,
                request.Size,
                cancellationToken);
        }
    }
}
