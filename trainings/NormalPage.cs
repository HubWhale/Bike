using OxyPlot;
using OxyPlot.Wpf;
using Sensor_Windows;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Threading;
using Bike.Other;

namespace Bike.Trainings
{
    public partial class NormalPage : Page, INotifyPropertyChanged, IComponentConnector
	{
		private double speed;

		private double power;

		private double distance;

		private double energy;

		private double velocity;

		private double heart;

		private double tramp;

		private string HeartRate;

		private string SpeedDate;

		private string TrampDate;

		private int time;

		private int gear;

		private bool bPause = false;

		private IList<DataPoint> speedDataList;

		private IList<DataPoint> powerDataList;

		private DispatcherTimer timer = new DispatcherTimer();

        //internal TabControl tabControl;

        //internal TextBlock km;

        //internal TextBox textBox1;

        //internal TextBox textBox2;

        //internal Plot speedChar;

        //internal TextBlock W;

        //internal TextBox textBox;

        //internal TextBox textBox3;

        //internal Plot powerChar;

        //internal TextBlock Speed;

        //internal TextBlock speedText;

        //internal TextBlock kmh;

        //internal TextBlock Power;

        //internal TextBlock powerText;

        //internal TextBlock w;

        //internal TextBlock Distance;

        //internal TextBlock distanceText;

        //internal TextBlock Km;

        //internal TextBlock Energy;

        //internal TextBlock energyText;

        //internal TextBlock Kcal;

        //internal TextBlock Time;

        //internal TextBlock timeText;

        //internal Button Start;

        //internal Button Stop;

        //internal Button Add;

        //internal TextBlock gearText;

        //internal Button Miuns;

        //internal TextBlock textBlock;

        //private bool _contentLoaded;

		public event PropertyChangedEventHandler PropertyChanged;

		public IList<DataPoint> SpeedDataList
		{
			get
			{
				return this.speedDataList;
			}
			private set
			{
				this.speedDataList = value;
				this.OnPropertyChanged("SpeedDataList");
			}
		}

