using DataAccessExample_Lab4_TelefonDirectory.DataAccessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataAccessExample_Lab4_TelefonDirectory.EntityLayer.Concrete
{
    public class Temizle : IDeleteUsers //IDeleteUsers interfaces ndan yararlanmak için tanımladık ve implement ettik.
    {
        public void Eraser(GroupBox groupBox)
        {
            foreach (Control item in groupBox.Controls)
            {
                if (item is TextBox || item is MaskedTextBox)
                {
                    item.Text = string.Empty;
                }
            }
        }
    }
}
