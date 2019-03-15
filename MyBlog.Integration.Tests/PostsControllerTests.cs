using System.Net.Http;
using System.Threading.Tasks;
using MyBlog.Api;
using Xunit;

namespace MyBlog.Integration.Tests
{
    public class PostsControllerTests : IClassFixture<TestWebApplicationFactory<Startup>>
    {
        private HttpClient _client;

        public PostsControllerTests(TestWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Must_Have_Posts()
        {
            
            //
            // Arrange and Act
            //
            var httpResponse = await _client.GetAsync(@"/api/posts").ConfigureAwait(false);
            //
            // Assert
            //
            httpResponse.EnsureSuccessStatusCode();
        }
    }
}
