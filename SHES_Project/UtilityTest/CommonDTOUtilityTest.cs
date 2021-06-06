using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityTest
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class CommonDTOUtilityTest
    {
        int time;
        DateTime timeAsDT;
        Mock<Common.DTO.UtilityDTO> mockB;

        [SetUp]
        public void Setup()
        {
            mockB = new Mock<Common.DTO.UtilityDTO>();

            DateTime centuryBegin = new DateTime(2020, 1, 1);
            timeAsDT = DateTime.Now;


            long elapsedTicks = timeAsDT.Ticks - centuryBegin.Ticks;
            TimeSpan elapsedSpan = new TimeSpan(elapsedTicks);
            time = (int)Math.Floor(elapsedSpan.TotalSeconds);

            mockB.Object.Time = time;
        }


        [Test]
        [TestCase(500, 44841900)]
        public void DobriParametriKonstruktor(double power, int time)
        {
            Common.DTO.UtilityDTO dtoB = new Common.DTO.UtilityDTO(power, time);

            Assert.AreNotEqual(null, dtoB);
            Assert.AreEqual(dtoB.Power, power);
            Assert.AreEqual(dtoB.Time, time);

        }

        [Test]
        [TestCase(200, -442560)]
        public void LosiParametriKonstruktor(double power, int time)
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () =>
                {
                    Common.DTO.UtilityDTO dtoB = new Common.DTO.UtilityDTO(power, time);
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
