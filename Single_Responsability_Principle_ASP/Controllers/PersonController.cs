using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Single_Responsability_Principle_ASP.Models;
using Single_Responsability_Principle_ASP.Service;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace Single_Responsability_Principle_ASP.Controllers
{
    public class PersonController : Controller
    {
        // GET: PersonController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PersonController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Good_Create(Person person)
        {
            if(!ModelState.IsValid) return View();

            try
            {
                //The logic is inside the services. The controller only "give orders"
                var personService = new PersonService();
                personService.Create(person);
            }
            catch { }
            return Ok();
        }


        // POST: PersonController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Bad_Create(int id, string name, int age)
        {
            //Would be better to receive a "Person" in the parameters
            
            //everything is here. If i need some part of the process i have to copy paste it
            //Hard to mantain

            //Saving in the database
            //What if tomorrow we wish to change something in the logic of writing in the database? (Hundred of lines of code should be changed)
            Console.WriteLine("Saving in database...");
            Console.WriteLine($"{name} has been saved in the database");
            Console.WriteLine("Saved. Closing database...");

            //Saving in the log
            //The issue with the previous database code we have here in the log code lines
            System.IO.File.AppendAllText($"Saving in log..", "log.txt");
            System.IO.File.AppendAllText($"{name} has been saved in the log", "log.txt");
            System.IO.File.AppendAllText($"Saved. Closing file...", "log.txt");
            return Ok();
        }




    }
}
