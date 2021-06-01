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
        [TestCase(200, "1", Common.Enums.ConsumerRezim.ON)]
        [TestCase(350, "2", Common.Enums.ConsumerRezim.OFF)]
        public void ConsmerKonstruktorDobriParametri(double energyConsumption, string id, Common.Enums.ConsumerRezim rezim)
        {
            Common.Consumer consumer = new Common.Consumer(energyConsumption, id, rezim);

            Assert.AreEqual(consumer.Id, id);
            Assert.AreEqual(consumer.EnergyConsumption, energyConsumption);
            Assert.AreEqual(consumer.Rezim, rezim);

            consumer = new Common.Consumer();

            Assert.AreNotEqual(null, consumer);

        }

        [Test]
        [TestCase(-100, "1", Common.Enums.ConsumerRezim.ON)]
        [TestCase(-20.5, "1", Common.Enums.ConsumerRezim.ON)]
        public void ConsumerKonstruktorNegativniParametri(double energyConsumption, string id, Common.Enums.ConsumerRezim rezim)
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () =>
                {
                    Common.Consumer consumer = new Common.Consumer(energyConsumption, id, rezim);
                }
                );
        }

        [Test]
        [TestCase(250, null, Common.Enums.ConsumerRezim.OFF)]
        public void ConsumerKonstruktorNullParametri(double energyConsumption, string id, Common.Enums.ConsumerRezim rezim)
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                {
                    Common.Consumer consumer = new Common.Consumer(energyConsumption, id, rezim);
                }
                );
        }

        [Test]
        [TestCase(200, "nesto1", Common.Enums.ConsumerRezim.ON)]
        [TestCase(100, "123pp", Common.Enums.ConsumerRezim.OFF)]
        public void ConsumerKonstruktorTxtParametri(double energyConsumption, string id, Common.Enums.ConsumerRezim rezim)
        {
            Assert.Throws<ArgumentException>(
                () =>
                {
                    Common.Consumer consumer = new Common.Consumer(energyConsumption, id, rezim);
                }
                );
        }

    }
}
