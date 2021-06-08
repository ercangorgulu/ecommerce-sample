using System;

namespace ECommerce.Domain.Services
{
    public class DateTimeService
    {
        private static Func<DateTime> _timeProvider;
        public static DateTime Current => _timeProvider?.Invoke() ?? DateTime.Now;

        public static void SetTime(DateTime dateTime)
            => _timeProvider = () => dateTime;

        public static void SetTime(Func<DateTime> timeProvider)
            => _timeProvider = timeProvider;
    }
}
