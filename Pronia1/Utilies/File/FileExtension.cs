using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Pronia1.Utilies.File
{
    public static class FileExtension
    {
        public static bool CheckSize(this IFormFile file,int kb)
        {
            if (file.Length / 1024 > kb) return true;
            return false;
           
        }
        public static bool CheckType(this IFormFile file, string type)
        {
            if (file.ContentType.Contains(type)) return true;
            return false;

        }
        public async static Task<string> SaveFileAsync(this IFormFile file, string savepath)
        {
           string fileName= Guid.NewGuid().ToString() + file.FileName;
            string path = Path.Combine(savepath, fileName);
            FileStream stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);
            return fileName;
        }
        public static void DeleteFile(string path)
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
    }
}
