using DataAccessExample_Lab4_TelefonDirectory.EntityLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessExample_Lab4_TelefonDirectory.EntityLayer.Abstract
{
    public abstract class BaseEntity
    {
        public abstract int Id { get; set; } //  Id diğer sınıfların da kalıtım alabilmesi için Abstract olarak işaretledik

        //private DateTime _createTime = DateTime.Now;
        public DateTime? CreateTime { get; set; } // Kişi Oluşturma tarihini alır.


        public DateTime? UpdateDate { get; set; } // Güncelleme tarihini alır.


        public DateTime? DeleteDate { get; set; } // Silme Tarihini alır.

        private Status _status = Status.Active;
        public Status Status { get => _status; set => _status = value; }


    }
}
