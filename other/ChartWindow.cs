using OxyPlot;
using OxyPlot.Wpf;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Bike.Other
{
    public partial class ChartWindow : Window, IComponentConnector
	{
		private IList<DataPoint> dataList;

		private string Training;

		private string SpeedData;

        //internal Button delete;

        //internal Plot speedChart;

        //internal TextBlock recordName;

        //private bool _contentLoaded;

		public event PropertyChangedEventHandler PropertyChanged;

		public IList<DataPoint> DataList
		{
			get
			{
				return this.dataList;
			}
			private set
			{
				this.dataList = value;
				this.OnPropertyChanged("DataList");
			}
		}

		protected void OnPropertyChanged(string name)
		{
			bool flag = this.PropertyChanged != null;
			if (flag)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(name));
			}
		}

		public ChartWindow()
		{
			this.InitializeComponent();
			this.languageInit();
			this.recordName.Text = GlobalData.RecordName;
			this.dataList = new List<DataPoint>();
			base.DataContext = this;
			string start = XMLHelper.RecordReader(GlobalData.RecordName, 3);
			string FileName = Environment.CurrentDirectory + "\\" + start + ".xml";
			this.Training = XMLHelper.CurrentReader(FileName, GlobalData.RecordName, 7);
			bool flag = this.Training != null;
			if (flag)
			{
				int i = 0;
				for (int j = 0; j < this.Training.Length / 7; j++)
				{
					this.SpeedData = this.Training.Substring(i, 6);
					double temp = Convert.ToDouble(this.SpeedData) / 1000.0;
					this.dataList.Add(new DataPoint((double)j, temp));
					i += 7;
				}
				this.speedChart.InvalidatePlot(true);
			}
		}

		public void languageInit()
		{
			this.delete.Content = MultiLanguage.delete;
			base.Title = MultiLanguage.ChartWindow_Title_t;
		}

		private void delete_Click(object sender, RoutedEventArgs e)
		{
			string FileName = XMLHelper.RecordReader(GlobalData.RecordName, 3);
			string NowFileName = Environment.CurrentDirectory + "\\" + FileName + ".xml";
			XMLHelper.RecordDeleter(GlobalData.RecordName);
			bool flag = File.Exists(NowFileName);
			if (flag)
			{
				File.Delete(NowFileName);
			}
			base.Close();
		}

        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        //public void InitializeComponent()
        //{
        //    bool contentLoaded = this._contentLoaded;
        //    if (!contentLoaded)
        //    {
        //        this._contentLoaded = true;
        //        Uri resourceLocater = new Uri("/Bike;component/other/chartwindow.xaml", UriKind.Relative);
        //        Application.LoadComponent(this, resourceLocater);
        //    }
        //}

        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
        //void IComponentConnector.Connect(int connectionId, object target)
        //{
        //    switch (connectionId)
        //    {
        //    case 1:
        //        this.delete = (Button)target;
        //        this.delete.Click += new RoutedEventHandler(this.delete_Click);
        //        break;
        //    case 2:
        //        this.speedChart = (Plot)target;
        //        break;
        //    case 3:
        //        this.recordName = (TextBlock)target;
        //        break;
        //    default:
        //        this._contentLoaded = true;
        //        break;
        //    }
        //}
	}
}
