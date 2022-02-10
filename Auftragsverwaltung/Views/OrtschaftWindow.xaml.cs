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
using BusinessLogik;

namespace Auftragsverwaltung.Views
{
    /// <summary>
    /// Interaktionslogik für OrtschaftWindow.xaml
    /// </summary>
    public partial class OrtschaftWindow : Window
    {
        private ControllerOrtschaft controllerOrtschaft = new ControllerOrtschaft();

        public OrtschaftWindow()
        {
            InitializeComponent();
        }

        private void cmdSpeichern_Click(object sender, RoutedEventArgs e)
        {
            var postleitzahl = Convert.ToInt32(txtPLZ.Text);
            var ortschaft = txtOrtschaft.Text;

            try
            {
                controllerOrtschaft.NeueOrtschaftAnlegen(postleitzahl, ortschaft);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Fehler beim anlegen der Ortschaft" + "r/n" + exception);

            }
            LadeOrtschaften();
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            LadeOrtschaften();
        }

        private void LadeOrtschaften()
        {
            dgvOrtschaft.ItemsSource = controllerOrtschaft.LadeOrtschaften();

        }
    }
}
