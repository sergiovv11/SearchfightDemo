using Searchfight.SL.Interfaces;
using Searchfight.Common;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Searchfight.SL.Models;

namespace Searchfight.SL.Services
{
    public class GoogleSearch : ISearchEngine
    {
        public string EngineName => "Google";

        public async Task<long> GetResultsAsync(string term)
        {
            try
            {
                if (string.IsNullOrEmpty(term))
                {
                    return -1;
                }

                var httpClient = new HttpClient();

                // Generate url
                var url = string.Format(ConfigurationUtilities.GoogleUrl, ConfigurationUtilities.GoogleKey, ConfigurationUtilities.GoogleCXKey, term);

                using (var response = await httpClient.GetAsync(url))
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var googleResponse = result.DeserializeJson<GoogleModel>();

                    return long.Parse(googleResponse.SearchInformation.TotalResults);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
