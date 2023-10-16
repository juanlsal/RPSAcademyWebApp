namespace RPSAcademy.Services
{
    public interface IFileService
    {
        Tuple<int, string> SaveImage(IFormFile imageFile);
        public bool DeleteImage(string imageFileName);
        Tuple<int, string> SaveImageAdmin(IFormFile imageFile);
        public bool DeleteImageAdmin(string imageFileName);
    }
}
