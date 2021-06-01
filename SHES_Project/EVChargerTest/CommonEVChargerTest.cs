using System;
using System.Diagnostics.CodeAnalysis;
using Common;
using NUnit.Framework;

namespace EVChargerTest
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class CommonEVChargerTest
    {
        private Common.EVCharger evc;

        [Test]
        public void PrazanKonstruktor()
        {
            evc = new Common.EVCharger();

            Assert.AreNotEqual(null, evc);
        }

        [Test]
        [TestCase(250, "1", 500, Enums.BatteryRezim.PRAZNJENJE, true, true)]
        public void DobriParametriKonstruktor(double capacity, string id, double maxPower, Enums.BatteryRezim state, bool charge, bool connected)
        {
            evc = new Common.EVCharger(capacity, id, maxPower, state, charge, connected);

            Assert.AreNotEqual(null, evc);

            Assert.AreEqual(capacity, evc.Capacity);
            Assert.AreEqual(id, evc.Id);
            Assert.AreEqual(maxPower, evc.MaxPower);
            Assert.AreEqual(state, evc.State);
            Assert.AreEqual(charge, evc.Charge);
            Assert.AreEqual(connected, evc.Connected);

        }

        [Test]
        [TestCase(-250, "1", 500, Enums.BatteryRezim.PRAZNJENJE, true, true)]
        [TestCase(250, "1", -500, Enums.BatteryRezim.PRAZNJENJE, true, true)]
        public void NegativniParametriKonstruktor(double capacity, string id, double maxPower, Enums.BatteryRezim state, bool charge, bool connected)
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () =>
                {
                    evc = new Common.EVCharger(capacity, id, maxPower, state, charge, connected);
                }
                );
        }

        [Test]
        [TestCase(250, null, 500, Enums.BatteryRezim.PRAZNJENJE, true, true)]
        public void NullParametriKonstruktor(double capacity, string id, double maxPower, Enums.BatteryRezim state, bool charge, bool connected)
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                {
                    evc = new Common.EVCharger(capacity, id, maxPower, state, charge, connected);
                }
                );
        }
    }
}
