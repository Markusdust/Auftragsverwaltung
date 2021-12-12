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
    /// Interaction logic for ArtikelArtikelgruppe.xaml
    /// </summary>
    public partial class ArtikelArtikelgruppe : Page
    {
        private ControllerArtikel controller;
        public ArtikelArtikelgruppe()
        {
            InitializeComponent();
            controller = new ControllerArtikel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Test Artikel anlegen
          //  controller.testartikelanlegen();

            //Test ArtikelGruppe anlegen
            controller.testartikelgruppeanlegen();
        }
    }
}
