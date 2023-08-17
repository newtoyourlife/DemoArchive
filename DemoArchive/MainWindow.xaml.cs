using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Security.Policy;
using System.Windows.Media.TextFormatting;
using Microsoft.Web.WebView2.WinForms;
using Microsoft.Web.WebView2.Core;
using static System.Net.WebRequestMethods;
using System.Diagnostics;
using System.Net.Mail;

namespace DemoArchive
{
    public partial class MainWindow : Window
    {
        List<Button> myButtons = new List<Button>();
        int currentX = 0;
        const int STEP = 2;
        const int WIDTH = 80;
        const int HEIGHT = 25;
        XDocument doc;
        string CodToSearch = "Bo2";

        private async void PlayPreview(string url)
        {
            await DemoPreview.EnsureCoreWebView2Async(null);
            DemoPreview.CoreWebView2.Navigate("https://www.youtube.com/embed/" + url+ "?modestbranding=0&rel=0");

        }


        public MainWindow()
        {
            LoadXmlDocument("Bo2DemoArchiveDatabase.xml");
            InitializeComponent();
            XMLSearchBo2("", CodToSearch);
            btnBo2.Background = Brushes.Green;

        }

        private void XMLReset()
        {

            XElement entryElement = doc.Root?.Element("entry");

            {
                string weapons = entryElement.Element("weapons")?.Value;
                txtWeapons.Text = "Weapon: " + weapons;

                string camo = entryElement.Element("camo")?.Value;
                txtCamo.Text = "Camo: " + camo;

                string attachment = entryElement.Element("attachment")?.Value;
                txtAttachment.Text = "Attachment: " + attachment;

                string map = entryElement.Element("map")?.Value;
                txtMap.Text = "Map: " + map;

                string numofkills = entryElement.Element("numberofkills")?.Value;
                txtNumofkills.Text = "Number of Kills: " + numofkills;

                string tags = entryElement.Element("tags")?.Value;
                txtTags.Text = "Tags: " + tags;

                string player = entryElement.Element("player")?.Value;
                txtPlayer.Text = "Player: " + player;

                string timeoffrag = entryElement.Element("timeoffrag")?.Value;
                txtTimeoffrag.Text = "Timestamp: " + timeoffrag;

                string url = entryElement.Element("url")?.Value;
                txtUrl.Text = "YouTube URL: " + url;
                PlayPreview(url);

                string downloadUrl = entryElement.Element("downloadurl")?.Value;
                txtDownloadUrl.Text = "Download URL: " + downloadUrl;


            }
        }
        private void XMLSearchBo2(string searchval, string CodToSearch)
        {
            // Clear existing children
            spSearchResults.Children.Clear();

            // Get all entries from the XML
            var matchingEntries = doc.Descendants("entry");

            if (CodToSearch == "Bo2" || CodToSearch == "Mw3" || CodToSearch == "Bo3")
            {
                if (!string.IsNullOrWhiteSpace(searchval))
                {
                    string[] searchTerms = searchval.ToLower().Split(' ');

                    foreach (string term in searchTerms)
                    {
                        if (term.StartsWith("weapon:"))
                        {
                            string weaponSearchTerm = term.Substring("weapon:".Length).Trim();
                            matchingEntries = matchingEntries.Where(entry =>
                                entry.Element("weapons").Value.ToLower().Contains(weaponSearchTerm));
                        }
                        if (term.StartsWith("w:"))
                        {
                            string weaponSearchTerm = term.Substring("w:".Length).Trim();
                            matchingEntries = matchingEntries.Where(entry =>
                                entry.Element("weapons").Value.ToLower().Contains(weaponSearchTerm));
                        }
                        else if (term.StartsWith("camo:"))
                        {
                            string camoSearchTerm = term.Substring("camo:".Length).Trim();
                            matchingEntries = matchingEntries.Where(entry =>
                                entry.Element("camo").Value.ToLower().Contains(camoSearchTerm));
                        }
                        if (term.StartsWith("c:"))
                        {
                            string camoSearchTerm = term.Substring("c:".Length).Trim();
                            matchingEntries = matchingEntries.Where(entry =>
                                entry.Element("camo").Value.ToLower().Contains(camoSearchTerm));
                        }
                        else if (term.StartsWith("attachment:"))
                        {
                            string attachmentSearchTerm = term.Substring("attachment:".Length).Trim();
                            matchingEntries = matchingEntries.Where(entry =>
                                entry.Element("attachment").Value.ToLower().Contains(attachmentSearchTerm));
                        }
                        else if (term.StartsWith("a:"))
                        {
                            string attachmentSearchTerm = term.Substring("a:".Length).Trim();
                            matchingEntries = matchingEntries.Where(entry =>
                                entry.Element("attachment").Value.ToLower().Contains(attachmentSearchTerm));
                        }
                        else if (term.StartsWith("map:"))
                        {
                            string mapSearchTerm = term.Substring("map:".Length).Trim();
                            matchingEntries = matchingEntries.Where(entry =>
                                entry.Element("map").Value.ToLower().Contains(mapSearchTerm));
                        }
                        else if (term.StartsWith("m:"))
                        {
                            string mapSearchTerm = term.Substring("m:".Length).Trim();
                            matchingEntries = matchingEntries.Where(entry =>
                                entry.Element("map").Value.ToLower().Contains(mapSearchTerm));
                        }
                        else if (term.StartsWith("kills:"))
                        {
                            string killsSearchTerm = term.Substring("kills:".Length).Trim();
                            matchingEntries = matchingEntries.Where(entry =>
                                entry.Element("numberofkills").Value.ToLower().Contains(killsSearchTerm));
                        }
                        else if (term.StartsWith("k:"))
                        {
                            string killsSearchTerm = term.Substring("k:".Length).Trim();
                            matchingEntries = matchingEntries.Where(entry =>
                                entry.Element("numberofkills").Value.ToLower().Contains(killsSearchTerm));
                        }
                        else if (term.StartsWith("tags:"))
                        {
                            string tagsSearchTerm = term.Substring("tags:".Length).Trim();
                            matchingEntries = matchingEntries.Where(entry =>
                                entry.Element("tags").Value.ToLower().Contains(tagsSearchTerm));
                        }
                        else if (term.StartsWith("t:"))
                        {
                            string tagsSearchTerm = term.Substring("t:".Length).Trim();
                            matchingEntries = matchingEntries.Where(entry =>
                                entry.Element("tags").Value.ToLower().Contains(tagsSearchTerm));
                        }
                        else if (term.StartsWith("player:"))
                        {
                            string playerSearchTerm = term.Substring("player:".Length).Trim();
                            matchingEntries = matchingEntries.Where(entry =>
                                entry.Element("player").Value.ToLower().Contains(playerSearchTerm));
                        }
                        else if (term.StartsWith("p:"))
                        {
                            string playerSearchTerm = term.Substring("p:".Length).Trim();
                            matchingEntries = matchingEntries.Where(entry =>
                                entry.Element("player").Value.ToLower().Contains(playerSearchTerm));
                        }
                        else if (term.StartsWith("url:"))
                        {
                            string urlSearchTerm = term.Substring("url:".Length).Trim();
                            matchingEntries = matchingEntries.Where(entry =>
                                entry.Element("url").Value.ToLower().Contains(urlSearchTerm));
                        }
                        else if (term.StartsWith("u:"))
                        {
                            string urlSearchTerm = term.Substring("u:".Length).Trim();
                            matchingEntries = matchingEntries.Where(entry =>
                                entry.Element("url").Value.ToLower().Contains(urlSearchTerm));
                        }
                        else if (term.StartsWith("downloadurl:"))
                        {
                            string downloadurlSearchTerm = term.Substring("downloadurl:".Length).Trim();
                            matchingEntries = matchingEntries.Where(entry =>
                                entry.Element("downloadurl").Value.ToLower().Contains(downloadurlSearchTerm));
                        }
                        else if (term.StartsWith("d:"))
                        {
                            string downloadurlSearchTerm = term.Substring("downloadurl:".Length).Trim();
                            matchingEntries = matchingEntries.Where(entry =>
                                entry.Element("downloadurl").Value.ToLower().Contains(downloadurlSearchTerm));
                        }

                    }
                }

                    
                // Create buttons for each entry
                foreach (var entry in matchingEntries.Reverse())
                {
                    string weap = (string)entry.Element("weapons");
                    string camo = (string)entry.Element("camo");
                    string attachment = (string)entry.Element("attachment");
                    string map = (string)entry.Element("map");
                    string numberofkills = (string)entry.Element("numberofkills");
                    string tags = (string)entry.Element("tags");
                    string player = (string)entry.Element("player");
                    if (player == "ILOVEYOU") player = player + " catchheartz";
                    string timeoffrag = (string)entry.Element("timeoffrag");
                    string url = (string)entry.Element("url");
                    string downloadurl = (string)entry.Element("downloadurl");

                    Button btnNew = new Button
                    {
                        Content = weap + " " + numberofkills + "k " + map,
                        Style = (Style)FindResource("MyDynamicButtonStyle"),
                        HorizontalAlignment = HorizontalAlignment.Stretch
                    };

                    Grid.SetColumn(btnNew, 0);
                    btnNew.Name = "Button" + currentX;

                    btnNew.Click += (s, e) =>
                    {
                        txtWeapons.Text = "Weapon: " + weap;
                        txtCamo.Text = "Camo: " + camo;
                        txtAttachment.Text = "Attachment: " + attachment;
                        txtMap.Text = "Map: " + map;
                        txtNumofkills.Text = "Number of Kills: " + numberofkills;
                        txtTags.Text = "Tags: " + tags;
                        txtPlayer.Text = "Player: " + player;
                        txtTimeoffrag.Text = "Timestamp: " + timeoffrag;
                        txtUrl.Text = "Url: youtube.com/" + url;
                        txtDownloadUrl.Text = "Download Url: https://drive.google.com/file/d/" + downloadurl;
                        PlayPreview(url);

                        Button btnDownload = new Button
                        {
                            Content = "Download",
                            Style = (Style)FindResource("DownloadButton"),
                        };

                        Grid.SetColumn(btnDownload, 2);
                        gbDownloadBar.Content = btnDownload;

                        btnDownload.Click += (x, b) =>
                        {
                            Process.Start("https://drive.google.com/uc?export=download&id=" + downloadurl);
                        };
                    };
                    spSearchResults.Children.Add(btnNew);
                    currentX += 1;

                }
            }

            if (CodToSearch == "Cod4" || CodToSearch == "Mw2")
            {
                if (!string.IsNullOrWhiteSpace(searchval))
                {
                    string[] searchTerms = searchval.ToLower().Split(' ');

                    foreach (string term in searchTerms)
                    {
                        if (term.StartsWith("weapon:"))
                        {
                            string weaponSearchTerm = term.Substring("weapon:".Length).Trim();
                            matchingEntries = matchingEntries.Where(entry =>
                                entry.Element("weapons").Value.ToLower().Contains(weaponSearchTerm));
                        }
                        if (term.StartsWith("w:"))
                        {
                            string weaponSearchTerm = term.Substring("w:".Length).Trim();
                            matchingEntries = matchingEntries.Where(entry =>
                                entry.Element("weapons").Value.ToLower().Contains(weaponSearchTerm));
                        }
                        else if (term.StartsWith("camo:"))
                        {
                            string camoSearchTerm = term.Substring("camo:".Length).Trim();
                            matchingEntries = matchingEntries.Where(entry =>
                                entry.Element("camo").Value.ToLower().Contains(camoSearchTerm));
                        }
                        if (term.StartsWith("c:"))
                        {
                            string camoSearchTerm = term.Substring("c:".Length).Trim();
                            matchingEntries = matchingEntries.Where(entry =>
                                entry.Element("camo").Value.ToLower().Contains(camoSearchTerm));
                        }
                        else if (term.StartsWith("mod:"))
                        {
                            string modSearchTerm = term.Substring("mod:".Length).Trim();
                            matchingEntries = matchingEntries.Where(entry =>
                                entry.Element("mod").Value.ToLower().Contains(modSearchTerm));
                        }
                        else if (term.StartsWith("map:"))
                        {
                            string mapSearchTerm = term.Substring("map:".Length).Trim();
                            matchingEntries = matchingEntries.Where(entry =>
                                entry.Element("map").Value.ToLower().Contains(mapSearchTerm));
                        }
                        else if (term.StartsWith("m:"))
                        {
                            string mapSearchTerm = term.Substring("m:".Length).Trim();
                            matchingEntries = matchingEntries.Where(entry =>
                                entry.Element("map").Value.ToLower().Contains(mapSearchTerm));
                        }
                        else if (term.StartsWith("kills:"))
                        {
                            string killsSearchTerm = term.Substring("kills:".Length).Trim();
                            matchingEntries = matchingEntries.Where(entry =>
                                entry.Element("numberofkills").Value.ToLower().Contains(killsSearchTerm));
                        }
                        else if (term.StartsWith("k:"))
                        {
                            string killsSearchTerm = term.Substring("k:".Length).Trim();
                            matchingEntries = matchingEntries.Where(entry =>
                                entry.Element("numberofkills").Value.ToLower().Contains(killsSearchTerm));
                        }
                        else if (term.StartsWith("tags:"))
                        {
                            string tagsSearchTerm = term.Substring("tags:".Length).Trim();
                            matchingEntries = matchingEntries.Where(entry =>
                                entry.Element("tags").Value.ToLower().Contains(tagsSearchTerm));
                        }
                        else if (term.StartsWith("t:"))
                        {
                            string tagsSearchTerm = term.Substring("t:".Length).Trim();
                            matchingEntries = matchingEntries.Where(entry =>
                                entry.Element("tags").Value.ToLower().Contains(tagsSearchTerm));
                        }
                        else if (term.StartsWith("player:"))
                        {
                            string playerSearchTerm = term.Substring("player:".Length).Trim();
                            matchingEntries = matchingEntries.Where(entry =>
                                entry.Element("player").Value.ToLower().Contains(playerSearchTerm));
                        }
                        else if (term.StartsWith("p:"))
                        {
                            string playerSearchTerm = term.Substring("p:".Length).Trim();
                            matchingEntries = matchingEntries.Where(entry =>
                                entry.Element("player").Value.ToLower().Contains(playerSearchTerm));
                        }
                        else if (term.StartsWith("url:"))
                        {
                            string urlSearchTerm = term.Substring("url:".Length).Trim();
                            matchingEntries = matchingEntries.Where(entry =>
                                entry.Element("url").Value.ToLower().Contains(urlSearchTerm));
                        }
                        else if (term.StartsWith("u:"))
                        {
                            string urlSearchTerm = term.Substring("u:".Length).Trim();
                            matchingEntries = matchingEntries.Where(entry =>
                                entry.Element("url").Value.ToLower().Contains(urlSearchTerm));
                        }
                        else if (term.StartsWith("downloadurl:"))
                        {
                            string downloadurlSearchTerm = term.Substring("downloadurl:".Length).Trim();
                            matchingEntries = matchingEntries.Where(entry =>
                                entry.Element("downloadurl").Value.ToLower().Contains(downloadurlSearchTerm));
                        }
                        else if (term.StartsWith("d:"))
                        {
                            string downloadurlSearchTerm = term.Substring("downloadurl:".Length).Trim();
                            matchingEntries = matchingEntries.Where(entry =>
                                entry.Element("downloadurl").Value.ToLower().Contains(downloadurlSearchTerm));
                        }

                    }
                }


                // Create buttons for each entry
                foreach (var entry in matchingEntries)
                {
                    string weap = (string)entry.Element("weapons");
                    string camo = (string)entry.Element("camo");
                    string mod = (string)entry.Element("mod");
                    string map = (string)entry.Element("map");
                    string numberofkills = (string)entry.Element("numberofkills");
                    string tags = (string)entry.Element("tags");
                    string player = (string)entry.Element("player");
                    string timeoffrag = (string)entry.Element("timeoffrag");
                    string url = (string)entry.Element("url");
                    string downloadurl = (string)entry.Element("downloadurl");

                    Button btnNew = new Button
                    {
                        Content = weap + " " + numberofkills + "k " + map,
                        Style = (Style)FindResource("MyDynamicButtonStyle"),
                        HorizontalAlignment = HorizontalAlignment.Stretch
                    };

                    Grid.SetColumn(btnNew, 0);
                    btnNew.Name = "Button" + currentX;

                    btnNew.Click += (s, e) =>
                    {
                        txtWeapons.Text = "Weapon: " + weap;
                        txtCamo.Text = "Camo: " + camo;

                        txtAttachment.Text = "mod: " + mod;
                        txtMap.Text = "Map: " + map;
                        txtNumofkills.Text = "Number of Kills: " + numberofkills;
                        txtTags.Text = "Tags: " + tags;
                        txtPlayer.Text = "Player: " + player;
                        txtTimeoffrag.Text = "Timestamp: " + timeoffrag;
                        txtUrl.Text = "Url: youtube.com/" + url;
                        txtDownloadUrl.Text = "Download Url: https://drive.google.com/file/d/" + downloadurl;
                        PlayPreview(url);

                        Button btnDownload = new Button
                        {
                            Content = "Download",
                            Style = (Style)FindResource("DownloadButton"),
                        };

                        Grid.SetColumn(btnDownload, 2);
                        gbDownloadBar.Content = btnDownload;

                        btnDownload.Click += (x, b) =>
                        {
                            Process.Start("https://drive.google.com/uc?export=download&id=" + downloadurl);
                        };
                    };
                    spSearchResults.Children.Add(btnNew);
                    currentX += 1;

                }
            }
        }

