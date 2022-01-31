using Examples.DependencyInjection.Interfaces;
using Examples.DependencyInjection.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace Examples.DependencyInjection.Services
{
    public class PercentageRateServiceChargeService : IServiceCharge
    {
        private readonly ApplicationSettings _settings;
        private readonly ILogger<PercentageRateServiceChargeService> _logger;

        public PercentageRateServiceChargeService(IOptions<ApplicationSettings> options, ILogger<PercentageRateServiceChargeService> logger)
        {
            _settings = options.Value;
            _logger = logger;
        }

        public IEnumerable<Pizza> Apply(IEnumerable<Pizza> pizzas)
        {
            _logger.LogDebug($"Applying percentage service charge of {_settings.ServiceChargePercentage}");

            foreach (var pizza in pizzas)
            {
                pizza.RawPrice += pizza.RawPrice * _settings.ServiceChargePercentage;
            }

            return pizzas;
        }
    }
}
