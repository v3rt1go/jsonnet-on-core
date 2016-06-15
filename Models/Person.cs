using System;
using System.Collections.Generic;

namespace JsonNetCore.Models
{
     public class Person
    {
        // Public fields
        public string Age;
        public string Country;

        // Public properties
        public string Name { get; set; }
        public string[] Favorites { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Happy { get; set; }
        public object Issues { get; set; }
        public Car Car { get; set; }
        public IList<Person> Family { get; set; }
        public Relationship Relationship { get; set; }
    }
}
