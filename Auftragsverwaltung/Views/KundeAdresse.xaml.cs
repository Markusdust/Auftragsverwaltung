using BusinessLogik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DataAccessLayer.Entities;

namespace Auftragsverwaltung.Views
{
    /// <summary>
    /// Interaction logic for KundeAdresse.xaml
    /// </summary>
    public partial class KundeAdresse : Page
    {


        private ControllerKundeAdresse controllerKundeAdresse = new ControllerKundeAdresse();

        public KundeAdresse()
        {
            InitializeComponent();

        }

        private void cmdSpeichern_Click(object sender, RoutedEventArgs e)
        {
            var kundenNr = 111;
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
                controllerKundeAdresse.NeuerKundeAdresseAnlegen(kundenNr, vorname, nachname, firma,
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
        private void LadeKunden()
        {
            dgvKunde.ItemsSource = controllerKundeAdresse.LadeKunden();
        }
        private void LadeAdressen()
        {
            dgvAdresse.ItemsSource = controllerKundeAdresse.LadeAdressen();
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
            try
            {
                var aktuelleZeile = dgvKunde.SelectedCells.ToArray();
                var aktuellerKunde = (Kunde)aktuelleZeile[0].Item;

                SelectierterKundeZuFeldern(aktuellerKunde);
                // SelectierteAdresseZuFeldern(aktuelleAdresse);
                //Aktuelle Adresse soll automatisch selektiert werden wenn der kunde angewählt wird.
                // Hat ja verknüpfung zwischen kunde`/Adresse 

            }
            catch (Exception exception)
            {
                MessageBox.Show("Fehler: "+"r\n"+exception);
            }
            

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
            txtStrasse.Text = "";
            txtHausNr.Text = "";
            txtOrtschaft.Text = "";
        }
    }
}
