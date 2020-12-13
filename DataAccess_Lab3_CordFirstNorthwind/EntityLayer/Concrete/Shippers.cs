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
    public class Shippers : BaseEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public virtual List<Orders> Orders { get; set; }

    }
}
