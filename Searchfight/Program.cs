using Searchfight.BL;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Searchfight
{
    class Program
    {
        static void Main(string[] args)
        {
            Run(args).GetAwaiter().GetResult();
        }

        private static async Task Run(string[] args)
        {
            try
            {
                if (args.Length == 0)
                {
                    // Read search term and split to allow searching with spaces
                    Console.WriteLine("Enter a search term");
                    args = Console.ReadLine()?.Split(' ');
                }

                if (args != null && args.Length > 0)
                {
                    // Verify the search engines and print the result
                    var searchEngines = new SearchEnginesBL();
                    var result = await searchEngines.GetSearchResponse(args?.ToList());

                    // Print the results
                    Console.Clear();
                    Console.WriteLine(result);

                }
                else
                {
                    Console.WriteLine("No terms were entered");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error has ocurred: {ex.Message}");
            }

            Console.ReadKey();
        }
    }
}
