using System;
using System.Globalization;

namespace UtilitySale.Utility
{
    public static class Utilities
    {
        public static string Date( this DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(date).ToString() + "/" + pc.GetMonth(date).ToString("00") + "/" + pc.GetDayOfMonth(date).ToString("00");
        }

        public static int sum(int s1)
        {
            return sum(s1);
        }
    }
}
