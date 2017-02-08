using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using megan_mayle.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace megan_mayle.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            List<PostDisplay> posts;
            using (var connection = new SqlConnection(Constants.ConnectionString))
            {
                posts = connection.Query<PostDisplay>(Constants.PostsForBlog).ToList();
            }
            foreach (var post in posts)
            {
                post.Markdown = CommonMark.CommonMarkConverter.Convert(post.PostBody);
            }
            return View(posts);
        }


        public IActionResult Post(string id)
        {
            PostDisplay post;
            using (var connection = new SqlConnection(Constants.ConnectionString))
            {
                post = connection.Query<PostDisplay>(Constants.PostQuery, new { slug = id }).ToList().FirstOrDefault();
            }
            post.Markdown = CommonMark.CommonMarkConverter.Convert(post.PostBody);
            return View("Post", post);
        }
    }
}
