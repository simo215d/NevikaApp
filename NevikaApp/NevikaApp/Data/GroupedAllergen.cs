using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NevikaApp.Data
{
    public class GroupedAllergen : ObservableCollection<Allergen>
    {
        public string GroupName { get; set; }
        public bool HasChildren { get; set; }
        public bool Selected { get; set; }
        public bool Expanded { get; set; }
    }
}
