using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Searchfight.SL.Services;

namespace Searchfight.Tests
{
    [TestClass]
    public class GoogleTest
    {
        [TestMethod]
        public async Task ValidateType()
        {
            var googleService = new GoogleSearch();
            var term = ".net";

            var result = await googleService.GetResultsAsync(term);

            Assert.AreEqual(result.GetType(), typeof(long));
        }

        [TestMethod]
        public async Task ValidateEmpty()
        {
            var googleService = new GoogleSearch();
            var term = "";

            var result = await googleService.GetResultsAsync(term);

            // Empty term should give -1 as result
            Assert.AreEqual(result, -1);
        }
    }
}
