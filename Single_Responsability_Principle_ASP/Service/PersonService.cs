using Single_Responsability_Principle_ASP.Models;
using System.Security.Cryptography.X509Certificates;

namespace Single_Responsability_Principle_ASP.Service

{
    public class PersonService
    {
        public void Create(Person person)
        {
            var databaseService = new DatabaseService();
            var log = new LogService();

            databaseService.Save(person);
            log.Save($"{person.name} has been saved in the log");
        }
    }
}
