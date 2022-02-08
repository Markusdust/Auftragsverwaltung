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
    /// Interaction logic for AuftragPosition.xaml
    /// </summary>
    public partial class AuftragPosition : Page
    {
        private ControllerAuftrag controllerAuftrag = new ControllerAuftrag();

        public AuftragPosition()
        {
            InitializeComponent();
        }

        private void cmdSpeichern_Click(object sender, RoutedEventArgs e)
        {
            var auftragNr = 1;
            var datum = dpDatum.SelectedDate.Value;
            var kundeId = Convert.ToInt32(txtKundenNr.Text);
            try
            {
                controllerAuftrag.NeuerAuftragAnlegen(auftragNr, datum, kundeId);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Kunde kann nicht geladen werden.");
            }
            LadeAuftraege();
        }

        private void LadeAuftraege()
        {
            dgvAuftrag.ItemsSource = controllerAuftrag.LadeAuftraege();
        }

        private void LadePositionen()
        {
            dgvPosition.ItemsSource = controllerAuftrag.LadePositionen();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LadeAuftraege();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Daten können nicht geladen werden." + exception);
            }
        }

        private void cmdInBestellung_Click(object sender, RoutedEventArgs e)
        {
            var id = 1;
            var positionNr = 10;
            var auftrag = 1;
            var menge = Convert.ToInt32(txtMenge.Text);
            var artikelId = 1; //Convert.ToInt32(cmboArtikel.SelectedItem);
            try
            {
                controllerAuftrag.NeuerArtikelAnlegen(id, positionNr, menge, auftrag, artikelId);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Position kann nicht geladen werden.");
            }
            LadePositionen();
        }
    }
}
