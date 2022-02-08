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
        private static ControllerArtikelGruppe controllerArtikelGruppe;
        public ArtikelArtikelgruppe()
        {
            InitializeComponent();
            controllerArtikel = new ControllerArtikel();
            controllerArtikelGruppe = new ControllerArtikelGruppe();

            // Zählt anzahl einträge +1 => nächste verfügbare id
            LblArtikelNummmer.Content = controllerArtikel.GetCounterArtikel("SELECT COUNT(Id) FROM ARTIKEL").ToString();
            LblArtikekgruppeNummer.Content = controllerArtikelGruppe.GetCounterArtikelGruppe("SELECT COUNT(Id) FROM ARTIKELGRUPPE").ToString();

            // Füllt Grid mit Bestehenden Daten von DB
            LadeDataGrid("Artikel");
            LadeDataGrid("Artikelgruppe");
            LadeCmbAG();
        }

        // Artikel anlegen
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                string bezeichnung = TxtArtikelBezeichung.Text;
                decimal nettopreis = Convert.ToInt16(TxtPreisNetto.Text);
                bool aktiv = (bool)ChkAktiv.IsChecked ? true : false;
                int artikelgruppeid = CmbArtikelGruppe.Text=="" ? -1 : CmbArtikelGruppe.SelectedIndex + 1;



                controllerArtikel.NeuerArtieklAnlegen(bezeichnung, nettopreis, aktiv , artikelgruppeid);

                LadeDataGrid("Artikel");
            }
            catch (Exception exception)
            {
                MessageBox.Show("Konnte nicht geladen werden, überprüfen Sie ihre Eingabe" + exception);
            }
            
        }
        //ArtikelGruppe anlegen
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = TxtArtikelgruppeBezeichung.Text;
                bool akitve = (bool)ChkArtikelGruppeAktiv.IsChecked ? true : false;

                controllerArtikelGruppe.ArtikelGruppeAnlegen(name, akitve);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Konnte nicht geladen werden, überprüfen Sie ihre Eingabe" + exception);
                throw;
            }
        }
        // Ladet Grid
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

            //controllerArtikel.testartikelanlegen();
            LadeDataGrid("Artikel");
            //controllerArtikelGruppe.ArtikelGruppeAnlegen();
            LadeDataGrid("Artikelgruppe");
            LadeCmbAG();

        }
        // Ladet Cmb
        private void LadeCmbAG()
        {
            var tempGruppe = new Artikelgruppe();
            var testdaten = DgvArtikelGruppe.Items;

            for (int i = 0; i < testdaten.Count  ; i++)
            {
                var gruppen =(Artikelgruppe)testdaten[i];
                CmbArtikelGruppe.Items.Add(gruppen.Name);
            }
        }

        // Selektion im Grid zu Textfeldern
        private void DgvArtikel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var aktuelleZeile = DgvArtikel.SelectedCells.ToArray();
            var aktuellerArtikel = (Artikel)aktuelleZeile[0].Item;

            LadeArtikelinFeldern(aktuellerArtikel);
            
            
        }

        private void LadeArtikelinFeldern(Artikel aktuellerArtikel)
        {
            LblArtikelNummmer.Content = aktuellerArtikel.Id.ToString();
            TxtArtikelBezeichung.Text = aktuellerArtikel.Bezeichnung;
            TxtPreisNetto.Text = aktuellerArtikel.PreisNetto.ToString();
            ChkAktiv.Content = aktuellerArtikel.Aktiv;

            //CmbArtikelGruppe.SelectionBoxItemStringFormat()


        }

        
    }
}
