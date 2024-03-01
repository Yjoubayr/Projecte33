﻿using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using ReproductorMusicaComponentLibrary.Classes;
using ReproductorMusicaComponentLibrary.ConnexioAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using System.Collections.Generic;
using iText.Layout;

namespace TaulerDeControlRM
{
    /// <summary>
    /// Interaction logic for PageLlistes.xaml
    /// </summary>
    public partial class PageLlistes : Page
    {
        public PageLlistes()
        {
            InitializeComponent();
            carregarValors();
        }
        private async Task carregarValors()
        {
            await ObtenirCancons();
            await ObtenirLlistes();
            await ObtenirTitolAlbums();
            await ObtenirAnyAlbums();
        }

        private async Task ObtenirCancons()
        {
            List<Canco> llistaCancons = await CA_Canco.GetCanconsAsync();
            foreach (Canco canco in llistaCancons)
            {
                if (canco != null)
                {
                    cmbCanco.Items.Add(canco.IDCanco);
                }
                else
                {
                    throw new Exception("No s'ha pogut carregar la llista de cançons");
                }
            }
        }
        private async Task ObtenirLlistes()
        {
            List<Llista> llistaLlistes = await CA_Llista.GetLlistesAsync();
            foreach (Llista llista in llistaLlistes)
            {
                cmbLlistaMAC.Items.Add(llista.MACAddress);
            }
        }

        private async Task ObtenirTitolAlbums()
        {
            List<Album> llistaAlbums = await CA_Album.GetAlbumsAsync();
            foreach (Album album in llistaAlbums)
            {
                cmbTitolAlbum.Items.Add(album.Titol);
            }
        }

        private async Task ObtenirAnyAlbums()
        {
            List<Album> llistaAlbums = await CA_Album.GetAlbumsAsync();
            foreach (Album album in llistaAlbums)
            {
                cmbAnyAlbum.Items.Add(album.Any);
            }
        }

        private async void cmbLlistaMAC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbLlistaNom.Items.Clear();
            string MACAddress = cmbLlistaMAC.SelectedItem.ToString();
            List<Llista> llistaLlistes = await CA_Llista.GetLlistesAsync();
            foreach (Llista llista in llistaLlistes)
            {
                if (llista != null)
                {
                    if (llista.MACAddress == MACAddress)
                    {
                        cmbLlistaNom.Items.Add(llista.NomLlista);
                    }
                }
                else
                {
                    throw new Exception("No s'ha pogut carregar la llista de cançons");
                }
            }
        }

