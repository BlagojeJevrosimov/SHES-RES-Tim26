using DatabaseLayer.DAO;
using DatabaseLayer.DAO.Implementacije;
using DatabaseLayer.SERVICES;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayerTest
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class GetBatteriesTest
    {
        public Mock<IDBServices> service;

        [SetUp]
        public void Setup()
        {
            service = new Mock<IDBServices>();
        }

        [Test]
        public void TestDobar()
        {
            service.Setup(p => p.GetBatteries()).Verifiable();
            service.Object.GetBatteries();
            IBatteries ib = new Batteries();
        }
    }
}
