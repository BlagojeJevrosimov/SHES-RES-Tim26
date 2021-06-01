using Battery;
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
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class BatterySHESTest
    {
        private int num;
        public double[] maxPower;
        public IBatterySHES bs;
        public string idLos, idDobar;
        public Enums.BatteryRezim rezim, rezim2;
        public List<Common.Battery> batteries;
        public Mock<Common.Battery> mockBattery1;
        public Mock<Common.Battery> mockBattery2;

        [SetUp]
        public void SetUp()
        {
            num = 2;
            maxPower = new double[num];

            for(int i = 0; i < num; i++)
            {
                maxPower[i] = i + 1;
            }

            bs = new BatterySHES();
            idLos = "b2";
            idDobar = "2";
            rezim = Enums.BatteryRezim.PRAZNJENJE;
            rezim2 = Enums.BatteryRezim.PUNJENJE;

            batteries = new List<Common.Battery>();

            mockBattery1 = new Mock<Common.Battery>();
            mockBattery1.Setup(bat => bat.Capacity).Returns(500);
            mockBattery1.Setup(bat => bat.Id).Returns("1");
            mockBattery1.Setup(bat => bat.MaxPower).Returns(1000);
            mockBattery1.Setup(bat => bat.State).Returns(Common.Enums.BatteryRezim.PUNJENJE);

            mockBattery2 = new Mock<Common.Battery>();
            mockBattery2.Setup(bat => bat.Capacity).Returns(300);
            mockBattery2.Setup(bat => bat.Id).Returns("2");
            mockBattery2.Setup(bat => bat.MaxPower).Returns(100);
            mockBattery2.Setup(bat => bat.State).Returns(Common.Enums.BatteryRezim.PUNJENJE);

            batteries.Add(mockBattery1.Object);
            batteries.Add(mockBattery2.Object);
        }

        [Test]
        public void ArgumentNullExceptionInicijalizacija()
        {
            Assert.Throws<ArgumentNullException>(
            () =>
            {
                bs.InitializeBatteries(null);
            }
            );
        }

        [Test]
        public void InitializeDobriParametri()
        {
            bs.InitializeBatteries(batteries);
            int temp = BatterySHES.batteries.Count();
            var temp2 = BatterySHES.bufferRezim[idDobar];

            Assert.AreEqual(num, temp);

            bs.SendRegime(idDobar, rezim2);
            temp2 = BatterySHES.bufferRezim[idDobar];

            Assert.AreEqual(rezim2, temp2);

        }

        [Test]
        public void InitializeLosiParametri()
        {
            Assert.Throws<ArgumentException>(
            () => {
                bs.SendRegime(idLos, rezim);
            }
            );

            Assert.Throws<ArgumentNullException>(
            () => {
                bs.SendRegime(null, rezim);
            }
            );
        }
    }
}