        private void ToggleBtnColor(string CodToSearch)
        {
            btnBo2.Background = btnBo3.Background = btnMw3.Background = btnMw2.Background = btnCod4.Background = Brushes.Transparent;

            switch (CodToSearch.ToLower())
            {
                case "bo2":
                    btnBo2.Background = Brushes.Green;
                    break;
                case "bo3":
                    btnBo3.Background = Brushes.Green;
                    break;
                case "cod4":
                    btnCod4.Background = Brushes.Green;
                    break;
                case "mw3":
                    btnMw3.Background = Brushes.Green;
                    break;
                case "mw2":
                    btnMw2.Background = Brushes.Green;
                    break;
                default:
                    // Handle the default case if needed
                    break;
            }
            XMLReset();




        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();

        }
        private void tbSearchBar_KeyUp(object sender, KeyEventArgs e)
        {
            XMLSearchBo2(tbSearchBar.Text, CodToSearch);
        }

        private void btnTags_Click(object sender, RoutedEventArgs e)
        {
            AdvancedSettings popup = new AdvancedSettings(CodToSearch);
            popup.ShowDialog();
        }

        private void btnBo2_Click(object sender, RoutedEventArgs e)
        {
            LoadXmlDocument("Bo2DemoArchiveDatabase.xml");
            tbSearchBar.Text = "";
            CodToSearch = "Bo2";
            ToggleBtnColor(CodToSearch);
            XMLSearchBo2("",CodToSearch);
        }

