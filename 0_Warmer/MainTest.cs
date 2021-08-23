using _0_Warmer.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthWindCoreLibrary.Classes;

namespace _0_Warmer
{
    [TestClass]
    public partial class MainTest : TestBase
    {
        [TestMethod]
        [TestTraits(Trait.Warming)]
        public void A_Warmup()
        {
            ContactOperations.Warmup();
        }

    }
}
