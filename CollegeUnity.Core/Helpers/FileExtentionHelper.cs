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

        public static readonly string ProfilePictureFolder = GetProfilePicturePath();

        public static readonly string CardIdPictureFolder = GetCardIdPictureFolder();

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

        public static string GetProfilePicturePath(int accountId, IFormFile file)
        {
            string baseDirectory = Path.Combine(BaseDirectory, "accounts", "profile-pictures");

            string accountPath = Path.Combine(baseDirectory, accountId.ToString());

            Directory.CreateDirectory(accountPath);

            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            return Path.Combine(accountPath, fileName);
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

        private static string GetProfilePicturePath()
        {
            string profilePicturesPath = Path.Combine(BaseDirectory, "Profile-Pictures");


            bool exists = Directory.Exists(profilePicturesPath);
            if (exists)
            {
                return profilePicturesPath;
            }
            else
            {
                Directory.CreateDirectory(profilePicturesPath);
                return profilePicturesPath;
            }
        }
        
        private static string GetCardIdPictureFolder()
        {
            string profilePicturesPath = Path.Combine(BaseDirectory, "Students-Card-Id-Pictures");


            bool exists = Directory.Exists(profilePicturesPath);
            if (exists)
            {
                return profilePicturesPath;
            }
            else
            {
                Directory.CreateDirectory(profilePicturesPath);
                return profilePicturesPath;
            }
        }

        public static async Task<string> SaveProfilePictureFile(IFormFile ImageFile)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);

            string filePath = Path.Combine(ProfilePictureFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await ImageFile.CopyToAsync(stream);
            }

            return filePath;
        }
        
        public static async Task<string> SaveCardIdPictureFile(IFormFile ImageFile)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);

            string filePath = Path.Combine(CardIdPictureFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await ImageFile.CopyToAsync(stream);
            }

            return filePath;
        }
        public static bool IsValidImage(IFormFile imageFile)
        {
            HashSet<string> allowedImageExtensions =
                new HashSet<string>(StringComparer.OrdinalIgnoreCase)
                {
                    ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".webp"
                };
            if (imageFile == null || imageFile.Length == 0)
            {
                return false;
            }

            if (!allowedImageExtensions.Contains(Path.GetExtension(imageFile.FileName)))
            {
                return false;
            }

            return true;
        }

    }
}
