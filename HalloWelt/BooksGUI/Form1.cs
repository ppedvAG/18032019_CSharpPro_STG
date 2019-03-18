using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BooksGUI
{
    public partial class Form1 : Form
    {
        int clicks = 0;
        public Form1()
        {
            InitializeComponent();
            //this.MouseUp += (s, e) => MessageBox.Show(e.Button.ToString());




            button1.Click += (s, e) => { if (clicks++ % 3 == 0) Feierabend?.Invoke(DateTime.UtcNow); };
            button1.Click += Button1_Click;
            //this.Feierabend += MachZeug;
            //this.Feierabend += MachZeug;
            this.Feierabend -= MachZeug;
            this.Feierabend += MachZeug;
            this.Feierabend += dt => MessageBox.Show("Test");
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (sender is Button b)
                b.BackColor = Color.White;
        }

        private void MachZeug(DateTime obj)
        {
            MessageBox.Show("Test");
        }

        public event Action<DateTime> Feierabend;

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = HalloWelt.Program.LoadBooks().items.Select(x => x.volumeInfo).ToList();
        }
    }
}
