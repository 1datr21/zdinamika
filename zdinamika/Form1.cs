using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Collections.Specialized;
using System.IO;

namespace zdinamika
{
    public partial class Form1 : Form
    {
        string f_xml_folder;
        string XmlFolder
        {
            get { return f_xml_folder; }
            set
            {
                f_xml_folder = value;
                tbFolder.Text = value;

            //    DirectoryInfo dir = new DirectoryInfo(f_xml_folder);
                var subdirs = Directory.EnumerateDirectories(f_xml_folder);
                foreach(var test_object in subdirs)
                {
                    //var newrow =
                    dgvTests.Rows.Add(new string[] { Path.GetFileName(test_object) });
                   // newrow.TObject = test_object;
                    //Console.WriteLine(item.Name);
                }
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult res = fbd.ShowDialog();
            if (res != null)
            {
                this.XmlFolder = fbd.SelectedPath;
                System.Configuration.Configuration currentConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                // добавляем позицию в раздел AppSettings
                currentConfig.AppSettings.Settings.Add("folder", fbd.SelectedPath);
                //сохраняем
                currentConfig.Save(ConfigurationSaveMode.Full);
                //принудительно перезагружаем соотвествующую секцию
                ConfigurationManager.RefreshSection("appSettings");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string fldr = ConfigurationManager.AppSettings.Get("folder");
            if (fldr != null)
            {
                this.XmlFolder = fldr;
            }
        }
    }
}
