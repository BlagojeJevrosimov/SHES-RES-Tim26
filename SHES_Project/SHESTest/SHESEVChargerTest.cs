using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHESTest
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class SHESEVChargerTest
    {
        private Common.ISHESEVCharger shese;

        [SetUp]
        public void Setup()
        {
            shese = new SHES.SHESEVCharger();
        }

        [Test]
        [TestCase(true, false)]
        [TestCase(true, true)]
        [TestCase(false, true)]
        public void DobriParametri(bool plug, bool charge)
        {
            shese.SendRegime(plug, charge);

            Assert.AreEqual(SHES.SHESEVCharger.plugBuffer, plug);
            Assert.AreEqual(SHES.SHESEVCharger.rezimBuffer, charge);

        }
    }
}
