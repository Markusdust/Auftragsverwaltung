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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BusinessLogik;

namespace Auftragsverwaltung.Views
{
    /// <summary>
    /// Interaktionslogik für Ortschaft.xaml
    /// </summary>
    public partial class Ortschaft : Page
    {
        private ControllerOrtschaft controllerOrtschaft = new ControllerOrtschaft();
        public Ortschaft()
        {
            InitializeComponent();
        }

        private void cmdSpeichern_Click(object sender, RoutedEventArgs e)
        {
            var postleitzahl = txtPLZ.Text;
            var ortschaft = txtOrtschaft.Text;
            
            try
            {
                //controllerOrtschaft.NeueOrtschaftanlegen()
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);

            }
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void LadeOrtschaften()
        {
            dgvOrtschaft.ItemsSource = controllerOrtschaft.LadeOrtschaften();

        }
    }
}
