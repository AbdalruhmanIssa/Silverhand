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
            if (file != null && file.Length > 0)// check if file is not null and has content
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);// generate unique file name
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);// combine path

                using (var stream = File.Create(filePath))// create file stream in the specified path
                {
                    await file.CopyToAsync(stream);// copy the uploaded file to the stream asynchronously
                }

                return fileName;// return the unique file name
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
        
        public void Delete(string fileName)
        {
            var folderPath = Path.Combine(_env.WebRootPath, "images");//env.WebRootPath gets the wwwroot path
            var filePath = Path.Combine(folderPath, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }




    }
}
