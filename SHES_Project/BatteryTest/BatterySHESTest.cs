using Battery;
using Common;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatteryTest
{
    [TestFixture]
    public class BatterySHESTest
    {
        private int num = 5;
        public double[] maxPower;
        public IBatterySHES bs;
        public string idLos, idDobar;
        Enums.BatteryRezim rezim, rezim2;

        [SetUp]
        public void SetUp()
        {
            num = 5;
            maxPower = new double[num];

            for(int i = 0; i < num; i++)
            {
                maxPower[i] = i + 1;
            }

            bs = new BatterySHES();
            idLos = "b2";
            idDobar = "2";
            rezim = Enums.BatteryRezim.IDLE;
            rezim2 = Enums.BatteryRezim.PUNJENJE;
        }

        [Test]
        public void ArgumentOutOfRangeExceptionInicijalizacija()
        {
            //Mock<IBatterySHES> mockBS = new Mock<IBatterySHES>();

            Assert.Throws<ArgumentOutOfRangeException> (
            () =>
            { 
                bs.InitializeBatteries(4, maxPower);
            }
            );

            Assert.Throws<ArgumentOutOfRangeException>(
            () =>
            {
                bs.InitializeBatteries(6, maxPower);
            }
            );

            Assert.Throws<ArgumentOutOfRangeException>(
            () =>
            {
                bs.InitializeBatteries(-2, maxPower);
            }
            );

        }

        [Test]
        public void InitializeDobriParametri()
        {
            bs.InitializeBatteries(num, maxPower);
            int temp = BatterySHES.batteries.Count();
            var temp2 = BatterySHES.bufferRezim[idDobar];

            Assert.AreEqual(num, temp);
            Assert.AreEqual(num, temp);
            Assert.AreEqual(rezim, temp2);

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
