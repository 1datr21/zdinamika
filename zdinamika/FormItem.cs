using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace zdinamika
{
    public partial class FormItem : Form
    {
        private string Uid;
        private string folder;
        private List<List<string>> tparams;
        public FormItem(string Uid, string folder)
        {
            InitializeComponent();
            this.Uid = Uid;
            this.folder = folder;
            this.tparams = new List<List<string>>() // параметры вывода по схемам проверки
                {
                new List<string>(){ "UA", "IA", "PA", "QA", "SA", "Freq", "sigmaUy" },
                new List<string>(){ "UAB", "UBC", "UCA", "IAB", "IBC", "ICA", "IA", "IB", "IC", "PO", "PP", "QO", "QP", "SO", "SP", "UO", "UP", "IO", "IP", "KO", "Freq", "sigmaUy", "sigmaUyAB", "sigmaUyBC", "sigmaUyCA"}
                }
            ;
        }

        private void FormItem_Load(object sender, EventArgs e)
        {
            this.Text = "Выборка по "+this.Uid;

            this.build_table();
        }

        private void build_table()
        {
            int mode = 0;
            string _uid;
            foreach (var pke_file in Directory.EnumerateFiles(this.folder, this.Uid + "_*.pke"))// проходим по файлам с таким Uid из директории
            {
                string[] words = Path.GetFileName(pke_file).Split(new char[] { '_' });

                _uid = "";
                if (words.Length > 0)
                {
                    _uid = words[0];
                }
                XmlTextReader reader = new XmlTextReader(pke_file);

                if (mode == 0)
                {
                    this.DetectMode(reader, ref mode);
                    this.buildDGVColumns(mode); // создаем колонки согласно параметрам
                }
                else
                {
                    reader.ResetState();
                    this.SelectRows(reader, mode, _uid);
                }

                reader.Close();// закрыть ридер
            }
        }

        private void buildDGVColumns(int mode)
        {
            if (mode <= 2)
            {
                this.dgvResults.Rows.Clear();
                this.dgvResults.Columns.Clear();

                this.dgvResults.Columns.Add("DateTime", "Дата / время");
                foreach (string prop in tparams[mode - 1])
                {
                    this.dgvResults.Columns.Add(prop, prop);
                }
            }
        }

        private void SelectRows(XmlTextReader reader,int mode, string _uid)
        {
            if (mode <= 2)
            {
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element: // Узел является элементом.
                            if (reader.Name == "Param_Check_PKE")
                            {

                            }
                            if (reader.Name == "Result_Check_PKE")
                            {
                                //scheme = reader.GetAttribute("pke_cxema");
                                List<string> vals = new List<string>();
                                vals.Add(_uid);
                                foreach (string prop in tparams[mode - 1])
                                {
                                    string propval = reader.GetAttribute(prop);
                                    vals.Add(propval);
                                }
                                dgvResults.Rows.Add(vals.ToArray());
                            }
                            break;
                        case XmlNodeType.Text: // Вывести текст в каждом элементе.
                                       
                            break;
                        case XmlNodeType.EndElement: // Вывести конец элемента.

                            break;
                    }
                }
            }
        }

        private void DetectMode(XmlTextReader reader, ref int mode)
        {
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // Узел является элементом.
                                              // Console.Write("<" + reader.Name);
                                              //  Console.WriteLine(">");
                        if (reader.Name == "Param_Check_PKE")
                        {
                            
                            mode = Convert.ToInt16(reader.GetAttribute("active_cxema"));
                            return;
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
        }
    }
}
