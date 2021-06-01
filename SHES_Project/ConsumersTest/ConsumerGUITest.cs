using Common;
using Consumer;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
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
        private Common.Consumer temp;
        private Mock<Common.Consumer> mockConsumer1;
        private Mock<Common.Consumer> mockConsumer2;

        [SetUp]
        public void SetUp()
        {
            num = 2;
            rezim1 = Enums.ConsumerRezim.ON;
            rezim2 = Enums.ConsumerRezim.OFF;

            cg = new Consumer.ConsumerGUI();

            temp = new Common.Consumer();

            mockConsumer1 = new Mock<Common.Consumer>();
            mockConsumer1.Setup(temp => temp.Id).Returns("0");
            mockConsumer1.Setup(temp => temp.EnergyConsumption).Returns(100);
            mockConsumer1.Setup(temp => temp.Rezim).Returns(rezim1);

            mockConsumer2 = new Mock<Common.Consumer>();
            mockConsumer2.Setup(temp => temp.Id).Returns("1");
            mockConsumer2.Setup(temp => temp.EnergyConsumption).Returns(200);
            mockConsumer2.Setup(temp => temp.Rezim).Returns(rezim2);

            ConsumerSHES.consumersList.Add(mockConsumer1.Object);
            ConsumerSHES.consumersList.Add(mockConsumer2.Object);

        }

        [Test]
        [TestCase(0, 1, Enums.ConsumerRezim.ON, Enums.ConsumerRezim.OFF)]
        public void DobriParametri(int id1, int id2, Enums.ConsumerRezim rezim1, Enums.ConsumerRezim rezim2)
        {
            cg.ChangeConsumerState(id1, rezim1);
            cg.ChangeConsumerState(id2, rezim2);

            Assert.AreEqual(num, ConsumerGUI.rezimBuffer.Count());

            Assert.AreEqual(rezim1, ConsumerGUI.rezimBuffer[id1]);
            Assert.AreEqual(rezim2, ConsumerGUI.rezimBuffer[id2]);
            Assert.AreEqual(cg.ReturnTotal(), mockConsumer1.Object.EnergyConsumption);


            cg.ChangeConsumerState(id2, rezim1);
            Assert.AreEqual(rezim1, ConsumerGUI.rezimBuffer[id1]);
            Assert.AreEqual(rezim1, ConsumerGUI.rezimBuffer[id2]);
            Assert.AreEqual(cg.ReturnTotal(), mockConsumer1.Object.EnergyConsumption + mockConsumer2.Object.EnergyConsumption);

        }

        //[Test]
        //[TestCase(2, Enums.ConsumerRezim.OFF)]
        //public void LosiParametri(int id, Enums.ConsumerRezim rezim)
        //{
        //    Assert.Throws<ArgumentOutOfRangeException>(
        //        () =>
        //        {
        //            cg.ChangeConsumerState(id, rezim);
        //        }
        //        );
        //}

        [Test]
        [TestCase(-1, Enums.ConsumerRezim.ON)]
        public void NegativniParametri(int id, Enums.ConsumerRezim rezim)
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () =>
                {
                    cg.ChangeConsumerState(id, rezim);
                }
                );
        }

    }
}
