using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using megan_mayle;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using megan_mayle.ViewModels;

namespace megan_mayle.Controllers
{
    public class AdminController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }


        [Route("admin/posts/")]
        public IActionResult PostList()
        {
            List<PostDisplay> posts = new List<PostDisplay>();
            using (var connection = new SqlConnection(Constants.ConnectionString))
            {
                posts = connection.Query<PostDisplay>(Constants.AdminGetPosts).ToList();
            }
            foreach (var post in posts)
            {
                post.Markdown = CommonMark.CommonMarkConverter.Convert(post.PostBody);
            }
            return View(posts);
        }

        [Route("admin/posts/edit/{id}")]
        public IActionResult PostEdit(string id)
        {
            Post post;
            using (var connection = new SqlConnection(Constants.ConnectionString))
            {
                post = connection.Query<Post>(Constants.AdminGetPost, new { id }).ToList().FirstOrDefault();
            }
            post.Markdown = CommonMark.CommonMarkConverter.Convert(post.Body);
            return View(post);
        }

        [HttpPost]
        [Route("admin/posts/edit/{id}")]
        public IActionResult PostEdit(PostEditForm editForm)
        {
            if (!ModelState.IsValid)
            {
                CreateActionNotice("Error: " + ModelState.GetErrorListForDisplay(), ActionNoticeHelper.ActionNoticeType.Error);
                var viewModel = new Post
                {
                    AuthorId = editForm.AuthorId,
                    Body = editForm.Body,
                    DatePosted = editForm.DatePosted,
                    Excerpt = editForm.Excerpt,
                    Id = editForm.Id,
                    ImageUrl = editForm.ImageUrl,
                    Slug = editForm.Slug,
                    Title = editForm.Title
                };
                return View(viewModel);
            }
            int rows = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(Constants.ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand(Constants.AdminUpdatePost, conn))
                    {
                        command.Parameters.AddWithValue("@Body", editForm.Body);
                        command.Parameters.AddWithValue("@DatePosted", editForm.DatePosted);
                        command.Parameters.AddWithValue("@Excerpt", editForm.Excerpt);
                        command.Parameters.AddWithValue("@Id", editForm.Id);
                        command.Parameters.AddWithValue("@ImageUrl", !string.IsNullOrEmpty(editForm.ImageUrl) ? editForm.ImageUrl : "");
                        command.Parameters.AddWithValue("@Slug", editForm.Slug);
                        command.Parameters.AddWithValue("@Title", editForm.Title);

                        rows = command.ExecuteNonQuery();
                    }
                    conn.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                CreateActionNotice("Error: " + ex.Message, ActionNoticeHelper.ActionNoticeType.Error);
                var viewModel = new Post //todo automapper
                {
                    AuthorId = editForm.AuthorId,
                    Body = editForm.Body,
                    DatePosted = editForm.DatePosted,
                    Excerpt = editForm.Excerpt,
                    Id = editForm.Id,
                    ImageUrl = editForm.ImageUrl,
                    Slug = editForm.Slug,
                    Title = editForm.Title
                };
                return View(viewModel);
            }

            if (rows != 1)
            {
                //do something. error
                CreateActionNotice($"Something is not right. Db updated {rows} rows", ActionNoticeHelper.ActionNoticeType.Warning);
                Console.WriteLine("error updating");
            }
            else
            {
                CreateActionNotice("Post was successfully edited.", ActionNoticeHelper.ActionNoticeType.Success);
            }

            return RedirectToAction("PostList");
        }

        [Route("admin/posts/create")]
        public IActionResult PostCreate()
        {
            return View();
        }

        [HttpPost]
        [Route("admin/posts/create")]
        public IActionResult PostCreate(Post post)
        {
            if (!ModelState.IsValid)
            {
                CreateActionNotice("Error " + ModelState.GetErrorListForDisplay(), ActionNoticeHelper.ActionNoticeType.Error);

                return View(post);
            }
            
            int rows = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(Constants.ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand(Constants.AdminSaveaPost, conn))
                    {
                        command.Parameters.AddWithValue("@Body", post.Body);
                        command.Parameters.AddWithValue("@DatePosted", post.DatePosted);
                        command.Parameters.AddWithValue("@Excerpt", post.Excerpt);
                        command.Parameters.AddWithValue("@Id", post.Id);
                        command.Parameters.AddWithValue("@ImageUrl", !string.IsNullOrEmpty(post.ImageUrl) ? post.ImageUrl : "");
                        command.Parameters.AddWithValue("@Slug", post.Slug);
                        command.Parameters.AddWithValue("@Title", post.Title);

                        rows = command.ExecuteNonQuery();
                    }
                    conn.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                CreateActionNotice("Error: " + ex.Message, ActionNoticeHelper.ActionNoticeType.Error);
                return View(post);
            }

            if (rows != 1)
            {
                CreateActionNotice($"Something is not right. Db updated {rows} rows", ActionNoticeHelper.ActionNoticeType.Warning);
                Console.WriteLine("error updating");
            }
            else
            {
                CreateActionNotice("Post was successfully created", ActionNoticeHelper.ActionNoticeType.Success);
            }

            return RedirectToAction("PostList");
        }

        [Route("admin/posts/delete/{id}")]
        public IActionResult PostDelete(string id)
        {
            Post post;
            using (var connection = new SqlConnection(Constants.ConnectionString))
            {
                post = connection.Query<Post>(Constants.AdminGetPost, new { id }).ToList().FirstOrDefault();
            }
            post.Markdown = CommonMark.CommonMarkConverter.Convert(post.Body);
            return View(post);
        }

        [HttpPost]
        [Route("admin/posts/delete/{id}")]
        public IActionResult PostDelete(DeletePost post)
        {
            int rows = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(Constants.ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand(Constants.AdminDeletePost, conn))
                    {
                        command.Parameters.AddWithValue("@Id", post.Id);
                        rows = command.ExecuteNonQuery();
                    }
                    conn.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                CreateActionNotice("Error" + ex.Message, ActionNoticeHelper.ActionNoticeType.Error);
                return RedirectToAction("PostDelete", new { post.Id });
            }

            if (rows != 1)
            {
                CreateActionNotice($"Time out. Something is not right. Db updated {rows} rows", ActionNoticeHelper.ActionNoticeType.Warning);
                Console.WriteLine("error updating");
            }
            else
            {
                CreateActionNotice("Home Run! Post was successfully deleted.", ActionNoticeHelper.ActionNoticeType.Success);
            }

            return RedirectToAction("PostList");
        }

        

        protected void CreateActionNotice(string message, ActionNoticeHelper.ActionNoticeType noticeType)
        {
            var noticeDetails = ActionNoticeHelper.GetActionNoticeDetails().ToList().FirstOrDefault(x => x.Item1 == noticeType);
            TempData["Message"] = message;
            TempData["MessageType"] = (noticeDetails != null) ? noticeDetails.Item2 : "";
            TempData["MessageStrong"] = (noticeDetails != null) ? noticeDetails.Item3 : "";
        }
    }

    public class ActionNoticeHelper
    {
        public enum ActionNoticeType
        {
            Info,
            Warning,
            Error,
            Success
        }

        public static IList<Tuple<ActionNoticeType, string, string>> GetActionNoticeDetails()
        {
            var result = new List<Tuple<ActionNoticeType, string, string>>
            {
                new Tuple<ActionNoticeType, string, string>(ActionNoticeType.Info, "alert-info", "<i class='glyphicon glyphicon-arrow-right'></i> "),
                new Tuple<ActionNoticeType, string, string>(ActionNoticeType.Warning, "alert-warning", "<i class='glyphicon glyphicon-arrow-right'></i> "),
                new Tuple<ActionNoticeType, string, string>(ActionNoticeType.Error, "alert-danger", "<i class='glyphicon glyphicon-fire'></i> "),
                new Tuple<ActionNoticeType, string, string>(ActionNoticeType.Success, "alert-success", "<i class='glyphicon glyphicon-ok'></i> "),
            };

            return result;
        }
    }
    public static class ModelStateExtensions
    {
        private static IEnumerable<string> GetErrorListFromModelState(this ModelStateDictionary modelState)
        {
            if (modelState.IsValid)
            {
                return new List<string>();
            }
            var query = from state in modelState.Values
                        from error in state.Errors
                        select error.ErrorMessage;

            var errorList = query.ToList();
            return errorList;
        }

        public static string GetErrorListForDisplay(this ModelStateDictionary modelState)
        {
            var errors = GetErrorListFromModelState(modelState);
            var result = new StringBuilder();
            foreach (var error in errors)
            {
                result.AppendLine(error);
            }
            return result.ToString();
        }
    }

}

