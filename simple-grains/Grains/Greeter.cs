using System;
using System.Threading.Tasks;
using interfaces;
using Orleans;

namespace Grains
{
    public class Greeter : Grain, IGreeter
    {
        public Task<string> CreateGreetingFor(string person)
        {
            if (person == null) throw new ArgumentNullException(nameof(person));

            return Task.FromResult($"Hello {person}");
        }
    }
}