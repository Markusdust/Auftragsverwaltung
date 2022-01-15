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

                controllerKundeAdresse.KundeAnlegen(kundenNr,vorname,nachname,firma,email,passwort,website);
        }
    }
}
