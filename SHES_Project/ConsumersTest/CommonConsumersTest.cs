using System;
using NUnit.Framework;
using System.Diagnostics.CodeAnalysis;
using Moq;

namespace ConsumersTest
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class CommonConsumersTest
    {
        [Test]
        [TestCase("1", 200)]
        [TestCase("2", 350)]
        public void ConsmerKonstruktorDobriParametri(string id, double energyConsumption)
        {
            Common.Consumer consumer = new Common.Consumer(id, energyConsumption);

            Assert.AreEqual(consumer.Id, id);
            Assert.AreEqual(consumer.EnergyConsumption, energyConsumption);

            consumer = new Common.Consumer();

            Assert.AreNotEqual(null, consumer);

        }

        [Test]
        [TestCase("123", -50)]
        [TestCase("12", -55.20)]
        public void ConsumerKonstruktorNegativniParametri(string id, double energyConsumption)
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () =>
                {
                    Common.Consumer consumer = new Common.Consumer(id, energyConsumption);
                }
                );
        }

        [Test]
        [TestCase(null, 250)]
        public void ConsumerKonstruktorNullParametri(string id, double energyConsumption)
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                {
                    Common.Consumer consumer = new Common.Consumer(id, energyConsumption);
                }
                );
        }

        [Test]
        [TestCase("nesto1", 200)]
        [TestCase("123pp", 100)]
        public void ConsumerKonstruktorTxtParametri(string id, double energyConsumption)
        {
            Assert.Throws<ArgumentException>(
                () =>
                {
                    Common.Consumer consumer = new Common.Consumer(id, energyConsumption);
                }
                );
        }

    }
}
