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
    public partial class KullaniciKayit : Form
    {
        DbOperation _db = new DbOperation();
        bool _kullaniciAdiKontrol = true;
        bool _sifreKontrol = false;
        public KullaniciKayit()
        {
            InitializeComponent();
        }

        private void btnKayit_Click(object sender, EventArgs e)
        {
            if(_kullaniciAdiKontrol==true && _sifreKontrol==true)
            {
                if(textBox1.Text!=null && textBox2.Text!=null)
                {
                    var sql = "insert into Kullanicilar (kullaniciAdi,sifre)values('"+textBox1.Text+"','"+textBox2.Text+"')";
                    var kontrol = _db.runCommand(sql);
                    if(kontrol>0)
                    {
                        DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show("Kayıt Başarısız");
                    }
                }
            }
            else
            {
                MessageBox.Show("Kullanıcı Adını Kontrol Ediniz.");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var sql = "select * from Kullanicilar where kullaniciAdi='"+textBox1.Text+"'";
            var kullaniciAdi = _db.SelectTable(sql);
            if(kullaniciAdi.Rows.Count>0)
            {
                label3.Text = "Kullanıcı Adı Kullanılmaktadır.";
                _kullaniciAdiKontrol = false;
                label3.Visible = true;
            }
            else
            {
                _kullaniciAdiKontrol = true;
                label3.Visible = false;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text!=textBox3.Text)
            {
                label3.Text = "Şifreler Uyuşmuyor.";
                label3.Visible = true;
                _sifreKontrol = false;
            }
            else
            {
                label3.Visible = false;
                _sifreKontrol = true;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
