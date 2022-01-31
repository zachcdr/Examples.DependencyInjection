using Examples.DependencyInjection.Models;
using System.Collections.Generic;

namespace Examples.DependencyInjection.Interfaces
{
    public interface IServiceCharge
    {
        public IEnumerable<Pizza> Apply(IEnumerable<Pizza> pizzas);
    }
}
