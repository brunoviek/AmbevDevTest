using Ambev.DeveloperEvaluation.Integration.Factories;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.Shared.Responses;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Ambev.DeveloperEvaluation.WebApi;

namespace Ambev.DeveloperEvaluation.Integration.Products
{
    public class ProductsIntegrationTests
        : IClassFixture<CustomWebAppFactory<Program>>
    {
        private readonly HttpClient _client;

        public ProductsIntegrationTests(CustomWebAppFactory<Program> factory)
            => _client = factory.CreateClient();

        [Fact(DisplayName = "GET /products returns 200 and a paginated list")]
        public async Task GetProducts_ReturnsOkAndPaginatedList()
        {
            var response = await _client.GetAsync("/api/products?_page=1&_size=5");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var dto = await response.Content.ReadFromJsonAsync<PaginatedResponse<ProductResponse>>();
            dto.Should().NotBeNull();
            dto!.Data!.Count().Should().BeLessOrEqualTo(5);
        }

        [Fact(DisplayName = "POST /products creates a product and returns 201")]
        public async Task CreateProduct_ReturnsCreated()
        {
            var newProduct = new
            {
                title = "Product X",
                price = 9.99,
                description = "Description of product X",
                category = "Category Y",
                image = "http://...",
                rating = new { rate = 4.5, count = 10 }
            };

            var response = await _client.PostAsJsonAsync("/api/products", newProduct);
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var created = await response.Content.ReadFromJsonAsync<ApiResponseWithData<ProductResponse>>();
            created!.Data!.Title.Should().Be(newProduct.title);
        }
    }
}
