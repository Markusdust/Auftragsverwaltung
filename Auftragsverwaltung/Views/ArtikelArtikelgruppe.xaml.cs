using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Diagnostics;

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
                decimal nettopreis = Convert.ToDecimal(TxtPreisNetto.Text);
                bool aktiv = (bool)ChkAktiv.IsChecked ? true : false;
                int artikelgruppeid = CmbArtikelGruppe.Text=="" ? -1 : CmbArtikelGruppe.SelectedIndex + 1;

                controllerArtikel.NeuerArtieklAnlegen(bezeichnung, nettopreis, aktiv , artikelgruppeid);

                
            }
            catch (Exception exception)
            {
                MessageBox.Show("Konnte nicht geladen werden, überprüfen Sie ihre Eingabe" + exception);
            }
            LadeDataGrid("Artikel");
        }
        //ArtikelGruppe anlegen
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = TxtArtikelgruppeBezeichung.Text;
                bool akitve = (bool)ChkArtikelGruppeAktiv.IsChecked ? true : false;

                controllerArtikelGruppe.ArtikelGruppeAnlegen(name, akitve);
                LadeDataGrid("Artikelgruppe");
                LadeCmbAG();
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

        // Ladet Cmb
        private void LadeCmbAG()
        {
            CmbArtikelGruppe.Items.Clear();

            var testdaten = DgvArtikelGruppe.Items;

            for (int i = 0; i < testdaten.Count  ; i++)
            {
                var gruppen =(Artikelgruppe)testdaten[i];
                CmbArtikelGruppe.Items.Add(gruppen.Name);
            }
        }

        // Artikel im Grid zu Textfeldern
        private void DgvArtikel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var aktuelleZeile = (Artikel)DgvArtikel.SelectedCells[0].Item;
            //LadeArtikelinFeldern(aktuelleZeile);
        }

        private void LadeArtikelinFeldern(Artikel aktuellerArtikel)
        {
            LblArtikelNummmer.Content = aktuellerArtikel.Id.ToString();
            TxtArtikelBezeichung.Text = aktuellerArtikel.Bezeichnung;
            TxtPreisNetto.Text = aktuellerArtikel.PreisNetto.ToString();
            ChkAktiv.Content = aktuellerArtikel.Aktiv;
        }
        //Artikel Löschen
        private void CmdLöschen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectdeletedata =(Artikel) DgvArtikel.SelectedCells[0].Item;
                controllerArtikel.DeleteArtikel(selectdeletedata.Id);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Artikel konnte nicht gelöscht werden");
                throw;
            }

            LadeDataGrid("Artikel");
        }

        private void CmbTestArtikel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CmdArtikelGruppeLöschen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var deleteArtikelgruppe = (Artikelgruppe)DgvArtikelGruppe.SelectedCells[0].Item;

                if (controllerArtikelGruppe.ArtikelGruppeLöschen(deleteArtikelgruppe.Id) == true)
                    MessageBox.Show("Artikelgruppe wurde gelöscht");
                else
                    MessageBox.Show("Bei dieser Artikelgruppe gibt es noch dazugehörige Artikel");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }

            LadeDataGrid("Artikelgruppe");

        }

        private void DgvArtikelGruppe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var artikelgruppe = (Artikelgruppe)DgvArtikelGruppe.SelectedCells[0].Item;
            var aktuelleZeile = DgvArtikelGruppe.SelectedCells.ToArray();
            var aktuelleGruppe = (Artikelgruppe)aktuelleZeile[0].Item;

            LadeArtikelGruppeInFeldern(aktuelleGruppe);
        }

        private void LadeArtikelGruppeInFeldern(Artikelgruppe artikelgruppe)
        {
            LblArtikekgruppeNummer.Content = artikelgruppe.Id;
            TxtArtikelgruppeBezeichung.Text = artikelgruppe.Name;
            ChkArtikelGruppeAktiv.IsChecked = artikelgruppe.Active;
        }
    }
}
