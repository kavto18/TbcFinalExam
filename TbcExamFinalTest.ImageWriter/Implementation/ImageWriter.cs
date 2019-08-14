using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.IO;
using System.Threading.Tasks;
using TbcExamFinalTest.ImageWriter.Helper;
using TbcExamFinalTest.ImageWriter.Interface;

namespace TbcExamFinalTest.ImageWriter.Implementation
{
    public class ImageWriter : IImageWriter
    {
        public string UploadImage(IFormFile file, long id)
        {
            if (CheckIfImageFile(file))
            {
                return WriteFile(file, id);
            }
            return "Invalid Image File";
        }

        public IFormFile GetImage(string fileName)
        {
            IFormFile formFile;
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);
                if (File.Exists(path))
                {
                    using (var stream = File.OpenRead(path))
                    {
                        formFile = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return formFile;
        }

        public string GetImageFullPath(string fileName)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);
        }

        public void DeleteImage(string fileName)
        {
            if(string.IsNullOrEmpty(fileName))
            {
                return;
            }
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private string WriteFile(IFormFile file, long id)
        {
            string fileName;
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                fileName = id + extension;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                using (var bits = new FileStream(path, FileMode.Create))
                {
                    file.CopyToAsync(bits);
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return fileName;
        }

        private static bool CheckIfImageFile(IFormFile file)
        {
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
            }

            return WriterHelper.GetImageFormat(fileBytes) != ImageFormat.unknow;
        }
    }
}
