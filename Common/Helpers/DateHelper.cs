using System;

namespace Common.Helpers
{
    public static class DateHelper
    {
        public static string GetCurrentDateFormatted() =>
            DateTime.UtcNow.ToString(Common.Constants.AppConstants.DateFormat);
    }
}
