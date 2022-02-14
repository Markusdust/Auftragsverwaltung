using BusinessLogik;
using DataAccessLayer.Entities;
using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Auftragsverwaltung.Views;
using DataAccessLayer;

namespace Auftragsverwaltung.Views
{
    /// <summary>
    /// Interaction logic for KundeAdresse.xaml
    /// </summary>
    public partial class KundeAdresse : Page
    {
        private int aktuellerKundenId;

        private ControllerKundeAdresse controllerKundeAdresse = new ControllerKundeAdresse();

        private ControllerOrtschaft controllerOrtschaft = new ControllerOrtschaft();

        public KundeAdresse()
        {
            InitializeComponent();

        }

        private void cmdSpeichern_Click(object sender, RoutedEventArgs e)
        {
            var vorname = txtVorname.Text;
            var nachname = txtNachname.Text;
            var firma = txtFirma.Text;
            var email = txtEmail.Text;
            var passwort = txtPasswort.Text;
            var website = txtWebsite.Text;
            var strasse = txtStrasse.Text;
            var hausNr = txtHausNr.Text;
            var ortschaft = 2;

            try
            {
                controllerKundeAdresse.NeuerKundeAdresseAnlegen( vorname, nachname, firma,
                    email, passwort, website, strasse, hausNr, ortschaft);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);

            }
            LadeKunden();
            LadeAdressen();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
          cmbOrtschaft.ItemsSource=  controllerOrtschaft.LadeOrtschaften();
          


        }

        

        private void cmdFelderLeeren_Click(object sender, RoutedEventArgs e)
        {
            FelderLeeren();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LadeKunden();
                LadeAdressen();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Daten konnten nicht geladen werden: " + "r\n" + exception);
            }

        }

        private void dgvKunde_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Adresse adresseVonKunde;
            try
            {
                var aktuelleZeile = dgvKunde.SelectedCells.ToArray();
                var aktuellerKunde = (Kunde)aktuelleZeile[0].Item;
                

                SelectierterKundeZuFeldern(aktuellerKunde);
                aktuellerKundenId = aktuellerKunde.Id;

                adresseVonKunde=AdresseZuKundenId(aktuellerKundenId);
                MarkiereAdressInDg(adresseVonKunde.Id);
                SeletierteAdresseZuFeldern(adresseVonKunde);
                

            }
            catch (Exception exception)
            {
                MessageBox.Show("Fehler: " + "r\n" + exception);
            }


        }

        private void cmbOrtschaft_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void lblOrtschaft_PreviewMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OrtschaftWindow ortschaftWindow = new OrtschaftWindow();
            ortschaftWindow.Show();
        }

        private void SelectierterKundeZuFeldern(Kunde aktuellerKunde)
        {
            lblKundenId.Content = Convert.ToString(aktuellerKunde.Id);
            lblKundenNr.Content = Convert.ToString(aktuellerKunde.KundenNr);
            txtNachname.Text = aktuellerKunde.Nachname;
            txtVorname.Text = aktuellerKunde.Vorname;
            txtFirma.Text = aktuellerKunde.Firma;
            txtEmail.Text = aktuellerKunde.Email;
            txtWebsite.Text = aktuellerKunde.Website;
            txtPasswort.Text = aktuellerKunde.Passwort;
        }

        private void SeletierteAdresseZuFeldern(Adresse aktuelleAdresse)
        {
            txtStrasse.Text = aktuelleAdresse.Strasse;
            txtHausNr.Text = Convert.ToString(aktuelleAdresse.HausNr);
            txtOrtschaft.Text = Convert.ToString(aktuelleAdresse.OrtschaftId);
        }

        private Adresse AdresseZuKundenId(int kundenID)
        {
            return controllerKundeAdresse.AdresseZuKunde(kundenID);
        }
        private void LadeKunden()
        {
            dgvKunde.ItemsSource = controllerKundeAdresse.LadeKunden();
        }
        private void LadeAdressen()
        {
            dgvAdresse.ItemsSource = controllerKundeAdresse.LadeAdressen();
        }

        private void MarkiereAdressInDg(int AdressId)
        {
            for (int i = 0; i < dgvAdresse.Items.Count; i++)
            {
                if (AdressId == ((Adresse)dgvAdresse.Items[i]).Id)
                {
                    dgvAdresse.SelectedIndex = i;
                    
                }
            }

        }

        private void FelderLeeren()
        {
            lblKundenId.Content = "";
            lblKundenNr.Content = "";
            txtNachname.Text = "";
            txtVorname.Text = "";
            txtFirma.Text = "";
            txtEmail.Text = "";
            txtWebsite.Text = "";
            txtPasswort.Text = "";

            txtStrasse.Text = "";
            txtHausNr.Text = "";
            txtOrtschaft.Text = "";
        }
    }
}
