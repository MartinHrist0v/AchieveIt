namespace AchieveIt.Web.Models
{
    internal class Image
    {
        public string Extention { get; set; }
        public string FileName { get; set; }
        public byte[] ImageData { get; set; }
        public int ImageSize { get; set; }
    }
}