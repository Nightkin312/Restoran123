using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace IS_Restoran
{
    public class IDComboBoxItem : ComboBoxItem
    {
        public int ID;
        public IDComboBoxItem(int ID, string Content)
        {
            this.ID = ID;
            this.Content = Content.ToString();
        }
    }
}
