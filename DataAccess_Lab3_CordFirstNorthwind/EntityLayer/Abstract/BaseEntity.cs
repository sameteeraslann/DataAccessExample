using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_Lab3_CordFirstNorthwind.EntityLayer.Abstract
{
    public abstract class BaseEntity<T>
    {
        public abstract int Id { get; set; }
    }
}
