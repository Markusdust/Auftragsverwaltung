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
        private int TempPos = 0;
        private int TempAuftragNr = 0;

        public AuftragPosition()
        {
            InitializeComponent();
        }

        private void cmdSpeichern_Click(object sender, RoutedEventArgs e)
        {
            TempPos = 0;
            TempAuftragNr = TempAuftragNr + 1;
            var auftragsNr = TempAuftragNr;
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
                controllerAuftrag.NeuerAuftragAnlegen(auftragsNr, datum, kundeId);
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
            var auftragsNr = 2;
            DateTime datum = DateTime.Now;
            int kundeId = 1;
            int id = 1;

            try
            {
                kundeId = Convert.ToInt32(txtKundenNr.Text);
                try
                {
                    datum = dpDatum.SelectedDate.Value;
                    auftragsNr = Convert.ToInt32(txtAuftragNr.Text);
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
                controllerAuftrag.AlterAuftragBearbeiten(id, auftragsNr, datum, kundeId);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Kunde kann nicht geladen werden." +"r/n"+exception);
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
            dgvKunde2.ItemsSource = controllerKundeAdresse.LadeAlleKunden();
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
                FelderLeeren();
                PosFelderLeeren();                
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
                    //MessageBox.Show("Position kann nicht geladen werden.");
                }                
                LadeTeilPositionen(auftragId);
                PosFelderLeeren();
            }
            catch
            {
                //MessageBox.Show("Es gibt einen Fehler.");
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
            TempPos = 0;
            try
            {
                var aktuelleZeile = dgvAuftrag.SelectedCells.ToArray();
                var aktuellerAuftrag = (Auftrag)aktuelleZeile[0].Item;
                int auftragId = SelectierterAuftragZuFeld(aktuellerAuftrag);
                
                SelectierterAuftragZuFeldern(aktuellerAuftrag);
                LadeTeilPositionen(auftragId);
            }
            catch (Exception)
            {
                
            }
        }

        private void SelectierterAuftragZuFeldern(Auftrag aktuellerAuftrag)
        {
            txtId.Text = Convert.ToString(aktuellerAuftrag.Id);
            txtAuftragNr.Text = Convert.ToString(aktuellerAuftrag.AuftragsNr);
            txtKundenNr.Text = Convert.ToString(aktuellerAuftrag.KundeId);
            dpDatum.SelectedDate = aktuellerAuftrag.Datum;
        }

        private void cmdPosLeeren_Click(object sender, RoutedEventArgs e)
        {
            PosFelderLeeren();
        }

        private void PosFelderLeeren()
        {
            txtPosition.Text = "";
            txtMenge.Text = "";
            txtArtikelNr.Text = "";
            txtAuftragId.Text = "";
            txtId.Text = "";
        }

        private void cmdLoeschen_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Wirklich löschen?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning)== MessageBoxResult.Yes)
            {
                var aktuelleZeile = dgvAuftrag.SelectedCells.ToArray();
                var aktuellerAuftrag = (Auftrag)aktuelleZeile[0].Item;

                int auftragId = SelectierterAuftragZuFeld(aktuellerAuftrag);

                try
                {
                    controllerAuftrag.AuftragLoeschen(auftragId);
                }
                catch (Exception)
                {
                    MessageBox.Show("Fehler beim Löschen.");
                }

                //dgvAuftrag.ItemsSource = controllerAuftrag.LadeAuftraege();
                LadeAuftraege();
            }
            else
            {
                
            }  
        }

        private void dgvPosition_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var aktuelleZeile = dgvPosition.SelectedCells.ToArray();
                var aktuellePosition = (Position)aktuelleZeile[0].Item;

                SelectierterPosZuFeldern(aktuellePosition);
            }
            catch (Exception)
            {
                MessageBox.Show("Daten konnten nicht geladen werden.");
            }
        }

        private void SelectierterPosZuFeldern(Position aktuellePosition)
        {
            txtPosId.Text = Convert.ToString(aktuellePosition.Id);
            txtAuftragId.Text = Convert.ToString(aktuellePosition.AuftragId);
            txtPosition.Text = Convert.ToString(aktuellePosition.PositionNr);
            txtMenge.Text = Convert.ToString(aktuellePosition.Menge);
            txtArtikelNr.Text = Convert.ToString(aktuellePosition.ArtikelId);            
        }

        private void cmdPosAendern_Click(object sender, RoutedEventArgs e)
        {
            var artikelNr = 1;
            int menge = 1;
            int position = 1;
            int id = 1;
            int auftragId = 1;

            try
            {
                artikelNr = Convert.ToInt32(txtArtikelNr.Text);
                try
                {
                    auftragId = Convert.ToInt32(txtAuftragId.Text);
                    menge = Convert.ToInt32(txtMenge.Text);
                    position = Convert.ToInt32(txtPosition.Text);
                    id = Convert.ToInt32(txtPosId.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Es ist ein Fehler aufgetreten.");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Es wurde keine ArtieklNr eingegeben.");
            }

            try
            {
                controllerAuftrag.AltePositionBearbeiten(id, position, menge, auftragId, artikelNr);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Kunde kann nicht geladen werden.");
            }
            LadePositionen();
            PosFelderLeeren();
        }

        private void cmdPosLoeschen_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Wirklich löschen?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                //var aktuelleZeile = dgvPosition.SelectedCells.ToArray();
                //var aktuellePosition = (Auftrag)aktuelleZeile[0].Item;

                var aktuelleZeile = dgvAuftrag.SelectedCells.ToArray();
                var aktuellerAuftrag = (Auftrag)aktuelleZeile[0].Item;

                int auftragId = SelectierterAuftragZuFeld(aktuellerAuftrag);

                int posId = Convert.ToInt32(txtPosId.Text);

                try
                {
                    controllerAuftrag.PositionLoeschen(posId);
                }
                catch (Exception)
                {
                    MessageBox.Show("Fehler beim Löschen.");
                }

                //dgvAuftrag.ItemsSource = controllerAuftrag.LadeAuftraege();
                LadeTeilPositionen(auftragId);
            }
            else
            {

            }
        }

        private void cmdSuchen_Click(object sender, RoutedEventArgs e)
        {
            string input = txtSuche.Text;
            if (input == "")
            {
                LadeAuftraege();
            }
            else
            {
                dgvAuftrag.ItemsSource = controllerAuftrag.SucheAuftrag(input);
            }            
        }

        private void cmdPosSuchen_Click(object sender, RoutedEventArgs e)
        {
            int auftragId = 0;
            try
            {
                var aktuelleZeile = dgvAuftrag.SelectedCells.ToArray();
                var aktuellerAuftrag = (Auftrag)aktuelleZeile[0].Item;

                auftragId = SelectierterAuftragZuFeld(aktuellerAuftrag);
            }
            catch (Exception)
            {

            }
            

            if (txtPosSuche.Text == "")
            {
                LadeTeilPositionen(auftragId);
            }
            else
            {
                int input = Convert.ToInt32(txtPosSuche.Text);
                dgvPosition.ItemsSource = controllerAuftrag.SuchePositionen(input);
            }           
            
        }
    }
}
