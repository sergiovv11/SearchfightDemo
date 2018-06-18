using System.Threading.Tasks;

namespace Searchfight.SL.Interfaces
{
    public interface ISearchEngine
    {
        string EngineName { get; }
        Task<long> GetResultsAsync(string term);
    }
}
