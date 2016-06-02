namespace AchieveIt.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Post
    {
        private HashSet<Comment> comments;
        private int vote;

        public Post()
        {
            this.comments = new HashSet<Comment>();
            vote = 0;
            this.DateCreated = DateTime.Now;
        }
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        public virtual AchieveIt.Models.Image Image { get; set; }
        public string Url { get; set; }
        
        [Required]
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }
        public string DirectorySavedPicture { get; set; }
        public int Vote
        {
            get { return this.vote; }
            set { this.vote = value; }
        }
        public string SenderId { get; set; }
        public virtual User Sender { get; set; }
        public virtual HashSet<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }
    }
}