namespace AchieveIt.Web.Models
{
    using AchieveIt.Models;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class AddPostBindingModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        [Display(Name ="URL")]
        public string Uri { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual User Sender { get; set; }
    }
}