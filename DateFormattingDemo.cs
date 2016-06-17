using System;
using JsonNetCore.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace JsonNetCore
{
    public class DateFormattingDemo
    {
        public static void ShowDatesDemo() {
            Console.Clear();
            Console.WriteLine("*** Dates demo ***");

            Person author = new Person();
            author.Name = "Alex";
            author.DateOfBirth = new DateTime(1983, 11, 13, 23, 00, 00);

            // By default JSON.Net uses ISO 8601 date format
            Console.WriteLine("- Serialize object without specifying any date format (default)");
            string dateDefault = JsonConvert.SerializeObject(author, Formatting.Indented);
            Console.WriteLine(dateDefault);

            // Previously (before .net4.5) it was using the proprietary Microsoft Date Format
            Console.WriteLine("- Serialize object specifying Microsoft Date - Default pre .NET 4.5");
            JsonSerializerSettings settingsMicrosoftDate = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
            };
            string dateMicrosoftOldDefault = JsonConvert.SerializeObject(author, Formatting.Indented, settingsMicrosoftDate);
            Console.WriteLine(dateMicrosoftOldDefault);

            // We can control what type of iso format JSON will use to serialize
            Console.WriteLine("- Serialize object specifying to use Iso DateTime converter");
            string dateIso8601 = JsonConvert.SerializeObject(author, Formatting.Indented, new IsoDateTimeConverter());
            Console.WriteLine(dateIso8601);

            // Or we can give it a string representation of a date format that we want to use (Most used)
            Console.WriteLine("- Serialize object specifying custom date format");
            JsonSerializerSettings settingsCustomDate = new JsonSerializerSettings
            {
                DateFormatString = "dd/MM/yyyy"
            };
            string dateCustom = JsonConvert.SerializeObject(author, Formatting.Indented, settingsCustomDate);
            Console.WriteLine(dateCustom);

            // Additionally if we want UNIX time we can use JavaScriptDateTimeConverter which gives us the
            // # of miliseconds that passed since 01.01.1970
            Console.WriteLine("- Serialize object specifying to use JavaScript DateTime converter");
            string dateJavaScript = JsonConvert.SerializeObject(author, Formatting.Indented, new JavaScriptDateTimeConverter());
            Console.WriteLine(dateJavaScript);

            Console.ReadLine();
        }
    }
}
