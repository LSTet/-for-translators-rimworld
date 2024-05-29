using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace TrToolRim
{
    public partial class Form1 : Form
    {
        private string currentFilePath;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFolderPath = folderBrowserDialog.SelectedPath;
                LoadFolders(selectedFolderPath);
                LoadFiles(selectedFolderPath);
            }
        }

        private void LoadFolders(string selectedFolderPath)
        {
            checkedListBoxFolders.Items.Clear();
            string[] directories = Directory.GetDirectories(selectedFolderPath, "*", SearchOption.AllDirectories);
            foreach (string dir in directories)
            {
                checkedListBoxFolders.Items.Add(dir);
            }
        }

        private void LoadFiles(string selectedFolderPath)
        {
            listBoxFiles.Items.Clear();
            var ignoredFolders = checkedListBoxFolders.CheckedItems.Cast<string>().ToList();
            string[] xmlFiles = Directory.GetFiles(selectedFolderPath, "*.xml", SearchOption.AllDirectories);

            foreach (string file in xmlFiles)
            {
                bool shouldIgnore = ignoredFolders.Any(ignored => IsSubDirectory(ignored, file));
                if (!shouldIgnore)
                {
                    listBoxFiles.Items.Add(file);
                }
            }
        }

        private bool IsSubDirectory(string parentDir, string childPath)
        {
            var parentUri = new Uri(parentDir.EndsWith("\\") ? parentDir : parentDir + "\\");
            var childUri = new Uri(childPath);
            return parentUri.IsBaseOf(childUri);
        }

        private void listBoxFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxFiles.SelectedItem != null)
            {
                currentFilePath = listBoxFiles.SelectedItem.ToString();
                LoadXmlFile(currentFilePath);
            }
        }

        private void LoadXmlFile(string filePath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();
            dataGridView.Columns.Add("Tag", "Tag");
            dataGridView.Columns.Add("Value", "Value");

            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                if (node.NodeType == XmlNodeType.Element)
                {
                    string tagName = node.Name;
                    string tagValue = node.InnerText;
                    dataGridView.Rows.Add(tagName, tagValue);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentFilePath))
            {
                XmlDocument doc = new XmlDocument();
                XmlElement root = doc.CreateElement("LanguageData");
                doc.AppendChild(root);

                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[1].Value != null)
                    {
                        string tagName = row.Cells[0].Value.ToString();
                        string tagValue = row.Cells[1].Value.ToString();

                        XmlElement element = doc.CreateElement(tagName);
                        element.InnerText = tagValue;
                        root.AppendChild(element);
                    }
                }

                try
                {
                    doc.Save(currentFilePath);
                    MessageBox.Show("File saved successfully.");
                }
                catch (XmlException ex)
                {
                    MessageBox.Show("Error saving XML: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("No file is currently opened.");
            }
        }

        private void btnUpdateFiles_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(folderBrowserDialog.SelectedPath))
            {
                LoadFiles(folderBrowserDialog.SelectedPath);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://discord.gg/Hwen3TjJ7Q");
        }
    }
}
