using Common;
using Consumer;
using Moq;
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
    public class ConsumerSHESTest
    {
        private const int _ID1 = 1;
        private const int _ID2 = 2;
        private int num;
        private IConsumerSHES cs;
        private List<Common.Consumer> consumers;

        [SetUp]
        public void SetUp()
        {
            num = 2;
            cs = new Consumer.ConsumerSHES();
            consumers = new List<Common.Consumer>();

            // double energyConsumption , string id, Enums.ConsumerRezim rezim
            Mock<Common.Consumer> mockConsumer1 = new Mock<Common.Consumer>();
            mockConsumer1.Setup(cs => cs.EnergyConsumption).Returns(100);
            mockConsumer1.Setup(cs => cs.Id).Returns(_ID1.ToString());
            mockConsumer1.Setup(cs => cs.Rezim).Returns(Enums.ConsumerRezim.OFF);

            Mock<Common.Consumer> mockConsumer2 = new Mock<Common.Consumer>();
            mockConsumer2.Setup(cs => cs.EnergyConsumption).Returns(200);
            mockConsumer2.Setup(cs => cs.Id).Returns(_ID1.ToString());
            mockConsumer2.Setup(cs => cs.Rezim).Returns(Enums.ConsumerRezim.ON);

            consumers.Add(mockConsumer1.Object);
            consumers.Add(mockConsumer2.Object);

        }

        [Test]
        public void ArgumentNullExceptionInicijalizacija()
        {
            Assert.Throws<ArgumentNullException>(
            () =>
            {
                cs.InitializeConsumers(null);
            }
            );
        }

        [Test]
        public void InitializeDobriParametri()
        {
            cs.InitializeConsumers(consumers);

            int temp1 = ConsumerSHES.consumersList.Count();

            Assert.AreEqual(temp1, num);
        }

    }
}
