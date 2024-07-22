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
                CreateLanguageDirectories(selectedFolderPath);
                LoadFolders(selectedFolderPath);
                LoadFiles(selectedFolderPath);
            }
        }

        private void CreateLanguageDirectories(string selectedFolderPath)
        {
            var defsDirs = Directory.GetDirectories(selectedFolderPath, "Defs", SearchOption.AllDirectories);
            foreach (var defsDir in defsDirs)
            {
                string languageDir = Path.Combine(Path.GetDirectoryName(defsDir), "Languages", "Russian", "DefInjected");
                Directory.CreateDirectory(languageDir);

                foreach (var file in Directory.GetFiles(defsDir, "*.xml", SearchOption.AllDirectories))
                {
                    ProcessXmlFile(file, languageDir);
                }
            }
        }

        private void ProcessXmlFile(string filePath, string outputDir)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            bool hasLabelOrDescription = false;

            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                if (node.SelectSingleNode("label") != null || node.SelectSingleNode("description") != null)
                {
                    hasLabelOrDescription = true;
                    break;
                }
            }

            if (hasLabelOrDescription)
            {
                string fileName = Path.GetFileName(filePath);
                string newFilePath = Path.Combine(outputDir, fileName);

                XmlDocument newDoc = new XmlDocument();
                XmlElement rootElement = newDoc.CreateElement("LanguageData");
                newDoc.AppendChild(rootElement);

                foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                {
                    string defName = node["defName"]?.InnerText;
                    if (defName != null)
                    {
                        XmlNode labelNode = node.SelectSingleNode("label");
                        XmlNode descriptionNode = node.SelectSingleNode("description");

                        if (labelNode != null)
                        {
                            XmlElement labelElement = newDoc.CreateElement($"{defName}.label");
                            labelElement.InnerText = labelNode.InnerText;
                            rootElement.AppendChild(labelElement);
                        }

                        if (descriptionNode != null)
                        {
                            XmlElement descriptionElement = newDoc.CreateElement($"{defName}.description");
                            descriptionElement.InnerText = descriptionNode.InnerText;
                            rootElement.AppendChild(descriptionElement);
                        }
                    }
                }

                newDoc.Save(newFilePath);
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
                if (!ignoredFolders.Any(ignored => file.StartsWith(ignored)))
                {
                    listBoxFiles.Items.Add(file);
                }
            }
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

            if (dataGridView == null)
                throw new NullReferenceException("dataGridView is not initialized.");

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
