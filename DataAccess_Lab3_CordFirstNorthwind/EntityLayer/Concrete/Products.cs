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
    public class Products : BaseEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
        public string ProductName { get; set; }
        // --------------SUPPLİERS -----------

        [ForeignKey("Suppliers")]
        public int SuppliersId { get; set; }
        public virtual Suppliers Suppliers { get; set; }

        //------------CATEGORY--------------
        [ForeignKey("Categories")]
        public int CategoryId { get; set; }
        public virtual Categories Categories { get; set; }



        public string QuantityPerUnit { get; set; }
        public double UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public int ReoderLevel { get; set; }
        public byte Discontinued { get; set; }

        public virtual List<OrderDetails> OrderDetails { get; set; }
    }
}
