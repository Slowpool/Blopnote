using Blopnote;
using System.Drawing;
using blopnote = Blopnote.Blopnote;

namespace BlopnoteTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            blopnote blop = new blopnote();

        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}