using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auftragsverwaltung.Views
{

    public class TreeViewItem
    {
        public ObservableCollection<TreeViewItem> Items { get; set; }
        public string Title { get; set; }

        public TreeViewItem()
        {
            this.Items = new ObservableCollection<TreeViewItem>();
        }
    }
}
