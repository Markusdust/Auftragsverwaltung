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
            var ortschaft = txtOrtschaft.Text;
            var plz =Convert.ToInt32(txtPLZ.Text);

            try
            {
                controllerKundeAdresse.NeuerKundeAdresseAnlegen( vorname, nachname, firma,
                    email, passwort, website, strasse, hausNr, ortschaft, plz);
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
                LadeOrtschaften();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Daten konnten nicht geladen werden: " + "r\n" + exception);
            }

        }

        private void dgvKunde_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Adresse adresseVonKunde;
            Ortschaft ortschaftVonAdresse;
            try
            {
                var aktuelleZeile = dgvKunde.SelectedCells.ToArray();
                var aktuellerKunde = (Kunde)aktuelleZeile[0].Item;
                

                SelectierterKundeZuFeldern(aktuellerKunde);
                aktuellerKundenId = aktuellerKunde.Id;

                adresseVonKunde=AdresseZuKundenId(aktuellerKundenId);
                MarkiereAdressInDg(adresseVonKunde.Id);
                SeletierteAdresseZuFeldern(adresseVonKunde);

                ortschaftVonAdresse= OrtschaftzuAdresseId(adresseVonKunde.OrtschaftId);
                SelectierteOrtschaftZuFelder(ortschaftVonAdresse);
                MarkiereOrtschaftInDg(ortschaftVonAdresse.Id);


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
          //  txtOrtschaft.Text = Convert.ToString(aktuelleAdresse.OrtschaftId);
        }

        private void SelectierteOrtschaftZuFelder(Ortschaft aktuelleOrtschaft)
        {
            txtOrtschaft.Text = aktuelleOrtschaft.Ort;
            txtPLZ.Text = Convert.ToString(aktuelleOrtschaft.PLZ);
        }

        private Adresse AdresseZuKundenId(int kundenId)
        {
            return controllerKundeAdresse.AdresseZuKunde(kundenId);
        }

        private Ortschaft OrtschaftzuAdresseId(int ortschaftId)
        {
            return controllerOrtschaft.OrtschaftZuAdresse(ortschaftId);
        }
        private void LadeKunden()
        {
            dgvKunde.ItemsSource = controllerKundeAdresse.LadeKunden();
        }
        private void LadeAdressen()
        {
            dgvAdresse.ItemsSource = controllerKundeAdresse.LadeAdressen();
        }
        private void LadeOrtschaften()
        {
            dgvOrtschaft.ItemsSource = controllerOrtschaft.LadeOrtschaften();

        }

        private void MarkiereAdressInDg(int adressId)
        {
            for (int i = 0; i < dgvAdresse.Items.Count; i++)
            {
                if (adressId == ((Adresse)dgvAdresse.Items[i]).Id)
                {
                    dgvAdresse.SelectedIndex = i;
                    
                }
            }

        }

        private void MarkiereOrtschaftInDg(int ortschaftId)
        {
            for (int i = 0; i < dgvOrtschaft.Items.Count; i++)
            {
                if (ortschaftId == ((Ortschaft)dgvOrtschaft.Items[i]).Id)
                {
                    dgvOrtschaft.SelectedIndex = i;

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
