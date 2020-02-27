using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace test.utils
{
    class TimeUtils
    {
        public static long getDuration(DateTime startDate, DateTime endDate)
        {
            TimeSpan timeSpan = endDate - startDate;
            long duration = Convert.ToInt64(timeSpan.TotalMilliseconds);
            Debug.WriteLine($"duration = {duration} ms");

            return duration;
        }

        public static long getDuration(Action action)
        {

            DateTime startDate = DateTime.Now;

            action.Invoke();

            DateTime endDate = DateTime.Now;

            return getDuration(startDate, endDate);
        }

    }
}
