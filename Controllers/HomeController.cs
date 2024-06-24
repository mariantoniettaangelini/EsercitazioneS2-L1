using Esercitazione.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Esercitazione.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public Dictionary<int, (string, double)> menu { get; set; }
        public List<int> ordini{ get; set; }
        public double totale { get; set; }

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index() //MAIN
        {
            //MENU

            Dictionary<int, (string, double)> menu = new Dictionary<int, (string, double)>()
            {
                { 1, ("Coca Cola 150 ml", 2.50) },
                { 2, ("Insalata di pollo", 5.20) },
                { 3, ("Pizza Margherita", 10.00) },
                { 4, ("Pizza 4 Formaggi", 12.50) },
                { 5, ("Patatine Fritte", 3.50) },
                { 6, ("Insalata di Riso", 8.00) },
                { 7, ("Frutta", 5.00) },
                { 8, ("Pizza Fritta", 5.00) },
                { 9, ("Piadina", 6.00) },
                { 10, ("Hamburger", 7.90) },
            };


            List<int> ordini = new List<int>();
            int scegli;

            do
            {
                foreach (var prodotto in menu)
                {
                    Console.WriteLine($"{prodotto.Key}: {prodotto.Value.Item1} (euro {prodotto.Value.Item2:F2})");
                }
                Console.WriteLine("11: Stampa conto");
                Console.Write("Scegli un'opzione da 1 a 11: ");

                if (int.TryParse(Console.ReadLine(), out scegli) && scegli >= 1 && scegli <= 11)
                {
                    if (scegli == 11)
                    {
                        StampaConto(menu, ordini); break;
                    }
                    else if (scegli >= 1 && scegli <= 10)
                    {
                        ordini.Add(scegli);
                        Console.WriteLine($"Hai aggiunto {menu[scegli].Item1} - prezzo {menu[scegli].Item2:F2}");
                    }
                }
                else
                {
                    Console.WriteLine("Il prodotto non esiste!");
                }
            }
            while (scegli != 11);

            //METODO PER STAMPARE CONTO
             static void StampaConto(Dictionary<int, (string, double)> menu, List<int> ordini)
            {
                if(ordini.Count == 0)
                {
                    Console.WriteLine("Nessun ordine");
                }
                else
                {
                    Console.WriteLine("CONTO: ");
                    double totale = 0;
                    foreach (int ordine in ordini)
                    {
                        totale += menu[ordine].Item2;
                        Console.WriteLine($"RIEPILOGO ORDINE: {menu[ordine].Item1} - euro {menu[ordine].Item2}");
                    }
                    Console.WriteLine("Servizio al tavolo - euro 3.00");
                    totale += 3;
                    Console.WriteLine($"TOTALE DA PAGARE: euro {totale:F2}");
                }
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
