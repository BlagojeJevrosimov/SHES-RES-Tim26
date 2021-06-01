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
    public class CommonSolarPanelTest
    {
        private Common.SolarPanel sp;

        [Test]
        [TestCase("0", 500)]
        [TestCase("1", 1500)]
        public void DobriParametri(string id, double power)
        {
            sp = new Common.SolarPanel();

            Assert.AreNotEqual(null, sp);

            sp = new Common.SolarPanel(id, power);
            Assert.AreEqual(id, sp.Id);
            Assert.AreEqual(power, sp.MaxPower);

        }

        [Test]
        [TestCase(null, 1000)]
        public void NullParametri(string id, double power)
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                {
                    sp = new Common.SolarPanel(id, power);
                }
                );
        }

        [Test]
        [TestCase("1", -200)]
        public void NegativniParametri(string id, double power)
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () =>
                {
                    sp = new Common.SolarPanel(id, power);
                }
                );
        }

    }
}
