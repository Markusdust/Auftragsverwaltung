using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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
using DataAccessLayer;
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
                    email, passwort, website, strasse,hausNr,ortschaft);
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
                Console.WriteLine("Daten könnten nicht geladen werden." + exception);
                
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
                Console.WriteLine("Daten könnten nicht geladen werden."+exception);
                
            }
            
        }
    }
}
