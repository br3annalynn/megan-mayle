namespace megan_mayle
{
    public static class Constants
    {
        public static readonly string PostQuery = "SELECT Id, Body, Slug, Title, Excerpt, ImageUrl, DatePosted From BlogPosts WHERE Slug = @slug";
        public static readonly string PostsQuery = "SELECT Id, Body, Slug, Title, Excerpt, ImageUrl, DatePosted From BlogPosts";

        public static string PostsForBlog = PostsQuery + " WHERE DatePosted < GETDATE()";

        //Admin
        public static string AdminGetPosts = PostsQuery;

        public static string AdminGetPost = "SELECT Id, Body, Slug, Title, Excerpt, ImageUrl, DatePosted From BlogPosts WHERE Id = @id";

        public static string AdminSaveaPost = "INSERT INTO BlogPosts (Body, Slug, Title, Excerpt, ImageUrl, DatePosted) VALUES (@Body, @Slug, @Title, @Excerpt, @ImageUrl, @DatePosted); SELECT CAST(SCOPE_IDENTITY() as int)";

        public static string AdminUpdatePost = "UPDATE BlogPosts SET Body = @Body, Slug = @Slug, Title = @Title, Excerpt = @Excerpt, ImageUrl = @ImageUrl, DatePosted = @DatePosted WHERE Id = @Id; SELECT CAST(@Id as int)";

        public static string AdminDeletePost = "DELETE from BlogPosts WHERE Id = @Id";

        
        //public static string ConnectionString = "Server=.\\SQLEXPRESS;Database=teamvirtuoso;Trusted_Connection=True;";
        //public static string ConnectionString = "Server=localhost;Database=tv;User Id=sa;Password=tvLIVE!1;";
        public static string ConnectionString = "Server=DESKTOP-O6JTTAC\\SQLEXPRESS;Database=megan-mayle;User Id=bre;Password=tvLIVE!1;";

    }
}
