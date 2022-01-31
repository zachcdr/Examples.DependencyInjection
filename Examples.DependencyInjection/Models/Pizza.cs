using System;
using System.Text.Json.Serialization;

namespace Examples.DependencyInjection.Models
{
    public class Pizza
    {
        public string Name { get; set; }
        public double Price => Math.Round(RawPrice, 2);
        [JsonIgnore]
        public double RawPrice { get; set; }
    }
}
