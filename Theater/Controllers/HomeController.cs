using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SendAndStore.Models;
using System;
using MySql.Data;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Theater.Models;
using Database.Database;
using Theater.Database;

namespace Theater.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public IActionResult Index()
        {
            // lijst met producten ophalen
            var products = GetAllVoorstellingen();

            // de lijst met producten in de html stoppen
            return View(products);
        }

        public List<Voorstelling> GetAllVoorstellingen()
        {
            // alle producten ophalen uit de database
            var rows = DatabaseConnector.GetRows("select * from voorstelling");

            // lijst maken om alle producten in te stoppen
            List<Voorstelling> voorstellingen = new List<Voorstelling>();

            foreach (var row in rows)
            {
                // Voor elke rij maken we nu een product
                Voorstelling p = new Voorstelling();
                p.Id = Convert.ToInt32(row["id"]);
                p.Naam = row["naam"].ToString();
                p.Beschrijving = row["beschrijving"].ToString();
                p.Datum = DateTime.Parse(row["datum"].ToString());
                p.Poster = row["poster"].ToString();
                

                // en dat product voegen we toe aan de lijst met producten
                voorstellingen.Add(p);
            }

            return voorstellingen;
        }

        [Route("titanic")]
        public IActionResult Titanic()
        {
            return View();
        }

        [Route("show-all")]
        public IActionResult ShowAll()
        {
            return View();
        }
        [Route("contact")]

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [Route("contact")]
        public IActionResult Contact(Person person)
        {
            if (ModelState.IsValid)
                return Redirect("/Succes");
            return View(person);
        }

        [Route("succes")]
        public IActionResult Succes()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [Route("voorstelling/{id}/{naam}")]
        public IActionResult VoorstellingDetails(int id, string naam)
        {
            var Voorstelling = GetVoorstelling(id);

            return View(Voorstelling);
        }

        public Voorstelling GetVoorstelling(int id)
        {
            // alle producten ophalen uit de database
            var rows = DatabaseConnector.GetRows($"select * from voorstelling where id = {id}");

            // lijst maken om alle producten in te stoppen
            List<Voorstelling> voorstellingen = new List<Voorstelling>();

            foreach (var row in rows)
            {
                // Voor elke rij maken we nu een product
                Voorstelling p = new Voorstelling();
                p.Id = Convert.ToInt32(row["id"]);
                p.Naam = row["naam"].ToString();
                p.Beschrijving = row["beschrijving"].ToString();
                p.Datum = DateTime.Parse(row["datum"].ToString());


                // en dat product voegen we toe aan de lijst met producten
                voorstellingen.Add(p);
            }

            return voorstellingen[0];
        }
    }

}
