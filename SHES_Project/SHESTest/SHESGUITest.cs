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
    public class SHESGUITest
    {
        private Common.ISHESGUI shesg;
        private int brojPanela;
        private double[] snagePanela;
        private int brojBateija;
        private double[] snageBaterija;
        private double[] kapacitetiBaterija;
        private double snagaEVC;
        private double cenaUtility;
        private int brojPotrosaca;
        private double[] snagePotrosaca;

        private double[] kapacitetiBaterijaPogresno1;

        private double[] snagePanelaNegativno;
        private double[] snageBaterijaNegativno;
        private double[] snagePotrosacaNegativno;
        private double[] kapacitetiBaterijaNegativno;


        [SetUp]
        public void Setup()
        {
            shesg = new SHES.SHESGUI();

            //inicijalizovani dobri parametri
            brojPanela = 2;
            snagePanela = new double[2] { 5, 10 };
            brojBateija = 3;
            snageBaterija = new double[3] { 100, 200, 300 };
            kapacitetiBaterija = new double[3] { 500, 1000, 2000 };
            snagaEVC = 1200;
            cenaUtility = 50;
            brojPotrosaca = 1;
            snagePotrosaca = new double[1] { 250 };

            kapacitetiBaterijaPogresno1 = new double[2] { 10, 20 };

            snagePanelaNegativno = new double[2] { 1500, -1000 };
            snageBaterijaNegativno = new double[3] { -100, 200, -300 };
            kapacitetiBaterijaNegativno = new double[3] { 200, -500, 700 };
            snagePotrosacaNegativno = new double[1] { -70 };


        }

        [Test]
        public void DobriParametri()
        {
            shesg.Initialize(brojPanela, snagePanela, brojBateija, snageBaterija, kapacitetiBaterija, 
                snagaEVC, cenaUtility, brojPotrosaca, snagePotrosaca, true, true);

            Assert.AreEqual(brojPanela, SHES.SHESGUI.brojPanelaBuffer);
            Assert.AreEqual(snagePanela, SHES.SHESGUI.snagePanelaBuffer);
            Assert.AreEqual(snageBaterija, SHES.SHESGUI.snageBaterijaBuffer);
            Assert.AreEqual(kapacitetiBaterija, SHES.SHESGUI.kapacitetiBaterijaBuffer);
            Assert.AreEqual(snagaEVC, SHES.SHESGUI.snagaEVCBuffer);
            Assert.AreEqual(cenaUtility, SHES.SHESGUI.cenaUtilityBuffer);
            Assert.AreEqual(brojPotrosaca, SHES.SHESGUI.brojPotrosacaBuffer);
            Assert.AreEqual(snagePotrosaca, SHES.SHESGUI.snagePotrosacaBuffer);

        }

        [Test]
        public void LosiParametri()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () =>
                {
                    shesg.Initialize(3, snagePanela, brojBateija, snageBaterija, kapacitetiBaterija,
                        snagaEVC, cenaUtility, brojPotrosaca, snagePotrosaca, true, true);
                }
                );

            Assert.Throws<ArgumentOutOfRangeException>(
                () =>
                {
                    shesg.Initialize(-1, snagePanela, brojBateija, snageBaterija, kapacitetiBaterija,
                        snagaEVC, cenaUtility, brojPotrosaca, snagePotrosaca, true, true);
                }
                );

            Assert.Throws<ArgumentOutOfRangeException>(
                () =>
                {
                    shesg.Initialize(brojPanela, snagePanela, 5, snageBaterija, kapacitetiBaterija,
                        snagaEVC, cenaUtility, brojPotrosaca, snagePotrosaca, true, true);
                }
                );

            Assert.Throws<ArgumentOutOfRangeException>(
                () =>
                {
                    shesg.Initialize(brojPanela, snagePanela, -2, snageBaterija, kapacitetiBaterija,
                        snagaEVC, cenaUtility, brojPotrosaca, snagePotrosaca, true, true);
                }
                );

            Assert.Throws<ArgumentOutOfRangeException>(
                () =>
                {
                    shesg.Initialize(brojPanela, snagePanela, brojBateija, snageBaterija, kapacitetiBaterijaPogresno1,
                        snagaEVC, cenaUtility, brojPotrosaca, snagePotrosaca, true, true);
                }
                );

            Assert.Throws<ArgumentOutOfRangeException>(
                () =>
                {
                    shesg.Initialize(brojPanela, snagePanela, brojBateija, snageBaterija, kapacitetiBaterija,
                        -50, cenaUtility, brojPotrosaca, snagePotrosaca, true, true);
                }
                );

            Assert.Throws<ArgumentOutOfRangeException>(
                () =>
                {
                    shesg.Initialize(brojPanela, snagePanela, brojBateija, snageBaterija, kapacitetiBaterija,
                        snagaEVC, -200, brojPotrosaca, snagePotrosaca, true, true);
                }
                );

            Assert.Throws<ArgumentOutOfRangeException>(
                () =>
                {
                    shesg.Initialize(brojPanela, snagePanela, brojBateija, snageBaterija, kapacitetiBaterija,
                        snagaEVC, cenaUtility, -7, snagePotrosaca, true, true);
                }
                );

            Assert.Throws<ArgumentOutOfRangeException>(
                () =>
                {
                    shesg.Initialize(brojPanela, snagePanela, brojBateija, snageBaterija, kapacitetiBaterija,
                        snagaEVC, cenaUtility, 5, snagePotrosaca, true, true);
                }
                );

            Assert.Throws<ArgumentOutOfRangeException>(
               () =>
               {
                   shesg.Initialize(brojPanela, snagePanelaNegativno, brojBateija, snageBaterija, kapacitetiBaterija,
                       snagaEVC, cenaUtility, brojPotrosaca, snagePotrosaca, true, true);
               }
               );

            Assert.Throws<ArgumentOutOfRangeException>(
               () =>
               {
                   shesg.Initialize(brojPanela, snagePanela, brojBateija, snageBaterijaNegativno, kapacitetiBaterija,
                       snagaEVC, cenaUtility, brojPotrosaca, snagePotrosaca, true, true);
               }
               );

            Assert.Throws<ArgumentOutOfRangeException>(
               () =>
               {
                   shesg.Initialize(brojPanela, snagePanela, brojBateija, snageBaterija, kapacitetiBaterijaNegativno,
                       snagaEVC, cenaUtility, brojPotrosaca, snagePotrosaca, true, true);
               }
               );

            Assert.Throws<ArgumentOutOfRangeException>(
               () =>
               {
                   shesg.Initialize(brojPanela, snagePanela, brojBateija, snageBaterija, kapacitetiBaterija,
                       snagaEVC, cenaUtility, brojPotrosaca, snagePotrosacaNegativno, true, true);
               }
               );

        }

    }
}
