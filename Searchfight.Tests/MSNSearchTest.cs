using System;
using System.Threading.Tasks;
using Searchfight.SL.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Searchfight.Tests
{
    [TestClass]
    public class MSNSearchTest
    {
        [TestMethod]
        public async Task ValidateType()
        {
            var msnSearchService = new MSNSearch();
            var term = "java";

            var result = await msnSearchService.GetResultsAsync(term);

            Assert.AreEqual(result.GetType(), typeof(long));
        }

        [TestMethod]
        public async Task ValidateEmpty()
        {
            var msnSearchService = new MSNSearch();
            var term = "";

            var result = await msnSearchService.GetResultsAsync(term);

            // Empty term should give -1 as result
            Assert.AreEqual(result, -1);
        }
    }
}