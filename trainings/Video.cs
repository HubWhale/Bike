using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Bike.Trainings
{
	public class Video : INotifyPropertyChanged
	{
		private string name;

		private string imagePath;

		private int diffculty = 0;

		private string difficultyStr = "0";

		private string note;

		public event PropertyChangedEventHandler PropertyChanged;

		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				bool flag = this.name != value;
				if (flag)
				{
					this.name = value;
					bool flag2 = this.PropertyChanged != null;
					if (flag2)
					{
						this.PropertyChanged(this, new PropertyChangedEventArgs("Name"));
					}
				}
			}
		}

		public string ImagePath
		{
			get
			{
				return this.imagePath;
			}
			set
			{
				bool flag = this.imagePath != value;
				if (flag)
				{
					this.imagePath = value;
					bool flag2 = this.PropertyChanged != null;
					if (flag2)
					{
						this.PropertyChanged(this, new PropertyChangedEventArgs("ImagePath"));
					}
				}
			}
		}

		public DateTime Time
		{
			get;
			set;
		}

		public double Distance
		{
			get;
			set;
		}

		public int Diffculty
		{
			get
			{
				return this.diffculty;
			}
			set
			{
				bool flag = this.diffculty != value;
				if (flag)
				{
					this.diffculty = value;
					this.DiffcultyStr = this.diffculty.ToString();
				}
			}
		}

		public string DiffcultyStr
		{
			get
			{
				return this.difficultyStr;
			}
			set
			{
				bool flag = this.difficultyStr != value;
				if (flag)
				{
					this.difficultyStr = value;
					bool flag2 = this.PropertyChanged != null;
					if (flag2)
					{
						this.PropertyChanged(this, new PropertyChangedEventArgs("DiffcultyStr"));
					}
				}
			}
		}

		public string VideoPath
		{
			get;
			set;
		}

		public string SlopePath
		{
			get;
			set;
		}

		public string Note
		{
			get
			{
				return this.note;
			}
			set
			{
				bool flag = value != this.note;
				if (flag)
				{
					this.note = value;
					bool flag2 = this.PropertyChanged != null;
					if (flag2)
					{
						this.PropertyChanged(this, new PropertyChangedEventArgs("Note"));
					}
				}
			}
		}
	}
}
