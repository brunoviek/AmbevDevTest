using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Application.Products.ListProductCategories;
using Ambev.DeveloperEvaluation.Application.Products.ListProducts;
using Ambev.DeveloperEvaluation.Application.Products.ListProductsByCategory;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.Common.Pagination;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.Shared.Responses;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.Shared.Responses;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of ProductsController
        /// </summary>
        /// <param name="mediator">The mediator instance</param>
        /// <param name="mapper">The AutoMapper instance</param>
        public ProductsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a new product
        /// </summary>
        /// <param name="request">The product creation request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created product details</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<UserResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CreateProductCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            return Created(string.Empty, new ApiResponseWithData<ProductResponse>
            {
                Success = true,
                Message = "Products created successfully",
                Data = _mapper.Map<ProductResponse>(response)
            });
        }

        /// <summary>
        /// Retrieves a product by their ID
        /// </summary>
        /// <param name="id">The unique identifier of the product</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The product details if found</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<ProductResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProduct([FromRoute] int id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetProductQuery(id), cancellationToken);
            return Ok(_mapper.Map<ProductResponse>(response), "Products retrieved successfully");
        }

        /// <summary>
        /// Deletes a product by their ID
        /// </summary>
        /// <param name="id">The unique identifier of the product to delete</param>
        /// <param name="cancellationToken">Cancellation token</param>
        //// <returns>The product details if found</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<ProductResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new DeleteProductCommand(id), cancellationToken);
            return Ok(_mapper.Map<ProductResponse>(response), "Products deleted successfully");
        }

        /// <summary>
        /// Retrieves a paginated list of products.
        /// </summary>
        /// <param name="_page">
        /// The page number to retrieve (default is 1).
        /// </param>
        /// <param name="_size">
        /// The number of items per page (default is 10).
        /// </param>
        /// <param name="_order">
        /// A sorting expression, e.g. "Title asc,Price desc" (optional).
        /// </param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> to observe while waiting for the task to complete.
        /// </param>
        [HttpGet]
        [DynamicFilter(typeof(ProductResponse))]
        [ProducesResponseType(typeof(PaginatedResponse<ProductResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ListProducts(
            [FromQuery(Name = "_page")] int _page = 1,
            [FromQuery(Name = "_size")] int _size = 10,
            [FromQuery(Name = "_order")] string? _order = null,
            CancellationToken cancellationToken = default)
        {
            var filters = Request.Query.ToFilters();

            var result = await _mediator.Send(new ListProductsQuery
            {
                Page = _page,
                Size = _size,
                Order = _order,
                Filters = filters
            }, cancellationToken);

            return OkPaginated(_mapper.Map<PaginatedList<ProductResponse>>(result), "Products retrieved successfully");
        }

        /// <summary>
        /// Updates an existing product by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the product to update.</param>
        /// <param name="request">The updated product data.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The updated product details.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<ProductResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProduct(
            [FromRoute] int id,
            [FromBody] UpdateProductRequest request,
            CancellationToken cancellationToken)
        {
            var command = _mapper.Map<UpdateProductCommand>(request);
            command.Id = id;
            var response = await _mediator.Send(command, cancellationToken);

            return Ok(_mapper.Map<ProductResponse>(response), "Products updated successfully");
        }

        /// <summary>
        /// Retrieves all unique product categories.
        /// </summary>
        [HttpGet("categories")]
        [ProducesResponseType(typeof(ApiResponseWithData<List<string>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
        {
            var categories = await _mediator.Send(new ListProductCategoriesQuery(), cancellationToken);
            return Ok(categories, "Categories retrieved successfully");
        }

        /// <summary>
        /// Retrieves products of a specific category with pagination and optional sorting.
        /// </summary>
        /// <param name="category">Category name to filter products.</param>
        /// <param name="_page">Page number (default: 1).</param>
        /// <param name="_size">Items per page (default: 10).</param>
        /// <param name="_order">Sorting expression, e.g. "price desc" (optional).</param>
        /// <param name="cancellationToken">Cancellation token.</param>/// 
        [HttpGet("category/{category}")]
        [ProducesResponseType(typeof(PaginatedResponse<ProductResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ListByCategory(
            [FromRoute] string category,
            [FromQuery(Name = "_page")] int _page = 1,
            [FromQuery(Name = "_size")] int _size = 10,
            [FromQuery(Name = "_order")] string? _order = null,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new ListProductsByCategoryQuery
            {
                Category = category,
                Page = _page,
                Size = _size,
                Order = _order
            }, cancellationToken);

            return OkPaginated(_mapper.Map<PaginatedList<ProductResponse>>(result), "Products by category retrieved successfully");
        }
    }
}
