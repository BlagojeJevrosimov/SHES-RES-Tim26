using Moq;
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
    public class SHESBatteryTest
    {
        private Common.ISHESBattery shesb;

        [SetUp]
        public void Setup()
        {
            shesb = new SHES.SHESBattery();

            if (!SHES.SHESBattery.bufferCapacities.ContainsKey("0"))
            {
                SHES.SHESBattery.bufferCapacities.Add("0", 100);
                SHES.SHESBattery.bufferRezimi.Add("0", Common.Enums.BatteryRezim.PUNJENJE);
            }
        }

        [Test]
        [TestCase("0", 500, Common.Enums.BatteryRezim.PRAZNJENJE)]
        public void DobriParametriSendData(string id, double capacity, Common.Enums.BatteryRezim rezim)
        {
            shesb.SendData(id, capacity, rezim);

            Assert.AreEqual(SHES.SHESBattery.bufferCapacities[id], capacity);
            Assert.AreEqual(SHES.SHESBattery.bufferRezimi[id], rezim);

        }

        [Test]
        [TestCase(null, 200, Common.Enums.BatteryRezim.PRAZNJENJE)]
        public void NullParametri(string id, double capacity, Common.Enums.BatteryRezim rezim)
        {
            Assert.Throws<ArgumentNullException>(
                () => 
                {
                    shesb.SendData(id, capacity, rezim);
                }
                );
        }

        [Test]
        [TestCase("0", -300, Common.Enums.BatteryRezim.PUNJENJE)]
        public void NegativniParametri(string id, double capacity, Common.Enums.BatteryRezim rezim)
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () =>
                {
                    shesb.SendData(id, capacity, rezim);
                }
                );
        }

    }
}
