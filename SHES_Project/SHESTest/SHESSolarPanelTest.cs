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
    public class SHESSolarPanelTest
    {
        private Common.ISHESSolarPanel shessp;

        [SetUp]
        public void Setup()
        {
            shessp = new SHES.SHESSolarPanel();
        }

        [Test]
        [TestCase(3000)]
        [TestCase(1500)]
        public void DobriParametri(double output)
        {
            shessp.SendData(output);

            Assert.AreEqual(output, SHES.SHESSolarPanel.bufferPowerOutput);
        }

        [Test]
        [TestCase(-700)]
        [TestCase(-1200)]
        public void LosiParametri(double output)
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () =>
                {
                    shessp.SendData(output);
                }
                );
        }
    }
}
