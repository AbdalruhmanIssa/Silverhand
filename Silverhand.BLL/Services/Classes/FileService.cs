using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Silverhand.BLL.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverhand.BLL.Services.Classes
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _env;

        public FileService(IWebHostEnvironment env)
        {
            _env = env;
        }
        public async Task<string> UploadAsync(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = File.Create(filePath))
                {
                    await file.CopyToAsync(stream);
                }

                return fileName;
            }

            throw new Exception("error");
        }
        public async Task<string> UploadAsyncVid(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Vids", fileName);

                using (var stream = File.Create(filePath))
                {
                    await file.CopyToAsync(stream);
                }

                return fileName;
            }

            throw new Exception("error");
        }
        public async Task<List<string>> UploadManyAsync(List<IFormFile> files)
        {
            var fileNames = new List<string>();

            foreach (var file in files)
            {
                if (file != null && file.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    using (var stream = File.Create(filePath))
                    {
                        await file.CopyToAsync(stream);
                    }

                    fileNames.Add(fileName);
                }
            }

            return fileNames;
        }
        public void Delete(string fileName)
        {
            var folderPath = Path.Combine(_env.WebRootPath, "images");
            var filePath = Path.Combine(folderPath, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }




    }
}
