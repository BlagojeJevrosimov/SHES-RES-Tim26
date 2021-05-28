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
        private int num = 5;
        public double[] maxPower;
        public IBatterySHES bs;
        public string idLos, idDobar;
        public Enums.BatteryRezim rezim, rezim2;
        public List<Common.Battery> batteries;

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
            rezim = Enums.BatteryRezim.IDLE;
            rezim2 = Enums.BatteryRezim.PUNJENJE;

            batteries = new List<Common.Battery>();
            batteries.Add(new Common.Battery(200, "1", 700, Enums.BatteryRezim.PUNJENJE));
            batteries.Add(new Common.Battery(100, "2", 800, Enums.BatteryRezim.PUNJENJE));
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
            //Assert.AreNotEqual(null, bs);

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
