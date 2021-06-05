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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [ExcludeFromCodeCoverage]
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ChannelFactory<ISHESGUI> channelSHES = new ChannelFactory<ISHESGUI>("ISHESGUI");
            CommunicationData.proxySHES = channelSHES.CreateChannel();
        }

        private void Initialize_Click(object sender, RoutedEventArgs e)
        {
            //parsirati unos i poslati podatke shesu
            string[] temp = txtMaxPwrSP.Text.Split(' ');
            double[] tempD = new double[0];
            double[] tempSP = new double[temp.Count()];

            bool flag = true;

            if(txtNmbSP.Text == "" && flag == true)
            {
                flag = false;
                lblErrorMessage.Content = "Unesite broj solarnih panela";
            }

            if (flag == true && (temp.Count() == 0 || (temp.Count() == 1 && temp[0] == "") || (txtNmbSP.Text != null && txtNmbSP.Text != "" && temp.Count() != Int32.Parse(txtNmbSP.Text))))
            {
                lblErrorMessage.Content = "Pogresno unete vrednosti snage solarnih panela!";
                flag = false;
            }
            else if (flag == true)
            {
                try
                {
                    tempD = new double[temp.Count()];
                    for (int i = 0; i < temp.Count(); i++)
                    {
                        if (temp[i] == "")
                        {
                            throw new Exception("Pogresno unete vrednosti kapaciteta baterija!");
                        }
                        tempD[i] = double.Parse(temp[i]);
                    }
                    tempSP = tempD;
                }
                catch
                {
                    throw new Exception("Pogresno unete vrednosti snage solarnih panela!");
                }
            }

            temp = txtMaxPwrB.Text.Split(' ');
            tempD = new double[0];
            double[] tempBMP = new double[temp.Count()];

            if (txtNmbB.Text == "" && flag == true)
            {
                flag = false;
                lblErrorMessage.Content = "Unesite broj baterija";
            }

            if (flag == true && (temp.Count() == 0 || (temp.Count() == 1 && temp[0] == "") || (txtNmbB.Text != null && txtNmbB.Text != "" && temp.Count() != Int32.Parse(txtNmbB.Text))))
            {
                lblErrorMessage.Content = "Pogresno unete vrednosti snage baterija!";
                flag = false;
            }
            else if (flag == true)
            {
                try
                {
                    tempD = new double[temp.Count()];
                    for (int i = 0; i < temp.Count(); i++)
                    {
                        if (temp[i] == "")
                        {
                            throw new Exception("Pogresno unete vrednosti kapaciteta baterija!");
                        }
                        tempD[i] = double.Parse(temp[i]);
                    }
                    tempBMP = tempD;
                }
                catch
                {
                    throw new Exception("Pogresno unete vrednosti snage baterija!");
                }
            }

            temp = txtCapB.Text.Split(' ');
            tempD = new double[0];
            double[] tempBC = new double[temp.Count()];

            if (flag == true && (temp.Count() == 0 || (temp.Count() == 1 && temp[0] == "") || (txtNmbB.Text != null && txtNmbB.Text != "" && temp.Count() != Int32.Parse(txtNmbB.Text))))
            {
                lblErrorMessage.Content = "Pogresno unete vrednosti kapaciteta baterija!";
                flag = false;
            }
            else if (flag == true)
            {
                try
                {
                    tempD = new double[temp.Count()];
                    for (int i = 0; i < temp.Count(); i++)
                    {
                        if(temp[i] == "")
                        {
                            throw new Exception("Pogresno unete vrednosti kapaciteta baterija!");
                        }
                        tempD[i] = double.Parse(temp[i]);
                    }
                    tempBC = tempD;
                }
                catch
                {
                    throw new Exception("Pogresno unete vrednosti kapaciteta baterija!");
                }
            }

            if (txtMaxPwrEV.Text == "" && flag == true)
            {
                flag = false;
                lblErrorMessage.Content = "Unesite snagu EV Chargera";
            }

            if (txtCostU.Text == "" && flag == true)
            {
                flag = false;
                lblErrorMessage.Content = "Unesite cenu za Utility";
            }

            temp = txtMaxPwrConsumer.Text.Split(' ');
            tempD = new double[0];
            double[] tempMPC = new double[temp.Count()];

            if (txtNmbConsumer.Text == "" && flag == true)
            {
                flag = false;
                lblErrorMessage.Content = "Unesite broj potrosaca";
            }

            if (flag == true && (temp.Count() == 0 || (temp.Count() == 1 && temp[0] == "") || (txtNmbConsumer.Text != null && txtNmbConsumer.Text != "" && temp.Count() != Int32.Parse(txtNmbConsumer.Text))))
            {
                lblErrorMessage.Content = "Pogresno unete vrednosti snage potrosaca!";
                flag = false;
            }
            else if(flag == true)
            {
                try
                {
                    tempD = new double[temp.Count()];
                    for (int i = 0; i < temp.Count(); i++)
                    {
                        if (temp[i] == "")
                        {
                            throw new Exception("Pogresno unete vrednosti kapaciteta baterija!");
                        }
                        tempD[i] = double.Parse(temp[i]);
                    }
                    tempMPC = tempD;
                }
                catch
                {
                    throw new Exception("Pogresno unete vrednosti snage potrosaca!");
                }
            }

            if (flag == true)
            {
                CommunicationData.proxySHES.Initialize(Int32.Parse(txtNmbSP.Text), tempSP, Int32.Parse(txtNmbB.Text), tempBMP, 
                    tempBC, double.Parse(txtMaxPwrEV.Text), double.Parse(txtCostU.Text), Int32.Parse(txtNmbConsumer.Text), tempMPC,true,true);
                ChangeInputWindow newChangeInput = new ChangeInputWindow();
                newChangeInput.Show();
                this.Close();
            }
        }

        private void Skip_Click(object sender, RoutedEventArgs e)
        {
            //poslati prazne podatke shesu
            double[] niz = new double[0];
            CommunicationData.proxySHES.Initialize(0, niz, 0, niz, niz, 0, 0, 0, niz,false,true);

            ChangeInputWindow newChangeInput = new ChangeInputWindow();
            newChangeInput.Show();
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

    }
}
