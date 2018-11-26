using System;

namespace xTab.Tools.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime ParseDate(String date, DateTime defaultDate, DefaultTimeOfDay defaultTime = DefaultTimeOfDay.BeginOfDay)
        {
            DateTime parsedDate;
            DateTime result;

            if (DateTime.TryParse(date, out parsedDate))
                result = parsedDate;
            else
                result = defaultDate;

            return SetTimeOfDay(result, defaultTime).Value;
        }

        public static DateTime? ParseDateNullable(String date, DateTime? defaultDate = null, DefaultTimeOfDay defaultTime = DefaultTimeOfDay.BeginOfDay)
        {
            DateTime parsedDate;
            DateTime? result;

            if (DateTime.TryParse(date, out parsedDate))
                result = parsedDate;
            else
                result = defaultDate;

            return SetTimeOfDay(result, defaultTime);
        }

        public static String DateString(DateTime date, Boolean endOfDay = false, String format = "dd.MM.yyyy")
        {
            return NullableDateTimeString(date, null, endOfDay, format);
        }

        public static String DateString(DateTime date, DateTime defaultDate, Boolean endOfDay = false, String format = "dd.MM.yyyy")
        {
            return NullableDateTimeString(date, defaultDate, endOfDay, format);
        }

        public static String DateTimeString(DateTime date, Boolean endOfDay = false, String format = "dd.MM.yyyy HH:mm")
        {
            return NullableDateTimeString(date, null, endOfDay, format);
        }
        
        public static String DateTimeString(DateTime date, DateTime defaultDate, Boolean endOfDay = false, String format = "dd.MM.yyyy HH:mm")
        {
            return NullableDateTimeString(date, defaultDate, endOfDay, format);
        }

        public static String NullableDateTimeString(DateTime? Date, DateTime? DefaultDate = null, Boolean EndOfDay = false, String Format = "dd.MM.yyyy HH:mm")
        {
            var result = DefaultDate;

            if (Date.HasValue)
                result = EndOfDay ? Date.Value.Date.AddSeconds(86399) : Date.Value;

            if (result.HasValue)
                return result.Value.ToString(Format);
            else
                return null;
        }

        public static TimeSpan GetDbReadyTimeSpan()
        {
            return new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        }

        public static TimeSpan GetDbReadyTimeSpan(DateTime Date)
        {
            return new TimeSpan(Date.Hour, Date.Minute, Date.Second);
        }

        public static TimeSpan GetDbReadyTimeSpan(TimeSpan Time)
        {
            return new TimeSpan(Time.Hours, Time.Minutes, Time.Seconds);
        }

        public enum DefaultTimeOfDay
        {
            None, BeginOfDay, EndOfDay
        }

        private static DateTime? SetTimeOfDay(DateTime? date, DefaultTimeOfDay defaultTime)
        {
            if (date.HasValue)
            {
                switch (defaultTime)
                {
                    case DefaultTimeOfDay.None:
                        return date;
                    case DefaultTimeOfDay.BeginOfDay:
                        return date.Value.Date;
                    case DefaultTimeOfDay.EndOfDay:
                        return date.Value.Date.AddDays(1).AddSeconds(-1);
                    default:
                        return date;
                }
            }
            else
                return date;
        }
    }
}
