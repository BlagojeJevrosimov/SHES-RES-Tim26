using Common;
using Consumer;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumersTest
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class ConsumerGUITest
    {
        private Enums.ConsumerRezim rezim1;
        private Enums.ConsumerRezim rezim2;
        private int num;
        private IConsumerGUI cg;

        [SetUp]
        public void SetUp()
        {
            num = 2;
            rezim1 = Enums.ConsumerRezim.ON;
            rezim2 = Enums.ConsumerRezim.OFF;

            cg = new Consumer.ConsumerGUI();

            ConsumerGUI.rezimBuffer = new Enums.ConsumerRezim[2];
        }

        [Test]
        [TestCase(0, Enums.ConsumerRezim.ON)]
        public void DobriParametri(int id, Enums.ConsumerRezim rezim)
        {

            //PREPRAVITI PADA ZBOG CHANNEL FACTORY
            cg.ChangeConsumerState(id, rezim);

            Assert.AreEqual(rezim, ConsumerGUI.rezimBuffer[id]);

        }
    }
}
