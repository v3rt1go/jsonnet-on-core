using System;
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
    }
}
