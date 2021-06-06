using Common;
using Common.DTO;
using SHES;
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
        public double sum;
        public int count;
        public bool done;
        public double maxHgt;

        public BatteryDiagram(DateTime date, string id)
        {
            InitializeComponent();
            maxPowers = new List<double>();

            sum = 0;
            count = 0;
            done = false;
            maxHgt = 0;

            naslov.Text += "Date: " + date + ", ID: " + id;
            ChannelFactory<ISHESGUI> channel = new ChannelFactory<ISHESGUI>("ISHESGUI");
            ISHESGUI proxy = channel.CreateChannel();
            data = CommunicationData.proxySHES.GetBatteryData(date,id);

            ViewModelBattery vm = new ViewModelBattery();
            vm.Data = data;

            var beginning = new DateTime(2021, 01, 01, 0, 0, 0);
            TimeSpan hour = new TimeSpan(1, 0, 0);
            TimeSpan fiveHours = new TimeSpan(5, 0, 0);
            TimeSpan tenHours = new TimeSpan(10, 0, 0);
            TimeSpan fftHours = new TimeSpan(15, 0, 0);
            TimeSpan twtHours = new TimeSpan(20, 0, 0);

            //za svaku bateriju ucitati podatak iz data i upisati u odgovarajuci bar

            foreach (var bat in data)
            {
                maxPowers.Add(bat.MaxPower);
            }

            double temp1 = maxPowers.Max();
            double temp2 = maxPowers.Min();

            if (Math.Abs(temp2) > temp1)
            {
                maxHgt = Math.Abs(temp2);
            }
            else
            {
                maxHgt = temp1;
            }

            for (int i = 0; i < data.Count; i++)
            {

                if (done)
                {
                    count = 0;
                    sum = 0;
                    done = false;
                }

                var bat = data[i];

                //ako je izmedju 0 i 1h
                if (bat.TimeAsDT >= bat.TimeAsDT.Date && bat.TimeAsDT < (bat.TimeAsDT.Date + hour))
                {
                    count++;

                    if (bat.State == Enums.BatteryRezim.PUNJENJE)
                    {
                        sum += bat.MaxPower;
                    }
                    else
                    {
                        sum -= bat.MaxPower;
                    }
                    bar1Positive.MaxValue = maxHgt;
                    bar1Negative.MaxValue = maxHgt;

                    //ako je ovo poslednji iz ovog vremenskog intervala
                    if ((i + 1) == data.Count || data[i + 1].TimeAsDT >= (bat.TimeAsDT.Date + hour))
                    {
                        if (sum >= 0)
                        {
                            bar1Positive.Value = Math.Round(sum / count);
                            bar1Negative.Value = 0;
                            bar1Negative.ForegroundClr = Brushes.Transparent;
                        }
                        else
                        {
                            bar1Positive.Value = 0;
                            bar1Negative.Value = Math.Abs(Math.Round(sum / count));
                            bar1Positive.ForegroundClr = Brushes.Transparent;
                        }

                        done = true;
                    }
                }
                //izmedju 1 i 2h
                else if (bat.TimeAsDT >= (bat.TimeAsDT.Date + hour) && bat.TimeAsDT < (bat.TimeAsDT.Date + hour + hour))
                {
                    count++;
                    if (bat.State == Enums.BatteryRezim.PUNJENJE)
                    {
                        sum += bat.MaxPower;
                    }
                    else
                    {
                        sum -= bat.MaxPower;
                    }
                    bar2Positive.MaxValue = maxHgt;
                    bar2Negative.MaxValue = maxHgt;

                    //ako je ovo poslednji iz ovog vremenskog intervala
                    if ((i + 1) == data.Count || data[i + 1].TimeAsDT >= (bat.TimeAsDT.Date + hour + hour))
                    {
                        if (sum >= 0)
                        {
                            bar2Positive.Value = Math.Round(sum / count);
                            bar2Negative.Value = 0;
                            bar2Negative.ForegroundClr = Brushes.Transparent;
                        }
                        else
                        {
                            bar2Positive.Value = 0;
                            bar2Negative.Value = Math.Abs(Math.Round(sum / count));
                            bar2Positive.ForegroundClr = Brushes.Transparent;
                        }

                        done = true;
                    }
                }
                //izmedju 2 i 3h
                else if (bat.TimeAsDT >= (bat.TimeAsDT.Date + hour + hour) && bat.TimeAsDT < (bat.TimeAsDT.Date + hour + hour + hour))
                {
                    count++;
                    if (bat.State == Enums.BatteryRezim.PUNJENJE)
                    {
                        sum += bat.MaxPower;
                    }
                    else
                    {
                        sum -= bat.MaxPower;
                    }
                    bar3Positive.MaxValue = maxHgt;
                    bar3Negative.MaxValue = maxHgt;

                    //ako je ovo poslednji iz ovog vremenskog intervala
                    if ((i + 1) == data.Count || data[i + 1].TimeAsDT >= (bat.TimeAsDT.Date + hour + hour + hour))
                    {
                        if (sum >= 0)
                        {
                            bar3Positive.Value = Math.Round(sum / count);
                            bar3Negative.Value = 0;
                            bar3Negative.ForegroundClr = Brushes.Transparent;
                        }
                        else
                        {
                            bar3Positive.Value = 0;
                            bar3Negative.Value = Math.Abs(Math.Round(sum / count));
                            bar3Positive.ForegroundClr = Brushes.Transparent;
                        }

                        done = true;
                    }
                }
                //izmedju 3 i 4h
                else if (bat.TimeAsDT >= (bat.TimeAsDT.Date + hour + hour + hour) && bat.TimeAsDT < (bat.TimeAsDT.Date + hour + hour + hour + hour))
                {
                    count++;
                    if (bat.State == Enums.BatteryRezim.PUNJENJE)
                    {
                        sum += bat.MaxPower;
                    }
                    else
                    {
                        sum -= bat.MaxPower;
                    }
                    bar4Positive.MaxValue = maxHgt;
                    bar4Negative.MaxValue = maxHgt;

                    //ako je ovo poslednji iz ovog vremenskog intervala
                    if ((i + 1) == data.Count || data[i + 1].TimeAsDT >= (bat.TimeAsDT.Date + hour + hour + hour + hour))
                    {
                        if (sum >= 0)
                        {
                            bar4Positive.Value = Math.Round(sum / count);
                            bar4Negative.Value = 0;
                            bar4Negative.ForegroundClr = Brushes.Transparent;
                        }
                        else
                        {
                            bar4Positive.Value = 0;
                            bar4Negative.Value = Math.Abs(Math.Round(sum / count));
                            bar4Positive.ForegroundClr = Brushes.Transparent;
                        }

                        done = true;
                    }
                }
                //izmedju 4 i 5h
                else if (bat.TimeAsDT >= (bat.TimeAsDT.Date + hour + hour + hour + hour) && bat.TimeAsDT < (bat.TimeAsDT.Date + fiveHours))
                {
                    count++;
                    if (bat.State == Enums.BatteryRezim.PUNJENJE)
                    {
                        sum += bat.MaxPower;
                    }
                    else
                    {
                        sum -= bat.MaxPower;
                    }
                    bar5Positive.MaxValue = maxHgt;
                    bar5Negative.MaxValue = maxHgt;

                    //ako je ovo poslednji iz ovog vremenskog intervala
                    if ((i + 1) == data.Count || data[i + 1].TimeAsDT >= (bat.TimeAsDT.Date + fiveHours))
                    {
                        if (sum >= 0)
                        {
                            bar5Positive.Value = Math.Round(sum / count);
                            bar5Negative.Value = 0;
                            bar5Negative.ForegroundClr = Brushes.Transparent;
                        }
                        else
                        {
                            bar5Positive.Value = 0;
                            bar5Negative.Value = Math.Abs(Math.Round(sum / count));
                            bar5Positive.ForegroundClr = Brushes.Transparent;
                        }

                        done = true;
                    }
                }
                //izmedju 5 i 6
                else if (bat.TimeAsDT >= (bat.TimeAsDT.Date + fiveHours) && bat.TimeAsDT < (bat.TimeAsDT.Date + fiveHours + hour))
                {
                    count++;
                    if (bat.State == Enums.BatteryRezim.PUNJENJE)
                    {
                        sum += bat.MaxPower;
                    }
                    else
                    {
                        sum -= bat.MaxPower;
                    }
                    bar6Positive.MaxValue = maxHgt;
                    bar6Negative.MaxValue = maxHgt;

                    //ako je ovo poslednji iz ovog vremenskog intervala
                    if ((i + 1) == data.Count || data[i + 1].TimeAsDT >= (bat.TimeAsDT.Date + fiveHours + hour))
                    {
                        if (sum >= 0)
                        {
                            bar6Positive.Value = Math.Round(sum / count);
                            bar6Negative.Value = 0;
                            bar6Negative.ForegroundClr = Brushes.Transparent;
                        }
                        else
                        {
                            bar6Positive.Value = 0;
                            bar6Negative.Value = Math.Abs(Math.Round(sum / count));
                            bar6Positive.ForegroundClr = Brushes.Transparent;
                        }

                        done = true;
                    }
                }
                //izmedju 6 i 7
                else if (bat.TimeAsDT >= (bat.TimeAsDT.Date + fiveHours + hour) && bat.TimeAsDT < (bat.TimeAsDT.Date + fiveHours + hour + hour))
                {
                    count++;
                    if (bat.State == Enums.BatteryRezim.PUNJENJE)
                    {
                        sum += bat.MaxPower;
                    }
                    else
                    {
                        sum -= bat.MaxPower;
                    }
                    bar7Positive.MaxValue = maxHgt;
                    bar7Negative.MaxValue = maxHgt;

                    //ako je ovo poslednji iz ovog vremenskog intervala
                    if ((i + 1) == data.Count || data[i + 1].TimeAsDT >= (bat.TimeAsDT.Date + fiveHours + hour + hour))
                    {
                        if (sum >= 0)
                        {
                            bar7Positive.Value = Math.Round(sum / count);
                            bar7Negative.Value = 0;
                            bar7Negative.ForegroundClr = Brushes.Transparent;
                        }
                        else
                        {
                            bar7Positive.Value = 0;
                            bar7Negative.Value = Math.Abs(Math.Round(sum / count));
                            bar7Positive.ForegroundClr = Brushes.Transparent;
                        }

                        done = true;
                    }
                }
                //izmedju 7 i 8h
                else if (bat.TimeAsDT >= (bat.TimeAsDT.Date + fiveHours + hour + hour) && bat.TimeAsDT < (bat.TimeAsDT.Date + fiveHours + hour + hour + hour))
                {
                    count++;
                    if (bat.State == Enums.BatteryRezim.PUNJENJE)
                    {
                        sum += bat.MaxPower;
                    }
                    else
                    {
                        sum -= bat.MaxPower;
                    }
                    bar8Positive.MaxValue = maxHgt;
                    bar8Negative.MaxValue = maxHgt;

                    //ako je ovo poslednji iz ovog vremenskog intervala
                    if ((i + 1) == data.Count || data[i + 1].TimeAsDT >= (bat.TimeAsDT.Date + fiveHours + hour + hour + hour))
                    {
                        if (sum >= 0)
                        {
                            bar8Positive.Value = Math.Round(sum / count);
                            bar8Negative.Value = 0;
                            bar8Negative.ForegroundClr = Brushes.Transparent;
                        }
                        else
                        {
                            bar8Positive.Value = 0;
                            bar8Negative.Value = Math.Abs(Math.Round(sum / count));
                            bar8Positive.ForegroundClr = Brushes.Transparent;
                        }

                        done = true;
                    }
                }
                //izmedju 8 i 9h
                else if (bat.TimeAsDT >= (bat.TimeAsDT.Date + fiveHours + hour + hour + hour) && bat.TimeAsDT < (bat.TimeAsDT.Date + fiveHours + hour + hour + hour + hour))
                {
                    count++;
                    if (bat.State == Enums.BatteryRezim.PUNJENJE)
                    {
                        sum += bat.MaxPower;
                    }
                    else
                    {
                        sum -= bat.MaxPower;
                    }
                    bar9Positive.MaxValue = maxHgt;
                    bar9Negative.MaxValue = maxHgt;

                    //ako je ovo poslednji iz ovog vremenskog intervala
                    if ((i + 1) == data.Count || data[i + 1].TimeAsDT >= (bat.TimeAsDT.Date + fiveHours + hour + hour + hour + hour))
                    {
                        if (sum >= 0)
                        {
                            bar9Positive.Value = Math.Round(sum / count);
                            bar9Negative.Value = 0;
                            bar9Negative.ForegroundClr = Brushes.Transparent;
                        }
                        else
                        {
                            bar9Positive.Value = 0;
                            bar9Negative.Value = Math.Abs(Math.Round(sum / count));
                            bar9Positive.ForegroundClr = Brushes.Transparent;
                        }

                        done = true;
                    }
                }
                //izmedju 9 i 10
                else if (bat.TimeAsDT >= (bat.TimeAsDT.Date + fiveHours + hour + hour + hour + hour) && bat.TimeAsDT < (bat.TimeAsDT.Date + tenHours))
                {
                    count++;
                    if (bat.State == Enums.BatteryRezim.PUNJENJE)
                    {
                        sum += bat.MaxPower;
                    }
                    else
                    {
                        sum -= bat.MaxPower;
                    }
                    bar10Positive.MaxValue = maxHgt;
                    bar10Negative.MaxValue = maxHgt;

                    //ako je ovo poslednji iz ovog vremenskog intervala
                    if ((i + 1) == data.Count || data[i + 1].TimeAsDT >= (bat.TimeAsDT.Date + tenHours))
                    {
                        if (sum >= 0)
                        {
                            bar10Positive.Value = Math.Round(sum / count);
                            bar10Negative.Value = 0;
                            bar10Negative.ForegroundClr = Brushes.Transparent;
                        }
                        else
                        {
                            bar10Positive.Value = 0;
                            bar10Negative.Value = Math.Abs(Math.Round(sum / count));
                            bar10Positive.ForegroundClr = Brushes.Transparent;
                        }

                        done = true;
                    }
                }
                //izmedju 10 i 11
                else if (bat.TimeAsDT >= (bat.TimeAsDT.Date + fiveHours + fiveHours) && bat.TimeAsDT < (bat.TimeAsDT.Date + fiveHours + fiveHours + hour))
                {
                    count++;
                    if (bat.State == Enums.BatteryRezim.PUNJENJE)
                    {
                        sum += bat.MaxPower;
                    }
                    else
                    {
                        sum -= bat.MaxPower;
                    }
                    bar11Positive.MaxValue = maxHgt;
                    bar11Negative.MaxValue = maxHgt;

                    //ako je ovo poslednji iz ovog vremenskog intervala
                    if ((i + 1) == data.Count || data[i + 1].TimeAsDT >= (bat.TimeAsDT.Date + tenHours + hour))
                    {
                        if (sum >= 0)
                        {
                            bar11Positive.Value = Math.Round(sum / count);
                            bar11Negative.Value = 0;
                            bar11Negative.ForegroundClr = Brushes.Transparent;
                        }
                        else
                        {
                            bar11Positive.Value = 0;
                            bar11Negative.Value = Math.Abs(Math.Round(sum / count));
                            bar11Positive.ForegroundClr = Brushes.Transparent;
                        }

                        done = true;
                    }
                }
                //izmedju 11 i 12
                else if (bat.TimeAsDT >= (bat.TimeAsDT.Date + fiveHours + fiveHours + hour) && bat.TimeAsDT < (bat.TimeAsDT.Date + fiveHours + fiveHours + hour + hour))
                {
                    count++;
                    if (bat.State == Enums.BatteryRezim.PUNJENJE)
                    {
                        sum += bat.MaxPower;
                    }
                    else
                    {
                        sum -= bat.MaxPower;
                    }
                    bar12Positive.MaxValue = maxHgt;
                    bar12Negative.MaxValue = maxHgt;

                    //ako je ovo poslednji iz ovog vremenskog intervala
                    if ((i + 1) == data.Count || data[i + 1].TimeAsDT >= (bat.TimeAsDT.Date + tenHours + hour + hour))
                    {
                        if (sum >= 0)
                        {
                            bar12Positive.Value = Math.Round(sum / count);
                            bar12Negative.Value = 0;
                            bar12Negative.ForegroundClr = Brushes.Transparent;
                        }
                        else
                        {
                            bar12Positive.Value = 0;
                            bar12Negative.Value = Math.Abs(Math.Round(sum / count));
                            bar12Positive.ForegroundClr = Brushes.Transparent;
                        }

                        done = true;
                    }
                }
                //izmedju 12 i 13
                else if (bat.TimeAsDT >= (bat.TimeAsDT.Date + fiveHours + fiveHours + hour + hour) && bat.TimeAsDT < (bat.TimeAsDT.Date + tenHours + hour + hour + hour))
                {
                    count++;
                    if (bat.State == Enums.BatteryRezim.PUNJENJE)
                    {
                        sum += bat.MaxPower;
                    }
                    else
                    {
                        sum -= bat.MaxPower;
                    }
                    bar13Positive.MaxValue = maxHgt;
                    bar13Negative.MaxValue = maxHgt;

                    //ako je ovo poslednji iz ovog vremenskog intervala
                    if ((i + 1) == data.Count || data[i + 1].TimeAsDT >= (bat.TimeAsDT.Date + tenHours + hour + hour + hour))
                    {
                        if (sum >= 0)
                        {
                            bar13Positive.Value = Math.Round(sum / count);
                            bar13Negative.Value = 0;
                            bar13Negative.ForegroundClr = Brushes.Transparent;
                        }
                        else
                        {
                            bar13Positive.Value = 0;
                            bar13Negative.Value = Math.Abs(Math.Round(sum / count));
                            bar13Positive.ForegroundClr = Brushes.Transparent;
                        }

                        done = true;
                    }
                }
                //izmedju 13 i 14
                else if (bat.TimeAsDT >= (bat.TimeAsDT.Date + fiveHours + fiveHours + hour + hour + hour) && bat.TimeAsDT < (bat.TimeAsDT.Date + tenHours + hour + hour + hour + hour))
                {
                    count++;
                    if (bat.State == Enums.BatteryRezim.PUNJENJE)
                    {
                        sum += bat.MaxPower;
                    }
                    else
                    {
                        sum -= bat.MaxPower;
                    }
                    bar14Positive.MaxValue = maxHgt;
                    bar14Negative.MaxValue = maxHgt;

                    //ako je ovo poslednji iz ovog vremenskog intervala
                    if ((i + 1) == data.Count || data[i + 1].TimeAsDT >= (bat.TimeAsDT.Date + tenHours + hour + hour + hour + hour))
                    {
                        if (sum >= 0)
                        {
                            bar14Positive.Value = Math.Round(sum / count);
                            bar14Negative.Value = 0;
                            bar14Negative.ForegroundClr = Brushes.Transparent;
                        }
                        else
                        {
                            bar14Positive.Value = 0;
                            bar14Negative.Value = Math.Abs(Math.Round(sum / count));
                            bar14Positive.ForegroundClr = Brushes.Transparent;
                        }

                        done = true;
                    }
                }
                //izmedju 14 i 15
                else if (bat.TimeAsDT >= (bat.TimeAsDT.Date + tenHours + hour + hour + hour + hour) && bat.TimeAsDT < (bat.TimeAsDT.Date + fftHours))
                {
                    count++;
                    if (bat.State == Enums.BatteryRezim.PUNJENJE)
                    {
                        sum += bat.MaxPower;
                    }
                    else
                    {
                        sum -= bat.MaxPower;
                    }
                    bar15Positive.MaxValue = maxHgt;
                    bar15Negative.MaxValue = maxHgt;

                    //ako je ovo poslednji iz ovog vremenskog intervala
                    if ((i + 1) == data.Count || data[i + 1].TimeAsDT >= (bat.TimeAsDT.Date + fftHours))
                    {
                        if (sum >= 0)
                        {
                            bar15Positive.Value = Math.Round(sum / count);
                            bar15Negative.Value = 0;
                            bar15Negative.ForegroundClr = Brushes.Transparent;
                        }
                        else
                        {
                            bar15Positive.Value = 0;
                            bar15Negative.Value = Math.Abs(Math.Round(sum / count));
                            bar15Positive.ForegroundClr = Brushes.Transparent;
                        }

                        done = true;
                    }
                }
                //izmedju 15 i 16h
                else if (bat.TimeAsDT >= (bat.TimeAsDT.Date + fftHours) && bat.TimeAsDT < (bat.TimeAsDT.Date + fftHours + hour))
                {
                    count++;
                    if (bat.State == Enums.BatteryRezim.PUNJENJE)
                    {
                        sum += bat.MaxPower;
                    }
                    else
                    {
                        sum -= bat.MaxPower;
                    }
                    bar16Positive.MaxValue = maxHgt;
                    bar16Negative.MaxValue = maxHgt;

                    //ako je ovo poslednji iz ovog vremenskog intervala
                    if ((i + 1) == data.Count || data[i + 1].TimeAsDT >= (bat.TimeAsDT.Date + fftHours + hour))
                    {
                        if (sum >= 0)
                        {
                            bar16Positive.Value = Math.Round(sum / count);
                            bar16Negative.Value = 0;
                            bar16Negative.ForegroundClr = Brushes.Transparent;
                        }
                        else
                        {
                            bar16Positive.Value = 0;
                            bar16Negative.Value = Math.Abs(Math.Round(sum / count));
                            bar16Positive.ForegroundClr = Brushes.Transparent;
                        }

                        done = true;
                    }
                }
                //izmedju 16 i 17h
                else if (bat.TimeAsDT >= (bat.TimeAsDT.Date + fftHours + hour) && bat.TimeAsDT < (bat.TimeAsDT.Date + fftHours + hour + hour))
                {
                    count++;
                    if (bat.State == Enums.BatteryRezim.PUNJENJE)
                    {
                        sum += bat.MaxPower;
                    }
                    else
                    {
                        sum -= bat.MaxPower;
                    }
                    bar17Positive.MaxValue = maxHgt;
                    bar17Negative.MaxValue = maxHgt;

                    //ako je ovo poslednji iz ovog vremenskog intervala
                    if ((i + 1) == data.Count || data[i + 1].TimeAsDT >= (bat.TimeAsDT.Date + fftHours + hour + hour))
                    {
                        if (sum >= 0)
                        {
                            bar17Positive.Value = Math.Round(sum / count);
                            bar17Negative.Value = 0;
                            bar17Negative.ForegroundClr = Brushes.Transparent;
                        }
                        else
                        {
                            bar17Positive.Value = 0;
                            bar17Negative.Value = Math.Abs(Math.Round(sum / count));
                            bar17Positive.ForegroundClr = Brushes.Transparent;
                        }

                        done = true;
                    }
                }
                //izmedju 17 i 18h
                else if (bat.TimeAsDT >= (bat.TimeAsDT.Date + fftHours + hour + hour) && bat.TimeAsDT < (bat.TimeAsDT.Date + fftHours + hour + hour + hour))
                {
                    count++;
                    if (bat.State == Enums.BatteryRezim.PUNJENJE)
                    {
                        sum += bat.MaxPower;
                    }
                    else
                    {
                        sum -= bat.MaxPower;
                    }
                    bar18Positive.MaxValue = maxHgt;
                    bar18Negative.MaxValue = maxHgt;

                    //ako je ovo poslednji iz ovog vremenskog intervala
                    if ((i + 1) == data.Count || data[i + 1].TimeAsDT >= (bat.TimeAsDT.Date + fftHours + hour + hour + hour))
                    {
                        if (sum >= 0)
                        {
                            bar18Positive.Value = Math.Round(sum / count);
                            bar18Negative.Value = 0;
                            bar18Negative.ForegroundClr = Brushes.Transparent;
                        }
                        else
                        {
                            bar18Positive.Value = 0;
                            bar18Negative.Value = Math.Abs(Math.Round(sum / count));
                            bar18Positive.ForegroundClr = Brushes.Transparent;
                        }

                        done = true;
                    }
                }
                //izmedju 18 i 19h
                else if (bat.TimeAsDT >= (bat.TimeAsDT.Date + fftHours + hour + hour + hour) && bat.TimeAsDT < (bat.TimeAsDT.Date + fftHours + hour + hour + hour + hour))
                {
                    count++;
                    if (bat.State == Enums.BatteryRezim.PUNJENJE)
                    {
                        sum += bat.MaxPower;
                    }
                    else
                    {
                        sum -= bat.MaxPower;
                    }
                    bar19Positive.MaxValue = maxHgt;
                    bar19Negative.MaxValue = maxHgt;

                    //ako je ovo poslednji iz ovog vremenskog intervala
                    if ((i + 1) == data.Count || data[i + 1].TimeAsDT >= (bat.TimeAsDT.Date + fftHours + hour + hour + hour + hour))
                    {
                        if (sum >= 0)
                        {
                            bar19Positive.Value = Math.Round(sum / count);
                            bar19Negative.Value = 0;
                            bar19Negative.ForegroundClr = Brushes.Transparent;
                        }
                        else
                        {
                            bar19Positive.Value = 0;
                            bar19Negative.Value = Math.Abs(Math.Round(sum / count));
                            bar19Positive.ForegroundClr = Brushes.Transparent;
                        }

                        done = true;
                    }
                }
                //izmedju 19 i 20h
                else if (bat.TimeAsDT >= (bat.TimeAsDT.Date + fftHours + hour + hour + hour + hour) && bat.TimeAsDT < (bat.TimeAsDT.Date + twtHours))
                {
                    count++;
                    if (bat.State == Enums.BatteryRezim.PUNJENJE)
                    {
                        sum += bat.MaxPower;
                    }
                    else
                    {
                        sum -= bat.MaxPower;
                    }
                    bar20Positive.MaxValue = maxHgt;
                    bar20Negative.MaxValue = maxHgt;

                    //ako je ovo poslednji iz ovog vremenskog intervala
                    if ((i + 1) == data.Count || data[i + 1].TimeAsDT >= (bat.TimeAsDT.Date + twtHours))
                    {
                        if (sum >= 0)
                        {
                            bar20Positive.Value = Math.Round(sum / count);
                            bar20Negative.Value = 0;
                            bar20Negative.ForegroundClr = Brushes.Transparent;
                        }
                        else
                        {
                            bar20Positive.Value = 0;
                            bar20Negative.Value = Math.Abs(Math.Round(sum / count));
                            bar20Positive.ForegroundClr = Brushes.Transparent;
                        }

                        done = true;
                    }
                }
                //izmedju 20 i 21
                else if (bat.TimeAsDT >= (bat.TimeAsDT.Date + twtHours) && bat.TimeAsDT < (bat.TimeAsDT.Date + twtHours + hour))
                {
                    count++;
                    if (bat.State == Enums.BatteryRezim.PUNJENJE)
                    {
                        sum += bat.MaxPower;
                    }
                    else
                    {
                        sum -= bat.MaxPower;
                    }
                    bar21Positive.MaxValue = maxHgt;
                    bar21Negative.MaxValue = maxHgt;

                    //ako je ovo poslednji iz ovog vremenskog intervala
                    if ((i + 1) == data.Count || data[i + 1].TimeAsDT >= (bat.TimeAsDT.Date + twtHours + hour))
                    {
                        if (sum >= 0)
                        {
                            bar21Positive.Value = Math.Round(sum / count);
                            bar21Negative.Value = 0;
                            bar21Negative.ForegroundClr = Brushes.Transparent;
                        }
                        else
                        {
                            bar21Positive.Value = 0;
                            bar21Negative.Value = Math.Abs(Math.Round(sum / count));
                            bar21Positive.ForegroundClr = Brushes.Transparent;
                        }

                        done = true;
                    }
                }
                //izmedju 21 i 22h
                else if (bat.TimeAsDT >= (bat.TimeAsDT.Date + twtHours + hour) && bat.TimeAsDT < (bat.TimeAsDT.Date + twtHours + hour + hour))
                {
                    count++;
                    if (bat.State == Enums.BatteryRezim.PUNJENJE)
                    {
                        sum += bat.MaxPower;
                    }
                    else
                    {
                        sum -= bat.MaxPower;
                    }
                    bar22Positive.MaxValue = maxHgt;
                    bar22Negative.MaxValue = maxHgt;

                    //ako je ovo poslednji iz ovog vremenskog intervala
                    if ((i + 1) == data.Count || data[i + 1].TimeAsDT >= (bat.TimeAsDT.Date + twtHours + hour + hour))
                    {
                        if (sum >= 0)
                        {
                            bar22Positive.Value = Math.Round(sum / count);
                            bar22Negative.Value = 0;
                            bar22Negative.ForegroundClr = Brushes.Transparent;
                        }
                        else
                        {
                            bar22Positive.Value = 0;
                            bar22Negative.Value = Math.Abs(Math.Round(sum / count));
                            bar22Positive.ForegroundClr = Brushes.Transparent;
                        }

                        done = true;
                    }
                }
                //izmedju 22 i 23h
                else if (bat.TimeAsDT >= (bat.TimeAsDT.Date + twtHours + hour + hour) && bat.TimeAsDT < (bat.TimeAsDT.Date + twtHours + hour + hour + hour))
                {
                    count++;
                    if (bat.State == Enums.BatteryRezim.PUNJENJE)
                    {
                        sum += bat.MaxPower;
                    }
                    else
                    {
                        sum -= bat.MaxPower;
                    }
                    bar23Positive.MaxValue = maxHgt;
                    bar23Negative.MaxValue = maxHgt;

                    //ako je ovo poslednji iz ovog vremenskog intervala
                    if ((i + 1) == data.Count || data[i + 1].TimeAsDT >= (bat.TimeAsDT.Date + twtHours + hour + hour + hour))
                    {
                        if (sum >= 0)
                        {
                            bar23Positive.Value = Math.Round(sum / count);
                            bar23Negative.Value = 0;
                            bar23Negative.ForegroundClr = Brushes.Transparent;
                        }
                        else
                        {
                            bar23Positive.Value = 0;
                            bar23Negative.Value = Math.Abs(Math.Round(sum / count));
                            bar23Positive.ForegroundClr = Brushes.Transparent;
                        }

                        done = true;
                    }
                }
                //izmedju 23 i 24h
                else if (bat.TimeAsDT >= (bat.TimeAsDT.Date + twtHours + hour + hour + hour) && bat.TimeAsDT < (bat.TimeAsDT.Date + twtHours + hour + hour + hour + hour))
                {
                    count++;
                    if (bat.State == Enums.BatteryRezim.PUNJENJE)
                    {
                        sum += bat.MaxPower;
                    }
                    else
                    {
                        sum -= bat.MaxPower;
                    }
                    bar24Positive.MaxValue = maxHgt;
                    bar24Negative.MaxValue = maxHgt;

                    //ako je ovo poslednji iz ovog vremenskog intervala
                    if ((i + 1) == data.Count)
                    {
                        if (sum >= 0)
                        {
                            bar24Positive.Value = Math.Round(sum / count);
                            bar24Negative.Value = 0;
                            bar24Negative.ForegroundClr = Brushes.Transparent;
                        }
                        else
                        {
                            bar24Positive.Value = 0;
                            bar24Negative.Value = Math.Abs(Math.Round(sum / count));
                            bar24Positive.ForegroundClr = Brushes.Transparent;
                        }

                        done = true;
                    }
                }
            }

        }


        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChooseGraphWindow c = new ChooseGraphWindow();
            c.Show();
            this.Close();
        }
    }
}