        private void btnCod4_Click(object sender, RoutedEventArgs e)
        {
            LoadXmlDocument("Cod4DemoArchiveDatabase.xml");
            tbSearchBar.Text = "";
            CodToSearch = "Cod4";
            ToggleBtnColor(CodToSearch);
            XMLSearchBo2("", CodToSearch);
        }
        private void btnMW3_Click(object sender, RoutedEventArgs e)
        {
            LoadXmlDocument("Mw3DemoArchiveDatabase.xml");
            tbSearchBar.Text = "";
            CodToSearch = "MW3";
            ToggleBtnColor(CodToSearch);
            XMLSearchBo2("", CodToSearch);
        }
        private void btnBo3_Click(object sender, RoutedEventArgs e)
        {
            LoadXmlDocument("Bo3DemoArchiveDatabase.xml");
            tbSearchBar.Text = "";
            CodToSearch = "Bo3";
            ToggleBtnColor(CodToSearch);
            XMLSearchBo2("", CodToSearch);
        }

        private void btnMw2_Click(object sender, RoutedEventArgs e)
        {
            LoadXmlDocument("Mw2DemoArchiveDatabase.xml");
            tbSearchBar.Text = "";
            CodToSearch = "Mw2";
            ToggleBtnColor(CodToSearch);
            XMLSearchBo2("", CodToSearch);
        }   
        private void LoadXmlDocument(string fileName)
        {
            string xmlFilePath = Path.Combine(Environment.CurrentDirectory, fileName);

            try
            {
                doc = XDocument.Load(xmlFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading XML document: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
            }
            else if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
        }

    }
}
