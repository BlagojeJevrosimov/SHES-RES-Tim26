using Common;
using Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for BatteryDiagram.xaml
    /// </summary>
    public partial class BatteryDiagram : Window
    {
        private List<BatteryDTO> data;

        public BatteryDiagram(DateTime date, string id)
        {
            InitializeComponent();

            data = CommunicationData.proxySHES.GetBatteryData(date, id);

            //PROBLEM PROBLEM PROBLEM
            //ViewModelBattery vm = new ViewModelBattery(data);
        }
    }
}
