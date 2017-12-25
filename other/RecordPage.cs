using FinalClient;
using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace Bike.Other
{
    public partial class RecordPage : Page, IComponentConnector
	{
		public ObservableCollection<RidingData> ObservableObj;

		private int start = 0;

		private int num = 0;

		private string deleteID = "";

        //internal TabControl tabControl;

        //internal ListView listView;

        //internal TextBlock textBlock;

        //private bool _contentLoaded;

		public RecordPage()
		{
			this.InitializeComponent();
			this.ObservableObj = new ObservableCollection<RidingData>();
			this.dataLoad();
			this.languageInit();
			bool rejisterflag = GlobalData.Rejisterflag;
			if (rejisterflag)
			{
				string UserName = GlobalData.UserName;
				CustomProtocol proto = new CustomProtocol();
				proto.AddString("EC1");
				proto.AddString(UserName);
				Connection.Send(proto);
			}
			Connection.record += new Connection.RegisterW(this.LoginReturn);
			Connection.recordD += new Connection.RegisterW(this.LoginReturnD);
		}

		public void LoginReturn(ProtocolBase proto)
		{
			GlobalData.proto = proto;
			CustomProtocol protocol = (CustomProtocol)proto;
			string str = protocol.GetString(this.start, ref this.start);
			int ret = protocol.GetInt(this.start, ref this.start);
			int num = ret;
			if (num != 0)
			{
				MessageBox.Show(MultiLanguage.Warn26, MultiLanguage.Warn22);
			}
			else
			{
				this.onlinedataLoad(protocol);
				MessageBox.Show(MultiLanguage.Hint7, MultiLanguage.Warn);
				GlobalData.Recordflag = true;
			}
		}

		public void LoginReturnD(ProtocolBase proto)
		{
			CustomProtocol protocol = (CustomProtocol)proto;
			string str = protocol.GetString(this.start, ref this.start);
			int ret = protocol.GetInt(this.start, ref this.start);
			int num = ret;
			if (num != 0)
			{
				MessageBox.Show(MultiLanguage.Warn27, MultiLanguage.Warn22);
			}
			else
			{
				this.onlinedataLoad(protocol);
				MessageBox.Show(MultiLanguage.Hint8, MultiLanguage.Warn);
				CustomProtocol proto2 = new CustomProtocol();
				proto2.AddString("EC1");
				proto2.AddString(GlobalData.UserName);
				Connection.Send(proto2);
				this.DeleteData(this.deleteID);
			}
		}

		private void languageInit()
		{
			this.textBlock.Text = MultiLanguage.Records_t;
		}

		private void dataLoad()
		{
			int tnum = XMLHelper.RecordGetNum();
			this.num = tnum;
			bool flag = tnum > 0;
			if (flag)
			{
				for (int i = 0; i < tnum; i++)
				{
					string temp = XMLHelper.RecordGetName(i);
					RidingData dr = new RidingData();
					dr.ID = (i + 1).ToString();
					dr.Name = temp;
					dr.TrackName = XMLHelper.RecordReader(temp, 0);
					bool flag2 = XMLHelper.RecordReader(temp, 3) == "";
					if (flag2)
					{
						dr.Time1 = null;
					}
					else
					{
						double start = Convert.ToDouble(XMLHelper.RecordReader(temp, 3));
						dr.Time1 = UTCTimeOperate.ConvertIntDatetime(start).ToString();
					}
					dr.Time2 = XMLHelper.RecordReader(temp, 4);
					dr.Distance = XMLHelper.RecordReader(temp, 5);
					dr.Velocity = XMLHelper.RecordReader(temp, 6);
					this.ObservableObj.Add(dr);
				}
			}
			this.listView.DataContext = this.ObservableObj;
		}

		private void DeleteData(string id)
		{
			bool flag = this.ObservableObj == null;
			if (!flag)
			{
				int i2;
				int i;
				for (i = 0; i < this.ObservableObj.Count; i = i2 + 1)
				{
					bool flag2 = this.ObservableObj[i].ID == id;
					if (flag2)
					{
						base.Dispatcher.Invoke(delegate
						{
							this.ObservableObj.RemoveAt(i);
						});
						break;
					}
					i2 = i;
				}
				for (int j = 0; j < this.ObservableObj.Count; j++)
				{
					this.ObservableObj[j].ID = (j + 1).ToString();
				}
				base.Dispatcher.Invoke(delegate
				{
					this.listView.DataContext = null;
					this.listView.DataContext = this.ObservableObj;
				});
			}
		}

		private void onlinedataLoad(ProtocolBase proto)
		{
			CustomProtocol protocol = (CustomProtocol)proto;
			int tnum = protocol.GetInt(this.start, ref this.start);
			bool flag = tnum > 0;
			if (flag)
			{
				for (int i = 0; i < tnum; i++)
				{
					string temp = GlobalData.UserName;
					RidingData dr = new RidingData();
					dr.ID = (this.num + 1 + i).ToString();
					dr.Name = temp;
					dr.TrackName = protocol.GetString(this.start, ref this.start);
					dr.Time1 = protocol.GetString(this.start, ref this.start);
					dr.Time2 = protocol.GetString(this.start, ref this.start);
					dr.Distance = protocol.GetString(this.start, ref this.start);
					dr.Velocity = protocol.GetString(this.start, ref this.start);
					base.Dispatcher.Invoke(delegate
					{
						this.ObservableObj.Add(dr);
					});
				}
			}
		}

		private void listView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			sender = this.listView.SelectedItem;
			RidingData temp = sender as RidingData;
			bool flag = temp == null;
			if (!flag)
			{
				GlobalData.RecordName = temp.Name;
				GlobalData.RecordData = XMLHelper.RecordReader(temp.Name, 5);
				bool flag2 = GlobalData.RecordData != null;
				if (flag2)
				{
					ChartWindow cw = new ChartWindow();
					cw.ShowDialog();
					this.ObservableObj.Clear();
					this.dataLoad();
					bool recordflag = GlobalData.Recordflag;
					if (recordflag)
					{
						this.start = 11;
						this.onlinedataLoad(GlobalData.proto);
					}
				}
			}
		}

		private void RecordDelete_Click(object sender, RoutedEventArgs e)
		{
			sender = this.listView.SelectedItem;
			RidingData temp = sender as RidingData;
			bool flag = temp == null;
			if (!flag)
			{
				bool flag2 = MessageBox.Show(MultiLanguage.Warn24, MultiLanguage.Warn22, MessageBoxButton.YesNo) != MessageBoxResult.Yes;
				if (!flag2)
				{
					GlobalData.RecordName = temp.Name;
					GlobalData.RecordData = XMLHelper.RecordReader(temp.Name, 5);
					string FileName = XMLHelper.RecordReader(temp.Name, 3);
					string NowFileName = Environment.CurrentDirectory + "\\" + FileName + ".xml";
					bool flag3 = GlobalData.RecordData != null;
					if (flag3)
					{
						XMLHelper.RecordDeleter(GlobalData.RecordName);
						bool flag4 = File.Exists(NowFileName);
						if (flag4)
						{
							File.Delete(NowFileName);
						}
						this.ObservableObj.Clear();
						this.dataLoad();
						bool recordflag = GlobalData.Recordflag;
						if (recordflag)
						{
							this.start = 11;
							this.onlinedataLoad(GlobalData.proto);
						}
					}
					else
					{
						int id = Convert.ToInt32(temp.ID) - this.num;
						CustomProtocol proto = new CustomProtocol();
						proto.AddString("EF1");
						proto.AddInt(id);
						Connection.Send(proto);
						this.deleteID = temp.ID;
					}
				}
			}
		}

		private void Page_Unloaded(object sender, RoutedEventArgs e)
		{
			Connection.record -= new Connection.RegisterW(this.LoginReturn);
			Connection.recordD -= new Connection.RegisterW(this.LoginReturnD);
		}

        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        //public void InitializeComponent()
        //{
        //    bool contentLoaded = this._contentLoaded;
        //    if (!contentLoaded)
        //    {
        //        this._contentLoaded = true;
        //        Uri resourceLocater = new Uri("/Bike;component/other/recordpage.xaml", UriKind.Relative);
        //        Application.LoadComponent(this, resourceLocater);
        //    }
        //}

        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
        //void IComponentConnector.Connect(int connectionId, object target)
        //{
        //    switch (connectionId)
        //    {
        //    case 1:
        //        ((RecordPage)target).Unloaded += new RoutedEventHandler(this.Page_Unloaded);
        //        break;
        //    case 2:
        //        this.tabControl = (TabControl)target;
        //        break;
        //    case 3:
        //        this.listView = (ListView)target;
        //        this.listView.MouseDoubleClick += new MouseButtonEventHandler(this.listView_MouseDoubleClick);
        //        break;
        //    case 4:
        //        ((MenuItem)target).Click += new RoutedEventHandler(this.RecordDelete_Click);
        //        break;
        //    case 5:
        //        this.textBlock = (TextBlock)target;
        //        break;
        //    default:
        //        this._contentLoaded = true;
        //        break;
        //    }
        //}
	}
}
