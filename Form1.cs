using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace besinkalori
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglantim = new SqlConnection("Data Source=DESKTOP-I78CKSO;Initial Catalog=besindegerleri;Integrated Security=True");

        private void besinleri_listele()
        {
            try
            {
                baglantim.Open();
                SqlDataAdapter besin_goster = new SqlDataAdapter("select ad AS[ADI],kalori AS[KALORİ],karb AS[KARBONHİDRAT],protein AS[PROTEİN],yag AS[YAĞ] from degerler Order By ad ASC", baglantim);
                DataSet dshafiza = new DataSet();
                besin_goster.Fill(dshafiza);
                dataGridView1.DataSource = dshafiza.Tables[0];
                dataGridView2.DataSource = dshafiza.Tables[0];
                baglantim.Close();
            }
            catch (Exception hatamsj)
            {
                MessageBox.Show(hatamsj.Message);
                baglantim.Close();
            }
        }

        private void toppage2_temizle()
        {
            pictureBox1.Image = null;
            textBox1.Clear();textBox2.Clear();
            textBox3.Clear();textBox4.Clear();textBox5.Clear();
        }
        
        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Besin Değerleri";
            tabPage1.Text = "Besinler";
            tabPage2.Text = "Besin Ekle";
            pictureBox1.Height = 150;pictureBox1.Width = 150;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.BorderStyle = BorderStyle.Fixed3D;
            pictureBox2.BorderStyle = BorderStyle.Fixed3D;
            pictureBox2.Height = 150; pictureBox2.Width = 150;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            label11.ForeColor = Color.Red;
            label12.ForeColor = Color.Red;
            label13.ForeColor = Color.Red;
            label14.ForeColor = Color.Red;
            besinleri_listele();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog resimsec = new OpenFileDialog();
            resimsec.Title = "Yiyecek resmi seçiniz!";
            resimsec.Filter = "JPG Dosyalar(*.jpg) | *.jpg";
            if (resimsec.ShowDialog()==DialogResult.OK)
            {
                this.pictureBox1.Image = new Bitmap(resimsec.OpenFile());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool kayit_kontrol = false;
            baglantim.Open();
            SqlCommand selectsorgu = new SqlCommand("Select *from degerler where ad='" + textBox1.Text.ToString() + "'", baglantim);
            SqlDataReader kayitokuma = selectsorgu.ExecuteReader();
            while (kayitokuma.Read())
            {
                kayit_kontrol = true;
                break;
            }
            baglantim.Close();

            if (kayit_kontrol==false)
            {
                if (textBox1.Text!="" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && pictureBox1.Image!=null)
                {
                    try
                    {
                        baglantim.Open();
                        SqlCommand eklesorgusu = new SqlCommand("insert into degerler values('" + textBox1.Text.ToString() + "','" + textBox2.Text.ToString() + "','" + textBox3.Text.ToString() + "','" + textBox4.Text.ToString() + "','" + textBox5.Text.ToString() + "')", baglantim);
                        eklesorgusu.ExecuteNonQuery();
                        baglantim.Close();
                        pictureBox1.Image.Save(Application.StartupPath + "\\yiyecekresimler\\" + textBox1.Text + ".jpg");
                        MessageBox.Show("Yeni yiyecek kaydı oluştuldu!", MessageBoxButtons.OK.ToString());
                        besinleri_listele();
                        toppage2_temizle();
                    }
                    catch (Exception hatamsj)
                    {
                        MessageBox.Show(hatamsj.Message);
                        baglantim.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Boş alanlar var!", MessageBoxButtons.OK.ToString());
                }
                
            }
            else
            {
                MessageBox.Show("Girilen yiyecek zaten kayıtlı!", MessageBoxButtons.OK.ToString());
                toppage2_temizle();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            bool kayit_arama = false;
            try
            {
                baglantim.Open();
                SqlCommand selectsorgu = new SqlCommand("Select *from degerler where ad='" + textBox6.Text.ToString() + "'", baglantim);
                SqlDataReader kayitokuma = selectsorgu.ExecuteReader();
                while (kayitokuma.Read())
                {
                    kayit_arama = true;
                    label6.Text = kayitokuma.GetValue(0).ToString();
                    label11.Text = kayitokuma.GetValue(1).ToString();
                    label12.Text = kayitokuma.GetValue(2).ToString();
                    label13.Text = kayitokuma.GetValue(3).ToString();
                    label14.Text = kayitokuma.GetValue(4).ToString();
                    pictureBox2.Image = Image.FromFile(Application.StartupPath + "\\yiyecekresimler\\" + textBox6.Text + ".jpg");
                    break;
                }
                baglantim.Close();
            }
            catch (Exception hatamsj)
            {
                MessageBox.Show(hatamsj.Message);
                baglantim.Close();
            }
            if (kayit_arama==false)
            {
                MessageBox.Show("Aranan yiyecek bulunamadı. İsterseniz ekleyebilirsiniz!");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            bool kayit_arama = false;
            try
            {
                baglantim.Open();
                SqlCommand selectsorgu = new SqlCommand("Select *from degerler where ad='" + textBox1.Text.ToString() + "'", baglantim);
                SqlDataReader kayitokuma = selectsorgu.ExecuteReader();
                while (kayitokuma.Read())
                {
                    kayit_arama = true;
                    textBox2.Text = kayitokuma.GetValue(1).ToString();
                    textBox3.Text = kayitokuma.GetValue(2).ToString();
                    textBox4.Text = kayitokuma.GetValue(3).ToString();
                    textBox5.Text = kayitokuma.GetValue(4).ToString();
                    pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\yiyecekresimler\\" + textBox1.Text + ".jpg");
                    break;
                }
                baglantim.Close();
            }
            catch (Exception hatamsj)
            {
                MessageBox.Show(hatamsj.Message);
                baglantim.Close();
            }
            if (kayit_arama==false)
            {
                MessageBox.Show("Aradığınız yiyecek kayıtlı değildir!", MessageBoxButtons.OK.ToString());
                toppage2_temizle();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                baglantim.Open();
                SqlCommand guncellekomutu = new SqlCommand("update degerler set kalori='" + textBox2.Text.ToString() + "',karb='" + textBox3.Text.ToString() + "',protein='" + textBox4.Text.ToString() + "',yag='" + textBox5.Text.ToString() + "' where ad='"+textBox1.Text+"'", baglantim);
                guncellekomutu.ExecuteNonQuery();
                baglantim.Close();
                MessageBox.Show("Güncelleme yapıldı!");
                toppage2_temizle();
                besinleri_listele();
            }
            catch (Exception hatamsj)
            {
                MessageBox.Show(hatamsj.Message);
                baglantim.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                baglantim.Open();
                SqlCommand silkomutu = new SqlCommand("delete from degerler where ad='" + textBox1.Text + "'", baglantim);
                silkomutu.ExecuteNonQuery();
                baglantim.Close();
                MessageBox.Show("Besin silindi!");
                toppage2_temizle();
                besinleri_listele();
            }
            catch (Exception hatamsj)
            {
                MessageBox.Show(hatamsj.Message);
                baglantim.Close();
            }
        }
    }
}
