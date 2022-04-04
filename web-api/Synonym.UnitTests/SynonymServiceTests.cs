using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Synonym.UnitTests
{
    internal static class SynonymMockData
    {
        internal static string[] SynonymGoodData()
        {
            return new string[] { "good", "useful", "great", "healthy", "practical" };
        }

        internal static string[] SynonymDuplicateData()
        {
            return new string[] { "good", "useful", "great", "great" };
        }

        internal static string[] SynonymHappyData()
        {
            return new string[] { "happy", "glad", "peaceful", "blessed" };
        }
    }

    [TestClass]
    public class SynonymServiceTests
    {
        private SynonymService _synonymService;

        [TestInitialize]
        public void TestInitialize()
        {
          _synonymService = new SynonymService();
        }

        [TestMethod]
        public async Task AddSynonymsTest()
        {
            var requestData = SynonymMockData.SynonymGoodData();
            Assert.IsNotNull(requestData);

            await _synonymService.AddSynonyms(requestData);

            await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await _synonymService.AddSynonyms(requestData));
        }

        [TestMethod]
        public async Task AddSynonymsDuplicateTest()
        {
            var requestData = SynonymMockData.SynonymDuplicateData();

            await Assert.ThrowsExceptionAsync<Exception>(async () => await _synonymService.AddSynonyms(requestData));
        }

        [TestMethod]
        public async Task GetSynonymTest()
        {
            var requestData = SynonymMockData.SynonymHappyData();
            await _synonymService.AddSynonyms(requestData);

            var result = await _synonymService.FindSynonym("happy");
            Assert.IsNotNull(result);

            var noresult = await _synonymService.FindSynonym("sad");
            Assert.IsTrue(noresult.Count == 0);

            var synonymIndex = result.First().Value;
            var synonyms = await _synonymService.GetSynonyms(synonymIndex);
            Assert.IsTrue(synonyms.Count == 4);
        }
    }
}
