namespace AchieveIt.Web.Models
{
    using System.Collections.Generic;
    public class ProfileVewModel
    {

        public ProfileInfoViewModel ProfileInfo { get; set; }
        public AddPostBindingModel AddPost { get; set; }
        public ICollection<PostsViewModel> Posts { get; set; }
    }
}