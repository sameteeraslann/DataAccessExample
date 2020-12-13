using DataAccess_Lab3_CordFirstNorthwind.EntityLayer.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_Lab3_CordFirstNorthwind.EntityLayer.Concrete
{
    public class Categories : BaseEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
        public string CategoryName { get; set; }
        public string Descreption { get; set; }
        public byte Picture { get; set; }

        public virtual List<Products> Products { get; set; }

    }
}
