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
    public class SolarPanelSHESTest
    {
        private Common.ISolarPanelSHES spshes;
        private int num;
        private double[] powers;

        private double[] powers2;
        private double[] powers3;


        [SetUp]
        public void Setup()
        {
            num = 2;
            powers = new double[2] { 20, 50 };

            powers2 = new double[2] { -15, 10 };
            powers3 = new double[4] { 1, 2, 3, 4 };
        }


        [Test]
        public void DobriParametri()
        {
            spshes = new SolarPanels.SolarPanelSHES();

            Assert.AreNotEqual(null, spshes);

            spshes.InitializeSolarPanels(num, powers);

            Assert.AreEqual(num, SolarPanels.SolarPanelSHES.solarPanels.Count());
            Assert.AreEqual(powers[0], SolarPanels.SolarPanelSHES.solarPanels[0].MaxPower);
            Assert.AreEqual(powers[1], SolarPanels.SolarPanelSHES.solarPanels[1].MaxPower);
        }

        [Test]
        public void LosiParametri()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () =>
                {
                    spshes.InitializeSolarPanels(num, powers2);
                }
                );

            Assert.Throws<ArgumentOutOfRangeException>(
                () =>
                {
                    spshes.InitializeSolarPanels(num, powers3);
                }
                );

            Assert.Throws<ArgumentOutOfRangeException>(
                () =>
                {
                    spshes.InitializeSolarPanels(-2, powers);
                }
                );
        }

    }
}
