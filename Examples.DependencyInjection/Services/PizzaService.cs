using Examples.DependencyInjection.Interfaces;
using Examples.DependencyInjection.Models;
using Examples.DependencyInjection.Models.Enums;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examples.DependencyInjection.Services
{
    public class PizzaService : IPizzaService
    {
        private readonly ILogger<PizzaService> _logger;
        private readonly IPizzaRepo _repo;
        private readonly IServiceCharge _flatRateService;
        private readonly IServiceCharge _percentageService;

        public PizzaService(ILogger<PizzaService> logger, 
            IPizzaRepo repo, 
            Func<IServiceRateType, IServiceCharge> serviceRate)
        {
            _logger = logger;
            _repo = repo;
            _flatRateService = serviceRate(IServiceRateType.Flat); ;
            _percentageService = serviceRate(IServiceRateType.Percentage);
        }

        // I am greedy so I am going to apply two different service charges for using this aesome app to get your pizzas
        // Going to apply the flat rate first so the percentage rate is higher
        public async Task<IEnumerable<Pizza>> GetPizzasWithServiceCharge()
        {
            var pizzas = await _repo.GetPizzas();

            _logger.LogInformation($"Original sum of all pizzas: {pizzas.Sum(pizza => pizza.Price)}");

            pizzas = _flatRateService.Apply(pizzas);

            _logger.LogInformation($"Sum of all pizzas after applying flat rate service charge: {pizzas.Sum(pizza => pizza.Price)}");

            pizzas = _percentageService.Apply(pizzas);

            _logger.LogInformation($"Sum of all pizzas after applying percentage rate service charge: {pizzas.Sum(pizza => pizza.Price)}");

            return pizzas;
        }
    }
}
