using System;

namespace Bike
{
	public class UTCTimeOperate
	{
		public static double ConvertDateTimeInt(DateTime time)
		{
			DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
			return (time - startTime).TotalSeconds;
		}

		public static DateTime ConvertIntDatetime(double utc)
		{
			return TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)).AddSeconds(utc);
		}

		public static double ConvertDateTimeIntDay(DateTime time)
		{
			DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1899, 12, 30));
			return (time - startTime).TotalDays;
		}
	}
}
