namespace AchieveIt.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Image
    {
        public int Id { get; set; }
        [Required]
        public int ImageSize { get; set; }
        [Required]
        public string FileName { get; set; }
        [Required]
        public byte[] ImageData { get; set; }
        [MaxLength(30)]
        [Required]
        public string Extention { get; set; }


        //public HttpStyleUriParser File { get; set; }
        // public int OwnerId { get; set; }

    }
}
