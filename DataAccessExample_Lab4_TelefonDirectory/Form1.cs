using DataAccessExample_Lab4_TelefonDirectory.DataAccessLayer.Context;
using DataAccessExample_Lab4_TelefonDirectory.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataAccessExample_Lab4_TelefonDirectory
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ProjectContext db = new ProjectContext();
        private void btnEkle_Click(object sender, EventArgs e)
        {
            // Ekle Butonun basıldığında txtAdiSoyadi & txtTelefonNumarasi 'da ki bilgileri Try Catch ile Database ekleme işleminiyapmakta.

            try
            {
                Islemler.Ekle(txtAdiSoyadi, mskTelefon, txtAdres, rdSimkart, rdTelefon, grpRehberIslemleri,
                    dataGridView1);
                MessageBox.Show("Kayıt Başarıyla Eklendi");

            }
            catch (Exception)
            {
                MessageBox.Show("Kayıt Eklemedi.", "Bilgilendirme Panosu");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Islemler.Update(txtAdiSoyadi, mskTelefon, txtAdres, groupBox1, dataGridView1);
                MessageBox.Show("Güncelleme işlemi başarıyla gerçekleştirildi.");

            }
            catch (Exception exception)
            {
                MessageBox.Show($"Güncelleme başarısız Alınan hata : {exception}");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Islemler.Delete(dataGridView1, groupBox1);
                MessageBox.Show("Silme işlemi başarıyla gerçekleştirildi.");
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Silme başarısız Alınan hata : {exception}");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Islemler.Search(dataGridView1, txtAdiSoyadi, mskTelefon, txtAdres);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Islemler.KayitSatiriSecme(dataGridView1, txtAdiSoyadi, mskTelefon, txtAdres);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Islemler.ListOfAppUsers(dataGridView1);
            rdSimkart.Checked = true;
        }
    }
}
