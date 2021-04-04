using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NevikaApp.Data
{
	public class GroupedVeggieModel : ObservableCollection<VeggieModel>
	{
		public string GroupName { get; set; }
		public bool Expanded { get; set; }
	}
}
