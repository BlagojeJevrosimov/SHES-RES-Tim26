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
        public List<BatteryDTO> data;
        public List<double> maxPowers;

        public BatteryDiagram(DateTime date, string id)
        {
            InitializeComponent();
            maxPowers = new List<double>();

            data = CommunicationData.proxySHES.GetBatteryData(date, id);

            ViewModelBattery vm = new ViewModelBattery();
            vm.data = data;

            var beginning = new DateTime(2021, 01, 01, 0, 0, 0);
            TimeSpan hour = new TimeSpan(1, 0, 0);
            TimeSpan fiveHours = new TimeSpan(5, 0, 0);
            TimeSpan tenHours = new TimeSpan(10, 0, 0);
            TimeSpan fftHours = new TimeSpan(15, 0, 0);
            TimeSpan twtHours = new TimeSpan(20, 0, 0);

            //za svaku bateriju ucitati podatak iz data i upisati u odgovarajuci bar
            foreach(var bat in data)
            {
                maxPowers.Add(bat.MaxPower);
            }

            for (int i = 0; i < data.Count(); i++)
            {
                var bat = data[i];

                //ako je izmedju 0 i 1h
                if (bat.TimeAsDT >= bat.TimeAsDT.Date && bat.TimeAsDT < (bat.TimeAsDT.Date + hour))
                {
                    if (bat.State == Enums.BatteryRezim.PUNJENJE)
                    {
                        bar1Positive.Value = bat.MaxPower;
                        bar1Negative.Value = 0;
                    }
                    else
                    {
                        bar1Positive.Value = 0;
                        bar1Negative.Value = bat.MaxPower;
                    }
                    bar1Positive.MaxValue = maxPowers.Max();
                    bar1Negative.MaxValue = maxPowers.Max();
                }
                //izmedju 1 i 2h
                else if (bat.TimeAsDT >= (bat.TimeAsDT.Date + hour) && bat.TimeAsDT < (bat.TimeAsDT.Date + hour + hour))
                {
                    if (bat.State == Enums.BatteryRezim.PUNJENJE)
                    {
                        bar2Positive.Value = bat.MaxPower;
                        bar2Negative.Value = 0;
                    }
                    else
                    {
                        bar2Positive.Value = 0;
                        bar2Negative.Value = bat.MaxPower;
                    }
                    bar2Positive.MaxValue = maxPowers.Max();
                    bar2Negative.MaxValue = maxPowers.Max();
                }
                //izmedju 2 i 3h
                else if (bat.TimeAsDT >= (bat.TimeAsDT.Date + hour + hour) && bat.TimeAsDT < (bat.TimeAsDT.Date + hour + hour + hour))
                {
                    if (bat.State == Enums.BatteryRezim.PUNJENJE)
                    {
                        bar3Positive.Value = bat.MaxPower;
                        bar3Negative.Value = 0;
                    }
                    else
                    {
                        bar3Positive.Value = 0;
                        bar3Negative.Value = bat.MaxPower;
                    }
                    bar3Positive.MaxValue = maxPowers.Max();
                    bar3Negative.MaxValue = maxPowers.Max();
                }
                //izmedju 3 i 4h
                else if (bat.TimeAsDT >= (bat.TimeAsDT.Date + hour + hour + hour) && bat.TimeAsDT < (bat.TimeAsDT.Date + hour + hour + hour + hour))
                {
                    if (bat.State == Enums.BatteryRezim.PUNJENJE)
                    {
                        bar4Positive.Value = bat.MaxPower;
                        bar4Negative.Value = 0;
                    }
                    else
                    {
                        bar4Positive.Value = 0;
                        bar4Negative.Value = bat.MaxPower;
                    }
                    bar4Positive.MaxValue = maxPowers.Max();
                    bar4Negative.MaxValue = maxPowers.Max();
                }
                //izmedju 4 i 5h
                else if (bat.TimeAsDT >= (bat.TimeAsDT.Date + hour + hour + hour + hour) && bat.TimeAsDT < (bat.TimeAsDT.Date + fiveHours))
                {
                    if (bat.State == Enums.BatteryRezim.PUNJENJE)
                    {
                        bar5Positive.Value = bat.MaxPower;
                        bar5Negative.Value = 0;
                    }
                    else
                    {
                        bar5Positive.Value = 0;
                        bar5Negative.Value = bat.MaxPower;
                    }
                    bar5Positive.MaxValue = maxPowers.Max();
                    bar5Negative.MaxValue = maxPowers.Max();
                }
                //izmedju 5 i 6
                else if (bat.TimeAsDT >= (bat.TimeAsDT.Date + fiveHours) && bat.TimeAsDT < (bat.TimeAsDT.Date + fiveHours + hour))
                {
                    if (bat.State == Enums.BatteryRezim.PUNJENJE)
                    {
                        bar6Positive.Value = bat.MaxPower;
                        bar6Negative.Value = 0;
                    }
                    else
                    {
                        bar6Positive.Value = 0;
                        bar6Negative.Value = bat.MaxPower;
                    }
                    bar6Positive.MaxValue = maxPowers.Max();
                    bar6Negative.MaxValue = maxPowers.Max();
                }
                //izmedju 6 i 7
                else if (bat.TimeAsDT >= (bat.TimeAsDT.Date + fiveHours + hour) && bat.TimeAsDT < (bat.TimeAsDT.Date + fiveHours + hour + hour))
                {
                    if (bat.State == Enums.BatteryRezim.PUNJENJE)
                    {
                        bar7Positive.Value = bat.MaxPower;
                        bar7Negative.Value = 0;
                    }
                    else
                    {
                        bar7Positive.Value = 0;
                        bar7Negative.Value = bat.MaxPower;
                    }
                    bar7Positive.MaxValue = maxPowers.Max();
                    bar7Negative.MaxValue = maxPowers.Max();
                }
                //izmedju 7 i 8h
                else if (bat.TimeAsDT >= (bat.TimeAsDT.Date + fiveHours + hour + hour) && bat.TimeAsDT < (bat.TimeAsDT.Date + fiveHours + hour + hour + hour))
                {
                    if (bat.State == Enums.BatteryRezim.PUNJENJE)
                    {
                        bar8Positive.Value = bat.MaxPower;
                        bar8Negative.Value = 0;
                    }
                    else
                    {
                        bar8Positive.Value = 0;
                        bar8Negative.Value = bat.MaxPower;
                    }
                    bar8Positive.MaxValue = maxPowers.Max();
                    bar8Negative.MaxValue = maxPowers.Max();
                }
                //izmedju 8 i 9h
                else if (bat.TimeAsDT >= (bat.TimeAsDT.Date + fiveHours + hour + hour + hour) && bat.TimeAsDT < (bat.TimeAsDT.Date + fiveHours + hour + hour + hour + hour))
                {
                    if (bat.State == Enums.BatteryRezim.PUNJENJE)
                    {
                        bar9Positive.Value = bat.MaxPower;
                        bar9Negative.Value = 0;
                    }
                    else
                    {
                        bar9Positive.Value = 0;
                        bar9Negative.Value = bat.MaxPower;
                    }
                    bar9Positive.MaxValue = maxPowers.Max();
                    bar9Negative.MaxValue = maxPowers.Max();
                }
                //izmedju 9 i 10
                else if (bat.TimeAsDT >= (bat.TimeAsDT.Date + fiveHours + hour + hour + hour + hour) && bat.TimeAsDT < (bat.TimeAsDT.Date + fiveHours + fiveHours))
                {
                    if (bat.State == Enums.BatteryRezim.PUNJENJE)
                    {
                        bar10Positive.Value = bat.MaxPower;
                        bar10Negative.Value = 0;
                    }
                    else
                    {
                        bar10Positive.Value = 0;
                        bar10Negative.Value = bat.MaxPower;
                    }
                    bar10Positive.MaxValue = maxPowers.Max();
                    bar10Negative.MaxValue = maxPowers.Max();
                }
                //izmedju 10 i 11
                else if (bat.TimeAsDT >= (bat.TimeAsDT.Date + fiveHours + fiveHours) && bat.TimeAsDT < (bat.TimeAsDT.Date + fiveHours + fiveHours + hour))
                {
                    if (bat.State == Enums.BatteryRezim.PUNJENJE)
                    {
                        bar11Positive.Value = bat.MaxPower;
                        bar11Negative.Value = 0;
                    }
                    else
                    {
                        bar11Positive.Value = 0;
                        bar11Negative.Value = bat.MaxPower;
                    }
                    bar11Positive.MaxValue = maxPowers.Max();
                    bar11Negative.MaxValue = maxPowers.Max();
                }
                //izmedju 11 i 12
                else if (bat.TimeAsDT >= (bat.TimeAsDT.Date + fiveHours + fiveHours + hour) && bat.TimeAsDT < (bat.TimeAsDT.Date + fiveHours + fiveHours + hour + hour))
                {
                    if (bat.State == Enums.BatteryRezim.PUNJENJE)
                    {
                        bar12Positive.Value = bat.MaxPower;
                        bar12Negative.Value = 0;
                    }
                    else
                    {
                        bar12Positive.Value = 0;
                        bar12Negative.Value = bat.MaxPower;
                    }
                    bar12Positive.MaxValue = maxPowers.Max();
                    bar12Negative.MaxValue = maxPowers.Max();
                }
                //izmedju 12 i 13
                else if (bat.TimeAsDT >= (bat.TimeAsDT.Date + fiveHours + fiveHours + hour + hour) && bat.TimeAsDT < (bat.TimeAsDT.Date + fiveHours + fiveHours + hour + hour + hour))
                {
                    if (bat.State == Enums.BatteryRezim.PUNJENJE)
                    {
                        bar13Positive.Value = bat.MaxPower;
                        bar13Negative.Value = 0;
                    }
                    else
                    {
                        bar13Positive.Value = 0;
                        bar13Negative.Value = bat.MaxPower;
                    }
                    bar13Positive.MaxValue = maxPowers.Max();
                    bar13Negative.MaxValue = maxPowers.Max();
                }
                //izmedju 13 i 14
                else if (bat.TimeAsDT >= (bat.TimeAsDT.Date + fiveHours + fiveHours + hour + hour + hour) && bat.TimeAsDT < (bat.TimeAsDT.Date + fiveHours + fiveHours + hour + hour + hour + hour))
                {
                    if (bat.State == Enums.BatteryRezim.PUNJENJE)
                    {
                        bar14Positive.Value = bat.MaxPower;
                        bar14Negative.Value = 0;
                    }
                    else
                    {
                        bar14Positive.Value = 0;
                        bar14Negative.Value = bat.MaxPower;
                    }
                    bar14Positive.MaxValue = maxPowers.Max();
                    bar14Negative.MaxValue = maxPowers.Max();
                }
                //izmedju 14 i 15
                else if (bat.TimeAsDT >= (bat.TimeAsDT.Date + tenHours + hour + hour + hour + hour) && bat.TimeAsDT < (bat.TimeAsDT.Date + fftHours))
                {
                    if (bat.State == Enums.BatteryRezim.PUNJENJE)
                    {
                        bar15Positive.Value = bat.MaxPower;
                        bar15Negative.Value = 0;
                    }
                    else
                    {
                        bar15Positive.Value = 0;
                        bar15Negative.Value = bat.MaxPower;
                    }
                    bar15Positive.MaxValue = maxPowers.Max();
                    bar15Negative.MaxValue = maxPowers.Max();
                }
                //izmedju 15 i 16h
                else if (bat.TimeAsDT >= (bat.TimeAsDT.Date + fftHours) && bat.TimeAsDT < (bat.TimeAsDT.Date + fftHours + hour))
                {
                    if (bat.State == Enums.BatteryRezim.PUNJENJE)
                    {
                        bar16Positive.Value = bat.MaxPower;
                        bar16Negative.Value = 0;
                    }
                    else
                    {
                        bar16Positive.Value = 0;
                        bar16Negative.Value = bat.MaxPower;
                    }
                    bar16Positive.MaxValue = maxPowers.Max();
                    bar16Negative.MaxValue = maxPowers.Max();
                }
                //izmedju 16 i 17h
                else if (bat.TimeAsDT >= (bat.TimeAsDT.Date + fftHours + hour) && bat.TimeAsDT < (bat.TimeAsDT.Date + fftHours + hour + hour))
                {
                    if (bat.State == Enums.BatteryRezim.PUNJENJE)
                    {
                        bar17Positive.Value = bat.MaxPower;
                        bar17Negative.Value = 0;
                    }
                    else
                    {
                        bar17Positive.Value = 0;
                        bar17Negative.Value = bat.MaxPower;
                    }
                    bar17Positive.MaxValue = maxPowers.Max();
                    bar17Negative.MaxValue = maxPowers.Max();
                }
                //izmedju 17 i 18h
                else if (bat.TimeAsDT >= (bat.TimeAsDT.Date + fftHours + hour + hour) && bat.TimeAsDT < (bat.TimeAsDT.Date + fftHours + hour + hour + hour))
                {
                    if (bat.State == Enums.BatteryRezim.PUNJENJE)
                    {
                        bar18Positive.Value = bat.MaxPower;
                        bar18Negative.Value = 0;
                    }
                    else
                    {
                        bar18Positive.Value = 0;
                        bar18Negative.Value = bat.MaxPower;
                    }
                    bar18Positive.MaxValue = maxPowers.Max();
                    bar18Negative.MaxValue = maxPowers.Max();
                }
                //izmedju 18 i 19h
                else if (bat.TimeAsDT >= (bat.TimeAsDT.Date + fftHours + hour + hour + hour) && bat.TimeAsDT < (bat.TimeAsDT.Date + fftHours + hour + hour + hour + hour))
                {
                    if (bat.State == Enums.BatteryRezim.PUNJENJE)
                    {
                        bar19Positive.Value = bat.MaxPower;
                        bar19Negative.Value = 0;
                    }
                    else
                    {
                        bar19Positive.Value = 0;
                        bar19Negative.Value = bat.MaxPower;
                    }
                    bar19Positive.MaxValue = maxPowers.Max();
                    bar19Negative.MaxValue = maxPowers.Max();
                }
                //izmedju 19 i 20h
                else if (bat.TimeAsDT >= (bat.TimeAsDT.Date + fftHours + hour + hour + hour + hour) && bat.TimeAsDT < (bat.TimeAsDT.Date + twtHours))
                {
                    if (bat.State == Enums.BatteryRezim.PUNJENJE)
                    {
                        bar20Positive.Value = bat.MaxPower;
                        bar20Negative.Value = 0;
                    }
                    else
                    {
                        bar20Positive.Value = 0;
                        bar20Negative.Value = bat.MaxPower;
                    }
                    bar20Positive.MaxValue = maxPowers.Max();
                    bar20Negative.MaxValue = maxPowers.Max();
                }
                //izmedju 20 i 21
                else if (bat.TimeAsDT >= (bat.TimeAsDT.Date + twtHours) && bat.TimeAsDT < (bat.TimeAsDT.Date + twtHours + hour))
                {
                    if (bat.State == Enums.BatteryRezim.PUNJENJE)
                    {
                        bar21Positive.Value = bat.MaxPower;
                        bar21Negative.Value = 0;
                    }
                    else
                    {
                        bar21Positive.Value = 0;
                        bar21Negative.Value = bat.MaxPower;
                    }
                    bar21Positive.MaxValue = maxPowers.Max();
                    bar21Negative.MaxValue = maxPowers.Max();
                }
                //izmedju 21 i 22h
                else if (bat.TimeAsDT >= (bat.TimeAsDT.Date + twtHours + hour) && bat.TimeAsDT < (bat.TimeAsDT.Date + twtHours + hour + hour))
                {
                    if (bat.State == Enums.BatteryRezim.PUNJENJE)
                    {
                        bar22Positive.Value = bat.MaxPower;
                        bar22Negative.Value = 0;
                    }
                    else
                    {
                        bar22Positive.Value = 0;
                        bar22Negative.Value = bat.MaxPower;
                    }
                    bar22Positive.MaxValue = maxPowers.Max();
                    bar22Negative.MaxValue = maxPowers.Max();
                }
                //izmedju 22 i 23h
                else if (bat.TimeAsDT >= (bat.TimeAsDT.Date + twtHours + hour + hour) && bat.TimeAsDT < (bat.TimeAsDT.Date + twtHours + hour + hour + hour))
                {
                    if (bat.State == Enums.BatteryRezim.PUNJENJE)
                    {
                        bar23Positive.Value = bat.MaxPower;
                        bar23Negative.Value = 0;
                    }
                    else
                    {
                        bar23Positive.Value = 0;
                        bar23Negative.Value = bat.MaxPower;
                    }
                    bar23Positive.MaxValue = maxPowers.Max();
                    bar23Negative.MaxValue = maxPowers.Max();
                }

            }

        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
