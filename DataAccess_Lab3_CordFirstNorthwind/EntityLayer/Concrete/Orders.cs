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
    public class Orders : BaseEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
        // -------------COSTOMER ID-----------------
        [ForeignKey("Customers")]
        public int CustomerId { get; set; }
        public virtual Customers Customers { get; set; }
        //--------------EMPLOYEES ID-----------------------
        [ForeignKey("Employees")]
        public int EmployeeId { get; set; }
        public virtual Employees Employees { get; set; }

        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        // ------------SHİPPERS ------------------------
        public DateTime ShippedDate { get; set; }
        [ForeignKey("Shippers")]
        public int ShipVia { get; set; }
        public virtual Shippers Shippers { get; set; }
        public float Freight { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipRegion { get; set; }
        public string ShipPastalCode { get; set; }

        public virtual List<OrderDetails> OrderDetails { get; set; }


    }
}
