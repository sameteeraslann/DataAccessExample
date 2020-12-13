using DataAccessExample_Lab4_TelefonDirectory.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessExample_Lab4_TelefonDirectory.DataAccessLayer.Context
{
    public class ProjectContext : DbContext
    {
        public ProjectContext()
        {
            // Database ile bağlantı işlemini gerçekleştirdik.
            Database.Connection.ConnectionString = @"Server=DESKTOP-9CA7T86\MSQL;DataBase=TelephoneDirectory;Integrated Security=True";
        }

        // Gerekli rowlar hazırlandı.
        public DbSet<AppUser> AppUsers { get; set; }
    }
}
