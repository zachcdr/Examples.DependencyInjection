using Examples.DependencyInjection.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Examples.DependencyInjection.Interfaces
{
    public interface IPizzaService
    {
        public Task<IEnumerable<Pizza>> GetPizzasWithServiceCharge();
    }
}
