using System;
using System.Diagnostics;

namespace csf.main.utils
{
    class TimeUtils
    {
        public static long GetDuration(DateTime startDate, DateTime endDate)
        {
            TimeSpan timeSpan = endDate - startDate;
            long duration = Convert.ToInt64(timeSpan.TotalMilliseconds);
            Debug.WriteLine($"duration = {duration} ms");

            return duration;
        }

        public static long GetDuration(Action action)
        {

            DateTime startDate = DateTime.Now;

            action.Invoke();

            DateTime endDate = DateTime.Now;

            return GetDuration(startDate, endDate);
        }

    }
}
