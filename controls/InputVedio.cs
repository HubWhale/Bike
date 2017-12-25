using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using Bike.Trainings;

namespace Bike.Controls
{
	public class InputVedio : Video, INotifyPropertyChanged
	{
		public string VedioExtension;

		public bool CanInput = false;

		private string rateShow;

		private int rate = 0;

		private string note;

		public new event PropertyChangedEventHandler PropertyChanged;

		public string ImageExtension
		{
			get;
			set;
		}

		public string RateShow
		{
			get
			{
				return this.rateShow;
			}
			set
			{
				bool flag = this.rateShow != value;
				if (flag)
				{
					this.rateShow = value;
					bool flag2 = this.PropertyChanged != null;
					if (flag2)
					{
						this.PropertyChanged(this, new PropertyChangedEventArgs("RateShow"));
					}
				}
			}
		}

		public int Rate
		{
			get
			{
				return this.rate;
			}
			set
			{
				bool flag = this.rate != value;
				if (flag)
				{
					this.rate = value;
					this.RateShow = this.rate.ToString() + "%(" + this.CopyCount.ToString() + "/3)";
					bool flag2 = this.PropertyChanged != null;
					if (flag2)
					{
						this.PropertyChanged(this, new PropertyChangedEventArgs("Rate"));
					}
				}
			}
		}

		public int CopyCount
		{
			get;
			set;
		}

		public new string Note
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

		public InputVedio()
		{
			this.CopyCount = 1;
		}

		public bool CopyFile(string fromPath, string toPath, int eachReadLength)
		{
			FileStream fromFile = new FileStream(fromPath, FileMode.Open, FileAccess.Read);
			FileStream toFile = new FileStream(toPath, FileMode.Append, FileAccess.Write);
			bool flag = (long)eachReadLength < fromFile.Length;
			if (flag)
			{
				byte[] buffer = new byte[eachReadLength];
				long copied = 0L;
				int toCopyLength;
				while (copied <= fromFile.Length - (long)eachReadLength)
				{
					toCopyLength = fromFile.Read(buffer, 0, eachReadLength);
					fromFile.Flush();
					toFile.Write(buffer, 0, eachReadLength);
					toFile.Flush();
					toFile.Position = fromFile.Position;
					copied += (long)toCopyLength;
					this.Rate = (int)(copied * 100L / fromFile.Length);
				}
				int left = (int)(fromFile.Length - copied);
				toCopyLength = fromFile.Read(buffer, 0, left);
				fromFile.Flush();
				toFile.Write(buffer, 0, left);
				toFile.Flush();
				this.Rate = 100;
			}
			else
			{
				byte[] buffer2 = new byte[fromFile.Length];
				fromFile.Read(buffer2, 0, buffer2.Length);
				fromFile.Flush();
				toFile.Write(buffer2, 0, buffer2.Length);
				toFile.Flush();
				this.Rate = 100;
			}
			fromFile.Close();
			toFile.Close();
			int copyCount = this.CopyCount;
			this.CopyCount = copyCount + 1;
			return true;
		}

		public void CopyVedio()
		{
			try
			{
				string imagetoPath = GlobalData.ImagePath + "\\" + base.Name + this.ImageExtension;
				string slopetoPath = GlobalData.SlopePath + "\\" + base.Name + ".txt";
				string vediotoPath = GlobalData.VedioPath + "\\" + base.Name + this.VedioExtension;
				bool flag = File.Exists(vediotoPath);
				if (!flag)
				{
					this.CopyFile(base.ImagePath, imagetoPath, 20971520);
					this.CopyFile(base.SlopePath, slopetoPath, 20971520);
					this.CopyFile(base.VideoPath, vediotoPath, 20971520);
					XMLHelper.VedioConfigeWrite(XMLHelper.VedioConfige, this);
				}
			}
			catch (Exception ex_A9)
			{
				this.Note = MultiLanguage.Note4;
			}
		}
	}
}
