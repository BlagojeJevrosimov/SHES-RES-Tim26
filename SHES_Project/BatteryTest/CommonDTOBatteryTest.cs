using Common;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatteryTest
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class CommonDTOBatteryTest
    {
        int time;
        DateTime timeAsDT;
        Mock<Common.DTO.BatteryDTO> mockB;

        [SetUp]
        public void Setup()
        {
            mockB = new Mock<Common.DTO.BatteryDTO>();

            DateTime centuryBegin = new DateTime(2020, 1, 1);
            timeAsDT = DateTime.Now;


            long elapsedTicks = timeAsDT.Ticks - centuryBegin.Ticks;
            TimeSpan elapsedSpan = new TimeSpan(elapsedTicks);
            time = (int)Math.Floor(elapsedSpan.TotalSeconds);

            mockB.Object.Time = time;
        }


        [Test]
        [TestCase(500, "0", 100, Enums.BatteryRezim.PUNJENJE, 44841900)]
        public void DobriParametriKonstruktor(double capacity, string id, double maxPower, Enums.BatteryRezim state, int time)
        {
            Common.DTO.BatteryDTO dtoB = new Common.DTO.BatteryDTO(capacity, id, maxPower, state, time);

            Assert.AreNotEqual(null, dtoB);
            Assert.AreEqual(dtoB.Capacity, capacity);
            Assert.AreEqual(dtoB.Id, id);
            Assert.AreEqual(dtoB.MaxPower, maxPower);
            Assert.AreEqual(dtoB.State, state);
            Assert.AreEqual(dtoB.Time, time);

        }

        [Test]
        [TestCase(500, "0", 100, Enums.BatteryRezim.PUNJENJE, -441900)]
        public void LosiParametriKonstruktor(double capacity, string id, double maxPower, Enums.BatteryRezim state, int time)
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () =>
                {
                    Common.DTO.BatteryDTO dtoB = new Common.DTO.BatteryDTO(capacity, id, maxPower, state, time);
                }
                );
        }

        [Test]
        public void ToDateTimeDobriParametri()
        {
            Assert.AreEqual(mockB.Object.TimeAsDT.Hour, timeAsDT.Hour);
            Assert.AreEqual(mockB.Object.TimeAsDT.Minute, timeAsDT.Minute);
            Assert.AreEqual(mockB.Object.TimeAsDT.Second, timeAsDT.Second);

        }

    }
}
