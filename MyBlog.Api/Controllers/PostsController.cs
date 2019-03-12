using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Api.DataAccess.Abstractions;

namespace MyBlog.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostsRepository _postsRepository;

        public PostsController(IPostsRepository postsRepository)
        {
            _postsRepository = postsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var posts = await _postsRepository.GetPostsAsync().ConfigureAwait(false);
            if (posts == null || !posts.Any())
            {
                return NotFound();
            }

            return Ok(posts);
        }
    }
}