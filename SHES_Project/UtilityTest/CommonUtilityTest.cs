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
    public class CommonUtilityTest
    {
        private Common.Utility utility;

        [Test]
        public void DobarKonstruktor()
        {
            utility = new Common.Utility();

            Assert.AreNotEqual(null, utility);

            utility = new Common.Utility(500);

            Assert.AreEqual(utility.Price, 500);

            utility = new Common.Utility(1000, 2000);

            Assert.AreEqual(utility.Power, 1000);
            Assert.AreEqual(utility.Price, 2000);

        }

        [Test]
        public void LosKonstruktor()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () =>
                {
                    utility = new Common.Utility(-1000);
                }
                );

            Assert.Throws<ArgumentOutOfRangeException>(
                () =>
                {
                    utility = new Common.Utility(200, -500);
                }
                );
        }
    }
}
