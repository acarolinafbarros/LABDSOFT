using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace GAM.Helpers
{
    public static class FileHelper
    {

        public static async Task<string> SaveFileDefault(this IFormFile file, IHostingEnvironment _hostingEnvironment, string filePath)
        {
            var fileName = string.Empty;

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            if(Directory.Exists(filePath))
            {
                fileName = $"{Guid.NewGuid()}.{file.FileName.Split('.').Last()}";
                if (file.Length > 0)
                {
                    using (var stream = new FileStream(Path.Combine(filePath, fileName), FileMode.Create))
                    {
                       await file.CopyToAsync(stream);
                        return fileName;
                    }
                }
            }

            return "";

        }
    }
}
