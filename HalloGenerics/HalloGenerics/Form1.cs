using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HalloGenerics
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var dd = new Dinge<Button, int, DateTime>();
            dd.GetDing();

            Type t = dd.GetType();


        }


        class Dinge<T, R, U> where T : ButtonBase
        {
            private T ding;

            public T GetDing()
            {
                return ding;
            }

            public void SetDing(T obj)
            {
                this.ding = obj;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Task.Run(()=>            {
                for (int i = 0; i < 1000; i++)
                {
                    new Thread(new ThreadStart(delegate () { Thread.Sleep(300000); })).Start();
                }
            });
        }
    }
}
