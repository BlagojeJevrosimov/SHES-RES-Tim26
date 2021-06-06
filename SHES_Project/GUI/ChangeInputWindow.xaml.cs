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

            

        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void BtnChange_Click(object sender, RoutedEventArgs e)
        {
            //parsirati unos svaki
            ChannelFactory<ISolarPanelGUI> SolarPanelChannel = new ChannelFactory<ISolarPanelGUI>("ISolarPanelGUI");
            ISolarPanelGUI proxySP = SolarPanelChannel.CreateChannel();

            ChannelFactory<IEVChargerGUI> EVChargerChannel = new ChannelFactory<IEVChargerGUI>("IEVChargerGUI");
            IEVChargerGUI proxyEV = EVChargerChannel.CreateChannel();

            ChannelFactory<IConsumerGUI> ConsumerChannel = new ChannelFactory<IConsumerGUI>("IConsumerGUI");
            IConsumerGUI proxyConsumer = ConsumerChannel.CreateChannel();

            ChannelFactory<IUtilityGUI> UtilityChannel = new ChannelFactory<IUtilityGUI>("IUtilityGUI");
            IUtilityGUI proxyUtility = UtilityChannel.CreateChannel();

            double sunIntensity = 0;
            var consumerRezim = Common.Enums.ConsumerRezim.OFF;
            int consumerID = 0;
            var ev = Common.Enums.BatteryRezim.PRAZNJENJE;
            double util = 0;

            if (txtSun.Text != null && txtSun.Text != "")
            {
                if(double.TryParse(txtSun.Text, out sunIntensity) && sunIntensity >= 0 && sunIntensity <= 1)
                {
                    proxySP.ChangeSunIntensity(sunIntensity);
                    txtSun.Text = "";
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Intenzitet Sunca mora biti broj u intervalu 0-1!");
                }
            }

            if(txtConsumerId.Text != null && txtConsumerId.Text != "")
            {
                if(Int32.TryParse(txtConsumerId.Text, out consumerID))
                {
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
                        proxyConsumer.ChangeConsumerState(consumerID, consumerRezim);
                        txtConsumerId.Text = "";
                    }
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Id potrosaca mora biti nenegativan broj!");
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
               proxyEV.SendRegime(Convert.ToBoolean(cmbBoxBatteryOnPlug.Text), ev);
            }

            if(txtUtilityPrice.Text != null && txtUtilityPrice.Text != "")
            {
                if(double.TryParse(txtUtilityPrice.Text, out util) && util >= 0)
                {
                    util = double.Parse(txtUtilityPrice.Text);
                    proxyUtility.SendPrice(util);
                    txtUtilityPrice.Text = "";
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Cena elektrodistribucije mora biti pozitivan broj!");
                }
            }
        }

        private void BtnShowGraph_Click(object sender, RoutedEventArgs e)
        {
            ChooseGraphWindow choose = new ChooseGraphWindow();
            choose.Show();
            this.Close();
        }
    }
}
