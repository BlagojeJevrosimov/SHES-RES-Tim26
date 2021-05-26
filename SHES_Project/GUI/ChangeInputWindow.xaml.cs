using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GUI
{
    /// <summary>
    /// Interaction logic for ChangeDataWindow.xaml
    /// </summary>
    public partial class ChangeInputWindow : Window
    {
        public ChangeInputWindow()
        {
            InitializeComponent();

            List<string> rezimConsumer = new List<string> { Common.Enums.ConsumerRezim.OFF.ToString(), Common.Enums.ConsumerRezim.ON.ToString() };
            cmbBoxConsumer.ItemsSource = rezimConsumer;

            List<string> rezimBattery = new List<string> { Common.Enums.BatteryRezim.PUNJENJE.ToString(), Common.Enums.BatteryRezim.PRAZNJENJE.ToString() };
            cmbBoxBattery.ItemsSource = rezimBattery;

            ChannelFactory<ISolarPanelGUI> SolarPanelChannel = new ChannelFactory<ISolarPanelGUI>("ISolarPanelGUI");
            CommunicationData.proxySP = SolarPanelChannel.CreateChannel();

            ChannelFactory<IEVChargerGUI> EVChargerChannel = new ChannelFactory<IEVChargerGUI>("IEVChargerGUI");
            CommunicationData.proxyEV = EVChargerChannel.CreateChannel();

            
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void BtnChange_Click(object sender, RoutedEventArgs e)
        {
            //parsirati unos svaki
            double sunIntensity = 0;
            var consumer = Common.Enums.ConsumerRezim.OFF;
            var ev = Common.Enums.BatteryRezim.IDLE;

            if (txtSun.Text != null && txtSun.Text != "")
            {
                sunIntensity = double.Parse(txtSun.Text);
                CommunicationData.proxySP.ChangeSunIntensity(sunIntensity);
                txtSun.Text = "";
            }

            if (cmbBoxConsumer.Text != null && cmbBoxConsumer.Text != "")
            {
                switch (cmbBoxConsumer.Text)
                {
                    case "ON":
                        consumer = Enums.ConsumerRezim.ON;
                        break;
                    default:
                        consumer = Enums.ConsumerRezim.OFF;
                        break;
                }

                //proxy.ChangeSunIntensity(sunIntensity);
            }

            if (cmbBoxBattery.Text != null && cmbBoxBattery.Text != "")
            {
                switch (cmbBoxBattery.Text)
                {
                    case "PUNJENJE":
                        ev = Enums.BatteryRezim.PUNJENJE;
                        break;
                    default:
                        ev = Enums.BatteryRezim.PRAZNJENJE;
                        break;
                }
                Trace.TraceInformation("GUI to EV: " + ev.ToString());
                CommunicationData.proxyEV.SendRegime(ev);
            }
        }

        private void BtnInitialize_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
