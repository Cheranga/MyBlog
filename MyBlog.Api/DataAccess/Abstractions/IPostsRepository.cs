using System.Collections.Generic;
using System.Threading.Tasks;
using MyBlog.Api.DataAccess.Models;

namespace MyBlog.Api.DataAccess.Abstractions
{
    public interface IPostsRepository
    {
        Task<List<Post>> GetPostsAsync();
        Task<Post> GetPostAsync(int postId);
        Task<bool> AddPostAsync(Post post);
        Task<bool> UpdatePostAsync(Post post);
        Task<bool> DeletePostAsync(int postId);
    }
}