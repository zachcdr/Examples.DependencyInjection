using Examples.DependencyInjection.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Examples.DependencyInjection.Interfaces
{
    public interface IPizzaRepo
    {
        public Task<IEnumerable<Pizza>> GetPizzas();
    }
}
