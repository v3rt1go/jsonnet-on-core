using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace JsonNetCore
{
    public static class HandlingJsonErrorsDemo
    {
        private const string JsonDates = @"[
            '1978-10-30T00:00:00Z',
            '1978-30-10T00:00:00Z',
            'Error in the making',
            [1],
            '1979-08-26T00:00:00Z',
            null
            ]";
        private static IList<string> ErrorLog = new List<string>();

        public static void ShowTryCatchErrors() {
            // The simples and most straight forward way to handle deserialization errors
            // is to put the code that performs deserialization in a try catch block
            Console.Clear();
            Console.WriteLine("*** Deserialize Error - try-catch block ***");

            try
            {
                // When the converter reaches a value that cannot be serialized into a datetime object
                // it will throw, and the exception will be handled by the catch block.
                List<DateTime> deserializedDates = JsonConvert.DeserializeObject<List<DateTime>>(JsonDates);
                Console.WriteLine(deserializedDates.Count);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to deserialize object: " + ex.Message);
            }
            Console.ReadLine();
        }

        public static void ShowInlineDelegateDemo() {
            Console.Clear();
            Console.WriteLine("*** Conversion Errors with Delegate ***");
            // We create an in memory list to store our errors.
            var errors = new List<string>();

            JsonSerializerSettings jSS = new JsonSerializerSettings
            {
                // A more elegant method of handling errors when deserializing an object with
                // JSON.Net is to pass a delegate to the Error property of JsonSerializerSettings
                // This delegate takes an Newtonsoft.Json.Serialization.ErrorEventArgs argument
                // that will hold the error information
                Error = delegate(object sender, Newtonsoft.Json.Serialization.ErrorEventArgs errorArgs)
                {
                    // We can then add the error message to our errors list
                    errors.Add(errorArgs.ErrorContext.Error.Message);
                    // And finally mark the error as handled, so JSON.Net will continue execution
                    // and not throw, stopping our program to run
                    errorArgs.ErrorContext.Handled = true;
                },
                Converters = { new IsoDateTimeConverter() }
            };

            // We can pass the JsonSerializerSettings object to our deserializer and errors will be handled
            List<DateTime> deserializedDates = JsonConvert.DeserializeObject<List<DateTime>>(JsonDates, jSS);

            Console.WriteLine("Dates:");
            foreach (DateTime date in deserializedDates)
            {
                // In net4.5 we would've used .ToShortDateString(), but this API has been removed in
                // .NET Core, because it's an equivalent of .ToString("d");
                // The equivalents to use in .NET Core are:
                /*
                *   ToShortDateString use DateTime.ToString("d")
                *   ToShortTimeString use DateTime.ToString("t")
                *   ToLongDateString use DateTime.ToString("D")
                *   ToLongTimeString use DateTime.ToString("T")
                */
                Console.WriteLine(date.ToString("d"));
            }

            Console.WriteLine("Errors:");
            foreach (var err in errors)
            {
                Console.WriteLine(err);
            }

            Console.ReadLine();
        }

        public static void ShowExternalDelegateDemo() {
            Console.Clear();
            Console.WriteLine("*** Conversion Errors with Method Delegate ***");
            // Like in the method above, the same can be achieved while using a method delegate

            JsonSerializerSettings jSS = new JsonSerializerSettings
            {
                Error = HandleDeserializationError,
                Converters = { new IsoDateTimeConverter() }
            };

            List<DateTime> deserializedDates = JsonConvert.DeserializeObject<List<DateTime>>(JsonDates, jSS);

            Console.WriteLine("Dates:");
            foreach (DateTime date in deserializedDates)
            {
                Console.WriteLine(date.ToString("d"));
            }
            Console.WriteLine("Errors:");
            foreach (var error in ErrorLog)
            {
                Console.WriteLine(error);
            }
            Console.ReadLine();
        }

        private static void HandleDeserializationError(object sender, ErrorEventArgs errorArgs)
        {
            var currentError = errorArgs.ErrorContext.Error.Message;
            ErrorLog.Add(currentError);
            errorArgs.ErrorContext.Handled = true;
        }
    }
}
