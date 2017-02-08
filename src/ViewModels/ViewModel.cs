using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace megan_mayle.ViewModels
{
    public class ViewModel
    {
        public IList<PostDisplay> Posts { get; set; } = new List<PostDisplay>();
    }

    public class PostDisplay
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Excerpt { get; set; }
        public string ImageUrl { get; set; }
        public string Slug { get; set; }
        public DateTime DatePosted { get; set; }
        public string Markdown { get; set; }
    }

    public class Post
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public string Excerpt { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        public string Slug { get; set; }
        [Required]
        public string DatePosted { get; set; }
        [Required]
        public int AuthorId { get; set; }
        public string Markdown { get; set; }
    }

    public class DeletePost
    {
        public int Id { get; set; }
    }


    public class PostEditForm
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public string Excerpt { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        public string Slug { get; set; }
        [Required]
        public string DatePosted { get; set; }
        [Required]
        public int AuthorId { get; set; }
    }
}