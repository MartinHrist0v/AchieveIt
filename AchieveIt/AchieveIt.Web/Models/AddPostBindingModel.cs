namespace AchieveIt.Web.Models
{
    using AchieveIt.Models;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public class AddPostBindingModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }
        [Display(Name = "URL")]
        [Url]
        public string Uri { get; set; }
        //[FileExtensions(ErrorMessage = "Must choose .jpeg .jpg or gif file.", Extensions = "jpeg,jpg,gif,png")]
        public HttpPostedFileBase Image { get; set; }
    }
}