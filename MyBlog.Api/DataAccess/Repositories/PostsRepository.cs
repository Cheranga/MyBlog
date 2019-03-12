using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MyBlog.Api.Configs;
using MyBlog.Api.DataAccess.Abstractions;
using MyBlog.Api.DataAccess.Models;

namespace MyBlog.Api.DataAccess.Repositories
{
    public class PostsRepository : IPostsRepository
    {
        private readonly string _connectionString;

        public PostsRepository(DatabaseConfig config)
        {
            _connectionString = config?.ConnectionString;
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new ArgumentNullException(nameof(config));
            }
        }

        public async Task<List<Post>> GetPostsAsync()
        {
            const string query = "SELECT * FROM Posts";

            using (var connection = new SqlConnection(_connectionString))
            {
                var posts = await connection.QueryAsync<Post>(query).ConfigureAwait(false);
                return posts?.ToList();
            }
        }

        public Task<Post> GetPostAsync(int postId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddPostAsync(Post post)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdatePostAsync(Post post)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePostAsync(int postId)
        {
            throw new NotImplementedException();
        }
    }
}