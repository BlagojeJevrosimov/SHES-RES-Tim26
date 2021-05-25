using Common;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static ISolarPanelGUI proxy;

        public MainWindow()
        {
            InitializeComponent();

            List<string> rezimConsumer = new List<string> { Common.Enums.ConsumerRezim.OFF.ToString(), Common.Enums.ConsumerRezim.ON.ToString() };
            cmbBoxConsumer.ItemsSource = rezimConsumer;

            List<string> rezimBattery = new List<string> { Common.Enums.BatteryRezim.PUNJENJE.ToString(), Common.Enums.BatteryRezim.PRAZNJENJE.ToString() };
            cmbBoxBattery.ItemsSource = rezimBattery;

            ChannelFactory<ISolarPanelGUI> channel = new ChannelFactory<ISolarPanelGUI>("ISolarPanelGUI");
            proxy = channel.CreateChannel();

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

            if(txtSun.Text != null && txtSun.Text != "")
            {
                sunIntensity = double.Parse(txtSun.Text);
                proxy.ChangeSunIntensity(sunIntensity);
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

        }
    }
}
