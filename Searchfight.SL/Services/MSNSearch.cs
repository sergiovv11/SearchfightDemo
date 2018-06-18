using Searchfight.SL.Interfaces;
using Searchfight.Common;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Searchfight.SL.Models;

namespace Searchfight.SL.Services
{
    public class MSNSearch : ISearchEngine
    {
        public string EngineName => "MSN Search";

        public async Task<long> GetResultsAsync(string term)
        {
            try
            {
                if (string.IsNullOrEmpty(term))
                {
                    return -1;
                }

                var httpClient = new HttpClient()
                {
                    BaseAddress = new Uri(ConfigurationUtilities.MSNUrl),
                    DefaultRequestHeaders = { { "Ocp-Apim-Subscription-Key", ConfigurationUtilities.MSNKey } }
                };

                using (var response = await httpClient.GetAsync($"?q={term}"))
                {
                    // Verify response code
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        var bingResponse = result.DeserializeJson<MSNModel>();

                        return long.Parse(bingResponse.WebPages.TotalEstimatedMatches);
                    }

                    // In case the service is not available, the number of matches will be 0
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
