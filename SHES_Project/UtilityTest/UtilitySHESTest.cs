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
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class UtilitySHESTest
    {
        private Common.IUtilitySHES ushes;
        private Mock<Common.Utility> mockUtil;
        private Mock<Common.Utility> mockUtil2;

        [SetUp]
        public void Setup()
        {
            ushes = new Utility.UtilitySHES();
            mockUtil = new Mock<Common.Utility>();
            mockUtil2 = new Mock<Common.Utility>();

            mockUtil.Setup(ut => ut.Power).Returns(1000);
            mockUtil.Setup(ut => ut.Price).Returns(700);

            mockUtil2.Setup(ut => ut.Power).Returns(2000);
            mockUtil2.Setup(ut => ut.Price).Returns(-500);

            //zbog inicijalizacije util-a
            ushes.initializeUtility(mockUtil.Object);

        }


        [Test]
        public void DobriParametriInicijalizacija()
        {
            ushes.initializeUtility(mockUtil.Object);

            Assert.AreEqual(Utility.UtilitySHES.utility.Power, mockUtil.Object.Power);
            Assert.AreEqual(Utility.UtilitySHES.utility.Price, mockUtil.Object.Price);

        }

        [Test]
        public void DobriParametriEnergija()
        {
            ushes.sendRequestforEnergy(-2000);

            Assert.AreEqual(mockUtil.Object.Power, -2000);

            ushes.sendRequestforEnergy(1000);

            Assert.AreEqual(mockUtil.Object.Power, 1000);

        }

        [Test]
        public void LosiParametriIncijalizacija()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                {
                    ushes.initializeUtility(null);
                }
                );

            Assert.Throws<ArgumentOutOfRangeException>(
               () =>
               {
                   ushes.initializeUtility(mockUtil2.Object);
               }
               );

        }

        [Test]
        public void GetPriceTest()
        {
            //mi smo poslali kod inicijalizacije mock objekat sa cenom 700
            double temp = 700;

            Assert.AreEqual(temp, ushes.getPrice());

        }
    }
}
