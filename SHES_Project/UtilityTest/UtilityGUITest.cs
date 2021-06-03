using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityTest
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class UtilityGUITest
    {
        private Common.IUtilityGUI ugui;

        [SetUp]
        public void Setup()
        {
            ugui = new Utility.UtilityGUI();
        }

        [Test]
        [TestCase(150)]
        [TestCase(2000)]
        public void DobriParametri(double price)
        {
            ugui.SendPrice(price);

            Assert.AreEqual(price, Utility.UtilityGUI.bufferPrice);

        }

        [Test]
        [TestCase(-150)]
        [TestCase(-1000)]
        public void LosiParametri(double price)
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () =>
                {
                    ugui.SendPrice(price);
                }
                );
        }

    }
}
