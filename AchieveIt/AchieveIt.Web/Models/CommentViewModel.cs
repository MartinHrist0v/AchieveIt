using System;
using System.ComponentModel.DataAnnotations;

namespace AchieveIt.Web.Models
{
    public class CommentViewModel
    {
            public int Id { get; set; }
            [Required]
            public string Text { get; set; }
            public int AuthorId { get; set; }
            public string Author { get; set; }
            public DateTime DateCreated { get; set; }
            public int Vote { get; set; }
        }
}