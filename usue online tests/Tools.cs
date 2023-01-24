using System;
using System.Reflection.Metadata;
using usue_online_tests.Migrations;

namespace usue_online_tests
{
    public static class Tools
    {
        public static DateTime ToNowEkb(this DateTime time)
        {
            return DateTime.Now.AddHours(5);
            TimeZoneInfo ekaterinburgTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Ekaterinburg Standard Time");
            DateTime ekaterinburgDateTime = TimeZoneInfo.ConvertTime(DateTime.UtcNow, ekaterinburgTimeZone);
            return ekaterinburgDateTime;
        }
    }
}
