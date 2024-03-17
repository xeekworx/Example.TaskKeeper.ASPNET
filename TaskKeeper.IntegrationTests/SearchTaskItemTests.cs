using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace TaskKeeper.IntegrationTests
{
    public class SearchTaskItemTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public SearchTaskItemTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Get_TaskItems_ReturnOk()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/TaskItem");

            // Assert 
            response.IsSuccessStatusCode.Should().BeTrue();
        }
    }
}
