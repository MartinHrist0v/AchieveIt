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
        public byte[] Picture { get; set; }
        public string DirectorySavedPicture { get; set; }
        public string Url { get; set; }
        [Required]
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }
        [Required]
        public string Sender { get; set; }
        
        public List<CommentViewModel> Comments { get; set; }

    }
}