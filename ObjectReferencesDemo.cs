using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using JsonNetCore.Models;

namespace JsonNetCore
{
    public static class ObjectReferencesDemo
    {
        public static void ShowObjectReferences() {
            Console.Clear();
            Console.WriteLine("*** Object References Demo***");

            Person alex = new Person() { Name = "Alex Griciuc", Favorites = new string[] {".NET Core", "JSON.Net", "foo bar"}};
            Person nicky = new Person() { Name = "Nicky Griciuc", Favorites = new string[] {"Marketing", "commercials", "google"}};
            Person emma = new Person() { Name = "Emma Griciuc", Favorites = new string[] {"sweets", "toys", "cartoons"}};
            Person silviu = new Person() { Name = "Dan Silviu", Favorites = new string[] {"bikes", "cars", "trams"}};

            // Including references to our own object creates a circular reference.
            // In this case, alex is the object and also has a reference to himself
            // in the Family list. Circular references cause issues when deserialized
            // in C# classes, but JSON.Net has a workaround for this
            alex.Family = new List<Person> {alex, nicky, emma, nicky, emma};
            string alexJson = JsonConvert.SerializeObject(alex, new JsonSerializerSettings {
                // With PreserveReferencesHandling we tell JSON.Net to preserve objects.
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                Formatting = Formatting.Indented
            });
            Console.WriteLine(alexJson);
            Console.ReadLine();
        }
    }
}
