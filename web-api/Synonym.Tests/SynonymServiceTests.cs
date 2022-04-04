using NUnit.Framework;
using Services.Services;
using System;
using System.Threading.Tasks;

namespace Synonyms.UnitTests
{
    internal static class SynonymMockData
    {
        internal static string[] SynonymData()
        {
            return new string[] { "good", "useful", "great", "healthy", "practical" };
        }

        internal static string[] SynonymDuplicateData()
        {
            return new string[] { "good", "useful", "great", "great" };
        }
    }

    public class SynonymServiceTests
    {
        private SynonymService _synonymService;

        [OneTimeSetUp]
        public void Setup()
        { 
            _synonymService = new SynonymService();
        }

        [Test]
        public void AddSynonymsTest()
        {
            var requestData = SynonymMockData.SynonymData();
            Assert.IsNotNull(requestData);

            Assert.DoesNotThrow(async () => await _synonymService.AddSynonyms(requestData));
        }

        [Test]
        public void AddSynonymsDuplicateTest()
        {
            var requestData = SynonymMockData.SynonymDuplicateData();

            Assert.ThrowsAsync<Exception>(async () => await _synonymService.AddSynonyms(requestData));
        }

        [Test]
        [TestCase("good")]
        [TestCase("healthy")]
        public async Task FindSynonymTest(string searchTerm)
        {
           var result = await _synonymService.FindSynonym(searchTerm);
           Assert.IsNotNull(result);
        }
    }
}