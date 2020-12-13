using DataAccessExample_Lab4_TelefonDirectory.EntityLayer.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessExample_Lab4_TelefonDirectory.EntityLayer.Concrete
{
    public class AppUser : BaseEntity
    {
        [Key] // 'Id' Primary Key olarak belirttik.
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // 'Id' Identity verdik.
        public override int Id { get; set; } // BaseEntity.cs'dan gelen Id override ettik


        public string Name { get; set; }
        public string TelNumber { get; set; }
        public string Adres { get; set; }
        public string KayitYeri { get; set; }

    }
}
