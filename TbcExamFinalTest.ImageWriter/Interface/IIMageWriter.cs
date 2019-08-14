using Microsoft.AspNetCore.Http;

namespace TbcExamFinalTest.ImageWriter.Interface
{
    public interface IImageWriter
    {
        string UploadImage(IFormFile file, long id);

        IFormFile GetImage(string fileName);

        string GetImageFullPath(string fileName);

        void DeleteImage(string fileName);
    }
}
