using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Single_Responsability_Principle
{
    //You have the info
    public class Person
    {
        public int id { get; set; }
        public string name { get; set; }
        public int age { get; set; }

        public Person(string name, int age)
        {
            this.age = age;
            this.name = name;
        }
    }


    //You send info
    public class PersonRequest
    {
        public Person person { get; set; }
        public PersonRequest(Person person) { this.person = person; }
        public void Send() { Console.WriteLine($"{person.name} has been saved"); }

    }


    //You save info
    public class PersonDB
    {
        public Person person { get; set; }
        public PersonDB(Person person) { this.person = person;}
        public void Save() { Console.WriteLine("You saved the new person!"); }
    }
}
