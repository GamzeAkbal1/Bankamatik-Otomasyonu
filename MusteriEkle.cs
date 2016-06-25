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
    public partial class MusteriEkle : Form
    {
        int _type;
        int _id;
        DbOperation _db = new DbOperation();

        /// <summary>
        /// Müsteri Kaydet
        /// </summary>
        public MusteriEkle()
        {
            InitializeComponent();
            MusteriNoOlustur();
            _type = 0;
        }

        /// <summary>
        /// Müşteri Güncelle
        /// </summary>
        /// <param name="musteriId"></param>
        public MusteriEkle(int musteriId)
        {
            InitializeComponent();
            _type = 1;
            _id = musteriId;
            var sql = "select * from Musteriler where id=" + _id;
            var musteri = _db.SelectTable(sql);
            tbMusteriNo.Text = (musteri.Rows[0]).ItemArray[1].ToString();
            tbAd.Text = (musteri.Rows[0]).ItemArray[2].ToString();
            tbSoyad.Text = (musteri.Rows[0]).ItemArray[3].ToString();
            tbAdres.Text = (musteri.Rows[0]).ItemArray[4].ToString();
            tbTelefon.Text = (musteri.Rows[0]).ItemArray[5].ToString();
            tbEmail.Text = (musteri.Rows[0]).ItemArray[6].ToString();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnVazgac_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MusteriEkle_Load(object sender, EventArgs e)
        {
            
        }

        public void MusteriNoOlustur()
        {
            
            var r = new Random();
            
            var musteriNo = r.Next(1000000, 9999999);
            var sql = "select * from Musteriler where musteriNo='"+musteriNo+"'";
            var kontrol = _db.SelectTable(sql);
            
                while (kontrol.Rows.Count!=0)
                {

                musteriNo = r.Next(1000000, 9999999);
                sql = "select * from Musteriler where musteriNo='" + musteriNo + "'";
                kontrol = _db.SelectTable(sql);

            }

            
           
            
            tbMusteriNo.Text = musteriNo.ToString();

        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (BoslukKontrol())
            {
                if (_type == 0)
                {
                    var sql = "insert into Musteriler (musteriNo,ad,soyad,adres,telefon,email)values(" + tbMusteriNo.Text + ",'" + tbAd.Text + "','" + tbSoyad.Text + "','" + tbAdres.Text + "','" + tbTelefon.Text + "','" + tbEmail.Text + "')";
                    var kontrol = _db.runCommand(sql);
                    if (kontrol > 0)
                    {
                        DialogResult = DialogResult.OK;
                    }
                }
                else
                {
                    var sql = "update Musteriler set musteriNo='" + tbMusteriNo.Text + "' ,ad='" + tbAd.Text + "' ,soyad='" + tbSoyad.Text + "' ,adres='" + tbAdres.Text + "' ,telefon='" + tbTelefon.Text + "' ,email='" + tbEmail.Text + "' where id=" + _id;
                    var kontrol = _db.runCommand(sql);
                    if (kontrol > 0)
                    {
                        DialogResult = DialogResult.OK;
                    }
                }
            }
            else
            {
                label7.Text = "Boşlukları Kontrol Ediniz.";
                label7.Visible = true;
            }
        }

        public bool BoslukKontrol()
        {
            if (tbAd.Text != null && tbSoyad.Text != null && tbAdres.Text != null && tbTelefon.Text != null && tbEmail.Text != null)
                return true;
            else
                return false;
        }

        private void tbTelefon_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbTelefon_Leave(object sender, EventArgs e)
        {

        }

        private void tbTelefon_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
