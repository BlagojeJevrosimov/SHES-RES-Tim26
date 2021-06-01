using Common;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace BatteryTest
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class CommonBatteryTest
    {
        [Test]
        [TestCase(100, "1", 500, Common.Enums.BatteryRezim.PUNJENJE)]
        [TestCase(200, "2", 1200, Common.Enums.BatteryRezim.PRAZNJENJE)]
        public void BatteryKonstruktorDobriParametri(double capacity, string id, double maxPower, Enums.BatteryRezim state)
        {
            Common.Battery battery = new Common.Battery(capacity, id, maxPower, state);

            Assert.AreNotEqual(null, battery);

            Assert.AreEqual(battery.Capacity, capacity);
            Assert.AreEqual(battery.Id, id);
            Assert.AreEqual(battery.MaxPower, maxPower);
            Assert.AreEqual(battery.State, state);

            battery = new Common.Battery();
            
            Assert.AreEqual(battery.Capacity, 0);

        }

        [Test]
        //[ExpectedException(typeof(ArgumentException))]
        [TestCase(-1, "1", 500, Common.Enums.BatteryRezim.PUNJENJE)]
        [TestCase(100, "1", -5, Common.Enums.BatteryRezim.PRAZNJENJE)]
        public void BatteryKonstruktoLosiParametri(double capacity, string id, double maxPower, Enums.BatteryRezim state)
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () =>
                {
                    Common.Battery battery = new Common.Battery(capacity, id, maxPower, state);
                }
            );
        }

        [Test]
        [TestCase(200, null, 500, Common.Enums.BatteryRezim.PUNJENJE)]
        public void BatteryKonstruktoNullParametri(double capacity, string id, double maxPower, Enums.BatteryRezim state)
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                {
                    Common.Battery battery = new Common.Battery(capacity, id, maxPower, state);
                }
            );
        }

        //[Test]
        //[TestCase(200, "broj", 300, Enums.BatteryRezim.PUNJENJE)]
        //[TestCase(100, "broj52", 100, Enums.BatteryRezim.PRAZNJENJE)]
        //public void BatteryKonstruktorIdNonNumber(double capacity, string id, double maxPower, Enums.BatteryRezim state)
        //{
        //    Assert.Throws<ArgumentException>(
        //        () =>
        //        {
        //            Common.Battery battery = new Common.Battery(capacity, id, maxPower, state);
        //        }
        //    );
        //}

        [Test]
        [TestCase(150)]
        public void CapacityDobriParametri(double capacity)
        {
            Common.Battery battery = new Common.Battery();
            battery.Capacity = capacity;       
            Assert.AreEqual(capacity, battery.Capacity);
        }

        [Test]
        [TestCase("150")]
        public void IdDobriParametri(string id)
        {
            Common.Battery battery = new Common.Battery();
            battery.Id = id;
            Assert.AreEqual(id, battery.Id);
        }
    }
}
