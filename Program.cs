using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JsonNetCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Step 1: Output the JSON document
            SerializationRepository.OutputJson();
            // Step 2: Deserialize the JSON document;
            SerializationRepository.DeserializeJson();
            // Step 3: Serialize a C# object to a JSON document
            SerializationRepository.SerializeJson();
            // Step 4: Serialize a C# object to a JSON document with indentation
            SerializationRepository.SerializeJsonIndented();
            // Step 5: Serialize a C# object to a JSON document with indentation and camel case
            SerializationRepository.SerializeJsonCamelCasedIndented();
            // Object References
            ObjectReferencesDemo.ShowObjectReferences();
            // Dynamic Objects
            SerializationRepository.DynamicObjectsDemo();
            // Deserialize to special types
            SerializationRepository.DeserializeToSpecialTypes();
            JsonSerializerDemo.ShowJsonSerializer();
            JsonSerializerDemo.JsonTextReaderDemo();
            JsonSerializerDemo.JsonTextWriterDemo();
            DateFormattingDemo.ShowDatesDemo();
            HandlingJsonErrorsDemo.ShowTryCatchErrors();
            HandlingJsonErrorsDemo.ShowInlineDelegateDemo();
            HandlingJsonErrorsDemo.ShowExternalDelegateDemo();
        }
    }
}
