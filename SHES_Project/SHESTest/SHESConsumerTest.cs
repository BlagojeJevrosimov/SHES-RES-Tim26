using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHESTest
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class SHESConsumerTest
    {
        private Common.ISHESConsumer shesc;
        private List<Common.Consumer> lista;

        [SetUp]
        public void Setup()
        {
            shesc = new SHES.SHESConsumer();
            lista = new List<Common.Consumer>();

            Mock<Common.Consumer> c1 = new Mock<Common.Consumer>();
            c1.Setup(p => p.Id).Returns("0");
            c1.Setup(p => p.Rezim).Returns(Common.Enums.ConsumerRezim.ON);
            c1.Setup(p => p.EnergyConsumption).Returns(500);

            lista.Add(c1.Object);

            Mock<Common.Consumer> c2 = new Mock<Common.Consumer>();
            c2.Setup(p => p.Id).Returns("1");
            c2.Setup(p => p.Rezim).Returns(Common.Enums.ConsumerRezim.ON);
            c2.Setup(p => p.EnergyConsumption).Returns(200);

            lista.Add(c2.Object);

        }

        [Test]
        [TestCase(500)]
        public void DobriParametri(double potrosnja)
        {
            shesc.sendEnergyConsumption(potrosnja, lista);

            Assert.AreEqual(potrosnja, SHES.SHESConsumer.energyConsumptioneBuffer);
            Assert.AreEqual(lista, SHES.SHESConsumer.consumers);

        }

        [Test]
        [TestCase(-100)]
        public void LosiParametri(double potrosnja)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                shesc.sendEnergyConsumption(potrosnja, lista);
            }
            );
        }


        [Test]
        public void NullParametri()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                shesc.sendEnergyConsumption(1000, null);
            }
           );
        }
    }
}
