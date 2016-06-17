using System;
using System.IO;
using Newtonsoft.Json;
using JsonNetCore.Models;

namespace JsonNetCore
{
    public static class JsonSerializerDemo
    {
        public static void ShowJsonSerializer() {
            Console.Clear();
            Console.WriteLine("*** JsonSerializer Demo ***");

            Person alex = new Person();
            alex.Name = "Alex Griciuc";
            alex.DateOfBirth = new DateTime(1983, 11, 13);

            JsonSerializer serializer;

            Console.WriteLine("- Writing json to file");
            // Using the JsonSerializer class we have a much greater control over
            // the serializer settings.
            serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;
            serializer.Formatting = Formatting.Indented;

            // IMPORTANT: A difference in .Net Core api vs .net4.5 is that the StreamWriter no longer
            // accepts a string as a param to the ctor. We have to use the static File.Open or a new
            // instance of FileStream() class to control the path. This is because .Net core is designed
            // to be portable, and these two file classes handle different filesystems
            using(StreamWriter sw = new StreamWriter(File.Open("myjsonfile.json", FileMode.Create)))
            {
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, alex);
                }
            }
            Console.WriteLine("- Done");
            Console.ReadLine();
        }

        public static void JsonTextReaderDemo() {
            // JsonConvert and JsonSerializer use reflections to read the json text and convert
            // it. While both are fairly fast, reflections do come with their performance
            // penalty. An alternative to them is the JsonTextReader, which only reads json
            // - it's not two ways - but it doesn't use reflections, so it's much faster when
            // working with huge json files.
            Console.Clear();
            Console.WriteLine("*** JsonTextReader Demo ***");
            var personDocument = @"{
                'name': 'Alex Griciuc',
                'favorites': [
                    '.Net Core',
                    'Game of Thrones',
                    'Sleeping'
                ],
                'dateOfBirth': '1983-11-13T00:00:00',
                'happy': true,
                'issues': null,
                'car': {
                    'model': 'Renault Symbol 16v',
                    'year': 2008
                },
                'relationship': 1
            }";

            JsonTextReader jsonReader = new JsonTextReader(new StringReader(personDocument));
            while (jsonReader.Read())
            {
                // JsonReader parses the json document one token at a time. A JsonToken is also a { or a [ and
                // of course their closing counterparts. These tokens do not have values, only a token type.
                // To account for that we need to check if a value exists, and act output the token accordingly
                if (jsonReader.Value != null)
                    Console.WriteLine("Token: {0}, Value: {1}", jsonReader.TokenType, jsonReader.Value);
                else
                    Console.WriteLine("Token: {0}", jsonReader.TokenType);
            }
            Console.ReadLine();
        }

        public static void JsonTextWriterDemo() {
            Console.Clear();
            Console.WriteLine("*** JsonTextWriter Demo ***");
            // The JsonTextWriter is the opposite of the JsonTextReader method. It is one way only,
            // so it only writes, does not use reflections (so is higly performant) and it's not cacheable.
            // Like the JsonTextReader method, it is itended to be used with very large json documents that
            // need to squeeze every bit of performance
            // While the JsonTextReader takes a json document and produces a series of json tokens and values,
            // the JsonTextWriter takes a series of tokens and values and produces a json document.

            // In C# strings are immutable, so we need to use a StringWriter, which produces a StringBuilder
            // to build our string, else we might pay a pretty big performance penalty
            var sw = new StringWriter();
            var writer = new JsonTextWriter(sw);

            // Before we start writing our json document, we need to configure any serialization formatters
            // we might want, because writing is done instantly.
            writer.Formatting = Formatting.Indented;

            writer.WriteStartObject();
            writer.WritePropertyName("name");
            writer.WriteValue("Alex Griciuc");
            writer.WritePropertyName("favorites");
            writer.WriteStartArray();
            writer.WriteValue(".NET Core");
            writer.WriteValue("JSON.Net");
            writer.WriteEndArray();
            writer.WritePropertyName("dateOfBirth");
            writer.WriteValue(new DateTime(1983, 11, 13));
            writer.WritePropertyName("happy");
            writer.WriteValue(true);
            writer.WritePropertyName("issues");
            writer.WriteNull();
            // Attention! Nested objects must start with a property name, else JSON.Net
            // will throw an exception
            writer.WritePropertyName("car");
            writer.WriteStartObject();
            writer.WritePropertyName("model");
            writer.WriteValue("Renault Symbol 16v");
            writer.WritePropertyName("year");
            writer.WriteValue(2008);
            // Close the nested object }
            writer.WriteEndObject();
            // Close the parent object }
            writer.WriteEndObject();
            // After we are done writing the json document we need to Flush the writer stream.
            writer.Flush();

            // Finally to be able to output the string we need to get the StringBuilder from
            // the StringWriter object
            string jsonText = sw.GetStringBuilder().ToString();

            Console.WriteLine(jsonText);
            Console.ReadLine();
        }
    }
}
