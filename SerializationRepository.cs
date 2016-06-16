using System;
using System.Dynamic;
using System.Collections.Generic;
using JsonNetCore.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace JsonNetCore
{
    public static class SerializationRepository
    {
        private const string PersonDocument = @"{
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
        private static Person PersonObject {get; set;}

        public static void OutputJson() {
            Console.Clear();
            Console.WriteLine("Step 1: Output JSON");
            Console.WriteLine(PersonDocument);
            Console.ReadLine();
        }

        public static void DeserializeJson() {
            Console.Clear();
            Console.WriteLine(Environment.NewLine + "Step 2: Output property Person.Name from deserialized class");
            PersonObject = JsonConvert.DeserializeObject<Person>(PersonDocument);
            Console.WriteLine($"Person's name is: {PersonObject.Name}");
            Console.ReadLine();
        }

        public static void SerializeJson() {
            Console.Clear();
            Console.WriteLine(Environment.NewLine + "Step 3: Output serialized Person class");
            var personObjectSerialized = JsonConvert.SerializeObject(PersonObject);
            Console.WriteLine(personObjectSerialized);
            Console.ReadLine();
        }

        public static void SerializeJsonIndented() {
            Console.Clear();
            Console.WriteLine(Environment.NewLine + "Step 4: Output serialized Person class with indentation");
            var personObjectSerialized = JsonConvert.SerializeObject(PersonObject, Formatting.Indented);
            Console.WriteLine(personObjectSerialized);
            Console.ReadLine();
        }

        public static void SerializeJsonCamelCasedIndented() {
            Console.Clear();
            Console.WriteLine(Environment.NewLine + "Step 5: Output serialized Person class with indentation and camelCase");

            var personObjectSerialized = JsonConvert.SerializeObject(PersonObject, Formatting.Indented,
                new JsonSerializerSettings {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }
            );

            Console.WriteLine(personObjectSerialized);
            Console.ReadLine();
        }

        public static void DynamicObjectsDemo() {
            Console.Clear();
            Console.WriteLine("***  Dynamic and ExpandoObject Demo ***");

            Console.WriteLine("- Serialize");
            // We use System.Dynamic.ExpandoObject class to create our dynamic object
            dynamic authorDynamic = new ExpandoObject();
            authorDynamic.FriendlyName = "Alex Griciuc";
            authorDynamic.Likes = new List<string>() { "JSON.Net", ".NET Core", "Learning" };
            authorDynamic.Happy = true;
            string jsonDynamicAuthor = JsonConvert.SerializeObject(authorDynamic, Formatting.Indented);
            Console.WriteLine(jsonDynamicAuthor);

            Console.WriteLine("- Deserialize");
            // Notice how we don't have to pass an object type anymore.
            // We just pass the json string and deserialize it to a dynamic object
            dynamic authorDeserialized = JsonConvert.DeserializeObject(jsonDynamicAuthor);
            Console.WriteLine("Friendly Name: " + authorDeserialized.FriendlyName);
            Console.ReadLine();
        }

        public static void DeserializeToSpecialTypes() {
            Console.Clear();
            Console.WriteLine("*** Types Demo ***");
            string authorProperties = @"{
                                   'name': 'Alex Griciuc',
                                   'happy': true,
                                   'favorites': 4
                                 }
                                ";

            Console.WriteLine("- var");
            var authorVar = JsonConvert.DeserializeObject(authorProperties);
            Console.WriteLine(authorVar);

            Console.WriteLine("- Dictionary");
            Dictionary<string, string> authorDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(authorProperties);
            Console.WriteLine(authorDictionary["name"]);

            Console.WriteLine("- Anonymous types");
            var authorAnonymousTypeDefinition = new
            {
                Name = string.Empty,
                Happy = false,
                Favorites = 0,
                SomeOtherProp = string.Empty
            };

            // Notice how we use DeserializeAnonymousType instead of DeserializeObject
            // to perform deserialization to an anonymous type
            var authorAnonymous = JsonConvert.DeserializeAnonymousType(authorProperties, authorAnonymousTypeDefinition);
            Console.WriteLine(authorAnonymous.Name);
            Console.ReadLine();
        }
    }
}
