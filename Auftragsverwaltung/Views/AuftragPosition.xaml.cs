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
using DataAccessLayer.Entities;

namespace Auftragsverwaltung.Views
{
    /// <summary>
    /// Interaction logic for AuftragPosition.xaml
    /// </summary>
    public partial class AuftragPosition : Page
    {
        private ControllerAuftrag controllerAuftrag = new ControllerAuftrag();
        private ControllerArtikel controllerArtikel = new ControllerArtikel();
        private ControllerKundeAdresse controllerKundeAdresse = new ControllerKundeAdresse();
        private int TempPos = 10;

        public AuftragPosition()
        {
            InitializeComponent();
        }

        private void cmdSpeichern_Click(object sender, RoutedEventArgs e)
        {
            var auftragNr = 2;
            DateTime datum = DateTime.Now;
            int kundeId = 1;

            try
            {
                kundeId = Convert.ToInt32(txtKundenNr.Text);
                try
                {
                    datum = dpDatum.SelectedDate.Value;
                }
                catch (Exception)
                {
                    MessageBox.Show("Es wurde kein Datum ausgewählt.");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Es wurde keine KundenId eingegeben.");
            }

            try
            {
                controllerAuftrag.NeuerAuftragAnlegen(auftragNr, datum, kundeId);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Kunde kann nicht geladen werden.");
            }
            LadeAuftraege();
            FelderLeeren();
        }
        private void cmdAendern_Click(object sender, RoutedEventArgs e)
        {
            var auftragNr = 1;
            DateTime datum = DateTime.Now;
            int kundeId = 1;
            int id = 1;

            try
            {
                kundeId = Convert.ToInt32(txtKundenNr.Text);
                try
                {
                    datum = dpDatum.SelectedDate.Value;
                    auftragNr = Convert.ToInt32(txtAuftragNr.Text);
                    id = Convert.ToInt32(txtId.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Es wurde kein Datum ausgewählt.");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Es wurde keine KundenId eingegeben.");
            }

            try
            {
                controllerAuftrag.AlterAuftragBearbeiten(id, auftragNr, datum, kundeId);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Kunde kann nicht geladen werden.");
            }
            LadeAuftraege();
            FelderLeeren();
        }

        private void FelderLeeren()
        {
            txtAuftragNr.Text = "";
            txtId.Text = "";
            txtKundenNr.Text = "";
            dpDatum.SelectedDate = null;
        }

        private void LadeAuftraege()
        {
            dgvAuftrag.ItemsSource = controllerAuftrag.LadeAuftraege();
        }

        private void LadePositionen()
        {
            dgvPosition.ItemsSource = controllerAuftrag.LadePositionen();
        }

        private void LadeArtikel()
        {
            dgvArtikel.ItemsSource = controllerArtikel.LadeArtikel();
        }

        private void LadeKunden()
        {
            dgvKunde2.ItemsSource = controllerKundeAdresse.LadeKunden();
        }

        private void LadeTeilPositionen(int auftragId)
        {
            dgvPosition.ItemsSource = controllerAuftrag.LadeTeilPositionen(auftragId);            
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LadeAuftraege();                
                LadeTeilPositionen(1);
                LadeArtikel();
                LadeKunden();
                WaehleErstesElement();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Daten können nicht geladen werden." + exception);
            }
        }

        private void WaehleErstesElement()
        {
            dgvAuftrag.SelectedIndex=0;
        }

        private void cmdInBestellung_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TempPos = TempPos + 10;
                var positionNr = TempPos;
                var menge = Convert.ToInt32(txtMenge.Text);

                var aktuelleZeile = dgvAuftrag.SelectedCells.ToArray();
                var aktuellerAuftrag = (Auftrag)aktuelleZeile[0].Item;

                var auftragId = SelectierterAuftragZuFeld(aktuellerAuftrag);

                var artikelId = Convert.ToInt32(txtArtikelNr.Text); //Convert.ToInt32(cmboArtikel.SelectedItem);
                try
                {
                    controllerAuftrag.NeuePositionAnlegen(positionNr, menge, auftragId, artikelId);
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Position kann nicht geladen werden.");
                }
                //LadePositionen();
                LadeTeilPositionen(auftragId);
            }
            catch
            {
                MessageBox.Show("Es gibt einen Fehler.");
            }
        }       

        private int SelectierterAuftragZuFeld(Auftrag aktuellerAuftrag)
        {
            return aktuellerAuftrag.AuftragsNr;
        }

        private void cmdLeeren_Click(object sender, RoutedEventArgs e)
        {
            FelderLeeren();
        }

        private void dgvAuftrag_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var aktuelleZeile = dgvAuftrag.SelectedCells.ToArray();
                var aktuellerAuftrag = (Auftrag)aktuelleZeile[0].Item;

                SelectierterAuftragZuFeldern(aktuellerAuftrag);               
            }
            catch (Exception)
            {
                MessageBox.Show("Daten konnten nicht geladen werden.");
            }
        }

        private void SelectierterAuftragZuFeldern(Auftrag aktuellerAuftrag)
        {
            txtId.Text = Convert.ToString(aktuellerAuftrag.Id);
            txtAuftragNr.Text = Convert.ToString(aktuellerAuftrag.AuftragsNr);
            txtKundenNr.Text = Convert.ToString(aktuellerAuftrag.KundeId);
            dpDatum.SelectedDate = aktuellerAuftrag.Datum;
        }        
    }
}
