using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Bike.Other
{
	public class RidingData
	{
		private string id;

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

		public string Name
		{
			get;
			set;
		}

		public string TrackName
		{
			get;
			set;
		}

		public string Time1
		{
			get;
			set;
		}

		public string Time2
		{
			get;
			set;
		}

		public string Distance
		{
			get;
			set;
		}

		public string Velocity
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

		public RidingData(string d, string n, string T, string t1, string t2, string distance, string velocity)
		{
			this.ID = d;
			this.Name = n;
			this.TrackName = T;
			this.Time1 = t1;
			this.Time2 = t2;
			this.Distance = distance;
			this.Velocity = velocity;
		}

		public RidingData()
		{
		}
	}
}
