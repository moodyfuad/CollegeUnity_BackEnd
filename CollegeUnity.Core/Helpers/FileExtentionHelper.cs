using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Helpers
{
    public static class FileExtentionhelper
    {
        private static readonly string BaseDirectory = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "uploads");

        static FileExtentionhelper()
        {
            Directory.CreateDirectory(BaseDirectory);
        }

        public static string GetPostPicturePath(int postId, IFormFile file)
        {
            string datePath = DateTime.Now.ToString("yyyy/MM");
            string postPath = Path.Combine(BaseDirectory, "posts", datePath, postId.ToString(), "pictures");

            Directory.CreateDirectory(postPath);

            return Path.Combine(postPath, file.FileName);
        }

        public static async Task SaveFileAsync(string filePath, IFormFile file)
        {
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }

        public static string GetFileExtention(this string filePath)
        {
            return Path.GetExtension(filePath).ToLower();
        }
    }
}
