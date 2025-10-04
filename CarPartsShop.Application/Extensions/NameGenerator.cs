using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;

namespace CarPartsShop.Application.Extensions
{
    public static class NameGenerator
    {
        public static string FileNameGenerator(this IFormFile formFile)
        {
            return Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
        }
        public static string GenerateSlug(this string input, bool supportPersian = true)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            // تبدیل حروف عربی به فارسی
            var arabicChars = new[] { 'ك', 'ئ', 'ي' };
            var persianChars = new[] { 'ک', 'ی', 'ی' };
            for (int i = 0; i < arabicChars.Length; i++)
            {
                input = input.Replace(arabicChars[i], persianChars[i]);
            }

            // حذف اعداد انگلیسی (اختیاری)
            // input = Regex.Replace(input, @"[0-9]+", "");

            // تبدیل به حروف کوچک
            var lowerCase = input.ToLowerInvariant();

            // الگوی کاراکترهای مجاز
            var pattern = supportPersian
                ? @"[^a-z0-9\u0600-\u06FF\-]"
                : @"[^a-z0-9\-]";

            // جایگزینی کاراکترهای غیرمجاز
            var cleaned = Regex.Replace(lowerCase, pattern, "-");

            // بهینه‌سازی نهایی
            var finalSlug = Regex.Replace(cleaned, @"-+", "-").Trim('-');

            return finalSlug;
        }

    }
}
