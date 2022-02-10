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
using DataAccessLayer.Model;
using Microsoft.EntityFrameworkCore;

namespace Auftragsverwaltung.Views
{
    /// <summary>
    /// Interaction logic for ArtikelArtikelgruppe.xaml
    /// </summary>
    public partial class ArtikelArtikelgruppe : Page
    {
        private ControllerArtikel controllerArtikel;
        private ControllerArtikelGruppe controllerArtikelGruppe;
        public ArtikelArtikelgruppe()
        {
            InitializeComponent();
            controllerArtikel = new ControllerArtikel();
            controllerArtikelGruppe = new ControllerArtikelGruppe();

            LblArtikelNummmer.Content = controllerArtikel.GetCounterArtikel().ToString();
            LblArtikekgruppeNummer.Content = controllerArtikelGruppe.GetCounterArtikelGruppe().ToString();

            LadeDataGrid("Artikel");
            LadeDataGrid("Artikelgruppe");
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Test Artikel anlegen
            //  controller.testartikelanlegen();

            //Test ArtikelGruppe anlegen
            //controller.testartikelgruppeanlegen();

            try
            {
                string bezeichnung = TxtArtikelBezeichung.Text;
                decimal nettopreis = Convert.ToInt16(TxtPreisNetto.Text);
                bool aktiv = (bool)ChkAktiv.IsChecked ? true : false;
                string artikelgruppe = CmbArtikelGruppe.Text;

                controllerArtikel.NeuerArtieklAnlegen(bezeichnung, nettopreis, aktiv /*artikelgruppe*/);


            }
            catch (Exception exception)
            {
                MessageBox.Show("Konnte nicht geladen werden" + "r/n" + exception);
            }
            LadeDataGrid("Artikel");
        }

        private void LadeDataGrid(string grid)
        {
            switch (grid)
            {
                case "Artikel":
                    DgvArtikel.ItemsSource = controllerArtikel.LadeArtikel();
                    break;
                case "Artikelgruppe":
                    DgvArtikelGruppe.ItemsSource = controllerArtikelGruppe.LadeArtikelgruppe();
                    break;

            }

            DgvArtikel.ItemsSource = controllerArtikel.LadeArtikel();
        }

        //TestArtikel
        private void CmbTestArtikel_Click(object sender, RoutedEventArgs e)
        {
            //Artikelgruppe muss zuerst erstellt werden, Artikel ist abhängig von Artikelgruppe
            // controllerArtikelGruppe.ArtikelGruppeAnlegen();
            // LadeDataGrid("Artikelgruppe");

            controllerArtikel.testartikelanlegen();
            LadeDataGrid("Artikel");
            

        }
    }
}
