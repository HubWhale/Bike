using FinalClient;
using Sensor_Windows;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using Bike.Other;

namespace Bike
{
    public partial class MainWindow : Window, INotifyPropertyChanged, IComponentConnector
	{
		private Uri rejisterPage;

		private Uri devivePage;

		private Uri recordPage;

		private Uri settingPage;

		private Uri noraml_training;

		private Uri video_training;

		private Uri unity3D_training;

		private int prepageIndex = 0;

		private int pageIndex = 0;

		private Visibility winVisibility = Visibility.Visible;

        //internal new MainWindow Icon;

        //internal new Image Title;

        //internal Button Cloce_button;

        //internal Button Min_button;

        //internal Grid LeftPane;

        //internal Button rejister;

        //internal Image rejister_ico;

        //internal TextBlock rejister_t;

        //internal Button devices;

        //internal ImageBrush devices_bg;

        //internal Image devices_ico;

        //internal TextBlock devices_t;

        //internal Button records;

        //internal ImageBrush records_bg;

        //internal Image records_ico;

        //internal TextBlock records_t;

        //internal Button normal;

        //internal ImageBrush normal_bg;

        //internal Image normal_ico;

        //internal TextBlock normal_t;

        //internal Button video;

        //internal ImageBrush video_bg;

        //internal Image video_ico;

        //internal TextBlock video_t;

        //internal Button unity3D;

        //internal ImageBrush unity3D_bg;

        //internal Image unity3D_ico;

        //internal TextBlock unity3D_t;

        //internal Frame rootFrame;

        //private bool _contentLoaded;

		public event PropertyChangedEventHandler PropertyChanged;

		public int PageIndex
		{
			get
			{
				return this.pageIndex;
			}
			set
			{
				bool flag = value != this.pageIndex;
				if (flag)
				{
					this.pageIndex = value;
					bool flag2 = GlobalData.NormalTrainingflag || GlobalData.VideoTrainingflag || GlobalData.Unity3DTrainingflag;
					if (!flag2)
					{
						switch (this.pageIndex)
						{
						case 0:
							this.rootFrame.Source = this.rejisterPage;
							break;
						case 1:
							this.rootFrame.Source = this.devivePage;
							break;
						case 2:
							this.rootFrame.Source = this.recordPage;
							break;
						case 3:
							this.rootFrame.Source = this.noraml_training;
							break;
						case 4:
							this.rootFrame.Source = this.video_training;
							break;
						case 5:
							this.rootFrame.Source = this.unity3D_training;
							break;
						}
						this.ButtonChoosed(this.prepageIndex, this.pageIndex);
						this.prepageIndex = this.pageIndex;
						this.OnPropertyChanged("PageIndex");
					}
				}
			}
		}

