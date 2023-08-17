using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace DemoArchive
{
    public partial class AdvancedSettings : Window
    {

        public AdvancedSettings(string variable)
        {
            InitializeComponent();
            string CodToQuery = variable;
            string xmlFilePath = "";
            if (CodToQuery == "Bo2")
            {
                xmlFilePath = "Bo2DemoArchiveDatabase.xml";
            }
            else if (CodToQuery == "Bo3")
            {
                xmlFilePath = "Bo3DemoArchiveDatabase.xml";
            }
            else if (CodToQuery == "Cod4")
            {
                xmlFilePath = "Cod4DemoArchiveDatabase.xml";
            }
            else if (CodToQuery == "Mw3")
            {
                xmlFilePath = "Mw3DemoArchiveDatabase.xml";
            }
            else if (CodToQuery == "Mw2")
            {
                xmlFilePath = "Mw2DemoArchiveDatabase.xml";
            }

            if (File.Exists(xmlFilePath))
            {
                string xml = File.ReadAllText(xmlFilePath);
                XDocument doc = XDocument.Parse(xml);
                XElement database = doc.Element("database");

                Dictionary<string, int> mapsTally = new Dictionary<string, int>();
                Dictionary<string, int> tagsTally = new Dictionary<string, int>();
                Dictionary<string, int> weaponsTally = new Dictionary<string, int>();
                Dictionary<string, int> playerTally = new Dictionary<string, int>();
                Dictionary<string, int> attachmentTally = new Dictionary<string, int>();

                foreach (XElement entry in database.Elements("entry"))
                {
                    string maps = entry.Element("map")?.Value;
                    string tags = entry.Element("tags")?.Value;
                    string weapons = entry.Element("weapons")?.Value;
                    string player = entry.Element("player")?.Value;
                    if (CodToQuery == "Bo2" || CodToQuery == "Mw3" || CodToQuery == "Bo3")
                    {
                        string attachment = entry.Element("attachment")?.Value;
                        UpdateTally(attachmentTally, attachment);
                        txtAttachment.Text = "Attachment:";
                    }
                    else if(CodToQuery == "Cod4" || CodToQuery == "Mw2")
                    {
                        string mod = entry.Element("mod")?.Value;
                        UpdateTally(attachmentTally, mod);
                        txtAttachment.Text = "Mod:";
                    }

                    UpdateTally(mapsTally, maps);
                    UpdateTally(tagsTally, tags);
                    UpdateTally(weaponsTally, weapons);
                    UpdateTally(playerTally, player);
                }

                PopulateTallyUI(mapsTally, mapsTallyStackPanel);
                PopulateTallyUI(tagsTally, tagsTallyStackPanel);
                PopulateTallyUI(weaponsTally, weaponsTallyStackPanel);
                PopulateTallyUI(playerTally, playerTallyStackPanel);
                PopulateTallyUI(attachmentTally, attachmentTallyStackPanel);
            }
            else
            {
                Console.WriteLine("XML file not found.");
            }
        }

        private void UpdateTally(Dictionary<string, int> tallyDictionary, string values)
        {
            if (!string.IsNullOrEmpty(values))
            {
                string[] items = values.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string item in items)
                {
                    string trimmedItem = item.Trim(); // Remove extra spaces
                    if (tallyDictionary.ContainsKey(trimmedItem))
                        tallyDictionary[trimmedItem]++;
                    else
                        tallyDictionary[trimmedItem] = 1;
                }
            }
        }

        private void PopulateTallyUI(Dictionary<string, int> tallyDictionary, StackPanel stackPanel)
        {
            var sortedTally = tallyDictionary.OrderByDescending(kvp => kvp.Value);

            foreach (var kvp in sortedTally)
            {
                TextBlock tallyTextBlock = new TextBlock();
                tallyTextBlock.Text = $"{kvp.Key}: {kvp.Value}";
                stackPanel.Children.Add(tallyTextBlock);
            }
        }


        private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
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
