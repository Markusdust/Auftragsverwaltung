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
using System.IO.Packaging;

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

            LblArtikelNummmer.Content = "0";
            LblArtikekgruppeNummer.Content = "0";

            // Füllt Grid mit Bestehenden Daten von DB 
            LadeDataGrid("Artikel");
            LadeDataGrid("Artikelgruppe");
            LadeCmbAG();
        }

        #region --------------------Artikel------------------
        // Artikel anlegen
        private void CmdArtikelAnlegen(object sender, RoutedEventArgs e)
        {
            try
            {
                Artikel a1 = new Artikel()
                {
                    Bezeichnung = TxtArtikelBezeichung.Text,
                    PreisNetto = Convert.ToDecimal(TxtPreisNetto.Text),
                    PreisBrutto = (MWST() / 100 + 1) * Convert.ToDecimal(TxtPreisNetto.Text),
                    Aktiv = (bool)ChkAktiv.IsChecked ? true : false,
                    Mwst = MWST(),
                    ArtikelgruppeId = controllerArtikelGruppe.ArtikelGruppeID(CmbArtikelGruppe.Text)
                };
                controllerArtikel.NeuerArtieklAnlegen(a1);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Konnte nicht geladen werden, überprüfen Sie ihre Eingabe");
            }
            LadeDataGrid("Artikel");
            LeereFelder();
        }
        //Artikel Löschen
        private void CmdLöschen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectdeletedata = (Artikel)DgvArtikel.SelectedCells[0].Item;
                controllerArtikel.DeleteArtikel(selectdeletedata.Id);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Artikel konnte nicht gelöscht werden");
                throw;
            }

            DgvArtikel.SelectedItem = false;
            LadeDataGrid("Artikel");

        }
        //Artikel Ändern
        private void CmDArtikelAendern_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DgvArtikel.SelectedCells[0] != null)
                {
                    Artikel a1 = new Artikel()
                    {
                        ArtikelNr = Convert.ToInt16(LblArtikelNummmer.Content),
                        Bezeichnung = TxtArtikelBezeichung.Text,
                        PreisNetto = Convert.ToDecimal(TxtPreisNetto.Text),
                        Aktiv = (bool)ChkAktiv.IsChecked ? true : false,
                        Mwst = MWST(),
                        ArtikelgruppeId = CmbArtikelGruppe.Text == "" ? -1 : CmbArtikelGruppe.SelectedIndex + 1
                    };
                    controllerArtikel.AendereArtikel(a1);
                    LadeDataGrid("Artikel");
                }
                else
                    MessageBox.Show("Selektieren Sie den gewünschten Artikel im Grid");
            }
            catch (Exception exception)
            {
                MessageBox.Show("Alle Artikelfelder müssen ausgefüllt sein");
            }




        }

        //Artikel Suchen
        private void CmdArtikelSuchen_Click(object sender, RoutedEventArgs e)
        {
            string? bezeichung = TxtArtikelBezeichung.Text;
            int? artikelgruppe = controllerArtikelGruppe.ArtikelGruppeID(CmbArtikelGruppe.Text);

            var suche = controllerArtikel.SuchArtikel(bezeichung, artikelgruppe);

            DgvArtikel.ItemsSource = suche;
            LeereFelder();
        }

        // Artikel im Grid zu Textfeldern
        private void DgvArtikel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var aktuelleZeile = (Artikel)DgvArtikel.SelectedCells[0].Item;

                LblArtikelNummmer.Content = aktuelleZeile.Id.ToString();
                TxtArtikelBezeichung.Text = aktuelleZeile.Bezeichnung;
                TxtPreisNetto.Text = aktuelleZeile.PreisNetto.ToString();
                ChkAktiv.Content = aktuelleZeile.Aktiv;
                CmbArtikelGruppe.Text = DgvArtikel.SelectedCells[8].Item.ToString();
            }
            catch (Exception exception)
            {
                Console.WriteLine("exception");
            }
        }

        // Grid laden für Artikel
        private void CmdGridLadenA_Click(object sender, RoutedEventArgs e)
        {
            LeereFelder();
            LadeDataGrid("Artikel");
        }


        #endregion ----------------Artikel-------------------







        #region ----------------Artikelgruppe----------------
        //ArtikelGruppe anlegen
        private void CmdArtikelgruppeAnlegen(object sender, RoutedEventArgs e)
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
                Console.WriteLine("Konnte nicht geladen werden, überprüfen Sie ihre Eingabe");
                throw;
            }
            LeereFelder();
        }

        // ArtikelGruppe löschen
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

        // ArtikelGruppe Ändern
        private void CmdArtikelGruppeÄndern_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Artikelgruppe ag1 = new Artikelgruppe()
                {
                    Id = Convert.ToInt16(LblArtikekgruppeNummer.Content),
                    Name = TxtArtikelgruppeBezeichung.Text,
                    Active = (bool)ChkArtikelGruppeAktiv.IsChecked ? true : false

                };
                controllerArtikelGruppe.ArtikelGruppeÄndern(ag1);
                LadeDataGrid("Artikelgruppe");
            }
            catch (Exception exception)
            {
                MessageBox.Show("Überprüfen Ihre Eingabe");
            }
            LeereFelder();

        }

        // Lade Combobox mit ArtikelGruppe
        private void LadeCmbAG()
        {
            CmbArtikelGruppe.Items.Clear();

            var testdaten = DgvArtikelGruppe.Items;

            for (int i = 0; i < testdaten.Count; i++)
            {
                var gruppen = (Artikelgruppe)testdaten[i];
                CmbArtikelGruppe.Items.Add(gruppen.Name);
            }
        }

        // Lade ArtikelGruppe in Feldern
        private void DgvArtikelGruppe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var aktuelleZeile = (Artikelgruppe)DgvArtikelGruppe.SelectedCells[0].Item;
                LblArtikekgruppeNummer.Content = aktuelleZeile.Id;
                TxtArtikelgruppeBezeichung.Text = aktuelleZeile.Name;
                ChkArtikelGruppeAktiv.IsChecked = aktuelleZeile.Active;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);

            }
        }

        // Artikelgruppe Suchen
        private void ArtikelgruppeSuchen_Click(object sender, RoutedEventArgs e)
        {
            string name = TxtArtikelgruppeBezeichung.Text;

            DgvArtikelGruppe.ItemsSource = controllerArtikelGruppe.SucheArtikelgruppe(name);

        }

        // Grid laden für ArtikelGruppe
        private void CmdGridLadenAG_Click(object sender, RoutedEventArgs e)
        {
            LeereFelder();
            LadeDataGrid("Artikelgruppe");
        }

        #endregion --------------Artikelgruppe---------------



        //return MWST
        private decimal MWST()
        {
            if (RadNormalMWST.IsChecked == true)
                return 7.7m;
            else if (RadReduziert.IsChecked == true)
                return 2.5m;
             else 
                return 0;
        }

       
        // Lade Grid
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

        }

        // Leere alle Textfelder
        private void LeereFelder()
        {
            TxtArtikelgruppeBezeichung.Text = "";
            TxtArtikelBezeichung.Text = "";
            TxtPreisNetto.Text = "";
            CmbArtikelGruppe.Text = "";
        }
    }
}
