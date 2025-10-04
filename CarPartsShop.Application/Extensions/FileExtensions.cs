using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace CarPartsShop.Application.Extensions;

public static class FileExtensions
{
    public static async Task<bool> UploadFile(this IFormFile file, string[] allowedExtensions, string name, string path)
    {
        var fileExtension = Path.GetExtension(file.FileName).ToLower();
        if (!allowedExtensions.Contains(fileExtension))
        {
            return false;
        }

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        var up = path + name;

        await using var stream = new FileStream(up, FileMode.Create);
        await file.CopyToAsync(stream);

        return true;
    }

    public static bool DeleteFile(this string name, string path)
    {
        if (File.Exists(path + name))
        {
            File.Delete(path + name);
            return true;
        }

        return false;
    }

    public static async Task<bool> UploadImage(
        this IFormFile imageFile,
        string fileName,
        string originalPath,
        string thumbPath,
        int thumbWidth = 200,
        int thumbHeight = 200)
    {
        var fileExtension = Path.GetExtension(imageFile.FileName).ToLower();
        var allowedExtensions = new[] { ".jpg",".png",".jpeg",".webp" };
        if (!allowedExtensions.Contains(fileExtension))
        {
            return false;
        }

        if (!Directory.Exists(originalPath))
        {
            Directory.CreateDirectory(originalPath);
        }

        if (!Directory.Exists(thumbPath))
        {
            Directory.CreateDirectory(thumbPath);
        }

        var originalFilePath = Path.Combine(originalPath, fileName);
        var thumbFilePath = Path.Combine(thumbPath, fileName);

        await using (var stream = new FileStream(originalFilePath, FileMode.Create))
        {
            await imageFile.CopyToAsync(stream);
        }

        using (var imageStream = new MemoryStream())
        {
            await imageFile.CopyToAsync(imageStream);
            imageStream.Position = 0;

            using (var image = Image.Load(imageStream))
            {
                var options = new ResizeOptions
                {
                    Size = new Size(thumbWidth, thumbHeight),
                    Mode = ResizeMode.Max
                };

                image.Mutate(x => x.Resize(options));

                var encoder = new JpegEncoder
                {
                    Quality = 75
                };

                await image.SaveAsync(thumbFilePath, encoder);
            }
        }

        return true;
    }

    public static void DeleteImage(this string fileName, string originalPath, string thumbPath)
    {
        var originalFilePath = Path.Combine(originalPath, fileName);
        if (File.Exists(originalFilePath))
        {
            File.Delete(originalFilePath);
        }

        var thumbFilePath = Path.Combine(thumbPath, fileName);
        if (File.Exists(thumbFilePath))
        {
            File.Delete(thumbFilePath);
        }
    }
}