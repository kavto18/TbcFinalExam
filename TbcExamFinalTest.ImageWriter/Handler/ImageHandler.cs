using Microsoft.AspNetCore.Http;
using TbcExamFinalTest.ImageWriter.Interface;

namespace TbcExamFinalTest.ImageWriter.Handler
{
    public interface IIMageHandler
    {
        string UploadImage(IFormFile file, long personalId);

        IFormFile GetImage(string fileName);

        string GetImageFullPath(string fileName);

        void DeleteImage(string fileName);
    }

    public class ImageHandler : IIMageHandler
    {
        private readonly IImageWriter _imageWriter;

        public ImageHandler(IImageWriter imageWriter)
        {
            _imageWriter = imageWriter;
        }

        public string UploadImage(IFormFile file, long personalId)
        {
            return _imageWriter.UploadImage(file, personalId);
        }

        public IFormFile GetImage(string fileName)
        {
            return _imageWriter.GetImage(fileName);
        }

        public string GetImageFullPath(string fileName)
        {
            return _imageWriter.GetImageFullPath(fileName);
        }

        public void DeleteImage(string fileName)
        {
            _imageWriter.DeleteImage(fileName);
        }
    }
}
