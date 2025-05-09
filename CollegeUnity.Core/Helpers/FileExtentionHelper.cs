using CollegeUnity.Core.Entities;
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
        private static readonly string RootPath = Directory.GetParent(Directory.GetCurrentDirectory()).FullName + "/Files";

        public static readonly string ProfilePictureFolder = GetProfilePicturePath();

        public static readonly string CardIdPictureFolder = GetCardIdPictureFolder();

        static FileExtentionhelper()
        {
            Directory.CreateDirectory(RootPath);
        }

        public static string ConvertBaseDirctoryToBaseUrl(string path)
        {
            return path.Replace("D:/Sites/site22988/", "http://collageunity.runasp.net/");
        }

        public static string ConvertToUrlPath(string filePath)
        {            
            return filePath.Replace("\\", "/");
        }

        public static string GetPostPicturePath(int postId, IFormFile file)
        {
            string datePath = DateTime.Now.ToString("yyyy/MM");
            string postPath = Path.Combine(RootPath, "posts", datePath, postId.ToString(), "pictures");

            Directory.CreateDirectory(postPath);

            string fullFilePath = Path.Combine(postPath, file.FileName);

            return ConvertToUrlPath(fullFilePath);
        }

        public static string GetCardPicturePath(IFormFile file)
        {
            string cardPicturePath = Path.Combine(RootPath, "Card-Pictures");

            Directory.CreateDirectory(cardPicturePath);

            var fileName = Guid.NewGuid().ToString() + file.FileName;

            string fullPath = Path.Combine(cardPicturePath, fileName);

            return ConvertToUrlPath(fullPath);
        }

        public static string GetScheduleFilesPath(IFormFile scheduleFiles)
        {
            string scheduleFilePath = Path.Combine(RootPath, "Schedule");

            Directory.CreateDirectory(scheduleFilePath);

            var fileName = Guid.NewGuid().ToString() + scheduleFiles.FileName;

            string fullFilePath = Path.Combine(scheduleFilePath, fileName);

            return ConvertToUrlPath(fullFilePath);
        }

        public static string GetProfilePicturePath(IFormFile file)
        {
            string baseDirectory = Path.Combine(RootPath, "accounts", "profile-pictures");
            string accountPath = Path.Combine(baseDirectory);

            Directory.CreateDirectory(accountPath);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            string fullFilePath = Path.Combine(accountPath, fileName);

            return ConvertToUrlPath(fullFilePath);
        }

        public static string GetscheduleFilePicturePath(IFormFile file)
        {
            string baseDirectory = Path.Combine(RootPath, "schedules");
            string accountPath = Path.Combine(baseDirectory);

            Directory.CreateDirectory(accountPath);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            string fullFilePath = Path.Combine(accountPath, fileName);

            return ConvertToUrlPath(fullFilePath);
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
            string profilePicturesPath = Path.Combine(RootPath, "Profile-Pictures");


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
            string profilePicturesPath = Path.Combine(RootPath, "Students-Card-Id-Pictures");


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
            if (imageFile is null || imageFile.Length == 0)
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
