using Common;
using EVCharger;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVChargerTest
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class EVChargerGUITest
    {
        private Common.Enums.BatteryRezim rezim;
        private bool plug;
        private IEVChargerGUI evcg;

        [SetUp]
        public void Setup()
        {
            rezim = Common.Enums.BatteryRezim.PUNJENJE;
            plug = true;
            evcg = new EVChargerGUI();

        }

        [Test]
        public void DobriParametri()
        {
            evcg.SendRegime(plug, rezim);

            Assert.AreNotEqual(null, evcg);

            Assert.AreEqual(plug, EVChargerGUI.plugBuffer);
            Assert.AreEqual(rezim, EVChargerGUI.rezimBuffer);

        }
    }
}
