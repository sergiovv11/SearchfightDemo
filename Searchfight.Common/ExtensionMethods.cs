using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace Searchfight.Common
{
    public static class ObjectExtensions
    {
        public static T To<T>(this object obj) where T : class
        {
            return obj as T;
        }
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }
        public static bool NotNull(this object obj)
        {
            return obj != null;
        }
        public static bool ToBool(this object obj)
        {
            return Convert.ToBoolean(obj);
        }
        public static int ToInt(this object obj)
        {
            return Convert.ToInt32(obj);
        }
        public static char ToChar(this object obj)
        {
            return Convert.ToChar(obj);
        }
        public static string ToStringDefault<T>(this Nullable<T> obj, string defaultValue) where T : struct
        {
            return obj.HasValue ? obj.Value.ToString() : defaultValue;
        }
        public static string ToStringEmpty(this object obj)
        {
            return obj == null ? string.Empty : obj.ToString();
        }
        public static string ToStringDefault(this object obj, string defaultValue)
        {
            return obj == null ? defaultValue : obj.ToString();
        }
        public static string GetName<T>(Expression<Func<T>> expression)
        {
            return (expression.Body as MemberExpression).Member.Name;
        }
        public static string SerializeToCleanXmlString<T>(this object obj)
        {
            try
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                var emptyNs = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.OmitXmlDeclaration = true;
                using (var memoryStream = new MemoryStream())
                {
                    XmlWriter writer = XmlWriter.Create(memoryStream, settings);
                    xmlSerializer.Serialize(writer, obj, emptyNs);
                    memoryStream.Position = 0;
                    var streamReader = new StreamReader(memoryStream);
                    return streamReader.ReadToEnd();
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }        
    }

    public static class EnumExtensions
    {
        public static int ToInt(this Enum obj)
        {
            return Convert.ToInt32(obj);
        }
    }

    public static class IntExtensions
    {
        public static int ToIntDefault(this int? obj)
        {
            return obj.HasValue ? obj.Value : 0;
        }
        public static int ToIntDefault(this int? obj, int defaultValue)
        {
            return obj.HasValue ? obj.Value : defaultValue;
        }
        public static Nullable<int> ToNullableInt(this int obj)
        {
            return (int?)obj;
        }
        public static string ToFormattedMoneySolesString(this int obj)
        {
            return string.Format(CultureInfo.GetCultureInfo("es-PE"), "{0:C}", obj);
        }
        public static string ToFormattedMoneyDolarString(this int obj)
        {
            return string.Format(CultureInfo.GetCultureInfo("en-US"), "{0:C}", obj);
        }
        public static decimal ToDecimal(this int obj)
        {
            var ui = new CultureInfo("es-PE");
            ui.NumberFormat = new NumberFormatInfo() { NumberDecimalSeparator = "." };
            return Convert.ToDecimal(obj, ui);
        }
        public static string ToFormattedPercentString(this int obj)
        {
            return string.Format(CultureInfo.GetCultureInfo("es-PE"), "{0:P}", obj.ToDecimal() / 100);
        }
    }

    public static class StringExtensions
    {
        public static bool TryParse<T>(this string obj)
        {
            if (typeof(T) == typeof(int))
            {
                var temp = 0;
                return int.TryParse(obj, out temp);
            }

            if (typeof(T) == typeof(char))
            {
                var temp = char.MinValue;
                return char.TryParse(obj, out temp);
            }

            if (typeof(T) == typeof(bool))
            {
                var temp = false;
                return bool.TryParse(obj, out temp);
            }

            return false;
        }
        public static dynamic Parse<T>(this string obj)
        {
            if (typeof(T) == typeof(int))
            {
                return obj.ToInt();
            }

            if (typeof(T) == typeof(char))
            {
                return obj.ToChar();
            }

            if (typeof(T) == typeof(bool))
            {
                return obj.ToBool();
            }

            return obj;
        }
        public static int ToInt(this string obj)
        {
            return Convert.ToInt32(obj);
        }
        public static bool TryParseToInt(this string obj)
        {
            int temp;
            return int.TryParse(obj, out temp);
        }
        public static bool Empty(this string obj)
        {
            return string.IsNullOrWhiteSpace(obj);
        }
        public static bool NotEmpty(this string obj)
        {
            return !string.IsNullOrWhiteSpace(obj);
        }
        public static double ToDouble(this string obj)
        {
            return Convert.ToDouble(obj);
        }
        public static decimal ToDecimal(this string obj)
        {
            var ui = new CultureInfo("es-PE");
            ui.NumberFormat = new NumberFormatInfo() { NumberDecimalSeparator = "." };
            return Convert.ToDecimal(obj, ui);
        }
        public static decimal ToDecimal(this string obj, string culture)
        {
            var ui = new CultureInfo(culture);
            ui.NumberFormat = new NumberFormatInfo() { NumberDecimalSeparator = "." };
            return Convert.ToDecimal(obj, ui);
        }
        public static DateTime ToDatetime(this string obj)
        {
            return DateTime.Parse(obj);
        }
        public static DateTime ToDatetime(this string obj, string format)
        {
            return DateTime.ParseExact(obj, format, null);
        }
        public static DateTime? ToNullableDatetime(this string obj)
        {
            return string.IsNullOrWhiteSpace(obj) ? default(DateTime?) : obj.ToDatetime();
        }
        public static DateTime? ToNullableDatetime(this string obj, double addedDays)
        {
            return string.IsNullOrWhiteSpace(obj) ? default(DateTime?) : obj.ToDatetime().AddDays(addedDays);
        }
        public static byte[] ToBytesFromBase64String(this string obj)
        {
            return Convert.FromBase64String(obj);
        }
        public static Nullable<int> ToNullableInt(this string obj)
        {
            return (int?)obj.ToInt();
        }
        public static Nullable<TimeSpan> ToNullableTimespan(this string obj)
        {
            return string.IsNullOrWhiteSpace(obj) ? default(Nullable<TimeSpan>) : new TimeSpan(obj.Split(':')[0].ToInt(), obj.Split(':')[1].ToInt(), 0).ToNullableTimespan();
        }
        public static string ToStringWithSlash(this object obj)
        {
            return obj.ToString() + "/";
        }
        public static string ToFormattedPercentString(this string obj)
        {
            return string.Format(CultureInfo.GetCultureInfo("es-PE"), "{0:P}", obj.ToDecimal() / 100);
        }
        public static byte[] ToBytes(this string obj)
        {
            return Encoding.UTF8.GetBytes(obj);
        }
        public static string TrimNullable(this string obj)
        {
            return string.IsNullOrWhiteSpace(obj) ? string.Empty : obj.Trim();
        }
        public static string ToStringDefault(this string obj, string defaultValue)
        {
            return string.IsNullOrWhiteSpace(obj) ? defaultValue : obj;
        }
        public static string ToStringEmpty(this string obj)
        {
            return string.IsNullOrWhiteSpace(obj) ? string.Empty : obj;
        }
        public static string ToCapitallize(this string obj)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(obj.ToStringEmpty().ToLower());
        }
        public static string ToCapitallizeFirstLetter(this string obj)
        {
            obj = obj.ToStringEmpty();

            switch (obj.Length)
            {
                case 0:
                    return string.Empty;
                case 1:
                    return obj.ToUpper();
                default:
                    return obj.First().ToString().ToUpper() + obj.Substring(1);
            }
        }
        public static int DayOfWeek(this string obj)
        {
            var dayNames = CultureInfo.CurrentUICulture.DateTimeFormat.DayNames;
            var dayOfweek = 0;

            for (int i = 0; i < dayNames.Length; i++)
            {
                if (dayNames[i] == obj)
                {
                    dayOfweek = i;
                    break;
                }
            }
            return dayOfweek;
        }
        public static int DayOfWeek(this string obj, string culture)
        {
            var dayNames = new CultureInfo(culture).DateTimeFormat.DayNames;
            var dayOfweek = 0;

            for (int i = 0; i < dayNames.Length; i++)
            {
                if (dayNames[i].ToLower() == obj.ToStringEmpty().ToLower())
                {
                    dayOfweek = i;
                    break;
                }
            }
            return dayOfweek;
        }
        public static string ToOriginalFileName(this string obj)
        {
            return string.IsNullOrWhiteSpace(obj) ? string.Empty : obj.Trim().Split(new char[] { '\\', '/' }, StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
        }
        public static string ToDomainUrl(this string obj)
        {
            return string.IsNullOrWhiteSpace(obj) ? string.Empty : obj.Trim().Split(new char[] { '?' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
        }
        public static bool CompareToCaseInsensitive(this string obj, string objToCompare)
        {
            obj = obj ?? string.Empty;
            objToCompare = objToCompare ?? string.Empty;

            return obj.Equals(objToCompare, StringComparison.InvariantCultureIgnoreCase);
        }
		public static string GetValueOrDefault(this string obj)
		  {
				return obj == null ? string.Empty : obj;
		  }
        public static string AsString(this XmlDocument xmlDoc)
        {
            using (StringWriter sw = new StringWriter())
            using (XmlTextWriter tx = new XmlTextWriter(sw))
            {
                xmlDoc.WriteTo(tx);
                return sw.ToString();
            }
        }
        public static T DeserializeJson<T>(this string json)
        {
            var javaScriptSerializer = new JavaScriptSerializer();
            return javaScriptSerializer.Deserialize<T>(json);
        }
    }

    public static class BoolExtensions
    {
        public static bool ToBool(this bool? obj)
        {
            return obj.HasValue ? obj.Value : false;
        }
        public static bool IsTrue(this bool? obj)
        {
            return obj.HasValue && obj.Value;
        }
        public static Nullable<bool> ToNullableBool(this bool obj)
        {
            return (bool?)obj;
        }
    }

    public static class DecimalExtensions
    {
        public static decimal ToDecimal(this decimal? obj)
        {
            return obj.HasValue ? obj.Value : 0;
        }
        public static string ToDecimalDefaultValueString(this decimal? obj, string format, string defaultValue)
        {
            return obj.HasValue ? obj.Value.ToDecimalFormat(format) : defaultValue;
        }
        public static decimal? ToNullableDecimal(this decimal obj)
        {
            return (decimal?)obj;
        }
        public static decimal ToDecimalZeros(this decimal obj, int numberOfDecimals)
        {
            return Math.Round(obj, numberOfDecimals);
        }
        public static string ToDecimalPointFormat(this decimal obj)
        {
            return obj.ToString("0.00", new NumberFormatInfo() { NumberDecimalSeparator = "." });
        }
        public static string ToDecimalFormat(this decimal obj, string format)
        {
            return obj.ToString(format, new NumberFormatInfo() { NumberDecimalSeparator = "." });
        }
        public static string ToFormattedMoneySolesString(this decimal obj)
        {
            return string.Format(CultureInfo.GetCultureInfo("es-PE"), "{0:C}", obj);
        }
        public static string ToFormattedMoneyDolarString(this decimal obj)
        {
            return string.Format(CultureInfo.GetCultureInfo("en-US"), "{0:C}", obj);
        }
        public static string ToMoneyFormat(this decimal obj)
        {
            return string.Format("S/. {0:0.00}", obj);
        }
        public static decimal GetIGV(this decimal obj)
        {
            return obj * 18 / 100;
        }
        public static decimal GetWithoutIGV(this decimal obj)
        {
            return obj * 82 / 100;
        }
    }

    public static class DateTimeExtensions
    {
        public static Nullable<DateTime> ToNullableDatetime(this DateTime date)
        {
            return (DateTime?)date;
        }
        public static string ToFullFormattedDate(this DateTime obj)
        {
            return obj.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
        }
        public static string ToFormattedDate(this Nullable<DateTime> obj)
        {
            return obj.HasValue ? obj.Value.ToFormattedDate() : string.Empty;
        }
        public static string ToFormattedDate(this DateTime obj)
        {
            return obj.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        }
        public static string ToFormattedDate(this DateTime obj, string format)
        {
            return obj.ToString(format, CultureInfo.InvariantCulture);
        }
        public static string ToFormattedTime(this DateTime obj, string format)
        {
            return obj.ToString(format, CultureInfo.InvariantCulture);
        }
        public static string ToTimeHourMinuteFormat(this DateTime obj)
        {
            return obj.ToString(@"hh\:mm");
        }
        public static string ToFileFormattedStringDate(this DateTime obj)
        {
            return obj.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
        }
        public static string ToFileFormattedStringDate(this DateTime obj, string format)
        {
            return obj.ToString(format, CultureInfo.InvariantCulture);
        }
        public static string DayName(this DateTime obj)
        {
            var dayOfWeek = obj.DayOfWeek.ToInt();
            return CultureInfo.CurrentUICulture.DateTimeFormat.DayNames[dayOfWeek];
        }
        public static string DayName(this DateTime obj, string culture)
        {
            var dayOfWeek = obj.DayOfWeek.ToInt();
            return new CultureInfo(culture).DateTimeFormat.DayNames[dayOfWeek];
        }
        public static bool IsBetween(this DateTime obj, DateTime date1, DateTime date2)
        {
            return (obj > date1 && obj < date2);
        }

    }

    public static class TimeSpanExtensions
    {
        public static Nullable<TimeSpan> ToNullableTimespan(this TimeSpan obj)
        {
            return (Nullable<TimeSpan>)obj;
        }
        public static string ToFormattedTime(this DateTime obj)
        {
            return obj.ToString("hh:mm tt", CultureInfo.InvariantCulture);
        }
        public static string ToFormattedTime24Hours(this DateTime obj)
        {
            return obj.ToString("HH:mm", CultureInfo.InvariantCulture);
        }
        public static string ToTimeHourMinuteFormat(this TimeSpan obj)
        {
            return obj.ToString(@"hh\:mm");
        }
        public static string ToTimeHourMinuteFormat(this Nullable<TimeSpan> obj)
        {
            return obj.HasValue ? obj.Value.ToString(@"hh\:mm") : "00:00";
        }
        public static string ToTimeHourMinuteFormat(this Nullable<TimeSpan> obj, Func<TimeSpan, TimeSpan> valueOperation)
        {
            return obj.HasValue ? valueOperation(obj.Value).ToString(@"hh\:mm") : "00:00";
        }
        public static string ToTimeHourMinuteFormat(this Nullable<TimeSpan> obj, string defaultValue)
        {
            return obj.HasValue ? obj.Value.ToString(@"hh\:mm") : defaultValue;
        }
        public static string ToTimeHourMinuteFormat(this Nullable<TimeSpan> obj, Func<TimeSpan, TimeSpan> valueOperation, string defaultValue)
        {
            return obj.HasValue ? valueOperation(obj.Value).ToString(@"hh\:mm") : defaultValue;
        }
        public static TimeSpan AddHours(this TimeSpan obj, int hours)
        {
            return obj.Add(new TimeSpan(hours, 0, 0));
        }
        public static TimeSpan AddMinutes(this TimeSpan obj, int minutes)
        {
            return obj.Add(new TimeSpan(0, minutes, 0));
        }
    }    

    public static class StreamExtensions
    {
        public static byte[] GetBytes(this Stream stream)
        {
            using (var ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}