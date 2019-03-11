using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MyBlog.Api.DataAccess.Models;

namespace MyBlog.Api.DataAccess.Repositories
{
    public class PostsRepository
    {
        private readonly string _connectionString;

        public PostsRepository(IConfiguration configuration)
        {
            _connectionString = configuration[""];
        }

        public Task<List<Post>> GetPostsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
