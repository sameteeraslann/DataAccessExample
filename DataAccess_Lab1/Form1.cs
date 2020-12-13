using DataAccess_Lab1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.SqlServer;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_1_DbFist
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        // ORM
        // Entitiy Framework ve altarnatiflerinin arkasında yatan mantık ORM mimarisidir. Entitiy Framework özünde ORM mimarisidir. ORM veritabanında oluşturmuş olduğumuz her varlığın yani tablonun karşılığında uygulama tarafında ilgili tabloların birer karşılığı objeler bulunmalıdır. Ef code generation tekniği kullanılarak bizim yazmamız gereken kodu otomatik olarak üretir. OOP mantığına ve SOLID prensiplerine uygun bir şekilde veritabanımızda bulunan tabloların birer nesne örneklerini oluşturuyoruz.

        //Entitiy Framework
        //ORM gerekliliklerini yukarıda tartıştık. Son cümlede tabloların birer nesne örnkeklerini oluşturuyoruz. Buradan anlaşılan veri tabanı yansıması projeye entegre etmemiz yada bu yansımayı bizim hazırlamamız gerektiğidir. Bu bağlamda EF bize 3 yaklaşımıyla yardımcı olmaktadır.

        // 1. Database First Approach: Bu lab uygulamamızda kullandığımız yaklaşımdır. Hazır bir veri tabanı varsa ve bu veri tabanını kullanacaksam ilgili veri tabanının yansımasını uygulamaya ekliyoruz. Sonuç olarak veri tabanında bulunan tablolararı projeme sınıf olarak EF tarafından otomatik olarak eklenir. Application tarafında bu yansıma üzerinde çalışırız. CRUD operasyonlarını bu yansıma üzerinden yürütüyoruz. Çok büyük projelerin ihtiyacı olan geniş çaplı veritabanlarında bu yaklaşım performans kayıplarına neden ollur. Ayrıca yansıma üzerinde bir değişiklik yaptığımızda bu değişikliği ver tabanındaki varlıklar üzerinde uygulanması gerekir.
        //2. Model First Approach : Uygulama tarafında SQL Server Object Explorer yardımıyla ver tabanında ihtiyaç duyulan varlıklar tablolar halinde oluşturulur. Oluşturulan bu şema veri tabanına gönderilir. 
        //3. Code First Approach: Veri tabanında ihtiyaç duyulan varlıkların ve bu varlıkların arasındaki ilişkileri OOP yaklaşımları çerçevesinde application tarafında developper tarafından sınıflar (class) halinde oluşturulması daha sonra sınıflardan teşekkül eden bu yapının veri tabanı tarafına migration edilerek EF tarafından veri tabanı ve onun varlıklarının otomatik olarak oluşturulduğu bir yaklaşımdır. Sonuç olarak bu oluşturulan sınıflar veri tabanına gidecek tablo olarak ayağa kalkacaklar.
        NorthwindEntities db = new NorthwindEntities();

        private void button1_Click(object sender, EventArgs e)
        {
            //ToList() => Metodu veri tabanında bulunan tablodaki tüm veriyi herhangi bir şartta bakmazsızın RAM'ın Heap alanına çıkarır. Genellikle uygulama tarafında veri tabanında bulunan varlığın yani tablonun uygulama tarafındaki yansıması tipinde bir generic liste oluşturulması için kullanılır.
            //db.Categories.ToList() bu linq to sorgusu SQL tarafında TSQL dönüştürülerek execute edilecek..
            dataGridView1.DataSource = db.Categories.ToList();//linq to
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //Customer (Müşteriler) CompanyName, ContactName, Phone ve Adress bilgilerini getiriniz
            dataGridView1.DataSource = db.Customers.Select(x => new
            {
                SirketAdı = x.CompanyName,
                Yetkili = x.ContactName,
                Telefon = x.Phone,
                Adres = x.Address
            }).ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Where() => Veritabanındaki tablolar üzerinden şartlar doğruştusunda verileri filtrelemeye yarar.

            //ürün birim fiyatı 20'den fazla olanları listeleyiniz
            //dataGridView1.DataSource = db.Products.Where(x => x.UnitPrice > 20).ToList();

            //Lamda Expression ( "x=> x." ): Lamda, bu örnekte Product sınıfının tüm özelliklerinin "x" generic ismine atamakya yramaktadır. Nasıl ki instance aldığımızda instance name + "." notasyonu ile sınıfın özelliklerine erişiyoruz burada aynı durum söz konusudur.

            //Soru: Products tablosundan UnitInStock bilgisi 100 ile 200 arasında olan ürünleri listleyin
            dataGridView1.DataSource = db.Products.Where(x => x.UnitsInStock >= 100 && x.UnitsInStock <= 200).ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            //T-SQL'deki mantığı buradada kullanılmaktadır. Sorugu sonucunda dönen generic list üzerinde sıralama işlemleri yapar. Burada SQL olduğu gibi default "asc" dir. Descendig işlemi için ayrıca bir method bulunmaktadır.

            //Ascending => Bu örnekte ürün adlarını a'dan z'ye sıraladık
            //dataGridView1.DataSource = db.Products.OrderBy(x => x.ProductName).ToList();

            //Ürünleri birim fiyatlarına göre çoktan aza sıralayarak, ürünlerin Id, ürün adınını, stok miktarını ve birim fiyatın bilgilerini getiriniz.
            dataGridView1.DataSource = db.Products.OrderByDescending(x => x.UnitPrice).Select(x => new
            {
                x.ProductID,
                x.ProductName,
                x.UnitsInStock,
                x.UnitPrice
            }).ToList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //T-SQL'de bulunan özelliği ile buradakli kullanımı aynıdır. Sonuç kümesi üzerinde belirli şartlara göre gruplama yapmamızı sağlar.

            //Hangi kategoride kaç tana ürün var
            dataGridView1.DataSource = db.Products
                .GroupBy(x => x.Category.CategoryName)
                .Select(x => new
                {
                    KategoriAdi = x.Key,
                    ToplamStok = x.Sum(z => z.UnitsInStock)
                }).ToList();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //First() => Bir koleksiyonda sorgu sonucunda bize dönen veri kümesindeki ilk elemanı seçmek için kullanılmaktadır. 

            //First methodunu kullanırken şu hususada dikkat etmek gerekmektedir. First methodu içerisine parametre girmezsek sorgu sonucunda bize dönen kümesinin ilk satırını bize default olarak döner. 
            Category category = db.Categories.First();
            MessageBox.Show($"First içerisinde şart vermedin sorgu sonucuda dönen ilk satırı getirdim: {category.CategoryName}");

            //Lakin bir şart belirtirsek şart sonucunda dönen sorgu kümesindeki ilk satırı döner.
            Category category_1 = db.Categories.First(x => x.CategoryID > 7);
            MessageBox.Show($"Category Id'si 7'den büyük olan ilk category: {category_1.CategoryName}");
            //Not: First methodu ile ToList() metodu birlikte kullanılamaz, çünkü first methodu tek bir veri satırı döner, bu yüzden ToList() kullanılamaz.

            //Dikkat: Null değer gelirse uygulama patlar.One göre try-catch yada if blokları ile gerekli kontorl mekanizmaları kurulmak zorundadır. TSQL'deki Top komutuna benzetebiliriz.
            //Category category_2 = db.Categories.First(x => x.CategoryID > 10);
            //Yukarıdaki linq to sorgusu çalışmamaktadır. Çünkü veritabanında 11 nolu bir Id bulunmamaktadır. Bunu try-catch blokları ile handle etmemiz gerekmektedir.

            try
            {
                Category category_2 = db.Categories.First(x => x.CategoryID > 10);
                MessageBox.Show($"Aradığını kategori: {category_2.CategoryName}");
            }
            catch (Exception)
            {
                MessageBox.Show("Aradığınız kategory bulunmamktadır..!");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //FirstOrDefault() => First ile aynı kullanım amacına sahiptir. Farklı olarak First methodu null değer geldiğinde patlamaktaydı FirtOrDefault bu hatayı yaşamıyor direk değer yoksa null dönmketedir. Yani şart sağlamadığında patlamak yerine Null dönüyor.

            Category category = db.Categories.FirstOrDefault(x => x.CategoryID > 7);

            if (category == null)
            {
                MessageBox.Show("Aradığınız kategori bulunmamaktadır..!");
            }
            else
            {
                MessageBox.Show($"Aradığınız kategori: {category.CategoryName}");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //Find() => SQL Server üzerinde veritabanı işlemlerni incelerken Identity Key (Id) olduğunu öğrenmişsinizdir. Uniq olduğunu tek bir kayıtı temsil ettiğini unutmayın. Eğer bir tek kayıt erişmek istersek Id'den yararlanmamız çok olağandır ve bu bağlamda find methodunu kullanabiliriz.

            //Employee employee = db.Employees.Find(1);

            //if (employee == null)
            //{
            //    MessageBox.Show("Aradığınız çalışan bulunmamaktadır..!");
            //}
            //else
            //{
            //    MessageBox.Show($"Aradığınız çalışanın adı: {employee.FirstName}");
            //}

            //Kategori ve Tedarikçi Id'si 1 olan ürünleri ismlerine göre tersten sıralayınız.
            dataGridView1.DataSource = db.Products.Where(x => x.CategoryID == 1 && x.SupplierID == 1).OrderByDescending(x => x.ProductName).ToList();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //Take() => Metodu T-SQL sorgulam dilindeki Top komutunun karşılığıdır diyebiliriz. Sorgu sonucunda bize dönen küme üzerinden sitediğimiz kadar satırı ekrana yazdırmamızı sağlayacaktır.


            //Product tablosundaki ürünleri UnitPrice göre çoktan aza sıralayaınız. İlk 5 ürünün Id, ProductName, UnitPrice, UnitInStock bilgilerini getirin
            dataGridView1.DataSource = db.Products.Select(x => new
            {
                x.ProductID,
                x.ProductName,
                x.UnitPrice,
                x.UnitsInStock
            }).OrderByDescending(x => x.UnitPrice).Take(5).ToList();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //Skip() => Sorgu sonucunda dönen sonuç kümesi üzerinde, methodun parametresine verilen değer kadar satırı görmezden gelir.

            //Product tabosundaki ürünleri UnitInStock bilgisine göre çoktan aza sıralayınız. Dönen sonuç kümesi içerisinden 10 ve 20 satır arasındaki ürünlerin Id'sini, ProductName, UnitInStock, UnitPrice bilgilerini listeleyin.
            dataGridView1.DataSource = db.Products.OrderByDescending(x => x.UnitsInStock).Skip(10).Take(10).Select(x => new
            {
                x.ProductID,
                x.ProductName,
                x.UnitsInStock,
                x.UnitPrice
            }).ToList();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //Belirli bir harf, hece yada söz öbeğinin ilgişlş alanda yer alıp almadığını kontorl eder. Where methodu ile birlikte kullanılır.

            //Çalışanlarımın isimleri içerisinde a harfi geçenleri listeleyin, a'dan z'ye sıralayın, ilk beşini görmezden gelin 5 ile 10 arasındakileir listeleyin
            dataGridView1.DataSource = db.Employees.Where(x => x.FirstName.Contains("a")).OrderBy(x => x.FirstName).Skip(5).Take(5).ToList();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //Any methodunun iki farklı kullanım şekli bulunmaktadır. İlk kullanımı olan bir tabloda kayıt var mı yok mu bunu kontrolo edebilirsiniz. İkici kullanımı ise tablodan verilen şartlara uygun kayıt var mı yok mu onu kontrol eder. Any methodunun geri dönüş tipi boolean'dır.

            bool sonuc = db.Categories.Any(x => x.CategoryName == "SeaFood");
            MessageBox.Show(sonuc.ToString());
        }

        private void button13_Click(object sender, EventArgs e)
        {
            //Count => Sorgu sonucunda dönen satır sayısını bize sayar. Int tipinde geri dönüş yapar

            int urunSayisi = db.Products.Count();
            MessageBox.Show($"Toplam Urun Sayı: {urunSayisi}");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            //Sorgu sonucunda dönen sonuç kümesinde bizim belirtitiğimiz sütundaki değerleri toplar

            int? stok = db.Products.Sum(x => x.UnitsInStock);
            MessageBox.Show($"Stok Durum: {stok}");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            //Çalışanların yaşlarını hesaplayınız
            dataGridView1.DataSource = db.Employees.Select(x => new
            {
                Adi = x.FirstName,
                Soyadi = x.LastName,
                DogumTarihi = x.BirthDate,
                Yasi = SqlFunctions.DateDiff("Year", x.BirthDate, DateTime.Now)
            }).ToList();
        }
    }
}
