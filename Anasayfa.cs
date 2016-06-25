using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace oopproject
{
    public partial class AnaSayfa : Form
    {
        ArrayList veri = new ArrayList();
        Thread th1;
        bool _admin;
        DbOperation _db = new DbOperation();  
        bool _first = true;
        bool _gonderen = false;
        bool _alan = false;
        string _kullaniciId;
        public AnaSayfa(string kullaniciId)
        {
            InitializeComponent();
            _kullaniciId = kullaniciId;
        }

        private void AnaSayfa_Load(object sender, EventArgs e)
        {
            progressBar2.Minimum = 0;
            progressBar2.Maximum = 100;
            progressBar2.Style = ProgressBarStyle.Continuous;

            CheckForIllegalCrossThreadCalls = false;
            var sql = "select * from Musteriler";
            var musteriler = _db.SelectTable(sql);
            dataGridView1.DataSource = musteriler;
            dataGridView1.Columns[0].Visible = false;
            var sql2 = "select * from Musteriler";
            var musteriler2 = _db.SelectTable(sql2);
            dataGridView2.DataSource = musteriler2;
            dataGridView2.Columns[0].Visible = false;
            dataGridView2.Columns[4].Visible = false;
            dataGridView2.Columns[5].Visible = false;
            dataGridView2.Columns[6].Visible = false;
            var sql3 = "select id, ad + ' ' + soyad as isim from Musteriler";
            var musteriBilgisi = _db.SelectTable(sql3);
            comboBox1.DataSource = musteriBilgisi;
            comboBox1.ValueMember = "id";
            comboBox1.DisplayMember = "isim";
            var sql4 = "select id, ad + ' ' + soyad as isim from Musteriler";
            var musteriBilgisi2 = _db.SelectTable(sql4);
            comboBox4.DataSource = musteriBilgisi2;
            comboBox4.ValueMember = "id";
            comboBox4.DisplayMember = "isim";
            var sql5 = "select * from Kullanicilar where id!=" + _kullaniciId;
            var kullanicilar = _db.SelectTable(sql5);
            dataGridView6.DataSource = kullanicilar;
            dataGridView6.Columns[0].Visible = false;
            dataGridView6.Columns[2].Visible = false;

            _first = false;

        }

      

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var sql = "select * from Musteriler where Ad like '%" + textBox1.Text + "%' or Soyad like '%" + textBox1.Text + "%'";
            var musteriler = _db.SelectTable(sql);
            dataGridView1.DataSource = musteriler;
            dataGridView1.Columns[0].Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var f = new MusteriEkle();
           
            var kontrol = f.ShowDialog();
            if (kontrol == DialogResult.OK)
            {
                var sql = "select * from Musteriler";
                var musteriler = _db.SelectTable(sql);
                dataGridView1.DataSource = musteriler;
                dataGridView1.Columns[0].Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {if (dataGridView1.CurrentRow != null)
            {
                var musteriId = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                var f = new MusteriEkle(int.Parse(musteriId));
                var kontrol = f.ShowDialog();
                if (kontrol == DialogResult.OK)
                {
                    var sql = "select * from Musteriler";
                    var musteriler = _db.SelectTable(sql);
                    dataGridView1.DataSource = musteriler;
                    dataGridView1.Columns[0].Visible = false;
                }
            }
            else
                MessageBox.Show("Muşteri seçiniz!!");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var musteriId = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            var sql = "delete from Musteriler where id = " + musteriId;
            var kontrol = _db.runCommand(sql);
            if (kontrol > 0)
            {
                var sql2 = "select * from Musteriler";
                var musteriler = _db.SelectTable(sql2);
                dataGridView1.DataSource = musteriler;
                dataGridView1.Columns[0].Visible = false;
            }
        }

        private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            var sql2 = "select * from Musteriler where Ad like '%" + textBox2.Text + "%' or Soyad like '%" + textBox2.Text + "%'";
            var musteriler2 = _db.SelectTable(sql2);
            dataGridView2.DataSource = musteriler2;
            dataGridView2.Columns[0].Visible = false;
            dataGridView2.Columns[4].Visible = false;
            dataGridView2.Columns[5].Visible = false;
            dataGridView2.Columns[6].Visible = false;
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView2_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            _first = true;
            var musteriNo = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            var sql = "select * from Hesaplar where musteriNo='" + musteriNo + "'";
            var hesapListesi = _db.SelectTable(sql);
            listBox1.DataSource = hesapListesi;
            listBox1.DisplayMember = "hesapNo";
            listBox1.ValueMember = "id";
            _first = false;

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (_first == false)
            {
                var sql = "select * from Hesaplar where id=" + listBox1.SelectedValue;
                var hesapBilgisi = _db.SelectTable(sql);
                var sql2 = "select * from Musteriler where musteriNo='" + hesapBilgisi.Rows[0].ItemArray[2].ToString() + "'";
                var musteriBilgisi = _db.SelectTable(sql2);
                textBox3.Text = hesapBilgisi.Rows[0].ItemArray[1].ToString();
                textBox4.Text = musteriBilgisi.Rows[0].ItemArray[1].ToString();
                textBox5.Text = musteriBilgisi.Rows[0].ItemArray[2].ToString();
                textBox6.Text = musteriBilgisi.Rows[0].ItemArray[3].ToString();
                textBox7.Text = hesapBilgisi.Rows[0].ItemArray[3].ToString();
                textBox8.Text = hesapBilgisi.Rows[0].ItemArray[4].ToString();
                textBox9.Text = hesapBilgisi.Rows[0].ItemArray[5].ToString();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var f = new HesapEkle(dataGridView2.CurrentRow.Cells[0].Value.ToString());
            var kontrol = f.ShowDialog();
            if (kontrol == DialogResult.OK)
            {
                _first = true;
                var musteriNo = dataGridView2.CurrentRow.Cells[1].Value.ToString();
                var sql = "select * from Hesaplar where musteriNo='" + musteriNo + "'";
                var hesapListesi = _db.SelectTable(sql);
                listBox1.DataSource = hesapListesi;
                listBox1.DisplayMember = "hesapNo";
                listBox1.ValueMember = "id";
                _first = false;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_first == false)
            {
                var sql = "select * from Musteriler where id = " + comboBox1.SelectedValue;
                var musteriBilgisi = _db.SelectTable(sql);
                textBox16.Text = musteriBilgisi.Rows[0].ItemArray[1].ToString();
                textBox15.Text = musteriBilgisi.Rows[0].ItemArray[2].ToString();
                textBox14.Text = musteriBilgisi.Rows[0].ItemArray[3].ToString();
                _first = true;
                var sql2 = "select * from Hesaplar where musteriNo=" + musteriBilgisi.Rows[0].ItemArray[1].ToString();
                var hesapBilgisi = _db.SelectTable(sql2);
                if (hesapBilgisi.Rows.Count > 0)
                {
                    comboBox2.DataSource = hesapBilgisi;
                    comboBox2.DisplayMember = "hesapNo";
                    comboBox2.ValueMember = "id";
                }
                else
                {
                    comboBox2.DataSource = null;
                }
                _first = false;

            }
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            var sql = "select id, ad + ' ' + soyad as isim from Musteriler where Ad like '%" + textBox10.Text + "%' or Soyad like '%" + textBox10.Text + "%'";
            var musteriBilgisi = _db.SelectTable(sql);
            comboBox1.DataSource = musteriBilgisi;
            comboBox1.ValueMember = "id";
            comboBox1.DisplayMember = "isim";
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_first == false)
            {
                var sql = "select * from Hesaplar where id=" + comboBox2.SelectedValue;
                var hesapBilgisi = _db.SelectTable(sql);
                textBox17.Text = hesapBilgisi.Rows[0].ItemArray[1].ToString();
                textBox13.Text = hesapBilgisi.Rows[0].ItemArray[3].ToString();
                textBox12.Text = hesapBilgisi.Rows[0].ItemArray[4].ToString();
                textBox11.Text = hesapBilgisi.Rows[0].ItemArray[5].ToString();
                textBox18.Enabled = true;
            }

        }

        public int IslemNoOlustur()
        {

            var r = new Random();  //random hesap numarası oluşturur

            var islemNo = r.Next(10000, 99999);
            var sql = "select * from Islemler where islemNo='" + islemNo + "'";
            var kontrol = _db.SelectTable(sql);

            while (kontrol.Rows.Count != 0)
            {
                islemNo = r.Next(10000, 99999);
                sql = "select * from Islemler where islemNo='" + islemNo + "'";
                kontrol = _db.SelectTable(sql);

            }


            return islemNo;



        }
        private void button6_Click(object sender, EventArgs e)
        {
            float yatirilacak = float.Parse(textBox18.Text);
            float bakiye = float.Parse(textBox12.Text);
            float ekHesap = float.Parse(textBox13.Text);
            float kullanilabilir = float.Parse(textBox11.Text);
            float yeniBakiye = bakiye + yatirilacak;
            kullanilabilir = yeniBakiye + ekHesap;
            var sql = "update Hesaplar set bakiye='" + yeniBakiye + "' , kullanilabilirBakiye='" + kullanilabilir + "' where id=" + comboBox2.SelectedValue;
            var kontrol = _db.runCommand(sql);
            if (kontrol > 0)
            {

                var islemNo = IslemNoOlustur();
                var sql3 = "insert into Islemler (islemNo,hesapNo,islemTuru,miktar,tarih)values('" + islemNo + "','" + textBox17.Text + "',1,'" + yatirilacak + "',GETDATE())";
                var kontrol2 = _db.runCommand(sql3);
                if (kontrol2 > 0)
                {
                    var sql2 = "select * from Hesaplar where id=" + comboBox2.SelectedValue;
                    var hesapBilgisi = _db.SelectTable(sql2);
                    textBox17.Text = hesapBilgisi.Rows[0].ItemArray[1].ToString();
                    textBox13.Text = hesapBilgisi.Rows[0].ItemArray[3].ToString();
                    textBox12.Text = hesapBilgisi.Rows[0].ItemArray[4].ToString();
                    textBox11.Text = hesapBilgisi.Rows[0].ItemArray[5].ToString();
                    textBox18.Clear();
                }

            }
        }

        private void textBox27_TextChanged(object sender, EventArgs e)
        {
            var sql = "select id, ad + ' ' + soyad as isim from Musteriler where Ad like '%" + textBox27.Text + "%' or Soyad like '%" + textBox27.Text + "%'";
            var musteriBilgisi = _db.SelectTable(sql);
            comboBox4.DataSource = musteriBilgisi;
            comboBox4.ValueMember = "id";
            comboBox4.DisplayMember = "isim";
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_first == false)
            {
                var sql = "select * from Musteriler where id = " + comboBox4.SelectedValue;
                var musteriBilgisi = _db.SelectTable(sql);
                textBox25.Text = musteriBilgisi.Rows[0].ItemArray[1].ToString();
                textBox24.Text = musteriBilgisi.Rows[0].ItemArray[2].ToString();
                textBox23.Text = musteriBilgisi.Rows[0].ItemArray[3].ToString();
                _first = true;
                var sql2 = "select * from Hesaplar where musteriNo=" + musteriBilgisi.Rows[0].ItemArray[1].ToString();
                var hesapBilgisi = _db.SelectTable(sql2);
                if (hesapBilgisi.Rows.Count > 0)
                {
                    comboBox3.DataSource = hesapBilgisi;
                    comboBox3.DisplayMember = "hesapNo";
                    comboBox3.ValueMember = "id";
                }
                else
                {
                    comboBox3.DataSource = null;
                }
                _first = false;

            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_first == false)
            {
                var sql = "select * from Hesaplar where id=" + comboBox3.SelectedValue;
                var hesapBilgisi = _db.SelectTable(sql);
                textBox26.Text = hesapBilgisi.Rows[0].ItemArray[1].ToString();
                textBox22.Text = hesapBilgisi.Rows[0].ItemArray[3].ToString();
                textBox21.Text = hesapBilgisi.Rows[0].ItemArray[4].ToString();
                textBox20.Text = hesapBilgisi.Rows[0].ItemArray[5].ToString();
                textBox19.Enabled = true;
            }
        }

        public float CekilenParaKontrol()
        {
            var sql = "select * from Islemler where hesapNo='" + textBox26.Text + "' and islemTuru=2 and tarih=Convert(date,GETDATE())";
            var cekilenler = _db.SelectTable(sql);
            float miktar = 0;
            for (int i = 0; i < cekilenler.Rows.Count; i++)
            {
                miktar = miktar + float.Parse(cekilenler.Rows[i].ItemArray[4].ToString());
            }
            return miktar;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var miktar = CekilenParaKontrol();
            
            float cekilecek = float.Parse(textBox19.Text);
            if (cekilecek + miktar <= 750)
            {
                float bakiye = float.Parse(textBox21.Text);
                float ekHesap = float.Parse(textBox22.Text);
                float kullanilabilir = float.Parse(textBox20.Text);
                if (kullanilabilir >= cekilecek)
                {
                    float yeniBakiye = bakiye - cekilecek;
                    kullanilabilir = kullanilabilir - cekilecek;
                    if (yeniBakiye < 0)
                    {
                        yeniBakiye = 0;
                    }
                    var sql = "update Hesaplar set bakiye='" + yeniBakiye + "' , kullanilabilirBakiye='" + kullanilabilir + "' where id=" + comboBox3.SelectedValue;
                    var kontrol = _db.runCommand(sql);
                    if (kontrol > 0)
                    {

                        var islemNo = IslemNoOlustur();
                        var sql3 = "insert into Islemler (islemNo,hesapNo,islemTuru,miktar,tarih)values('" + islemNo + "','" + textBox26.Text + "',2,'" + cekilecek + "',GETDATE())";
                        var kontrol2 = _db.runCommand(sql3);
                        if (kontrol2 > 0)
                        {
                            var sql2 = "select * from Hesaplar where id=" + comboBox3.SelectedValue;
                            var hesapBilgisi = _db.SelectTable(sql2);
                            textBox26.Text = hesapBilgisi.Rows[0].ItemArray[1].ToString();
                            textBox22.Text = hesapBilgisi.Rows[0].ItemArray[3].ToString();
                            textBox21.Text = hesapBilgisi.Rows[0].ItemArray[4].ToString();
                            textBox20.Text = hesapBilgisi.Rows[0].ItemArray[5].ToString();
                            textBox19.Clear();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Günlük Limit Dolmuştur!!");
            }
        }

        private void textBox34_TextChanged(object sender, EventArgs e)
        {
            var sql = "select * from Hesaplar where hesapNo='"+textBox34.Text+"'";
            var hesapBilgisi = _db.SelectTable(sql);
            if(hesapBilgisi.Rows.Count>0)
            {
                var sql2 = "select * from Musteriler where musteriNo='" + hesapBilgisi.Rows[0].ItemArray[2].ToString() + "'";
                var musteriBilgisi = _db.SelectTable(sql2);
               
                textBox33.Text = musteriBilgisi.Rows[0].ItemArray[1].ToString();
                textBox32.Text = musteriBilgisi.Rows[0].ItemArray[2].ToString();
                textBox31.Text = musteriBilgisi.Rows[0].ItemArray[3].ToString();
                textBox30.Text = hesapBilgisi.Rows[0].ItemArray[3].ToString();
                textBox29.Text = hesapBilgisi.Rows[0].ItemArray[4].ToString();
                textBox28.Text = hesapBilgisi.Rows[0].ItemArray[5].ToString();
                _gonderen = true;
                var havele = HavaleKontrol();
                if (havele)
                    textBox42.Enabled = true;
                else
                    textBox42.Enabled = false;
            }
            else
            {
                textBox33.Clear();
                textBox32.Clear();
                textBox31.Clear();
                textBox30.Clear();
                textBox29.Clear();
                textBox28.Clear();
                _gonderen = false;
            }
        }

        private void textBox41_TextChanged(object sender, EventArgs e)
        {
            if (textBox34.Text != textBox41.Text)
            {
                var sql = "select * from Hesaplar where hesapNo='" + textBox41.Text + "'";
                var hesapBilgisi = _db.SelectTable(sql);
                if (hesapBilgisi.Rows.Count > 0)
                {
                    var sql2 = "select * from Musteriler where musteriNo='" + hesapBilgisi.Rows[0].ItemArray[2].ToString() + "'";
                    var musteriBilgisi = _db.SelectTable(sql2);

                    textBox40.Text = musteriBilgisi.Rows[0].ItemArray[1].ToString();
                    textBox39.Text = musteriBilgisi.Rows[0].ItemArray[2].ToString();
                    textBox38.Text = musteriBilgisi.Rows[0].ItemArray[3].ToString();
                    textBox37.Text = hesapBilgisi.Rows[0].ItemArray[3].ToString();
                    textBox36.Text = hesapBilgisi.Rows[0].ItemArray[4].ToString();
                    textBox35.Text = hesapBilgisi.Rows[0].ItemArray[5].ToString();
                    _alan = true;
                    var havele = HavaleKontrol();
                    if (havele)
                        textBox42.Enabled = true;
                    else
                        textBox42.Enabled = false;


                }
                else
                {
                    textBox40.Clear();
                    textBox39.Clear();
                    textBox38.Clear();
                    textBox37.Clear();
                    textBox36.Clear();
                    textBox35.Clear();
                    _alan = false;
                }
            }
            else
            {
                textBox34.Clear();
                MessageBox.Show("Gönderen Hesap NO ile Alan Hesap No Aynı Olamaz.");
            }
        }
        public bool HavaleKontrol()
        {
            if (_gonderen == true && _alan == true)
                return true;
            else
                return false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var sql1 = "select * from Hesaplar where hesapNo='"+textBox34.Text+"'";
            var gonderen = _db.SelectTable(sql1);
            var sql2 = "select * from Hesaplar where hesapNo='" +textBox41.Text+ "'";
            var alan = _db.SelectTable(sql2);
            var miktar = float.Parse(textBox42.Text);
            if(float.Parse(gonderen.Rows[0].ItemArray[5].ToString())-miktar>0)
            {
                var alanEkHesap = float.Parse(alan.Rows[0].ItemArray[3].ToString());
                var alanBakiye = float.Parse(alan.Rows[0].ItemArray[4].ToString());
                var alanYeniBakiye = alanBakiye + miktar;
                var alanYeniKullanilabilir = alanEkHesap + alanYeniBakiye;
                var sql3 = "update Hesaplar set bakiye='"+alanYeniBakiye+"', kullanilabilirBakiye='"+alanYeniKullanilabilir+"' where id="+alan.Rows[0].ItemArray[0].ToString();
                _db.runCommand(sql3);
                var gonderenKullanilarbilir = float.Parse(gonderen.Rows[0].ItemArray[5].ToString());
                var gonderenBakiye = float.Parse(gonderen.Rows[0].ItemArray[4].ToString());
                if(gonderenBakiye-miktar<0)
                {
                    gonderenBakiye = 0;
                }
                else
                {
                    gonderenBakiye = gonderenBakiye - miktar;
                }
                gonderenKullanilarbilir = gonderenKullanilarbilir - miktar;
                var sql4= "update Hesaplar set bakiye='" + gonderenBakiye + "', kullanilabilirBakiye='" + gonderenKullanilarbilir + "' where id=" + gonderen.Rows[0].ItemArray[0].ToString();
                _db.runCommand(sql4);
                var islemNo = IslemNoOlustur();
                var sql5 = "insert into Islemler (islemNo,hesapNo,islemTuru,miktar,tarih)values('"+islemNo+"','"+gonderen.Rows[0].ItemArray[1].ToString()+"',3,'"+miktar+"',GETDATE())";
                _db.runCommand(sql5);
                var sql6 = "insert into Havaleler (islemNo,gonderenHesapNo,alanHesapNo,tarih)values('"+islemNo+"','"+ gonderen.Rows[0].ItemArray[1].ToString() + "','"+ alan.Rows[0].ItemArray[1].ToString() + "',GETDATE())";
               var kontrol = _db.runCommand(sql6);
                if(kontrol>0)
                {
                    var sql7 = "select * from Hesaplar where hesapNo='" + textBox41.Text + "'";
                    var hesapBilgisi1 = _db.SelectTable(sql7);
                    if (hesapBilgisi1.Rows.Count > 0)
                    {
                        var sql8 = "select * from Musteriler where musteriNo='" + hesapBilgisi1.Rows[0].ItemArray[2].ToString() + "'";
                        var musteriBilgisi1 = _db.SelectTable(sql8);

                        textBox40.Text = musteriBilgisi1.Rows[0].ItemArray[1].ToString();
                        textBox39.Text = musteriBilgisi1.Rows[0].ItemArray[2].ToString();
                        textBox38.Text = musteriBilgisi1.Rows[0].ItemArray[3].ToString();
                        textBox37.Text = hesapBilgisi1.Rows[0].ItemArray[3].ToString();
                        textBox36.Text = hesapBilgisi1.Rows[0].ItemArray[4].ToString();
                        textBox35.Text = hesapBilgisi1.Rows[0].ItemArray[5].ToString();
                        _alan = true;
                        var havele = HavaleKontrol();
                        if (havele)
                            textBox42.Enabled = true;
                        else
                            textBox42.Enabled = false;


                    }
                    else
                    {
                        textBox40.Clear();
                        textBox39.Clear();
                        textBox38.Clear();
                        textBox37.Clear();
                        textBox36.Clear();
                        textBox35.Clear();
                        _alan = false;
                    }
                    var sql9 = "select * from Hesaplar where hesapNo='" + textBox34.Text + "'";
                    var hesapBilgisi = _db.SelectTable(sql9);
                    if (hesapBilgisi.Rows.Count > 0)
                    {
                        var sql10 = "select * from Musteriler where musteriNo='" + hesapBilgisi.Rows[0].ItemArray[2].ToString() + "'";
                        var musteriBilgisi = _db.SelectTable(sql10);

                        textBox33.Text = musteriBilgisi.Rows[0].ItemArray[1].ToString();
                        textBox32.Text = musteriBilgisi.Rows[0].ItemArray[2].ToString();
                        textBox31.Text = musteriBilgisi.Rows[0].ItemArray[3].ToString();
                        textBox30.Text = hesapBilgisi.Rows[0].ItemArray[3].ToString();
                        textBox29.Text = hesapBilgisi.Rows[0].ItemArray[4].ToString();
                        textBox28.Text = hesapBilgisi.Rows[0].ItemArray[5].ToString();
                        _gonderen = true;
                        var havele = HavaleKontrol();
                        if (havele)
                            textBox42.Enabled = true;
                        else
                            textBox42.Enabled = false;
                    }
                    else
                    {
                        textBox33.Clear();
                        textBox32.Clear();
                        textBox31.Clear();
                        textBox30.Clear();
                        textBox29.Clear();
                        textBox28.Clear();
                        _gonderen = false;
                    }

                }
            }
            else
            {
                MessageBox.Show("Hesapta Yeterli Miktar Bulunmamaktadır.");
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

            var date = dateTimePicker1.Value;
            var dt = date.ToString("yyyy-MM-dd");

            var sql = "select * from Islemler where islemTuru=1 and tarih='"+dt+"'";
            var gelenGunluk = _db.SelectTable(sql);
            var sql2 = "select * from Islemler where islemTuru=2 and tarih='" + dt + "'";
            var gidenGunluk = _db.SelectTable(sql2);
            var sql3 = "select * from Islemler where islemTuru=1";
            var gelenToplam = _db.SelectTable(sql3);
            var sql4 = "select * from Islemler where islemTuru=2";
            var gidenToplam = _db.SelectTable(sql4);


            float gelenMiktar = 0;
            for (int i = 0; i < gelenGunluk.Rows.Count; i++)
            {
                gelenMiktar += float.Parse(gelenGunluk.Rows[i].ItemArray[4].ToString());
            }
            float gidenMiktar = 0;
            for (int i = 0; i < gidenGunluk.Rows.Count; i++)
            {
                gidenMiktar += float.Parse(gidenGunluk.Rows[i].ItemArray[4].ToString());
            }
            var toplamGunluk = gelenMiktar - gidenMiktar;
            float gelenToplamMiktar = 0;
            for (int i = 0; i < gelenToplam.Rows.Count; i++)
            {
                gelenToplamMiktar += float.Parse(gelenToplam.Rows[i].ItemArray[4].ToString());
            }
            float gidenToplamMiktar = 0;
            for (int i = 0; i < gidenToplam.Rows.Count; i++)
            {
                gidenToplamMiktar += float.Parse(gidenToplam.Rows[i].ItemArray[4].ToString());
            }
            var toplam = gelenToplamMiktar - gidenToplamMiktar;
            textBox43.Text = gelenMiktar.ToString();
            textBox44.Text = gidenMiktar.ToString();
            textBox45.Text = toplamGunluk.ToString();
            textBox46.Text = toplam.ToString();

        }

        private void label56_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            var date1 = dateTimePicker2.Value;
            var dtBaslangic = date1.ToString("yyyy-MM-dd");
            var date2 = dateTimePicker3.Value;
            var dtBitis = date2.ToString("yyyy-MM-dd");
            var sql= "select m.Ad + ' ' + m.Soyad as Isim, IT.aciklama, I.miktar, I.tarih from Islemler I, IslemTurleri IT, Musteriler M, Hesaplar H where I.islemTuru = IT.id and I.tarih >= '" + dtBaslangic + "' and I.tarih <= '" + dtBitis +"' and I.hesapNo = '" + textBox47.Text + "' and M.musteriNo = H.musteriNo and H.hesapNo = I.hesapNo";
            var ozet1 = _db.SelectTable(sql);
            dataGridView3.DataSource = ozet1;
            var sql2 = "select I.islemNo, IT.aciklama, M1.Ad + ' ' +M1.Soyad as Gonderen, M2.Ad + ' ' +M2.Soyad as Alan, I.miktar, I.tarih from Islemler I, IslemTurleri IT, Havaleler Hav, Hesaplar H,Hesaplar H2, Musteriler M1, Musteriler M2 where I.hesapNo = '"+textBox47.Text+"' and I.islemTuru=IT.id and I.tarih>='"+dtBaslangic+"' and I.tarih<='"+dtBitis+"' and I.islemNo=Hav.islemNo and Hav.alanHesapNo = H2.hesapNo and H2.musteriNo = M2.musteriNo and H.hesapNo=Hav.gonderenHesapNo and H.musteriNo=M1.musteriNo";
            var ozet2 = _db.SelectTable(sql2);
            dataGridView4.DataSource = ozet2;
            var sql3 = "select I.islemNo, IT.aciklama, M1.Ad + ' ' +M1.Soyad as Alan, M2.Ad + ' ' +M2.Soyad as Gonderen, I.miktar, I.tarih from Islemler I, IslemTurleri IT, Havaleler Hav, Hesaplar H,Hesaplar H2, Musteriler M1, Musteriler M2 where  H.hesapNo = '" + textBox47.Text + "' and I.islemTuru=IT.id and I.tarih>='" + dtBaslangic + "' and I.tarih<='" + dtBitis + "' and I.islemNo=Hav.islemNo and Hav.gonderenHesapNo = H2.hesapNo and H2.musteriNo = M2.musteriNo and H.hesapNo=Hav.alanHesapNo and H.musteriNo=M1.musteriNo";
            var ozet3 = _db.SelectTable(sql3);
            dataGridView5.DataSource = ozet3;
        }

        private void textBox48_TextChanged(object sender, EventArgs e)
        {
            var sql = "select * from Kullanicilar where id =" + _kullaniciId;
            var kullanici = _db.SelectTable(sql);
            var sifre = kullanici.Rows[0].ItemArray[2].ToString();
            if(textBox48.Text==sifre)
            {
                textBox49.Enabled = true;
                textBox50.Enabled = true;

                


            }
            else
            {
                textBox49.Enabled = false;
                textBox50.Enabled = false;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (textBox49.Text == textBox50.Text && textBox49.Text!=null)
            {
                var sql1 = "update Kullanicilar set sifre='" + textBox49.Text + "' where id =" + _kullaniciId;
                var kontrol = _db.runCommand(sql1);
                if (kontrol > 0)
                {
                    MessageBox.Show("Şifre Değişimi Başarılı");
                    textBox48.Clear();
                    textBox49.Clear();
                    textBox50.Clear();
                }
                else
                {
                    MessageBox.Show("Şifre Değiştirilemiyor");
                }
            }
            else
            {
                MessageBox.Show("Şifreler Uyuşmuyor");
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var kullaniciId = dataGridView6.CurrentRow.Cells[0].Value.ToString();
            var sql = "delete from Kullanicilar where id = " + kullaniciId;
            var kontrol = _db.runCommand(sql);
            if (kontrol > 0)
            {
                var sql2 = "select * from Kullanicilar where id!="+_kullaniciId;
                var kullanicilar = _db.SelectTable(sql2);
                dataGridView6.DataSource = kullanicilar;
            dataGridView6.Columns[0].Visible = false;
                dataGridView6.Columns[2].Visible = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(textBox7.Text==textBox9.Text && textBox8.Text=="0")
            {
                var sql = "delete from Hesaplar where id=" + listBox1.SelectedValue;
                var kontrol = _db.runCommand(sql);
                if (kontrol > 0)
                {
                    _first = true;
                    var musteriNo = dataGridView2.CurrentRow.Cells[1].Value.ToString();
                    var sql2 = "select * from Hesaplar where musteriNo='" + musteriNo + "'";
                    var hesapListesi = _db.SelectTable(sql2);
                    listBox1.DataSource = hesapListesi;
                    listBox1.DisplayMember = "hesapNo";
                    listBox1.ValueMember = "id";
                    _first = false;
                }
            }
            else
            {
                MessageBox.Show("Hesap boş olmadığıiçin Dondurulamaz");
            }
        }

        private void AnaSayfa_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("chrome.exe", "https://www.facebook.com/gamze.akbal.39");
        }

        private void tabPage9_Click(object sender, EventArgs e)
        {

        }
        public void AdresleriAl()
        {
            var sql11 = "select Email from Musteriler";
            var epostalar= _db.SelectTable(sql11);
            for (int i = 0; i < epostalar.Rows.Count; i++)  //burdan müşterilerinkini çektim
            {
                veri.Add(epostalar.Rows[i].ItemArray[0].ToString());
            }

        }
        public void TopluMail()
        {
            AdresleriAl();
            SmtpClient cl = new SmtpClient();
            cl.Credentials = new System.Net.NetworkCredential("gakbal112@gmail.com", "141093ehimmmm");
            cl.Port = 587;
            cl.Host = "smtp.gmail.com";
            cl.EnableSsl = true;


            //string[] veriler1 = new string[listBox1.Items.Count];
            //for (int i = 0; i < listBox1.Items.Count; i++)
            for (int i = 0; i < veri.Count; i++)
            {
                MailMessage mail = new MailMessage();

                //mail.To.Add(listBox1.Items[i].ToString());
                mail.To.Add(veri[i].ToString());
                mail.From = new MailAddress("gakbal112@gmail.com");
                mail.Subject = textBox51.Text;
                mail.Body = textBox53.Text;
                mail.IsBodyHtml = true;
                cl.Send(mail);
               
                progressBar2.Value +=Convert.ToInt32(Convert.ToSingle(100)/Convert.ToSingle(veri.Count));
             
                




            }
            progressBar2.Value = 100;
            if (progressBar2.Value == 100)
            {
                Thread.Sleep(1000);
                progressBar2.Visible = false;
                
                MessageBox.Show("Gonderim Tamamlandı!!");
            }

        }

        private void button12_Click(object sender, EventArgs e)
        {
            th1 = new Thread(new ThreadStart(TopluMail));
            th1.Start();
            button5.Enabled = true;
            button4.Enabled = false;
           
        }

        private void button13_Click(object sender, EventArgs e)
        {
           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
