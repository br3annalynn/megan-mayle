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
        public int PostId { get; set; }
        public string PostTitle { get; set; }
        public string PostBody { get; set; }
        public string PostExcerpt { get; set; }
        public string PostImageUrl { get; set; }
        public string PostSlug { get; set; }
        public DateTime PostDatePosted { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorTwitter { get; set; }
        public string AuthorUrl { get; set; }
        public string AuthorGitHub { get; set; }
        public string AuthorLinkedIn { get; set; }
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