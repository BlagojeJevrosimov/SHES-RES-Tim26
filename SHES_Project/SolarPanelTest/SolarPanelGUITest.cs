using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarPanelTest
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class SolarPanelGUITest
    {
        private Common.ISolarPanelGUI spg;

        [Test]
        [TestCase(0.2)]
        [TestCase(0.7)]
        public void DobriParametri(double sun)
        {
            spg = new SolarPanels.SolarPanelGUI();

            Assert.AreNotEqual(null, spg);

            spg.ChangeSunIntensity(sun);

            Assert.AreEqual(sun, SolarPanels.SolarPanelGUI.buffer);

        }

        [Test]
        [TestCase(2)]
        [TestCase(-1)]
        [TestCase(1.2)]
        public void LosiParametri(double sun)
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () =>
                {
                    spg.ChangeSunIntensity(sun);
                }
                );

        }

    }
}
