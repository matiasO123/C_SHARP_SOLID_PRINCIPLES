using Single_Responsability_Principle_ASP.Models;
using System.Xml.Linq;
namespace Single_Responsability_Principle_ASP.Service
{
    public class DatabaseService
    {
        //The implementation is not correct, but this is only to show the Single Resp. Princ.
        public void Save(Person person) {
            Console.WriteLine("Saving in database...");
            Console.WriteLine($"{person.name} has been saved in the database");
            Console.WriteLine("Saved. Closing database...");
        }
    }
}
