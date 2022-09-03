using BusinessLogik;
using DataAccessLayer.Entities;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Auftragsverwaltung.Views
{
    /// <summary>
    /// Interaction logic for KundeAdresse.xaml
    /// </summary>
    public partial class KundeAdresse : Page
    {

        private int aktuellerKundenId;
        private int aktuelleAdressId;
        private Kunde aktuellerKunde;
        private Adresse adresseVonKunde;
        private Ortschaft ortschaftVonAdresse;

        
        private string filtergradKunde;

        private ControllerKundeAdresse controllerKundeAdresse = new ControllerKundeAdresse();
        private ControllerOrtschaft controllerOrtschaft = new ControllerOrtschaft();

        public KundeAdresse()
        {
            InitializeComponent();

        }

        private void cmdSpeichern_Click(object sender, RoutedEventArgs e)
        {
            try
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
                var plz = Convert.ToInt32(txtPLZ.Text);


                controllerKundeAdresse.NeuerKundeAdresseAnlegen(vorname, nachname, firma,
                    email, passwort, website, strasse, hausNr, ortschaft, plz);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Fehler: " + "r\n" + exception);

            }
            LadeKunden(filtergradKunde);
            LadeAdressen(aktuellerKundenId);
            LadeOrtschaften();
        }

        private void cmdAendern_Click(object sender, RoutedEventArgs e)
        {
            //Idee: ändern button so machen das daten nur übergeben werden an controller (quasi wie bei speichern)
            //      und erst im controller geprüft wird ob änderungen da sind und welche...

            //ganze Objekte an controller übergeben welche von selectionchange erstllt werden und global verfügbar sind
            //aktuerllerKunde, aktuelleAdresse, aktuelleOrtschaft
            //diese an controller übergeben und irgendwie mit IComparable auf Änderungen prüfen.
            //wenn änderugen da sind dann erst speichern...

            try
            {
                var altKundenId = aktuellerKundenId;
                var altAdressId = adresseVonKunde.Id;
                var altOrtschaftId = ortschaftVonAdresse.Id;

                var vorname = txtVorname.Text;
                var nachname = txtNachname.Text;
                var firma = txtFirma.Text;
                var email = txtEmail.Text;
                var passwort = txtPasswort.Text;
                var website = txtWebsite.Text;
                var strasse = txtStrasse.Text;
                var hausNr = txtHausNr.Text;
                var ortschaft = txtOrtschaft.Text;
                var plz = Convert.ToInt32(txtPLZ.Text);


                    controllerKundeAdresse.AendereKundeAdresseOrtschaft(
                        altKundenId, vorname, nachname, firma, email,
                        passwort, website, strasse, hausNr, plz, ortschaft);

            }
            catch (Exception exception)
            {
                MessageBox.Show("Fehler: " + "r\n" + exception);

            }
            LadeKunden(filtergradKunde);
            LadeAdressen(aktuellerKundenId);
            LadeOrtschaften();


        }

        private void cmdFelderLeeren_Click(object sender, RoutedEventArgs e)
        {
            FelderLeeren();
        }





        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                radAktuelleKunden.IsChecked = true;
                LadeKunden("aktive");
                LadeAdressen(aktuellerKundenId);
                LadeOrtschaften();
            }
            catch (Exception exception)
            {
              //  MessageBox.Show("Daten konnten nicht geladen werden: " + "r\n" + exception);
            }

        }

        private void dgvKunde_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            try
            {
                var aktuelleZeile = dgvKunde.SelectedCells.ToArray();
                aktuellerKunde = (Kunde)aktuelleZeile[0].Item;


                SelectierterKundeZuFeldern(aktuellerKunde);
                aktuellerKundenId = aktuellerKunde.Id;

                //Ladet Adressen zu aktuellem Kunden
                LadeAdressen(aktuellerKundenId);

                adresseVonKunde = AdresseZuKundenId(aktuellerKundenId);
                MarkiereAdressInDg(adresseVonKunde.Id);
                SeletierteAdresseZuFeldern(adresseVonKunde);

                ortschaftVonAdresse = OrtschaftzuAdresseId(adresseVonKunde.OrtschaftId);
                SelectierteOrtschaftZuFelder(ortschaftVonAdresse);
                MarkiereOrtschaftInDg(ortschaftVonAdresse.Id);


            }
            catch (Exception exception)
            {
              //  MessageBox.Show("Fehler: " + "r\n" + exception);
            }

        }

        private void dgvAdresse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var aktuelleZeile = dgvAdresse.SelectedCells.ToArray();
            var aktuelleAdresse = (Adresse)aktuelleZeile[0].Item;

            aktuellerKundenId = aktuelleAdresse.Id;

        }

        private void cmbOrtschaft_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void lblOrtschaft_PreviewMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OrtschaftWindow ortschaftWindow = new OrtschaftWindow();
            ortschaftWindow.Show();
        }

        private void cmdExport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var adressId = 1;//aktuelleAdressId;
                var kundenId = SelectierterKunde_Id();
                
                //Alt
                //controllerKundeAdresse.KundeExportieren(kundenId);

                //neu
                controllerKundeAdresse.KundeAdresseExportieren(kundenId, adressId);


            }
            catch (Exception exception)
            {

            }
        }
        
        private int SelectierterKunde_Id()
        {
            var kundenId = Convert.ToInt32(lblKundenId.Content);
            return kundenId;
        }


        private void  SelectierterKundeZuFeldern(Kunde aktuellerKunde)
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

        private void LadeKunden(string filtergrad)
        {
            dgvKunde.ItemsSource = controllerKundeAdresse.LadeKunden(filtergrad);
        }
        private void LadeAdressen(int kundenId)
        {
            dgvAdresse.ItemsSource = controllerKundeAdresse.LadeAdressen(kundenId);
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
            txtKundenNr.Text = "";
            txtNachname.Text = "";
            txtVorname.Text = "";
            txtFirma.Text = "";
            txtEmail.Text = "";
            txtWebsite.Text = "";
            txtPasswort.Text = "";

            txtStrasse.Text = "";
            txtHausNr.Text = "";
            txtOrtschaft.Text = "";
            txtPLZ.Text = "";
        }

        private bool PruefeAenderungKunde()
        {
            //prüft ob txt eingaben mit selektierteme Kunden Änderungen vorweisen
            //aktuellerKunde

            //prüft ob ännderunge vorgenommen wruden
            if (txtVorname.Text != aktuellerKunde.Vorname ||
                txtNachname.Text != aktuellerKunde.Nachname ||
                txtEmail.Text != aktuellerKunde.Email ||
                txtFirma.Text != aktuellerKunde.Firma ||
                txtWebsite.Text != aktuellerKunde.Website ||
                txtPasswort.Text != aktuellerKunde.Passwort)
            {
                //return true => änderungen sind vorhanden
                return true;
            }
            else
            {
                //return false => keine änderungen vorhanden
                return false;
            }

        }
        private bool PruefeAnderungAdresse()
        {
            //prüft ob txt eingaben mit selektierteme Kunden Änderungen in Adresse vorweisen

            if (txtStrasse.Text != adresseVonKunde.Strasse ||
                txtHausNr.Text != adresseVonKunde.HausNr)
            {
                //return true => änderungen sind vorhanden
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool PruefeAnderungOrtschaft()
        {
            //prüft ob txt eingaben mit selektierteme Kunden Änderungen in Ortschaft vorweisen

            if (txtOrtschaft.Text != ortschaftVonAdresse.Ort ||
                Convert.ToInt32(txtPLZ.Text) != ortschaftVonAdresse.PLZ)
            {
                //return true => änderungen sind vorhanden
                return true;
            }
            else
            {
                return false;
            }
        }

        private void radAlleKunden_Checked(object sender, RoutedEventArgs e)
        {
            radAlteKunden.IsChecked = false;
            radAktuelleKunden.IsChecked = false;

            filtergradKunde = "alle";

            LadeKunden(filtergradKunde);
            cmdAendern.IsEnabled = false;
        }

        private void radAktuelleKunden_Checked(object sender, RoutedEventArgs e)
        {
            radAlleKunden.IsChecked = false;
            radAlteKunden.IsChecked = false;

            filtergradKunde = "aktive";
            LadeKunden(filtergradKunde);

            cmdAendern.IsEnabled = true;
        }

        private void radAlteKunden_Checked(object sender, RoutedEventArgs e)
        {
            radAktuelleKunden.IsChecked = false;
            radAlteKunden.IsChecked = false;

            filtergradKunde = "alte";
            LadeKunden(filtergradKunde);
            cmdAendern.IsEnabled = false;
        }

        private void cmdSuchen_Click(object sender, RoutedEventArgs e)
        {
            var kundenNr = Convert.ToString(txtKundenNr.Text);
            var vorname = txtVorname.Text;
            var nachname = txtNachname.Text;
            var firma = txtFirma.Text;
            var email = txtEmail.Text;
            var passwort = txtPasswort.Text;
            var website = txtWebsite.Text;
            //var strasse = txtStrasse.Text;
            //var hausNr = txtHausNr.Text;
            //var ortschaft = txtOrtschaft.Text;
            //var plz = Convert.ToInt32(txtPLZ.Text);

            dgvKunde.ItemsSource = controllerKundeAdresse.SucheDatensatz(kundenNr, vorname, nachname, firma, email, website);


        }

        
    }
}
