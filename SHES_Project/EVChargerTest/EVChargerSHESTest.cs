using Common;
using Moq;
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
    public class EVChargerSHESTest
    {
        private Mock<Common.EVCharger> mockEvc;
        private Common.EVCharger evc;
        private IEVChargerSHES evcs;
        private double capacity;
        private string id;
        private double maxPower;
        private Enums.BatteryRezim state;
        private bool charge;
        private bool connected;

        [SetUp]
        public void Setup()
        {
            mockEvc = new Mock<Common.EVCharger>();
            evcs = new EVCharger.EVChargerSHES();

            capacity = 500;
            id = "0";
            maxPower = 200;
            state = Enums.BatteryRezim.PUNJENJE;
            charge = true;
            connected = true;

            mockEvc.Setup(evcs => evcs.Capacity).Returns(capacity);
            mockEvc.Setup(evcs => evcs.Charge).Returns(charge);
            mockEvc.Setup(evcs => evcs.Connected).Returns(connected);
            mockEvc.Setup(evcs => evcs.Id).Returns(id);
            mockEvc.Setup(evcs => evcs.MaxPower).Returns(maxPower);
            mockEvc.Setup(evcs => evcs.State).Returns(state);

            evc = mockEvc.Object;
        }

        [Test]
        public void DobriParametriInicijalizacija()
        {
            evcs.InitializeEVCharger(evc);

            Assert.AreNotEqual(null, EVCharger.EVChargerSHES.EVCharger);

            Assert.AreEqual(evc, EVCharger.EVChargerSHES.EVCharger);
            Assert.AreEqual(Common.Enums.BatteryRezim.PRAZNJENJE, EVCharger.EVChargerSHES.rezimBuffer);

        }

        [Test]
        public void LosiParametriInicijalizacija()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                {
                    evcs.InitializeEVCharger(null);
                }
                );
        }

        [Test]
        public void SendRegimeDobriParametri()
        {
            evcs.SendRegime(state);

            Assert.AreEqual(state, EVCharger.EVChargerSHES.rezimBuffer);
        }
    }
}
