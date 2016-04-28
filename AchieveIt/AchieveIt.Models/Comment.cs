namespace AchieveIt.Models
{
    using System;
    public class Comment
    {
        private int vote;
        public Comment()
        {
            this.vote = 0;
            this.DateCreated = DateTime.Now;
        }
        public int Id { get; set; }
        public string Text { get; set; }
        public int AuthorId { get; set; }
        public User Author { get; set; }
        public DateTime DateCreated { get; set; }

        public int Vote
        {
            get { return this.vote; }
            set { this.vote = value; }
        }
    }
}