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
            foreach (var test_item in Directory.EnumerateFiles(this.folder, this.Uid+"_*.pke"))
            {

            }
        }
    }
}
