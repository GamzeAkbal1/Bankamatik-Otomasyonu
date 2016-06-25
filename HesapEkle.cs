using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace oopproject
{
    public partial class HesapEkle : Form
    {
        DbOperation _db = new DbOperation();
        string _musteriId;
        public HesapEkle(string musteriId)
        {
            InitializeComponent();
            _musteriId = musteriId;
            HesapNoOlustur();
            var sql = "select * from Musteriler where id=" + _musteriId;
            var musteriBilgisi = _db.SelectTable(sql);
            textBox2.Text = musteriBilgisi.Rows[0].ItemArray[1].ToString();
            textBox3.Text = musteriBilgisi.Rows[0].ItemArray[2].ToString();
            textBox4.Text = musteriBilgisi.Rows[0].ItemArray[3].ToString();
            textBox6.Text = "0";
            textBox7.Text = "0";
        }

        private void HesapEkle_Load(object sender, EventArgs e)
        {

        }

        public void HesapNoOlustur()
        {

            var r = new Random();

            var hesapNo = r.Next(10000000, 99999999);
            var sql = "select * from Hesaplar where hesapNo='" + hesapNo + "'";
            var kontrol = _db.SelectTable(sql);

            while (kontrol.Rows.Count != 0)
            {
                hesapNo = r.Next(1000000, 9999999);
                sql = "select * from Hesaplar where hesapNo='" + hesapNo + "'";
                kontrol = _db.SelectTable(sql);

            }




            textBox1.Text = hesapNo.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var sql = "insert into Hesaplar (hesapNo,musteriNo,ekHesap,bakiye,kullanilabilirBakiye)values('"+textBox1.Text+"','"+textBox2.Text+"','"+textBox5.Text+"','"+textBox6.Text+"','"+textBox7.Text+"')";
            var kontrol = _db.runCommand(sql);
            if(kontrol>0)
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Kayıt Hatalı");
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            textBox7.Text = textBox5.Text;
        }
    }
}
