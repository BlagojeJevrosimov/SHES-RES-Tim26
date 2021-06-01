using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
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
    [ExcludeFromCodeCoverage]
    public partial class ChangeInputWindow : Window
    {
        public ChangeInputWindow()
        {
            InitializeComponent();

            List<string> rezimConsumer = new List<string> { Common.Enums.ConsumerRezim.OFF.ToString(), Common.Enums.ConsumerRezim.ON.ToString() };
            cmbBoxConsumer.ItemsSource = rezimConsumer;

            List<string> rezimBattery = new List<string> { Common.Enums.BatteryRezim.PUNJENJE.ToString(), Common.Enums.BatteryRezim.PRAZNJENJE.ToString() };
            cmbBoxBatteryRegime.ItemsSource = rezimBattery;

            List<bool> plugBattery = new List<bool> { true, false };
            cmbBoxBatteryOnPlug.ItemsSource = plugBattery;

            ChannelFactory<ISolarPanelGUI> SolarPanelChannel = new ChannelFactory<ISolarPanelGUI>("ISolarPanelGUI");
            CommunicationData.proxySP = SolarPanelChannel.CreateChannel();

            ChannelFactory<IEVChargerGUI> EVChargerChannel = new ChannelFactory<IEVChargerGUI>("IEVChargerGUI");
            CommunicationData.proxyEV = EVChargerChannel.CreateChannel();

            ChannelFactory<IConsumerGUI> ConsumerChannel = new ChannelFactory<IConsumerGUI>("IConsumerGUI");
            CommunicationData.proxyConsumer = ConsumerChannel.CreateChannel();

        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void BtnChange_Click(object sender, RoutedEventArgs e)
        {
            //parsirati unos svaki
            double sunIntensity = 0;
            var consumerRezim = Common.Enums.ConsumerRezim.OFF;
            int consumerID = 0;
            var ev = Common.Enums.BatteryRezim.IDLE;

            if (txtSun.Text != null && txtSun.Text != "")
            {
                sunIntensity = double.Parse(txtSun.Text);
                CommunicationData.proxySP.ChangeSunIntensity(sunIntensity);
                txtSun.Text = "";
            }

            if(txtConsumerId.Text != null && txtConsumerId.Text != "")
            {
                consumerID = Int32.Parse(txtConsumerId.Text);

                if (cmbBoxConsumer.Text != null && cmbBoxConsumer.Text != "")
                {
                    switch (cmbBoxConsumer.Text)
                    {
                        case "ON":
                            consumerRezim = Enums.ConsumerRezim.ON;
                            break;
                        default:
                            consumerRezim = Enums.ConsumerRezim.OFF;
                            break;
                    }
                    Trace.TraceInformation("GUI sending: Consumer id-" + consumerID + ", state-" + consumerRezim.ToString());
                    CommunicationData.proxyConsumer.ChangeConsumerState(consumerID, consumerRezim);
                    txtConsumerId.Text = "";
                }
            }

            if (cmbBoxBatteryRegime.Text != null && cmbBoxBatteryRegime.Text != "")
            {
                switch (cmbBoxBatteryRegime.Text)
                {
                    case "PUNJENJE":
                        ev = Enums.BatteryRezim.PUNJENJE;
                        break;
                    default:
                        ev = Enums.BatteryRezim.PRAZNJENJE;
                        break;
                }
                Trace.TraceInformation("GUI to EV: " + Convert.ToBoolean(cmbBoxBatteryOnPlug.Text) + " " + ev.ToString());
                CommunicationData.proxyEV.SendRegime(Convert.ToBoolean(cmbBoxBatteryOnPlug.Text), ev);
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
