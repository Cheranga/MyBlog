using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MyBlog.Api;
using MyBlog.Api.DataAccess.Models;
using Newtonsoft.Json;
using Xunit;

namespace MyBlog.Integration.Tests
{
    public class PostsControllerTests : IClassFixture<TestWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public PostsControllerTests(TestWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task There_Must_Be_Posts()
        {
            //
            // Arrange and act
            //
            var httpResponse = await _client.GetAsync(@"/api/posts").ConfigureAwait(false);
            //
            // Assert
            //
            httpResponse.EnsureSuccessStatusCode();
            var posts = JsonConvert.DeserializeObject<List<Post>>(await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false));

            Assert.True(posts != null && posts.Any());
        }
    }
}
