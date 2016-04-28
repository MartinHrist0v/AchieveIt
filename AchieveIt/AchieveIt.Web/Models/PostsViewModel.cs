namespace AchieveIt.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class PostsViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public byte[] Uri { get; set; }
        [Required]
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }
        [Required]
        public string Sender { get; set; }
        public List<CommentViewModel> Comments { get; set; }

    }
}