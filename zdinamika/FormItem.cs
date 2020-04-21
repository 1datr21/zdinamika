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
        public FormItem(string Uid, string folder)
        {
            InitializeComponent();
            this.Uid = Uid;
            this.folder = folder;
        }

        private void FormItem_Load(object sender, EventArgs e)
        {
            int mode = 0;
            foreach (var pke_file in Directory.EnumerateFiles(this.folder, this.Uid+"_*.pke"))
            {
                XmlTextReader reader = new XmlTextReader(pke_file);

                if (mode == 0)
                {
                    this.DetectMode(reader, ref mode);
                }
                else
                {
                    reader.ResetState();
                    switch(mode)
                    {
                        case 1: {
                                // UA, IA, PA, QA, SA, Freq, sigmaUy
                            }
                            break;

                        case 2: {

                            } break;
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
