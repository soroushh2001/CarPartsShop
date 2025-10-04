using System.Globalization;

namespace CarPartsShop.Application.Extensions
{
    public static class DateConvertor
    {
        public static string ToShamsiDate(this DateTime dateTime)
        {
            PersianCalendar pc = new PersianCalendar();
            string year = pc.GetYear(dateTime).ToString();
            string month = pc.GetMonth(dateTime).ToString();
            string day = pc.GetDayOfMonth(dateTime).ToString();

            return $"{year}/{month}/{day} {dateTime.Hour.ToString("00")}:{dateTime.Minute.ToString("00")}";
        }
    }
}
