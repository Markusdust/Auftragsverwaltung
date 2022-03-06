using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            // Schreibt Anzahl Artikel,Artikelgruppen in Label
            Count();
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
                    ArtikelNr = Convert.ToInt16(TxtArtikelNummer.Text),
                    Bezeichnung = TxtArtikelBezeichung.Text,
                    PreisNetto = Convert.ToDecimal(TxtPreisNetto.Text),
                    PreisBrutto = (MWST() / 100 + 1) * Convert.ToDecimal(TxtPreisNetto.Text),
                    Aktiv = (bool)ChkAktiv.IsChecked ? true : false,
                    Mwst = MWST(),
                    ArtikelgruppeId = controllerArtikelGruppe.ArtikelGruppeID(CmbArtikelGruppe.Text)
                };
                controllerArtikel.NeuerArtieklAnlegen(a1);
                Count();
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
            LeereFelder();
            Count();

        }
        //Artikel Ändern
        private void CmDArtikelAendern_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DgvArtikel.SelectedCells[0] != null)
                {
                    var zeile = (Artikel)DgvArtikel.SelectedCells[0].Item;
                    
                    Artikel a1 = new Artikel()
                    {
                        Id = zeile.Id,
                        ArtikelNr = Convert.ToInt16(TxtArtikelNummer.Text),
                        Bezeichnung = TxtArtikelBezeichung.Text,
                        PreisNetto = Convert.ToDecimal(TxtPreisNetto.Text),
                        Aktiv = (bool)ChkAktiv.IsChecked ? true : false,
                        Mwst = MWST(),
                        ArtikelgruppeId = controllerArtikelGruppe.ArtikelGruppeID(CmbArtikelGruppe.Text)
                    };
                    controllerArtikel.AendereArtikel(a1);
                    LadeDataGrid("Artikel");
                    LeereFelder();
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

                TxtArtikelNummer.Text = aktuelleZeile.ArtikelNr.ToString();
                TxtArtikelBezeichung.Text = aktuelleZeile.Bezeichnung;
                TxtPreisNetto.Text = aktuelleZeile.PreisNetto.ToString();
                ChkAktiv.Content = aktuelleZeile.Aktiv;
                CmbArtikelGruppe.Text = DgvArtikel.SelectedCells[8].Item.ToString();
                TxtPreisBrutto.Text = aktuelleZeile.PreisBrutto.ToString();

                if (aktuelleZeile.Mwst == 7.7m)
                    RadNormalMWST.IsChecked = true;
                else if (aktuelleZeile.Mwst == 2.5m)
                    RadReduziert.IsChecked = true;
                else
                    RadSteuerfrei.IsChecked = true;
            }
            catch (Exception exception)
            {
                MessageBox.Show("überprüfen sie ihre eingabe");
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
                int? uebergeortneteAG = controllerArtikelGruppe.GetUebergeortneteAG();
                
                controllerArtikelGruppe.ArtikelGruppeAnlegen(name, akitve, uebergeortneteAG);
                LadeDataGrid("Artikelgruppe");
                LadeCmbAG();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Konnte nicht geladen werden, überprüfen Sie ihre Eingabe");
                throw;
            }
            LeereFelder();
            Count();
        }

        // ArtikelGruppe löschen
        private void CmdArtikelGruppeLöschen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var deleteArtikelgruppe = (Artikelgruppe)DgvArtikelGruppe.SelectedCells[0].Item;

                if (controllerArtikelGruppe.ArtikelGruppeLöschen(deleteArtikelgruppe) == true)
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
            LeereFelder();
            Count();

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

        #region ---------------TreeView----------------

        //Lade TreeView
        private void CmdTreeView_Click(object sender, RoutedEventArgs e)
        {
            TVArtikelgruppe.Items.Clear();
            var ladeCte = controllerArtikelGruppe.LadeCte();

            var tree = this.GenerateTreeView(ladeCte, x => x.Id, x => x.UebergeordneteGruppeId, null).ToList();
            var rootItem = new TreeViewItem()
            {
                Title = "Artikelgruppen",
                Items = new ObservableCollection<TreeViewItem>(tree)
            };
            TVArtikelgruppe.Items.Add(rootItem);
        }
        //GeneriereTreeView
        private IEnumerable<TreeViewItem> GenerateTreeView(List<Artikelgruppe> gruppen, Func<Artikelgruppe, int?> idSelector,
            Func<Artikelgruppe, int?> parentIdSelector, int? rootId = null)
        {
            var list = gruppen;
            foreach (var item in list.Where(x => parentIdSelector(x).Equals(rootId)))
            {
                var tvItem = new TreeViewItem
                {
                    Title = item.Name,
                };
                tvItem.Items = new ObservableCollection<TreeViewItem>(GenerateTreeView(gruppen, idSelector, parentIdSelector, idSelector(item)));
                yield return tvItem;
            }
        }

        #endregion ------------TreeView----------------

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
            TxtArtikelNummer.Text = "";
            TxtPreisBrutto.Text = "";
        }

        //Zähle alle Artikel und Artikelgruppen
        private void Count()
        {
            LblArtikelNummmer.Content = Convert.ToString(controllerArtikel.LadeArtikel().Count);
            LblArtikekgruppeNummer.Content = Convert.ToString(controllerArtikelGruppe.LadeArtikelgruppe().Count);
        }

    
        
    }
}
