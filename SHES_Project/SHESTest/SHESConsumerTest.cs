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

        [SetUp]
        public void Setup()
        {
            shesc = new SHES.SHESConsumer();
        }

        [Test]
        [TestCase(500)]
        public void DobriParametri(double potrosnja)
        {
            //DODATI MOCK LISTU
            shesc.sendEnergyConsumption(potrosnja);

            Assert.AreEqual(potrosnja, SHES.SHESConsumer.energyConsumptioneBuffer);

        }

        [Test]
        [TestCase(-100)]
        public void LosiParametri(double potrosnja)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                //DODATI MOCK LISTU
                shesc.sendEnergyConsumption(potrosnja, );
            }
            );

        }
    }
}
