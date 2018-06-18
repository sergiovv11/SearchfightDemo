using Searchfight.SL.Interfaces;
using Searchfight.SL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Searchfight.BL.Models;

namespace Searchfight.BL
{
    public class SearchEnginesBL
    {
        private readonly IEnumerable<ISearchEngine> _searchEngines;

        public SearchEnginesBL()
        {
            _searchEngines = new List<ISearchEngine>()
            {
                new MSNSearch(),
                new GoogleSearch()
            };
        }

        public async Task<string> GetSearchResponse(List<string> terms)
        {
            try
            {
                // Get the informaiton of every search engine
                var response = new StringBuilder();
                var searchResults = await GetResultsAsync(terms.Distinct());

                // Get the results by term
                response.AppendLine(GetResultsByTerm(terms, searchResults));

                // Get the winners by engine
                response.AppendLine(GetWinnersByEngine(searchResults));

                // Get the total winner
                response.AppendLine(GetTotalWinner(searchResults));

                return response.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetResultsByTerm(List<string> terms, List<SearchResult> searchResults)
        {
            var response = new StringBuilder();

            foreach(var term in terms)
            {
                var line = $"{term}: ";

                foreach(var result in searchResults.Where(x => x.Term.Equals(term)).OrderBy(x => x.SearchEngine))
                {
                    line += $"{result.SearchEngine}: {result.TotalResults} ";
                }

                response.AppendLine(line);
            }

            return response.ToString();
        }

        public string GetWinnersByEngine(List<SearchResult> searchResults)
        {
            var response = new StringBuilder();
            var searchEngines = searchResults.Select(x => x.SearchEngine).Distinct().ToList();

            foreach (var engine in searchEngines)
            {
                var winnerTerm = searchResults.Where(x => x.SearchEngine.Equals(engine)).OrderByDescending(x => x.TotalResults).ToList();
                var line = $"{engine} winner: {winnerTerm.FirstOrDefault()?.Term}";

                response.AppendLine(line);
            }

            return response.ToString();
        }

        public string GetTotalWinner(List<SearchResult> searchResults)
        {
            var response = new StringBuilder();

            var resultByTerm = searchResults.GroupBy(x => x.Term, x => x, (term, result) => new { Term = term, Total = result.Sum(r => r.TotalResults) }).OrderByDescending(x => x.Total).ToList();
            var line = $"Total winner: {resultByTerm.FirstOrDefault()?.Term}";

            response.AppendLine(line);
            return response.ToString();
        }

        public async Task<List<SearchResult>> GetResultsAsync(IEnumerable<string> terms)
        {
            var results = new List<SearchResult>();

            foreach (var term in terms)
            {
                foreach (var engine in _searchEngines)
                {
                    results.Add(new SearchResult
                    {
                        SearchEngine = engine.EngineName,
                        Term = term,
                        TotalResults = await engine.GetResultsAsync(term)
                    });
                }
            }

            return results.OrderBy(x => x.SearchEngine).ToList();
        }
    }
}
