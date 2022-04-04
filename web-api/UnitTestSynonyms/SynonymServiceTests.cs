using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.Service;

namespace UnitTestSynonyms
{
    [TestClass]
    public class SynonymServiceTests
    {
        private SynonymService _synonymSrvice;

        [OneTimeSetUp]
        public void Setup()
        {
            _synonymSrvice = new SynonymService();
        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
