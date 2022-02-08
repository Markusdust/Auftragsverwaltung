﻿using BusinessLogik;
using DataAccessLayer.Entities;
using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Auftragsverwaltung.Views
{
    /// <summary>
    /// Interaction logic for KundeAdresse.xaml
    /// </summary>
    public partial class KundeAdresse : Page
    {
        private int aktuellerKundenId;

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
                    email, passwort, website, strasse, hausNr, ortschaft);
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
            SelectiereAdressInDg(2);

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
                MessageBox.Show("Daten konnten nicht geladen werden: " + "r\n" + exception);
            }

        }

        private void dgvKunde_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Adresse adresseVonKunde;
            try
            {
                var aktuelleZeile = dgvKunde.SelectedCells.ToArray();
                var aktuellerKunde = (Kunde)aktuelleZeile[0].Item;
                

                SelectierterKundeZuFeldern(aktuellerKunde);
                aktuellerKundenId = aktuellerKunde.Id;

                adresseVonKunde=AdresseZuKundenId(aktuellerKundenId);
                SelectiereAdressInDg(adresseVonKunde.Id);
                SeletierteAdresseZuFeldern(adresseVonKunde);
                

            }
            catch (Exception exception)
            {
                MessageBox.Show("Fehler: " + "r\n" + exception);
            }


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
            txtOrtschaft.Text = Convert.ToString(aktuelleAdresse.OrtschaftId);
        }

        private Adresse AdresseZuKundenId(int kundenID)
        {
            return controllerKundeAdresse.AdresseZuKunde(kundenID);
        }
        private void LadeKunden()
        {
            dgvKunde.ItemsSource = controllerKundeAdresse.LadeKunden();
        }
        private void LadeAdressen()
        {
            dgvAdresse.ItemsSource = controllerKundeAdresse.LadeAdressen();
        }

        private void SelectiereAdressInDg(int AdressId)
        {
            for (int i = 0; i < dgvAdresse.Items.Count; i++)
            {
                if (AdressId == ((Adresse)dgvAdresse.Items[i]).Id)
                {
                    dgvAdresse.SelectedIndex = i;
                    
                }
            }

        }
    }
}