        private async void cmbTitolAlbum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbTitolAlbum.SelectedItem != null)
            {
                cmbAnyAlbum.IsEnabled = true;
            }
            else
            {
                cmbAnyAlbum.IsEnabled = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtMostrarCancons_Click(object sender, RoutedEventArgs e)
        {
            //var num = int.Parse(txtNum.Text);
            EliminarListView();

            //CrearListView

            ListView songListView = new ListView();
            songListView.Name = "songListView";
            songListView.Width = 786;

            var gridView = new GridView();

            var nomColumn = new GridViewColumn();
            nomColumn.Width = 260;
            nomColumn.Header = "Nom";
            nomColumn.DisplayMemberBinding = new Binding("Nom");

            var anyColumn = new GridViewColumn();
            anyColumn.Width = 260;
            anyColumn.Header = "Data";
            anyColumn.DisplayMemberBinding = new Binding("Any");

            // Add columns to GridView
            gridView.Columns.Add(nomColumn);
            gridView.Columns.Add(anyColumn);

            // Set GridView as the View of ListView
            songListView.View = gridView;

            // Add ListView to the grid (assuming you have a Grid named "yourGrid" in your Window)
            spGlobal.Children.Add(songListView);

            try
            {
                List<Canco> llista = await CA_Canco.GetCanconsAsync();
                foreach (Canco canco in llista)
                {
                    if (canco != null)
                    {
                        songListView.Items.Add(canco);


                    }
                    else
                    {
                        throw new Exception("No s'ha pogut carregar la llista de cançons");
                    }


                }
            }
            catch (Exception ex)
            {
                throw new Exception("No s'ha fet correctament la petico a l'api" + ex);
            }
        }
        private void EliminarListView()
        {
            var lastChild = spGlobal.Children[spGlobal.Children.Count - 1];

            // Check if the last child is a ListView
            if (lastChild is ListView listView)
            {
                // Remove the last child
                spGlobal.Children.Remove(listView);
            }
        }
        private async void llistesCanco(object sender, RoutedEventArgs e)
        {
            EliminarListView();
            ListView llistaListView = new ListView();
            llistaListView.Name = "llistaListView";

            var gridView = new GridView();

            var nomColumn = new GridViewColumn();
            nomColumn.Width = 260;
            nomColumn.Header = "Nom Llista";
            nomColumn.DisplayMemberBinding = new Binding("NomLlista");

            var anyColumn = new GridViewColumn();
            anyColumn.Width = 260;
            anyColumn.Header = "Dispositiu";
            anyColumn.DisplayMemberBinding = new Binding("NomDispositiu");

            // Add columns to GridView
            gridView.Columns.Add(nomColumn);
            gridView.Columns.Add(anyColumn);

            // Set GridView as the View of ListView
            llistaListView.View = gridView;

            // Add ListView to the grid (assuming you have a Grid named "yourGrid" in your Window)
            spGlobal.Children.Add(llistaListView);
            if (cmbCanco.SelectedItem != null)
            {
                string uid = cmbCanco.SelectedItem.ToString();
                Canco canco = await CA_Canco.GetCancoAsync(uid);

                foreach (Llista llista in canco.Llistes)
                {
                    llistaListView.Items.Add(llista);
                }
            }
            else
            {
                MessageBox.Show("No has seleccionat cap canço");
            }
        }
        private async void canconsLlista(object sender, RoutedEventArgs e)
        {
            EliminarListView();

            //CrearListView

            ListView songListView = new ListView(); songListView.Name = "songListView";
            songListView.Width = 786;

            var gridView = new GridView();

            var nomColumn = new GridViewColumn();
            nomColumn.Width = 260;
            nomColumn.Header = "Nom";
            nomColumn.DisplayMemberBinding = new Binding("Nom");

            var anyColumn = new GridViewColumn();
            anyColumn.Width = 260;
            anyColumn.Header = "Data";
            anyColumn.DisplayMemberBinding = new Binding("Any");

            // Add columns to GridView
            gridView.Columns.Add(nomColumn);
            gridView.Columns.Add(anyColumn);

            // Set GridView as the View of ListView
            songListView.View = gridView;

            // Add ListView to the grid (assuming you have a Grid named "yourGrid" in your Window)
            spGlobal.Children.Add(songListView);

            if (cmbLlistaNom.SelectedItem != null && cmbLlistaMAC.SelectedItem != null)
            {
                string nomLlista = cmbLlistaNom.SelectedItem.ToString();
                string MACAddress = cmbLlistaMAC.SelectedItem.ToString();
                Llista llista = await CA_Llista.GetLlistaAsync(nomLlista, MACAddress);

                foreach (Canco canco in llista.LCancons)
                {
                    songListView.Items.Add(canco);
                }
            }
            else
            {
                MessageBox.Show("No has seleccionat cap canço");
            }
        }

        private async void BtMostrarArtistesAlbum(object sender, RoutedEventArgs e)
        {
            EliminarListView();

            ListView albumArtistNomListView = new ListView();
            albumArtistNomListView.Name = "albumArtistNomListView";

            var gridView = new GridView();

            var NomColumn = new GridViewColumn();
            NomColumn.Width = 260;
            NomColumn.Header = "Artista";
            NomColumn.DisplayMemberBinding = new Binding("NomMusic");

            // Add columns to GridView
            gridView.Columns.Add(NomColumn);

            albumArtistNomListView.View = gridView;

            spGlobal.Children.Add(albumArtistNomListView);

            if (cmbTitolAlbum.SelectedItem != null && cmbAnyAlbum.SelectedItem != null)
            {
                string nomAlbum = cmbTitolAlbum.SelectedItem.ToString();
                string anyAlbum = cmbAnyAlbum.SelectedItem.ToString();

                List<Tocar> tocarLlista = await CA_Album.GetNomArtistesByAlbumAsync(nomAlbum, anyAlbum);

                foreach (Tocar tocar in tocarLlista)
                {
                    if (!albumArtistNomListView.Items.Contains(tocar.NomMusic))
                    {
                        albumArtistNomListView.Items.Add(tocar);
                    }
                }
            }
            else
            {
                MessageBox.Show("No has seleccionat ni el nom ni l'any");
            }
        }
    }
}
