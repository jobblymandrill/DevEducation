using System;
using System.Globalization;

namespace ElectronicsStore.Core
{
    public static class DateValidation
    {
        public static bool IsValidDate(string value)
        {
            var dateFormat = "dd.MM.yyyy";
            bool validDate = DateTime.TryParseExact(value, dateFormat, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out DateTime tempDate);
            if (validDate) { return true; }
            else { return false; }
        }
    }
}
