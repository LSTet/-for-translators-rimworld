using System;
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

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog == null)
                openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                currentFilePath = openFileDialog.FileName;
                XmlDocument doc = new XmlDocument();
                doc.Load(currentFilePath);

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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://discord.gg/Hwen3TjJ7Q");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
