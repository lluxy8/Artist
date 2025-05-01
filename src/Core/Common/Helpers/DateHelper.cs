namespace Core.Common.Helpers
{
    public static class DateTimeHelper
    {
        private static readonly TimeZoneInfo TurkeyTimeZone =
            TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");

        /// <summary>
        /// UTC DateTime değerini Türkiye saatine çevirir.
        /// </summary>
        /// <param name="utcDateTime">UTC olarak gelen DateTime</param>
        /// <returns>Türkiye saatine çevrilmiş DateTime</returns>
        public static DateTime ConvertToTurkeyTime(DateTime utcDateTime)
        {
            if (utcDateTime.Kind == DateTimeKind.Unspecified)
            {
                utcDateTime = DateTime.SpecifyKind(utcDateTime, DateTimeKind.Utc);
            }

            return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, TurkeyTimeZone);
        }
    }
}
