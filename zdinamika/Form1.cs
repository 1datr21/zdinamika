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
using System.Xml;

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
                try
                {
                    if (!Directory.Exists(f_xml_folder))
                    {
                        throw new Exception("Такой папки нет. Выберите другую папку.");                       
                    }
                    var subdirs = Directory.EnumerateDirectories(f_xml_folder);
                    foreach (var test_object in subdirs)
                    {
                        foreach (var start_obj in Directory.EnumerateDirectories(test_object))
                        {
                            foreach (var test_item in Directory.EnumerateFiles(start_obj, "*.pke"))
                            {
                                string end_date = "";
                                string scheme = "";
                                this.GetPKEInfo(test_item, ref end_date, ref scheme);
                                dgvTests.Rows.Add(new string[] {
                                    Path.GetFileName(test_object),
                                    Path.GetFileName(start_obj),
                                    end_date
                                });
                            }
                        }
                        // newrow.TObject = test_object;
                        //Console.WriteLine(item.Name);
                    }
                }
                catch(System.Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
        }

        private void GetPKEInfo(string pke_file,ref string end_date, ref string scheme)
        {
            XmlTextReader reader = new XmlTextReader(pke_file);
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // Узел является элементом.
                       // Console.Write("<" + reader.Name);
                      //  Console.WriteLine(">");
                        if(reader.Name == "Param_Check_PKE")
                        {
                             end_date = reader.GetAttribute("TimeStop");
                        }
                        break;
                    case XmlNodeType.Text: // Вывести текст в каждом элементе.
                     //   Console.WriteLine(reader.Value);
                        break;
                    case XmlNodeType.EndElement: // Вывести конец элемента.
                     //   Console.Write("</" + reader.Name);
                      //  Console.WriteLine(">");
                        break;
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