		public Visibility WinVisibility
		{
			get
			{
				return this.winVisibility;
			}
			set
			{
				bool flag = value != this.winVisibility;
				if (flag)
				{
					this.winVisibility = value;
				}
				bool flag2 = this.PropertyChanged != null;
				if (flag2)
				{
					this.PropertyChanged(this, new PropertyChangedEventArgs("WinVisibility"));
				}
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

		private void ButtonChoosed(int prepageIndex, int pageIndex)
		{
			switch (prepageIndex)
			{
			case 1:
				this.devices.Background.Opacity = 1.0;
				break;
			case 2:
				this.records.Background.Opacity = 1.0;
				break;
			case 3:
				this.normal.Background.Opacity = 1.0;
				break;
			case 4:
				this.video.Background.Opacity = 1.0;
				break;
			case 5:
				this.unity3D.Background.Opacity = 1.0;
				break;
			}
			switch (pageIndex)
			{
			case 1:
				this.devices.Background.Opacity = 1.0;
				break;
			case 2:
				this.records.Background.Opacity = 1.0;
				break;
			case 3:
				this.normal.Background.Opacity = 1.0;
				break;
			case 4:
				this.video.Background.Opacity = 1.0;
				break;
			case 5:
				this.unity3D.Background.Opacity = 1.0;
				break;
			}
		}

		public MainWindow()
		{
			this.InitializeComponent();
			Device.Init();
			GlobalData.Init();
			this.rejisterPage = new Uri("Other\\RejisterPage.xaml", UriKind.Relative);
			this.devivePage = new Uri("Other\\DevicePage.xaml", UriKind.Relative);
			this.recordPage = new Uri("Other\\RecordPage.xaml", UriKind.Relative);
			this.settingPage = new Uri("Other\\SettingPage.xaml", UriKind.Relative);
			this.noraml_training = new Uri("Trainings\\NormalPage.xaml", UriKind.Relative);
			this.video_training = new Uri("Trainings\\VideoPage.xaml", UriKind.Relative);
			this.unity3D_training = new Uri("Trainings\\U3Dpage.xaml", UriKind.Relative);
			this.ButtonChoosed(this.prepageIndex, this.pageIndex);
			XMLHelper.Init();
			GlobalData.language = XMLHelper.getLanguage();
			MultiLanguage.Init(GlobalData.language);
			this.languageInit();
			base.DataContext = this;
			GlobalData.mainWindow = this;
			RejisterPage.username += new RejisterPage.RegisterW(this.Username);
			bool flag = GlobalData.speedChannel == 9 && GlobalData.ANT_Deviceflag;
			if (flag)
			{
				bool flag2 = XMLHelper.SpeedDeviceNumberGetNum() > 0;
				if (flag2)
				{
					string SpeedDevice = XMLHelper.SpeedDeviceNumberReader("SpeedDeviceNumber", 0);
					bool flag3 = SpeedDevice != null;
					if (flag3)
					{
						GlobalData.speedequipment = SpeedDevice;
						string[] sArray = SpeedDevice.Split(new char[]
						{
							':'
						});
						string SpeedDevice2 = sArray[1];
						ANT_heart_rate.antBinding(SpeedDevice2, null, null);
						bool aNT_Deviceflag = GlobalData.ANT_Deviceflag;
						if (!aNT_Deviceflag)
						{
							return;
						}
						GlobalData.ANT_Connectflag = true;
					}
				}
			}
			bool flag4 = GlobalData.cadenceChannel == 9 && GlobalData.ANT_Deviceflag;
			if (flag4)
			{
				bool flag5 = XMLHelper.CadenceDeviceNumberGetNum() > 0;
				if (flag5)
				{
					string CadenceDevice = XMLHelper.CadenceDeviceNumberReader("CadenceDeviceNumber", 0);
					bool flag6 = CadenceDevice != null;
					if (flag6)
					{
						string[] sArray2 = CadenceDevice.Split(new char[]
						{
							':'
						});
						string CadenceDevice2 = sArray2[1];
						ANT_heart_rate.antBinding(null, CadenceDevice2, null);
						bool aNT_Deviceflag2 = GlobalData.ANT_Deviceflag;
						if (!aNT_Deviceflag2)
						{
							return;
						}
						GlobalData.cadenceequipment = CadenceDevice;
					}
				}
			}
			bool flag7 = GlobalData.heartChannel == 9 && GlobalData.ANT_Deviceflag;
			if (flag7)
			{
				bool flag8 = XMLHelper.HeartRateDeviceNumberGetNum() > 0;
				if (flag8)
				{
					string HeartRateDevice = XMLHelper.HeartRateDeviceNumberReader("HeartRateDeviceNumber", 0);
					bool flag9 = HeartRateDevice != null;
					if (flag9)
					{
						string[] sArray3 = HeartRateDevice.Split(new char[]
						{
							':'
						});
						string HeartRateDevice2 = sArray3[1];
						ANT_heart_rate.antBinding(null, null, HeartRateDevice2);
						bool aNT_Deviceflag3 = GlobalData.ANT_Deviceflag;
						if (aNT_Deviceflag3)
						{
							GlobalData.heartRateequipment = HeartRateDevice;
						}
					}
				}
			}
		}

		private void languageInit()
		{
			this.rejister_t.Text = MultiLanguage.rejister_t;
			this.devices_t.Text = MultiLanguage.devices_t;
			this.records_t.Text = MultiLanguage.records_t;
			this.normal_t.Text = MultiLanguage.normal_t;
			this.video_t.Text = MultiLanguage.video_t;
			this.unity3D_t.Text = MultiLanguage.unity3D_t;
		}

		private void devices_Click(object sender, RoutedEventArgs e)
		{
			this.PageIndex = 1;
		}

		private void records_Click(object sender, RoutedEventArgs e)
		{
			this.PageIndex = 2;
		}

		private void normal_Click(object sender, RoutedEventArgs e)
		{
			this.PageIndex = 3;
		}

		private void video_Click(object sender, RoutedEventArgs e)
		{
			this.PageIndex = 4;
		}

		private void unity3D_Click(object sender, RoutedEventArgs e)
		{
			this.PageIndex = 5;
		}

		private void Window_Closed(object sender, EventArgs e)
		{
			RejisterPage.username -= new RejisterPage.RegisterW(this.Username);
		}

		private void rejister_Click(object sender, RoutedEventArgs e)
		{
			this.PageIndex = 0;
		}

		private void button_Click(object sender, RoutedEventArgs e)
		{
			bool flag = !GlobalData.Rememberflag;
			if (flag)
			{
				XMLHelper.UsersDeleter("UsersFile");
			}
			bool isConnected = Connection.isConnected;
			if (isConnected)
			{
				Connection.Close();
			}
			Environment.Exit(0);
		}

		private void Min_button_Click(object sender, RoutedEventArgs e)
		{
			base.WindowState = WindowState.Minimized;
		}

		private void TitleBar_MouseMove(object sender, MouseEventArgs e)
		{
			bool flag = e.LeftButton == MouseButtonState.Pressed;
			if (flag)
			{
				base.DragMove();
			}
		}

		private void Window_KeyUp(object sender, KeyEventArgs e)
		{
			bool flag = e.Key == Key.Prior;
			if (flag)
			{
				int num = this.PageIndex;
				this.PageIndex = num - 1;
			}
			bool flag2 = e.Key == Key.Next;
			if (flag2)
			{
				int num = this.PageIndex;
				this.PageIndex = num + 1;
			}
			bool flag3 = e.Source != this;
			if (!flag3)
			{
				bool flag4 = e.Key == Key.Up;
				if (flag4)
				{
					bool flag5 = this.PageIndex > 0;
					if (flag5)
					{
						int num = this.PageIndex;
						this.PageIndex = num - 1;
					}
					else
					{
						this.PageIndex = 5;
					}
				}
				bool flag6 = e.Key == Key.Down;
				if (flag6)
				{
					bool flag7 = this.PageIndex < 5;
					if (flag7)
					{
						int num = this.PageIndex;
						this.PageIndex = num + 1;
					}
					else
					{
						this.PageIndex = 0;
					}
				}
			}
		}

		private void Username()
		{
			bool rejisterflag = GlobalData.Rejisterflag;
			if (rejisterflag)
			{
				base.Dispatcher.Invoke(delegate
				{
					this.rejister_t.Text = GlobalData.UserName;
				});
			}
			else
			{
				base.Dispatcher.Invoke(delegate
				{
					this.rejister_t.Text = MultiLanguage.rejister_t;
				});
			}
		}

        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        //public void InitializeComponent()
        //{
        //    bool contentLoaded = this._contentLoaded;
        //    if (!contentLoaded)
        //    {
        //        this._contentLoaded = true;
        //        Uri resourceLocater = new Uri("/Bike;component/mainwindow.xaml", UriKind.Relative);
        //        Application.LoadComponent(this, resourceLocater);
        //    }
        //}

        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
        //void IComponentConnector.Connect(int connectionId, object target)
        //{
        //    switch (connectionId)
        //    {
        //    case 1:
        //        this.Icon = (MainWindow)target;
        //        this.Icon.Closed += new EventHandler(this.Window_Closed);
        //        this.Icon.KeyUp += new KeyEventHandler(this.Window_KeyUp);
        //        break;
        //    case 2:
        //        ((Grid)target).MouseMove += new MouseEventHandler(this.TitleBar_MouseMove);
        //        break;
        //    case 3:
        //        this.Title = (Image)target;
        //        break;
        //    case 4:
        //        this.Cloce_button = (Button)target;
        //        this.Cloce_button.Click += new RoutedEventHandler(this.button_Click);
        //        break;
        //    case 5:
        //        this.Min_button = (Button)target;
        //        this.Min_button.Click += new RoutedEventHandler(this.Min_button_Click);
        //        break;
        //    case 6:
        //        this.LeftPane = (Grid)target;
        //        break;
        //    case 7:
        //        this.rejister = (Button)target;
        //        this.rejister.Click += new RoutedEventHandler(this.rejister_Click);
        //        break;
        //    case 8:
        //        this.rejister_ico = (Image)target;
        //        break;
        //    case 9:
        //        this.rejister_t = (TextBlock)target;
        //        break;
        //    case 10:
        //        this.devices = (Button)target;
        //        this.devices.Click += new RoutedEventHandler(this.devices_Click);
        //        break;
        //    case 11:
        //        this.devices_bg = (ImageBrush)target;
        //        break;
        //    case 12:
        //        this.devices_ico = (Image)target;
        //        break;
        //    case 13:
        //        this.devices_t = (TextBlock)target;
        //        break;
        //    case 14:
        //        this.records = (Button)target;
        //        this.records.Click += new RoutedEventHandler(this.records_Click);
        //        break;
        //    case 15:
        //        this.records_bg = (ImageBrush)target;
        //        break;
        //    case 16:
        //        this.records_ico = (Image)target;
        //        break;
        //    case 17:
        //        this.records_t = (TextBlock)target;
        //        break;
        //    case 18:
        //        this.normal = (Button)target;
        //        this.normal.Click += new RoutedEventHandler(this.normal_Click);
        //        break;
        //    case 19:
        //        this.normal_bg = (ImageBrush)target;
        //        break;
        //    case 20:
        //        this.normal_ico = (Image)target;
        //        break;
        //    case 21:
        //        this.normal_t = (TextBlock)target;
        //        break;
        //    case 22:
        //        this.video = (Button)target;
        //        this.video.Click += new RoutedEventHandler(this.video_Click);
        //        break;
        //    case 23:
        //        this.video_bg = (ImageBrush)target;
        //        break;
        //    case 24:
        //        this.video_ico = (Image)target;
        //        break;
        //    case 25:
        //        this.video_t = (TextBlock)target;
        //        break;
        //    case 26:
        //        this.unity3D = (Button)target;
        //        this.unity3D.Click += new RoutedEventHandler(this.unity3D_Click);
        //        break;
        //    case 27:
        //        this.unity3D_bg = (ImageBrush)target;
        //        break;
        //    case 28:
        //        this.unity3D_ico = (Image)target;
        //        break;
        //    case 29:
        //        this.unity3D_t = (TextBlock)target;
        //        break;
        //    case 30:
        //        this.rootFrame = (Frame)target;
        //        break;
        //    default:
        //        this._contentLoaded = true;
        //        break;
        //    }
        //}
	}
}
