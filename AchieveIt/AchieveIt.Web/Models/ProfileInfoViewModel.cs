using System;

namespace AchieveIt.Web.Models
{
    public class ProfileInfoViewModel
    {
        public ProfileInfoViewModel(string UserName, string Email, string Id, string PictureUri,
    string FacebookUrl, string Nationality, DateTime dateRegisterd, int votes)
        {
            this.UserName = UserName;
            this.Email = Email;
            this.Id = Id;
            this.PictureUri = PictureUri;
            this.FacebookUrl = FacebookUrl;
            this.Nationality = Nationality;
            this.DateRegisered = dateRegisterd;
            this.Votes = votes;
        }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
        public string PictureUri { get; set; }
        public string FacebookUrl { get; set; }
        public string Nationality { get; set; }
        public DateTime DateRegisered { get; set; }
        public int Votes { get; set; }
    }
}