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
    public class OrderDetails : BaseEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }

        [ForeignKey("Orders")]
        public int OrdersId { get; set; }
        public virtual Orders Orders { get; set; }

        [ForeignKey("Products")]
        public int ProductId { get; set; }
        public virtual Products Products { get; set; }

        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
        public float Discount { get; set; }

    }
}
