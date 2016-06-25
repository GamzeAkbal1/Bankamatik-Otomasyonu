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

namespace oopproject
{
    public partial class Intro : Form
    {
        System.Windows.Forms.Timer tmr;  //progressbar
        public Intro()
        {
            InitializeComponent();

            //for (int i = 0; i < 1000; i++)
            //{
            //    if (i % 10 == 0)
            //        progressBar1.Value++;
            //}
            Thread backgroundThread = new Thread(
           new ThreadStart(() =>
     {
         for (int n = 0; n < 100; n++)
         {
             Thread.Sleep(50);
             progressBar1.BeginInvoke(
                 new Action(() =>
                 {
                     progressBar1.Value = n;
                 }
             ));
         }



     }
    ));
            backgroundThread.Start();


        }

        private void Intro_Load(object sender, EventArgs e)
        {

        }



        private void Intro_Shown(object sender, EventArgs e)
        {
            tmr = new System.Windows.Forms.Timer();
            tmr.Interval = 5000;
            tmr.Start();

            tmr.Tick += tmr_Tick;
        }
        void tmr_Tick(object sender, EventArgs e)
        {
            tmr.Stop();
            var f = new GirisSayfasi();
            f.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
