using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Pizza_app
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> pizzaNames = new();

            var allPizzasList = JsonConvert.DeserializeObject<List<Pizza>>(File.ReadAllText("Pizzas.json"));

            foreach (var pizza in allPizzasList)
            {
                StringBuilder pizzaTopping = new();

                foreach (var topping in pizza.toppings.OrderBy(x => x))
                {
                    pizzaTopping.AppendFormat(topping + ", ");
                }

                pizzaNames.Add(pizzaTopping.Remove(pizzaTopping.Length - 2, 1).ToString());
            }

            int pizzaTopCount = 20;

            var topPizzas = pizzaNames.GroupBy(x => x)
              .Select(y => new { Element = y.Key, Counter = y.Count() })
              .OrderByDescending(u => u.Counter)
              .Take(pizzaTopCount)
              .ToList();

            foreach (var pizza in topPizzas)
            {
                Console.Write($"Pizza: {pizza.Element}");
                Console.CursorLeft = 40;
                Console.Write($"| {pizza.Counter} orders");
                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}
