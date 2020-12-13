using DataAccessExample_Lab4_TelefonDirectory.DataAccessLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataAccessExample_Lab4_TelefonDirectory.EntityLayer.Concrete
{
    public static class Islemler
    {
        static int id;

        public static void ListOfAppUsers(DataGridView dataGridView)
        {
            ProjectContext db = new ProjectContext();
            dataGridView.DataSource = db.AppUsers.ToList(); // Girilen dataları dataGridView1 de listeliyor. 
        }
        public static void KayitSatiriSecme(DataGridView dataGridView, TextBox txtAd, MaskedTextBox mskTel, TextBox txtAdres)
        {
            id = Convert.ToInt32(dataGridView.CurrentRow.Cells["Id"].Value);
            txtAd.Text = dataGridView.CurrentRow.Cells["Name"].Value.ToString();
            mskTel.Text = dataGridView.CurrentRow.Cells["TelNumber"].Value.ToString();
            txtAdres.Text = dataGridView.CurrentRow.Cells["Adres"].Value.ToString();
        }

        public static void Ekle(TextBox txtAd, MaskedTextBox mskTel, TextBox txtAdres, RadioButton rbsim, RadioButton rbtel, GroupBox grp, DataGridView dataGridView)
        {
            ProjectContext db = new ProjectContext();
            AppUser appUser = new AppUser();
            Temizle temizle = new Temizle();

            appUser.Name = txtAd.Text;
            appUser.TelNumber = mskTel.Text;
            appUser.Adres = txtAdres.Text;
            foreach (Control item in grp.Controls)
            {
                if (item is RadioButton)
                {
                    if (rbsim.Checked)
                    {
                        appUser.KayitYeri = rbsim.Text;
                    }
                    else
                    {
                        appUser.KayitYeri = rbtel.Text;
                    }
                }
            }
            appUser.CreateTime = DateTime.Now;
            db.AppUsers.Add(appUser);
            db.SaveChanges();
            ListOfAppUsers(dataGridView);
            temizle.Eraser(grp);
            rbsim.Checked = true;


            // Message bak buraya olması gerekiyor onu dene.

        }
        public static void Update(TextBox Ad, MaskedTextBox mskTel, TextBox Adres, GroupBox grp, DataGridView dataGridView)
        {
            ProjectContext db = new ProjectContext();
            AppUser appUser = new AppUser();
            appUser = db.AppUsers.FirstOrDefault(x => x.Id == id);
            Temizle temizle = new Temizle();

            appUser.Name = Ad.Text;
            appUser.TelNumber = mskTel.Text;
            appUser.Adres = Adres.Text;
            appUser.Status = Enums.Status.Update;
            appUser.UpdateDate = DateTime.Now;
            db.SaveChanges();
            ListOfAppUsers(dataGridView);
            temizle.Eraser(grp);

        }
        public static void Delete(DataGridView dataGridView, GroupBox grp)
        {
            ProjectContext db = new ProjectContext();
            AppUser appUser1 = new AppUser();
            Temizle temizle = new Temizle();
            appUser1 = db.AppUsers.FirstOrDefault(x => x.Id == id);

            appUser1.Status = Enums.Status.Delete;
            appUser1.DeleteDate = DateTime.Now;
            db.SaveChanges();
            ListOfAppUsers(dataGridView);
            temizle.Eraser(grp);

        }

        public static void Search(DataGridView dataGridView, TextBox Ad, MaskedTextBox mskTel, TextBox Adres)
        {
            ProjectContext db = new ProjectContext();
            AppUser appUser2 = new AppUser();
            Temizle temizle = new Temizle();
            appUser2 = db.AppUsers.FirstOrDefault(x => x.Id == id);
            ListOfAppUsers(dataGridView);
            dataGridView.DataSource = db.AppUsers.
                Where(x =>
                x.Name == Ad.Text ||
                x.TelNumber == mskTel.Text ||
                 x.Adres == Adres.Text).ToList();


        }

    }
}
