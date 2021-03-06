﻿using System;
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
                this.UpdateTable();
            }
        }

        private void UpdateTable()
        {
            try
            {
                if (!Directory.Exists(f_xml_folder))
                {
                    throw new Exception("Такой папки нет. Выберите другую папку.");
                }
                var subdirs = Directory.EnumerateDirectories(f_xml_folder);
                dgvTests.Rows.Clear();
                foreach (var test_object in subdirs)
                {
                    foreach (var start_obj in Directory.EnumerateDirectories(test_object))
                    {
                        foreach (var test_item in Directory.EnumerateFiles(start_obj, "*.pke"))
                        {
                            DateTime end_date = new DateTime(); 
                            string scheme = "";
                            string avg_time = "";
                            string uid = "";
                            this.GetPKEInfo(test_item, 
                                ref end_date, 
                                ref scheme,
                                ref avg_time,
                                ref uid);
                            DateTime start_date = DateTime.ParseExact(Path.GetFileName(start_obj), "dd.MM.yyyy HH.mm", System.Globalization.CultureInfo.InvariantCulture);
                            dgvTests.Rows.Add(new object[] {
                                    Path.GetFileName(test_object),
                                    start_date.ToString("dd'/'mm'/'yyyy hh:mm"),
                                    end_date.ToString("dd'/'mm'/'yyyy hh:mm"),
                                    scheme,
                                    avg_time,
                                    uid,
                                    start_obj
                                });
                        }
                    }
                }
            }
            catch (System.Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void GetPKEInfo(string pke_file,ref DateTime end_date, ref string scheme, ref string avg_time, ref string uid)
        {
            string[] words = Path.GetFileName(pke_file).Split(new char[] { '_' });
            if(words.Length>0)
            {
                uid = words[0];
            }
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
                            end_date = Utils.NumStr2DT(reader.GetAttribute("TimeStop"));
                            avg_time = reader.GetAttribute("averaging_interval_time");
                            scheme = reader.GetAttribute("active_cxema");
                        }
                        if (reader.Name == "Result_Check_PKE")
                        {
                            //scheme = reader.GetAttribute("pke_cxema");
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
            reader.Close();
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult res = fbd.ShowDialog();
            if (res == DialogResult.OK )
            {
                this.XmlFolder = fbd.SelectedPath;
                System.Configuration.Configuration currentConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                // добавляем позицию в раздел AppSetting
                AddUpdateAppSettings("folder", fbd.SelectedPath);
                //сохраняем
                currentConfig.Save(ConfigurationSaveMode.Full);
                //принудительно перезагружаем соотвествующую секцию
                ConfigurationManager.RefreshSection("appSettings");
            }
        }

        static void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            UpdateTable();
        }

        // открыть по uid
        private void dgvTests_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var curr_row = this.dgvTests.Rows[this.dgvTests.CurrentRow.Index];
            string uid = curr_row.Cells[5].Value.ToString();
            string fldr = curr_row.Cells[6].Value.ToString();
            var fi = new FormItem(uid,fldr);
            fi.Show();
        }
    }
}
