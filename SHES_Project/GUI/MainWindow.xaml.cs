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
        public MainWindow()
        {
            InitializeComponent();
            ChannelFactory<ISolarPanelGUI> channel = new ChannelFactory<ISolarPanelGUI>("ISolarPanelGUI");
            ISolarPanelGUI proxy = channel.CreateChannel();
            proxy.InitializeSolarPanels(3, new double[] { 50, 100, 200 });
            proxy.ChangeSunIntensity(0.5);
            
        }
    }
}
