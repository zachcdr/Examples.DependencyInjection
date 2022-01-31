using Examples.DependencyInjection.Interfaces;
using Examples.DependencyInjection.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace Examples.DependencyInjection.Services
{
    public class FlatRateServiceChargeService : IServiceCharge
    {
        private readonly ApplicationSettings _settings;
        private readonly ILogger<FlatRateServiceChargeService> _logger;

        public FlatRateServiceChargeService(IOptions<ApplicationSettings> options, ILogger<FlatRateServiceChargeService> logger)
        {
            _settings = options.Value;
            _logger = logger;
        }

        public IEnumerable<Pizza> Apply(IEnumerable<Pizza> pizzas)
        {
            _logger.LogDebug($"Applying flat rate service charge of {_settings.ServiceChargeFlatRate}");

            foreach (var pizza in pizzas)
            {
                pizza.RawPrice += _settings.ServiceChargeFlatRate;
            }

            return pizzas;
        }
    }
}
