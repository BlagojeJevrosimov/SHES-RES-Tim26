using Common;
using Common.DTO;
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
    /// Interaction logic for SolarPanelsDiagram.xaml
    /// </summary>
    public partial class SolarPanelsDiagram : Window
    {
        public List<SolarPanelsDTO> data;
        public List<double> maxPowers;
        public double sum;
        public int count;
        public bool done;
        public double maxHgt;

        public SolarPanelsDiagram(DateTime date)
        {
            InitializeComponent();
            maxPowers = new List<double>();

            sum = 0;
            count = 0;
            done = false;
            maxHgt = 0;

            naslov.Text += "Date: " + date;
            ChannelFactory<ISHESGUI> channel = new ChannelFactory<ISHESGUI>("ISHESGUI");
            ISHESGUI proxy = channel.CreateChannel();

            data = proxy.GetSolarPanelsData(date);

            ViewModelSP vm = new ViewModelSP();
            vm.Data = data;

            if (data.Count == 0)
            {
                ChooseGraphWindow c = new ChooseGraphWindow("Podaci nisu ucitani za izabrani datum!");
                c.Show();
                this.Close();
            }

            var beginning = new DateTime(2021, 01, 01, 0, 0, 0);
            TimeSpan hour = new TimeSpan(1, 0, 0);
            TimeSpan fiveHours = new TimeSpan(5, 0, 0);
            TimeSpan tenHours = new TimeSpan(10, 0, 0);
            TimeSpan fftHours = new TimeSpan(15, 0, 0);
            TimeSpan twtHours = new TimeSpan(20, 0, 0);

            //za svaki solarni panel ucitati podatak iz data i upisati u odgovarajuci bar

            foreach (var sp in data)
            {
                maxPowers.Add(sp.Power);
            }
            maxHgt = maxPowers.Max();

            for (int i = 0; i < data.Count; i++)
            {

                if (done)
                {
                    count = 0;
                    sum = 0;
                    done = false;
                }

                var sp = data[i];

                //ako je izmedju 0 i 1h
                if (sp.TimeAsDT >= sp.TimeAsDT.Date && sp.TimeAsDT < (sp.TimeAsDT.Date + hour))
                {
                    count++;
                    sum += sp.Power;
                    
                    bar1Positive.MaxValue = maxHgt;

                    //ako je ovo poslednji iz ovog vremenskog intervala
                    if ((i + 1) == data.Count || data[i + 1].TimeAsDT >= (sp.TimeAsDT.Date + hour))
                    {
                        if (sum > 0)
                        {
                            bar1Positive.Value = sum / count;
                        }
                        else
                        {
                            bar1Positive.Value = 0;
                        }

                        done = true;
                    }
                }
                //izmedju 1 i 2h
                else if (sp.TimeAsDT >= (sp.TimeAsDT.Date + hour) && sp.TimeAsDT < (sp.TimeAsDT.Date + hour + hour))
                {
                    count++;
                    sum += sp.Power;

                    bar2Positive.MaxValue = maxHgt;

                    //ako je ovo poslednji iz ovog vremenskog intervala
                    if ((i + 1) == data.Count || data[i + 1].TimeAsDT >= (sp.TimeAsDT.Date + hour + hour))
                    {
                        if (sum > 0)
                        {
                            bar2Positive.Value = sum / count;
                        }
                        else
                        {
                            bar2Positive.Value = 0;
                        }

                        done = true;
                    }
                }
                //izmedju 2 i 3h
                else if (sp.TimeAsDT >= (sp.TimeAsDT.Date + hour + hour) && sp.TimeAsDT < (sp.TimeAsDT.Date + hour + hour + hour))
                {
                    count++;
                    sum += sp.Power;

                    bar3Positive.MaxValue = maxHgt;

                    //ako je ovo poslednji iz ovog vremenskog intervala
                    if ((i + 1) == data.Count || data[i + 1].TimeAsDT >= (sp.TimeAsDT.Date + hour + hour + hour))
                    {
                        if (sum > 0)
                        {
                            bar3Positive.Value = sum / count;
                        }
                        else
                        {
                            bar3Positive.Value = 0;
                        }

                        done = true;
                    }
                }
                //izmedju 3 i 4h
                else if (sp.TimeAsDT >= (sp.TimeAsDT.Date + hour + hour + hour) && sp.TimeAsDT < (sp.TimeAsDT.Date + hour + hour + hour + hour))
                {
                    count++;
                    sum += sp.Power;

                    bar4Positive.MaxValue = maxHgt;

                    //ako je ovo poslednji iz ovog vremenskog intervala
                    if ((i + 1) == data.Count || data[i + 1].TimeAsDT >= (sp.TimeAsDT.Date + hour + hour + hour + hour))
                    {
                        if (sum > 0)
                        {
                            bar4Positive.Value = sum / count;
                        }
                        else
                        {
                            bar4Positive.Value = 0;
                        }

                        done = true;
                    }
                }
                //izmedju 4 i 5h
                else if (sp.TimeAsDT >= (sp.TimeAsDT.Date + hour + hour + hour + hour) && sp.TimeAsDT < (sp.TimeAsDT.Date + fiveHours))
                {
                    count++;
                    sum += sp.Power;

                    bar5Positive.MaxValue = maxHgt;

                    //ako je ovo poslednji iz ovog vremenskog intervala
                    if ((i + 1) == data.Count || data[i + 1].TimeAsDT >= (sp.TimeAsDT.Date + fiveHours))
                    {
                        if (sum > 0)
                        {
                            bar5Positive.Value = sum / count;
                        }
                        else
                        {
                            bar5Positive.Value = 0;
                        }

                        done = true;
                    }
                }
                //izmedju 5 i 6
                else if (sp.TimeAsDT >= (sp.TimeAsDT.Date + fiveHours) && sp.TimeAsDT < (sp.TimeAsDT.Date + fiveHours + hour))
                {
                    count++;
                    sum += sp.Power;

                    bar6Positive.MaxValue = maxHgt;

                    //ako je ovo poslednji iz ovog vremenskog intervala
                    if ((i + 1) == data.Count || data[i + 1].TimeAsDT >= (sp.TimeAsDT.Date + fiveHours + hour))
                    {
                        if (sum > 0)
                        {
                            bar6Positive.Value = sum / count;
                        }
                        else
                        {
                            bar6Positive.Value = 0;
                        }

                        done = true;
                    }
                }
                //izmedju 6 i 7
                else if (sp.TimeAsDT >= (sp.TimeAsDT.Date + fiveHours + hour) && sp.TimeAsDT < (sp.TimeAsDT.Date + fiveHours + hour + hour))
                {
                    count++;
                    sum += sp.Power;

                    bar7Positive.MaxValue = maxHgt;

                    //ako je ovo poslednji iz ovog vremenskog intervala
                    if ((i + 1) == data.Count || data[i + 1].TimeAsDT >= (sp.TimeAsDT.Date + fiveHours + hour + hour))
                    {
                        if (sum > 0)
                        {
                            bar7Positive.Value = sum / count;
                        }
                        else
                        {
                            bar7Positive.Value = 0;
                        }

                        done = true;
                    }
                }
                //izmedju 7 i 8h
                else if (sp.TimeAsDT >= (sp.TimeAsDT.Date + fiveHours + hour + hour) && sp.TimeAsDT < (sp.TimeAsDT.Date + fiveHours + hour + hour + hour))
                {
                    count++;
                    sum += sp.Power;

                    bar8Positive.MaxValue = maxHgt;

                    //ako je ovo poslednji iz ovog vremenskog intervala
                    if ((i + 1) == data.Count || data[i + 1].TimeAsDT >= (sp.TimeAsDT.Date + fiveHours + hour + hour + hour))
                    {
                        if (sum > 0)
                        {
                            bar8Positive.Value = sum / count;
                        }
                        else
                        {
                            bar8Positive.Value = 0;
                        }

                        done = true;
                    }
                }
                //izmedju 8 i 9h
                else if (sp.TimeAsDT >= (sp.TimeAsDT.Date + fiveHours + hour + hour + hour) && sp.TimeAsDT < (sp.TimeAsDT.Date + fiveHours + hour + hour + hour + hour))
                {
                    count++;
                    sum += sp.Power;

                    bar9Positive.MaxValue = maxHgt;

                    //ako je ovo poslednji iz ovog vremenskog intervala
                    if ((i + 1) == data.Count || data[i + 1].TimeAsDT >= (sp.TimeAsDT.Date + fiveHours + hour + hour + hour + hour))
                    {
                        if (sum > 0)
                        {
                            bar9Positive.Value = sum / count;
                        }
                        else
                        {
                            bar9Positive.Value = 0;
                        }

                        done = true;
                    }
                }
                //izmedju 9 i 10
                else if (sp.TimeAsDT >= (sp.TimeAsDT.Date + fiveHours + hour + hour + hour + hour) && sp.TimeAsDT < (sp.TimeAsDT.Date + tenHours))
                {
                    count++;
                    sum += sp.Power;

                    bar10Positive.MaxValue = maxHgt;

                    //ako je ovo poslednji iz ovog vremenskog intervala
                    if ((i + 1) == data.Count || data[i + 1].TimeAsDT >= (sp.TimeAsDT.Date + tenHours))
                    {
                        if (sum > 0)
                        {
                            bar10Positive.Value = sum / count;
                        }
                        else
                        {
                            bar10Positive.Value = 0;
                        }

                        done = true;
                    }
                }
                //izmedju 10 i 11
                else if (sp.TimeAsDT >= (sp.TimeAsDT.Date + fiveHours + fiveHours) && sp.TimeAsDT < (sp.TimeAsDT.Date + fiveHours + fiveHours + hour))
                {
                    count++;
                    sum += sp.Power;

                    bar11Positive.MaxValue = maxHgt;

                    //ako je ovo poslednji iz ovog vremenskog intervala
                    if ((i + 1) == data.Count || data[i + 1].TimeAsDT >= (sp.TimeAsDT.Date + tenHours + hour))
                    {
                        if (sum > 0)
                        {
                            bar11Positive.Value = sum / count;
                        }
                        else
                        {
                            bar11Positive.Value = 0;
                        }

                        done = true;
                    }
                }
                //izmedju 11 i 12
                else if (sp.TimeAsDT >= (sp.TimeAsDT.Date + fiveHours + fiveHours + hour) && sp.TimeAsDT < (sp.TimeAsDT.Date + fiveHours + fiveHours + hour + hour))
                {
                    count++;
                    sum += sp.Power;

                    bar12Positive.MaxValue = maxHgt;

                    //ako je ovo poslednji iz ovog vremenskog intervala
                    if ((i + 1) == data.Count || data[i + 1].TimeAsDT >= (sp.TimeAsDT.Date + tenHours + hour + hour))
                    {
                        if (sum > 0)
                        {
                            bar12Positive.Value = sum / count;
                        }
                        else
                        {
                            bar12Positive.Value = 0;
                        }

                        done = true;
                    }
                }
                //izmedju 12 i 13
                else if (sp.TimeAsDT >= (sp.TimeAsDT.Date + fiveHours + fiveHours + hour + hour) && sp.TimeAsDT < (sp.TimeAsDT.Date + tenHours + hour + hour + hour))
                {
                    count++;
                    sum += sp.Power;

                    bar13Positive.MaxValue = maxHgt;

                    //ako je ovo poslednji iz ovog vremenskog intervala
                    if ((i + 1) == data.Count || data[i + 1].TimeAsDT >= (sp.TimeAsDT.Date + tenHours + hour + hour + hour))
                    {
                        if (sum > 0)
                        {
                            bar13Positive.Value = sum / count;
                        }
                        else
                        {
                            bar13Positive.Value = 0;
                        }

                        done = true;
                    }
                }
                //izmedju 13 i 14
                else if (sp.TimeAsDT >= (sp.TimeAsDT.Date + fiveHours + fiveHours + hour + hour + hour) && sp.TimeAsDT < (sp.TimeAsDT.Date + tenHours + hour + hour + hour + hour))
                {
                    count++;
                    sum += sp.Power;

                    bar14Positive.MaxValue = maxHgt;

                    //ako je ovo poslednji iz ovog vremenskog intervala
                    if ((i + 1) == data.Count || data[i + 1].TimeAsDT >= (sp.TimeAsDT.Date + tenHours + hour + hour + hour + hour))
                    {
                        if (sum > 0)
                        {
                            bar14Positive.Value = sum / count;
                        }
                        else
                        {
                            bar14Positive.Value = 0;
                        }

                        done = true;
                    }
                }
                //izmedju 14 i 15
                else if (sp.TimeAsDT >= (sp.TimeAsDT.Date + tenHours + hour + hour + hour + hour) && sp.TimeAsDT < (sp.TimeAsDT.Date + fftHours))
                {
                    count++;
                    sum += sp.Power;

                    bar15Positive.MaxValue = maxHgt;

                    //ako je ovo poslednji iz ovog vremenskog intervala
                    if ((i + 1) == data.Count || data[i + 1].TimeAsDT >= (sp.TimeAsDT.Date + fftHours))
                    {
                        if (sum > 0)
                        {
                            bar15Positive.Value = sum / count;
                        }
                        else
                        {
                            bar15Positive.Value = 0;
                        }

                        done = true;
                    }
                }
                //izmedju 15 i 16h
                else if (sp.TimeAsDT >= (sp.TimeAsDT.Date + fftHours) && sp.TimeAsDT < (sp.TimeAsDT.Date + fftHours + hour))
                {
                    count++;
                    sum += sp.Power;

                    bar16Positive.MaxValue = maxHgt;

                    //ako je ovo poslednji iz ovog vremenskog intervala
                    if ((i + 1) == data.Count || data[i + 1].TimeAsDT >= (sp.TimeAsDT.Date + fftHours + hour))
                    {
                        if (sum > 0)
                        {
                            bar16Positive.Value = sum / count;
                        }
                        else
                        {
                            bar16Positive.Value = 0;
                        }

                        done = true;
                    }
                }
                //izmedju 16 i 17h
                else if (sp.TimeAsDT >= (sp.TimeAsDT.Date + fftHours + hour) && sp.TimeAsDT < (sp.TimeAsDT.Date + fftHours + hour + hour))
                {
                    count++;
                    sum += sp.Power;

                    bar17Positive.MaxValue = maxHgt;

                    //ako je ovo poslednji iz ovog vremenskog intervala
                    if ((i + 1) == data.Count || data[i + 1].TimeAsDT >= (sp.TimeAsDT.Date + fftHours + hour + hour))
                    {
                        if (sum > 0)
                        {
                            bar17Positive.Value = sum / count;
                        }
                        else
                        {
                            bar17Positive.Value = 0;
                        }

                        done = true;
                    }
                }
                //izmedju 17 i 18h
                else if (sp.TimeAsDT >= (sp.TimeAsDT.Date + fftHours + hour + hour) && sp.TimeAsDT < (sp.TimeAsDT.Date + fftHours + hour + hour + hour))
                {
                    count++;
                    sum += sp.Power;

                    bar18Positive.MaxValue = maxHgt;

                    //ako je ovo poslednji iz ovog vremenskog intervala
                    if ((i + 1) == data.Count || data[i + 1].TimeAsDT >= (sp.TimeAsDT.Date + fftHours + hour + hour + hour))
                    {
                        if (sum > 0)
                        {
                            bar18Positive.Value = sum / count;
                        }
                        else
                        {
                            bar18Positive.Value = 0;
                        }

                        done = true;
                    }
                }
                //izmedju 18 i 19h
                else if (sp.TimeAsDT >= (sp.TimeAsDT.Date + fftHours + hour + hour + hour) && sp.TimeAsDT < (sp.TimeAsDT.Date + fftHours + hour + hour + hour + hour))
                {
                    count++;
                    sum += sp.Power;

                    bar19Positive.MaxValue = maxHgt;

                    //ako je ovo poslednji iz ovog vremenskog intervala
                    if ((i + 1) == data.Count || data[i + 1].TimeAsDT >= (sp.TimeAsDT.Date + fftHours + hour + hour + hour + hour))
                    {
                        if (sum > 0)
                        {
                            bar19Positive.Value = sum / count;
                        }
                        else
                        {
                            bar19Positive.Value = 0;
                        }

                        done = true;
                    }
                }
                //izmedju 19 i 20h
                else if (sp.TimeAsDT >= (sp.TimeAsDT.Date + fftHours + hour + hour + hour + hour) && sp.TimeAsDT < (sp.TimeAsDT.Date + twtHours))
                {
                    count++;
                    sum += sp.Power;

                    bar20Positive.MaxValue = maxHgt;

                    //ako je ovo poslednji iz ovog vremenskog intervala
                    if ((i + 1) == data.Count || data[i + 1].TimeAsDT >= (sp.TimeAsDT.Date + twtHours))
                    {
                        if (sum > 0)
                        {
                            bar20Positive.Value = sum / count;
                        }
                        else
                        {
                            bar20Positive.Value = 0;
                        }

                        done = true;
                    }
                }
                //izmedju 20 i 21
                else if (sp.TimeAsDT >= (sp.TimeAsDT.Date + twtHours) && sp.TimeAsDT < (sp.TimeAsDT.Date + twtHours + hour))
                {
                    count++;
                    sum += sp.Power;

                    bar21Positive.MaxValue = maxHgt;

                    //ako je ovo poslednji iz ovog vremenskog intervala
                    if ((i + 1) == data.Count || data[i + 1].TimeAsDT >= (sp.TimeAsDT.Date + twtHours + hour))
                    {
                        if (sum > 0)
                        {
                            bar21Positive.Value = sum / count;
                        }
                        else
                        {
                            bar21Positive.Value = 0;
                        }

                        done = true;
                    }
                }
                //izmedju 21 i 22h
                else if (sp.TimeAsDT >= (sp.TimeAsDT.Date + twtHours + hour) && sp.TimeAsDT < (sp.TimeAsDT.Date + twtHours + hour + hour))
                {
                    count++;
                    sum += sp.Power;

                    bar22Positive.MaxValue = maxHgt;

                    //ako je ovo poslednji iz ovog vremenskog intervala
                    if ((i + 1) == data.Count || data[i + 1].TimeAsDT >= (sp.TimeAsDT.Date + twtHours + hour + hour))
                    {
                        if (sum >= 0)
                        {
                            bar22Positive.Value = sum / count;
                        }
                        else
                        {
                            bar22Positive.Value = 0;
                        }

                        done = true;
                    }
                }
                //izmedju 22 i 23h
                else if (sp.TimeAsDT >= (sp.TimeAsDT.Date + twtHours + hour + hour) && sp.TimeAsDT < (sp.TimeAsDT.Date + twtHours + hour + hour + hour))
                {
                    count++;
                    sum += sp.Power;

                    bar23Positive.MaxValue = maxHgt;

                    //ako je ovo poslednji iz ovog vremenskog intervala
                    if ((i + 1) == data.Count || data[i + 1].TimeAsDT >= (sp.TimeAsDT.Date + twtHours + hour + hour + hour))
                    {
                        if (sum >= 0)
                        {
                            bar23Positive.Value = sum / count;
                        }
                        else
                        {
                            bar23Positive.Value = 0;
                        }

                        done = true;
                    }
                }
                //izmedju 23 i 24h
                else if (sp.TimeAsDT >= (sp.TimeAsDT.Date + twtHours + hour + hour + hour) && sp.TimeAsDT < (sp.TimeAsDT.Date + twtHours + hour + hour + hour + hour))
                {
                    count++;
                    sum += sp.Power;

                    bar24Positive.MaxValue = maxHgt;

                    //ako je ovo poslednji iz ovog vremenskog intervala
                    if ((i + 1) == data.Count)
                    {
                        if (sum >= 0)
                        {
                            bar24Positive.Value = sum / count;
                        }
                        else
                        {
                            bar24Positive.Value = 0;
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


