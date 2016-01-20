using System.Threading.Tasks;
using Orleans;

namespace interfaces
{
    public interface IGreeter : IGrainWithIntegerKey
    {
        Task<string> CreateGreetingFor(string person);
    }
}