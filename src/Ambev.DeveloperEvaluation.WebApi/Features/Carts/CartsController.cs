using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;
using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Ambev.DeveloperEvaluation.Application.Carts.ListCarts;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using Ambev.DeveloperEvaluation.Common.Pagination;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.Shared.Responses;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of <see cref="CartsController"/>.
        /// </summary>
        public CartsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a new cart.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CartResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCart([
            FromBody] CreateCartRequest request,
            CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CreateCartCommand>(request);
            var result = await _mediator.Send(command, cancellationToken);

            return Created(string.Empty, new ApiResponseWithData<CartResponse>
            {
                Success = true,
                Message = "Cart created successfully",
                Data = _mapper.Map<CartResponse>(result)
            });
        }

        /// <summary>
        /// Retrieves a cart by its ID.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<CartResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCart([
            FromRoute] int id,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetCartQuery(id), cancellationToken);
            return Ok(_mapper.Map<CartResponse>(result), "Cart retrieved successfully");
        }

        /// <summary>
        /// Deletes a cart by its ID.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<CartResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCart([
            FromRoute] int id,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteCartCommand(id), cancellationToken);
            return Ok(result, "Cart deleted successfully");
        }

        /// <summary>
        /// Retrieves a paginated list of carts.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedResponse<CartResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ListCarts(
            [FromQuery(Name = "_page")] int page = 1,
            [FromQuery(Name = "_size")] int size = 10,
            [FromQuery(Name = "_order")] string? order = null,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new ListCartsQuery
            {
                Page = page,
                Size = size,
                Order = order
            }, cancellationToken);

            return OkPaginated(result, "Carts retrieved successfully");
        }

        /// <summary>
        /// Updates an existing cart by its ID.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<CartResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCart([
            FromRoute] int id,
            [FromBody] UpdateCartRequest request,
            CancellationToken cancellationToken)
        {
            var command = _mapper.Map<UpdateCartCommand>(request);
            command.Id = id;
            var result = await _mediator.Send(command, cancellationToken);

            return Ok(_mapper.Map<CartResponse>(result), "Cart updated successfully");
        }
    }
}