		public IList<DataPoint> PowerDataList
		{
			get
			{
				return this.powerDataList;
			}
			private set
			{
				this.powerDataList = value;
				this.OnPropertyChanged("PowerDataList");
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

		public NormalPage()
		{
			this.InitializeComponent();
			this.Data_Init();
			this.languageInit();
			this.speedDataList = new List<DataPoint>();
			this.powerDataList = new List<DataPoint>();
			base.DataContext = this;
		}

		private void Data_Init()
		{
			this.speed = 0.0;
			this.power = 0.0;
			this.distance = 0.0;
			this.energy = 0.0;
			this.time = 0;
			this.gear = 5;
			this.speedText.Text = "000.0";
			this.powerText.Text = "0000.0";
			this.distanceText.Text = "0000.0";
			this.energyText.Text = "0000.0";
			this.timeText.Text = this.timeGet(this.time);
			this.gearText.Text = this.gear.ToString();
		}

		private void languageInit()
		{
			this.textBlock.Text = MultiLanguage.Normal_t;
			this.Speed.Text = MultiLanguage.Speed_t;
			this.Power.Text = MultiLanguage.Power_t;
			this.Distance.Text = MultiLanguage.Distance_t;
			this.Energy.Text = MultiLanguage.Energy_t;
			this.Time.Text = MultiLanguage.Time_t;
			this.Start.Content = MultiLanguage.Start;
			this.Stop.Content = MultiLanguage.Stop;
		}

		private void timer_Init()
		{
			this.timer.Interval = new TimeSpan(0, 0, 1);
			this.timer.Tick += new EventHandler(this.timer_Tick);
			this.timer.Start();
			DateTime NOW = DateTime.Now;
			GlobalData.StartTime = Convert.ToInt64(UTCTimeOperate.ConvertDateTimeInt(NOW)).ToString();
			GlobalData.TrackName = null;
			GlobalData.Time = 0L;
			GlobalData.TrainingTime = "00:00:00";
			GlobalData.Distance = "0";
			GlobalData.Velocity = "0";
		}

		private void timer_Uninit()
		{
			this.timer.Tick -= new EventHandler(this.timer_Tick);
			this.timer.Stop();
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			this.time++;
			this.timeText.Text = this.timeGet(this.time);
			bool connectflag = GlobalData.Connectflag;
			if (connectflag)
			{
				GlobalData.Time = (long)this.time;
				GlobalData.SpeedDatas[(int)(checked((IntPtr)GlobalData.Time))] = (this.speed * 1000.0).ToString("#000000") + ";";
				checked
				{
					GlobalData.HeartRateData[(int)((IntPtr)GlobalData.Time)] = this.heart.ToString("#000") + ";";
					GlobalData.CadenceData[(int)((IntPtr)GlobalData.Time)] = this.tramp.ToString("#000") + ";";
					GlobalData.PowerData[(int)((IntPtr)GlobalData.Time)] = this.power.ToString("#0000.0") + ";";
					this.Refresh();
				}
			}
			else
			{
				MessageBox.Show(MultiLanguage.Warn5, MultiLanguage.Warn16);
				this.DisConnect();
			}
		}

		private void Refresh()
		{
			bool flag = Device.SpeedData == null;
			if (flag)
			{
				this.speed = 0.0;
			}
			else
			{
				try
				{
					this.speed = Convert.ToDouble(Device.SpeedData) / 10.0;
				}
				catch
				{
					this.speed = 0.0;
				}
			}
			this.speedText.Text = this.speed.ToString("#000.0");
			bool flag2 = this.speed != 0.0;
			if (flag2)
			{
				this.power = GlobalData.getPower(this.speed, this.gear);
			}
			else
			{
				this.power = 0.0;
			}
			this.powerText.Text = this.power.ToString("#0000.0");
			this.distance += this.speed * 10.0 * 25.0 / 9.0 / 100000.0;
			this.distanceText.Text = this.distance.ToString("#0000.0");
			GlobalData.Distance = this.distance.ToString("#0.0");
			bool flag3 = GlobalData.Time == 0L;
			if (flag3)
			{
				this.velocity = 0.0;
			}
			else
			{
				this.velocity = this.distance * 3600.0 / (double)GlobalData.Time;
			}
			GlobalData.Velocity = this.velocity.ToString("#0.0");
			this.energy += this.power * 0.239 / 1000.0;
			this.energyText.Text = this.energy.ToString("#0000.0");
			this.SpeedDataList.Add(new DataPoint((double)this.time, this.speed));
			this.PowerDataList.Add(new DataPoint((double)this.time, this.power));
			this.speedChar.InvalidatePlot(true);
			this.powerChar.InvalidatePlot(true);
			bool flag4 = GlobalData.heartChannel >= 0 && GlobalData.heartChannel < 8;
			if (flag4)
			{
				switch (GlobalData.heartChannel)
				{
				case 0:
					this.HeartRate = ANT_heart_rate.HR0;
					break;
				case 1:
					this.HeartRate = ANT_heart_rate.HR1;
					break;
				case 2:
					this.HeartRate = ANT_heart_rate.HR2;
					break;
				case 3:
					this.HeartRate = ANT_heart_rate.HR3;
					break;
				case 4:
					this.HeartRate = ANT_heart_rate.HR4;
					break;
				case 5:
					this.HeartRate = ANT_heart_rate.HR5;
					break;
				case 6:
					this.HeartRate = ANT_heart_rate.HR6;
					break;
				case 7:
					this.HeartRate = ANT_heart_rate.HR7;
					break;
				}
			}
			bool flag5 = this.HeartRate == null;
			if (flag5)
			{
				this.heart = 0.0;
			}
			else
			{
				try
				{
					this.heart = Convert.ToDouble(this.HeartRate);
				}
				catch
				{
					this.heart = 0.0;
				}
			}
			bool flag6 = GlobalData.cadenceChannel >= 0 && GlobalData.cadenceChannel < 8;
			if (flag6)
			{
				switch (GlobalData.cadenceChannel)
				{
				case 0:
					this.TrampDate = ANT_heart_rate.BC0;
					break;
				case 1:
					this.TrampDate = ANT_heart_rate.BC1;
					break;
				case 2:
					this.TrampDate = ANT_heart_rate.BC2;
					break;
				case 3:
					this.TrampDate = ANT_heart_rate.BC3;
					break;
				case 4:
					this.TrampDate = ANT_heart_rate.BC4;
					break;
				case 5:
					this.TrampDate = ANT_heart_rate.BC5;
					break;
				case 6:
					this.TrampDate = ANT_heart_rate.BC6;
					break;
				case 7:
					this.TrampDate = ANT_heart_rate.BC7;
					break;
				}
			}
			bool flag7 = this.TrampDate == null;
			if (flag7)
			{
				this.tramp = 0.0;
			}
			else
			{
				try
				{
					this.tramp = Convert.ToDouble(this.TrampDate);
				}
				catch
				{
					this.tramp = 0.0;
				}
			}
		}

		private string timeGet(int time)
		{
			bool flag = time >= 86400;
			if (flag)
			{
				time = 0;
			}
			return string.Concat(new string[]
			{
				(time / 3600).ToString("#00"),
				":",
				(time % 3600 / 60).ToString("#00"),
				":",
				(time % 3600 % 60).ToString("#00")
			});
		}

		private void Start_Click(object sender, RoutedEventArgs e)
		{
			bool flag = !this.bPause;
			if (flag)
			{
				bool flag2 = !GlobalData.Connectflag && !GlobalData.ANT_Connectflag;
				if (flag2)
				{
					MessageBox.Show(MultiLanguage.Warn6, MultiLanguage.Warn_t, MessageBoxButton.OK, MessageBoxImage.Exclamation);
					return;
				}
				this.bPause = true;
				this.timer_Init();
				bool connectflag = GlobalData.Connectflag;
				if (connectflag)
				{
					GlobalData.SendGear(this.gear);
				}
				DateTime NOW = DateTime.Now;
				GlobalData.StartTime = Convert.ToInt64(UTCTimeOperate.ConvertDateTimeInt(NOW)).ToString();
				this.Start.Content = MultiLanguage.Pause;
			}
			else
			{
				this.bPause = false;
				this.timer_Uninit();
				this.Start.Content = MultiLanguage.Start;
			}
			bool flag3 = !GlobalData.NormalTrainingflag;
			if (flag3)
			{
				GlobalData.NormalTrainingflag = true;
			}
		}

		private void Add_Click(object sender, RoutedEventArgs e)
		{
			bool flag = this.gear < 17;
			if (flag)
			{
				this.gear++;
			}
			this.gearText.Text = this.gear.ToString();
			GlobalData.SendGear(this.gear);
		}

		private void Stop_Click(object sender, RoutedEventArgs e)
		{
			this.timer_Uninit();
			this.Data_Init();
			bool flag = this.bPause;
			if (flag)
			{
				this.bPause = false;
				this.Start.Content = MultiLanguage.Start;
			}
			this.speedDataList.Clear();
			this.powerDataList.Clear();
			GlobalData.NormalTrainingflag = false;
			bool flag2 = GlobalData.Time >= 1L;
			if (flag2)
			{
				SaveWindow sw = new SaveWindow();
				sw.ShowDialog();
			}
		}

		private void Minus_Click(object sender, RoutedEventArgs e)
		{
			bool flag = this.gear > 1;
			if (flag)
			{
				this.gear--;
			}
			this.gearText.Text = this.gear.ToString();
			GlobalData.SendGear(this.gear);
		}

		private void DisConnect()
		{
			this.timer_Uninit();
			this.Data_Init();
			bool flag = this.bPause;
			if (flag)
			{
				this.bPause = false;
				this.Start.Content = MultiLanguage.Start;
			}
			this.speedDataList.Clear();
			this.powerDataList.Clear();
			GlobalData.NormalTrainingflag = false;
			SaveWindow sw = new SaveWindow();
			sw.ShowDialog();
		}

        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        //public void InitializeComponent()
        //{
        //    bool contentLoaded = this._contentLoaded;
        //    if (!contentLoaded)
        //    {
        //        this._contentLoaded = true;
        //        Uri resourceLocater = new Uri("/Bike;component/trainings/normalpage.xaml", UriKind.Relative);
        //        Application.LoadComponent(this, resourceLocater);
        //    }
        //}

        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
        //void IComponentConnector.Connect(int connectionId, object target)
        //{
        //    switch (connectionId)
        //    {
        //    case 1:
        //        this.tabControl = (TabControl)target;
        //        break;
        //    case 2:
        //        this.km = (TextBlock)target;
        //        break;
        //    case 3:
        //        this.textBox1 = (TextBox)target;
        //        break;
        //    case 4:
        //        this.textBox2 = (TextBox)target;
        //        break;
        //    case 5:
        //        this.speedChar = (Plot)target;
        //        break;
        //    case 6:
        //        this.W = (TextBlock)target;
        //        break;
        //    case 7:
        //        this.textBox = (TextBox)target;
        //        break;
        //    case 8:
        //        this.textBox3 = (TextBox)target;
        //        break;
        //    case 9:
        //        this.powerChar = (Plot)target;
        //        break;
        //    case 10:
        //        this.Speed = (TextBlock)target;
        //        break;
        //    case 11:
        //        this.speedText = (TextBlock)target;
        //        break;
        //    case 12:
        //        this.kmh = (TextBlock)target;
        //        break;
        //    case 13:
        //        this.Power = (TextBlock)target;
        //        break;
        //    case 14:
        //        this.powerText = (TextBlock)target;
        //        break;
        //    case 15:
        //        this.w = (TextBlock)target;
        //        break;
        //    case 16:
        //        this.Distance = (TextBlock)target;
        //        break;
        //    case 17:
        //        this.distanceText = (TextBlock)target;
        //        break;
        //    case 18:
        //        this.Km = (TextBlock)target;
        //        break;
        //    case 19:
        //        this.Energy = (TextBlock)target;
        //        break;
        //    case 20:
        //        this.energyText = (TextBlock)target;
        //        break;
        //    case 21:
        //        this.Kcal = (TextBlock)target;
        //        break;
        //    case 22:
        //        this.Time = (TextBlock)target;
        //        break;
        //    case 23:
        //        this.timeText = (TextBlock)target;
        //        break;
        //    case 24:
        //        this.Start = (Button)target;
        //        this.Start.Click += new RoutedEventHandler(this.Start_Click);
        //        break;
        //    case 25:
        //        this.Stop = (Button)target;
        //        this.Stop.Click += new RoutedEventHandler(this.Stop_Click);
        //        break;
        //    case 26:
        //        this.Add = (Button)target;
        //        this.Add.Click += new RoutedEventHandler(this.Add_Click);
        //        break;
        //    case 27:
        //        this.gearText = (TextBlock)target;
        //        break;
        //    case 28:
        //        this.Miuns = (Button)target;
        //        this.Miuns.Click += new RoutedEventHandler(this.Minus_Click);
        //        break;
        //    case 29:
        //        this.textBlock = (TextBlock)target;
        //        break;
        //    default:
        //        this._contentLoaded = true;
        //        break;
        //    }
        //}
	}
}
