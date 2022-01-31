using Examples.DependencyInjection.Interfaces;
using Examples.DependencyInjection.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Examples.DependencyInjection.Repositories
{
    public class PizzaRepo : IPizzaRepo
    {
        private readonly ApplicationSettings _settings;
        private readonly ILogger<PizzaRepo> _logger;

        private readonly Random _random = new Random();

        public PizzaRepo (IOptions<ApplicationSettings> options, ILogger<PizzaRepo> logger)
        {
            _settings = options.Value;
            _logger = logger;
        }

        public async Task<IEnumerable<Pizza>> GetPizzas()
        {
            _logger.LogDebug("Mocking the Task of getting some pizzas from a db/api");

            // example of how I could grab a connection string or api credential from IOptions
            var connectionString = _settings.PizzaConnectionString;

            // example of db or something throwing exception due to invalid connection string
            if (connectionString != "iLovePizza")
            {
                throw new Exception("Ruh roh, you don't love pizza");
            }

            await Task.Delay(1000);

            return new List<Pizza>()
            {
                new Pizza()
                {
                    Name = "Cheese",
                    RawPrice = _random.Next(15, 25)
                },
                new Pizza()
                {
                    Name = "Pepperoni",
                    RawPrice = _random.Next(15, 25)
                },
                new Pizza()
                {
                    Name = "Meat Lovers",
                    RawPrice = _random.Next(15, 25)
                },
                new Pizza()
                {
                    Name = "Hawaiian",
                    RawPrice = _random.Next(15, 25)
                },
                new Pizza()
                {
                    Name = "Veggie",
                    RawPrice = _random.Next(15, 25) 
                },
                new Pizza()
                {
                    Name = "Chicken Bacon Ranch",
                    RawPrice = _random.Next(15, 25)
                },
                new Pizza()
                {
                    Name = "Buffalo",
                    RawPrice = _random.Next(15, 25)
                }
            };
        }
    }
}
