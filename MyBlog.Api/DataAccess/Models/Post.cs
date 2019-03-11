namespace MyBlog.Api.DataAccess.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public int AuthorId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}