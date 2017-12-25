using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Bike.Other
{
	public class SearchData : INotifyPropertyChanged
	{
		private string id;

		private string data;

		private string number;

		public event PropertyChangedEventHandler PropertyChanged;

		public string ID
		{
			get
			{
				return this.id;
			}
			set
			{
				bool flag = this.id != value;
				if (flag)
				{
					this.id = value;
					this.OnPropertyChanged("ID");
				}
			}
		}

		public string Data
		{
			get
			{
				return this.data;
			}
			set
			{
				bool flag = this.data != value;
				if (flag)
				{
					this.data = value;
					this.OnPropertyChanged("Data");
				}
			}
		}

		public string Number
		{
			get
			{
				return this.number;
			}
			set
			{
				bool flag = this.number != value;
				if (flag)
				{
					this.number = value;
					this.OnPropertyChanged("Number");
				}
			}
		}

		public string Name
		{
			get;
			set;
		}

		public string Channel
		{
			get;
			set;
		}

		protected void OnPropertyChanged(string name)
		{
			bool flag = this.PropertyChanged != null;
			if (flag)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(name));
			}
		}

		public SearchData(string d, string n, string S, string C)
		{
			this.ID = d;
			this.Name = n;
			this.Number = S;
			this.Channel = C;
		}

		public SearchData()
		{
		}
	}
}
