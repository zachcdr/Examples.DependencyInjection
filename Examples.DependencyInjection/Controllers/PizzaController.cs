using Examples.DependencyInjection.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Examples.DependencyInjection.Controllers
{
    [ApiController]
    [Route("pizzas")]
    public class PizzaController : ControllerBase
    {
        private readonly ILogger<PizzaController> _logger;
        private readonly IPizzaService _pizzaService;

        public PizzaController(ILogger<PizzaController> logger, IPizzaService pizzaService)
        {
            _logger = logger;
            _pizzaService = pizzaService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var pizzas = await _pizzaService.GetPizzasWithServiceCharge();

                return Ok(pizzas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"PizzaController::GetPizzas: threw an unknown exception {ex.Message}");

                return StatusCode(500, null);
            }
        }
    }
}
