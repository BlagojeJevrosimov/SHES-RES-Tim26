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
        private int num;
        private IConsumerSHES cs;
        private List<Common.Consumer> consumers;


        [SetUp]
        public void SetUp()
        {
            num = 2;
            cs = new Consumer.ConsumerSHES();
            consumers = new List<Common.Consumer>();

            Mock<Common.Consumer> mockConsumer1 = new Mock<Common.Consumer>("1", 100);
            Mock<Common.Consumer> mockConsumer2 = new Mock<Common.Consumer>("2", 200);

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
