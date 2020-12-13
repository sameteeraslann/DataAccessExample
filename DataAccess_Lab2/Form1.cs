using DataAccess_Lab2.ORM;
using System;
using System.Data;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Windows.Forms;

namespace DataAccess_Lab2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        NorthwindEntities db = new NorthwindEntities();
        private void button1_Click(object sender, EventArgs e)
        {
            //Çalışanların isim, soyisim, ünvan, doğum tarihi bilgilerini getiren linq to entity ve linq to SQL sorgularını yazınız.

            #region Linq to Entity
            dataGridView1.DataSource = db.Employees.Select(x => new
            {
                x.FirstName,
                x.LastName,
                x.Title,
                x.BirthDate
            }).ToList();
            #endregion

            #region Linq to SQL
            var result = from emp in db.Employees
                         select new
                         {
                             emp.FirstName,
                             emp.LastName,
                             emp.Title,
                             emp.BirthDate
                         };

            dataGridView1.DataSource = result.ToList();
            #endregion
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Çalışanların Id'si 2 ile 8 arasında olanların A'dan Z'ye sıralayarak, Id, Adını, Soyadını
            #region Linq to Entity
            dataGridView1.DataSource = db.Employees.Where(x => x.EmployeeID > 2 && x.EmployeeID <= 8).OrderBy(x => x.FirstName).Select(x => new
            {
                x.EmployeeID,
                x.FirstName,
                x.LastName,
                x.TitleOfCourtesy
            }).ToList();
            #endregion

            #region Linq to SQL
            var result = from Employee in db.Employees
                         where Employee.EmployeeID > 2 && Employee.EmployeeID < 8
                         orderby Employee.FirstName
                         select new
                         {
                             Employee.EmployeeID,
                             Employee.FirstName,
                             Employee.LastName,
                             Employee.TitleOfCourtesy
                         };

            dataGridView1.DataSource = result.ToList();
            #endregion
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //1960 yılında doğan çalışanların adı,soyadı, doğum tarihi
            var result = from Employee in db.Employees
                         where SqlFunctions.DatePart("Year", Employee.BirthDate) == 1960
                         orderby Employee.FirstName
                         select new
                         {
                             Employee.FirstName,
                             Employee.LastName,
                             Employee.BirthDate
                         };
            dataGridView1.DataSource = result.ToList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //60 yaşından büyük olan çalışanların Adı, Soyadını , Doğum tarihini, A'dan Z'ye sıralayınız
            var result = from Employee in db.Employees
                         where SqlFunctions.DateDiff("Year", Employee.BirthDate, DateTime.Now) > 60
                         orderby Employee.BirthDate descending
                         select new
                         {
                             Adi = Employee.FirstName,
                             Soyadi = Employee.LastName,
                             DogumTarihi = Employee.BirthDate
                         };
            dataGridView1.DataSource = result.ToList();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Doğum tarihi 1930 ve 1960 arasında olan ve USA de yaşayan çalışanların listesini getiriniz.

            var result = from Emp in db.Employees
                         where Emp.Country == "USA" && (SqlFunctions.DatePart("Year", Emp.BirthDate) >= 1930 && SqlFunctions.DatePart("Year", Emp.BirthDate) <= 1960)
                         select new
                         {
                             Adi = Emp.FirstName,
                             Soyadi = Emp.LastName,
                             DogumTarihi = SqlFunctions.DatePart("Year", Emp.BirthDate)
                         };
            dataGridView1.DataSource = result.ToList();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //Kategorilerime göre stok durum nasıl?
            dataGridView1.DataSource = db.Categories
                .Join(db.Products,
                c => c.CategoryID,
                p => p.CategoryID,
                (c, p) => new { c, p })
                .GroupBy(x => x.c.CategoryName)
                .Select(x => new
                {
                    Name = x.Key,
                    Count = x.Sum(z => z.p.UnitsInStock)
                }).ToList();
        }
    }
}
