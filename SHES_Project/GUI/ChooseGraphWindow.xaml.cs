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
    /// Interaction logic for ChooseGraphWindow.xaml
    /// </summary>
    public partial class ChooseGraphWindow : Window
    {
        private List<DateTime> dates;

        public ChooseGraphWindow()
        {
            InitializeComponent();

            dates = CommunicationData.proxySHES.GetDates();

            if (dates == null)
                throw new ArgumentNullException("Datumi prosledjeni od SHES-a su null!");

            //ovde proveriti da li ih lepo prikazuje ili treba da se pretvaraju u string svi
            cmbBoxDatum.ItemsSource = dates;

            List<string> grafici = new List<string>();

            grafici.Add("Solar Panels");
            grafici.Add("Battery");
            grafici.Add("Utility");
            grafici.Add("Consumers");

            cmbBoxGraph.ItemsSource = grafici;

        }

        public ChooseGraphWindow(string message)
        {
            InitializeComponent();

            dates = CommunicationData.proxySHES.GetDates();

            if (dates == null)
                throw new ArgumentNullException("Datumi prosledjeni od SHES-a su null!");

            //ovde proveriti da li ih lepo prikazuje ili treba da se pretvaraju u string svi
            cmbBoxDatum.ItemsSource = dates;

            List<string> grafici = new List<string>();

            grafici.Add("Solar Panels");
            grafici.Add("Battery");
            grafici.Add("Utility");
            grafici.Add("Consumers");

            cmbBoxGraph.ItemsSource = grafici;

            lblID.Content = message;
            lblID.Foreground = Brushes.Red;
            txtBoxID.Visibility = Visibility.Hidden;

        }

        private void Show_Button_Click(object sender, RoutedEventArgs e)
        {
            if(cmbBoxDatum.Text != null && cmbBoxGraph != null)
            {
                //proveriti da li radi (DateTime)cmbBoxDatum.SelectedValue
                switch (cmbBoxGraph.Text)
                {
                    case "Solar Panels":
                        SolarPanelsDiagram spd = new SolarPanelsDiagram((DateTime)cmbBoxDatum.SelectedValue);
                        spd.Show();
                        this.Close();
                        break;
                    case "Battery":
                        
                        if (txtBoxID.Text != "") {
                            BatteryDiagram bdd = new BatteryDiagram((DateTime)cmbBoxDatum.SelectedValue, txtBoxID.Text);
                            bdd.Show();
                            this.Close();
                        }
                        break;
                    case "Utility":
                        UtilityDiagram ud = new UtilityDiagram((DateTime)cmbBoxDatum.SelectedValue);
                        ud.Show();
                        this.Close();
                        break;
                    case "Consumers":
                        ConsumersDiagram cd = new ConsumersDiagram((DateTime)cmbBoxDatum.SelectedValue);
                        cd.Show();
                        this.Close();
                        break;

                }
            }
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            ChangeInputWindow change = new ChangeInputWindow();
            change.Show();
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CmbBoxGraph_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbBoxGraph.Text == "Battery")
            {
                lblID.Content = "Battery ID:";
                txtBoxID.Visibility = Visibility.Visible;
            }
        }
    }
}
