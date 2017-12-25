using Sensor_Windows;
using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Threading;

namespace Bike.Other
{
    public partial class SearchWindow : Window, IComponentConnector
	{
		public delegate void RegisterW();

		public ObservableCollection<SearchData> ObservableObj;

		private DispatcherTimer timer = new DispatcherTimer();

		private int N = 0;

        //internal ListView listView;

        //internal Button Centerbutton;

        //private bool _contentLoaded;

		public static event SearchWindow.RegisterW sudu;

		public static event SearchWindow.RegisterW xinlv;

		public static event SearchWindow.RegisterW tapin;

		public SearchWindow()
		{
			this.InitializeComponent();
			this.ObservableObj = new ObservableCollection<SearchData>();
			this.timer_Init();
			this.languageInit();
			this.listView.DataContext = this.ObservableObj;
		}

		private void languageInit()
		{
			this.Centerbutton.Content = MultiLanguage.center;
		}

		private void dataLoad(string name, string number, string channel)
		{
			SearchData dr = new SearchData();
			this.N++;
			dr.ID = Convert.ToString(this.N);
			dr.Name = name;
			dr.Number = number;
			dr.Channel = channel;
			this.ObservableObj.Add(dr);
		}

		private void timer_Init()
		{
			this.timer.Interval = new TimeSpan(0, 0, 1);
			this.timer.Tick += new EventHandler(this.timer_Tick);
			this.timer.Start();
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			switch (GlobalData.deviceStyle)
			{
			case 0:
				this.Refresh16();
				this.Refresh17();
				this.Refresh18();
				this.Refresh19();
				this.Refresh20();
				this.Refresh21();
				this.Refresh22();
				this.Refresh23();
				break;
			case 1:
				this.Refresh8();
				this.Refresh9();
				this.Refresh10();
				this.Refresh11();
				this.Refresh12();
				this.Refresh13();
				this.Refresh14();
				this.Refresh15();
				break;
			case 2:
				this.Refresh0();
				this.Refresh1();
				this.Refresh2();
				this.Refresh3();
				this.Refresh4();
				this.Refresh5();
				this.Refresh6();
				this.Refresh7();
				break;
			}
		}

		private void Refresh0()
		{
			int heart0 = 0;
			bool flag = ANT_heart_rate.HR0 == null;
			if (!flag)
			{
				try
				{
					heart0 = Convert.ToInt32(ANT_heart_rate.HR0);
				}
				catch
				{
					heart0 = 0;
				}
				string number0 = heart0.ToString("#000") + " bpm";
				bool flag2 = GlobalData.speedequipment != null;
				if (flag2)
				{
					bool flag3 = ANT_heart_rate.dn0 == GlobalData.speedequipment.Substring(5, 5);
					if (flag3)
					{
						return;
					}
				}
				bool flag4 = GlobalData.cadenceequipment != null;
				if (flag4)
				{
					bool flag5 = ANT_heart_rate.dn0 == GlobalData.cadenceequipment.Substring(5, 5);
					if (flag5)
					{
						return;
					}
				}
				string name0 = MultiLanguage.XinLv + ANT_heart_rate.dn0;
				int Num = this.ObservableObj.Count;
				for (int i = 0; i < Num; i++)
				{
					bool flag6 = this.ObservableObj[i].Name == name0;
					if (flag6)
					{
						this.ObservableObj[i].Number = number0;
						return;
					}
				}
				this.dataLoad(name0, number0, "0");
			}
		}

		private void Refresh1()
		{
			int heart = 0;
			bool flag = ANT_heart_rate.HR1 == null;
			if (!flag)
			{
				try
				{
					heart = Convert.ToInt32(ANT_heart_rate.HR1);
				}
				catch
				{
					heart = 0;
				}
				string number = heart.ToString("#000") + " bpm";
				bool flag2 = GlobalData.speedequipment != null;
				if (flag2)
				{
					bool flag3 = ANT_heart_rate.dn1 == GlobalData.speedequipment.Substring(5, 5);
					if (flag3)
					{
						return;
					}
				}
				bool flag4 = GlobalData.cadenceequipment != null;
				if (flag4)
				{
					bool flag5 = ANT_heart_rate.dn1 == GlobalData.cadenceequipment.Substring(5, 5);
					if (flag5)
					{
						return;
					}
				}
				string name = MultiLanguage.XinLv + ANT_heart_rate.dn1;
				int Num = this.ObservableObj.Count;
				for (int i = 0; i < Num; i++)
				{
					bool flag6 = this.ObservableObj[i].Name == name;
					if (flag6)
					{
						this.ObservableObj[i].Number = number;
						return;
					}
				}
				this.dataLoad(name, number, "1");
			}
		}

		private void Refresh2()
		{
			int heart2 = 0;
			bool flag = ANT_heart_rate.HR2 == null;
			if (!flag)
			{
				try
				{
					heart2 = Convert.ToInt32(ANT_heart_rate.HR2);
				}
				catch
				{
					heart2 = 0;
				}
				string number2 = heart2.ToString("#000") + " bpm";
				bool flag2 = GlobalData.speedequipment != null;
				if (flag2)
				{
					bool flag3 = ANT_heart_rate.dn2 == GlobalData.speedequipment.Substring(5, 5);
					if (flag3)
					{
						return;
					}
				}
				bool flag4 = GlobalData.cadenceequipment != null;
				if (flag4)
				{
					bool flag5 = ANT_heart_rate.dn2 == GlobalData.cadenceequipment.Substring(5, 5);
					if (flag5)
					{
						return;
					}
				}
				string name2 = MultiLanguage.XinLv + ANT_heart_rate.dn2;
				int Num = this.ObservableObj.Count;
				for (int i = 0; i < Num; i++)
				{
					bool flag6 = this.ObservableObj[i].Name == name2;
					if (flag6)
					{
						this.ObservableObj[i].Number = number2;
						return;
					}
				}
				this.dataLoad(name2, number2, "2");
			}
		}

		private void Refresh3()
		{
			int heart3 = 0;
			bool flag = ANT_heart_rate.HR3 == null;
			if (!flag)
			{
				try
				{
					heart3 = Convert.ToInt32(ANT_heart_rate.HR3);
				}
				catch
				{
					heart3 = 0;
				}
				string number3 = heart3.ToString("#000") + " bpm";
				bool flag2 = GlobalData.speedequipment != null;
				if (flag2)
				{
					bool flag3 = ANT_heart_rate.dn3 == GlobalData.speedequipment.Substring(5, 5);
					if (flag3)
					{
						return;
					}
				}
				bool flag4 = GlobalData.cadenceequipment != null;
				if (flag4)
				{
					bool flag5 = ANT_heart_rate.dn3 == GlobalData.cadenceequipment.Substring(5, 5);
					if (flag5)
					{
						return;
					}
				}
				string name3 = MultiLanguage.XinLv + ANT_heart_rate.dn3;
				int Num = this.ObservableObj.Count;
				for (int i = 0; i < Num; i++)
				{
					bool flag6 = this.ObservableObj[i].Name == name3;
					if (flag6)
					{
						this.ObservableObj[i].Number = number3;
						return;
					}
				}
				this.dataLoad(name3, number3, "3");
			}
		}

		private void Refresh4()
		{
			int heart4 = 0;
			bool flag = ANT_heart_rate.HR4 == null;
			if (!flag)
			{
				try
				{
					heart4 = Convert.ToInt32(ANT_heart_rate.HR4);
				}
				catch
				{
					heart4 = 0;
				}
				string number4 = heart4.ToString("#000") + " bpm";
				bool flag2 = GlobalData.speedequipment != null;
				if (flag2)
				{
					bool flag3 = ANT_heart_rate.dn4 == GlobalData.speedequipment.Substring(5, 5);
					if (flag3)
					{
						return;
					}
				}
				bool flag4 = GlobalData.cadenceequipment != null;
				if (flag4)
				{
					bool flag5 = ANT_heart_rate.dn4 == GlobalData.cadenceequipment.Substring(5, 5);
					if (flag5)
					{
						return;
					}
				}
				string name4 = MultiLanguage.XinLv + ANT_heart_rate.dn4;
				int Num = this.ObservableObj.Count;
				for (int i = 0; i < Num; i++)
				{
					bool flag6 = this.ObservableObj[i].Name == name4;
					if (flag6)
					{
						this.ObservableObj[i].Number = number4;
						return;
					}
				}
				this.dataLoad(name4, number4, "4");
			}
		}

		private void Refresh5()
		{
			int heart5 = 0;
			bool flag = ANT_heart_rate.HR5 == null;
			if (!flag)
			{
				try
				{
					heart5 = Convert.ToInt32(ANT_heart_rate.HR5);
				}
				catch
				{
					heart5 = 0;
				}
				string number5 = heart5.ToString("#000") + " bpm";
				bool flag2 = GlobalData.speedequipment != null;
				if (flag2)
				{
					bool flag3 = ANT_heart_rate.dn5 == GlobalData.speedequipment.Substring(5, 5);
					if (flag3)
					{
						return;
					}
				}
				bool flag4 = GlobalData.cadenceequipment != null;
				if (flag4)
				{
					bool flag5 = ANT_heart_rate.dn5 == GlobalData.cadenceequipment.Substring(5, 5);
					if (flag5)
					{
						return;
					}
				}
				string name5 = MultiLanguage.XinLv + ANT_heart_rate.dn5;
				int Num = this.ObservableObj.Count;
				for (int i = 0; i < Num; i++)
				{
					bool flag6 = this.ObservableObj[i].Name == name5;
					if (flag6)
					{
						this.ObservableObj[i].Number = number5;
						return;
					}
				}
				this.dataLoad(name5, number5, "5");
			}
		}

		private void Refresh6()
		{
			int heart6 = 0;
			bool flag = ANT_heart_rate.HR6 == null;
			if (!flag)
			{
				try
				{
					heart6 = Convert.ToInt32(ANT_heart_rate.HR6);
				}
				catch
				{
					heart6 = 0;
				}
				string number6 = heart6.ToString("#000") + " bpm";
				bool flag2 = GlobalData.speedequipment != null;
				if (flag2)
				{
					bool flag3 = ANT_heart_rate.dn6 == GlobalData.speedequipment.Substring(5, 5);
					if (flag3)
					{
						return;
					}
				}
				bool flag4 = GlobalData.cadenceequipment != null;
				if (flag4)
				{
					bool flag5 = ANT_heart_rate.dn6 == GlobalData.cadenceequipment.Substring(5, 5);
					if (flag5)
					{
						return;
					}
				}
				string name6 = MultiLanguage.XinLv + ANT_heart_rate.dn6;
				int Num = this.ObservableObj.Count;
				for (int i = 0; i < Num; i++)
				{
					bool flag6 = this.ObservableObj[i].Name == name6;
					if (flag6)
					{
						this.ObservableObj[i].Number = number6;
						return;
					}
				}
				this.dataLoad(name6, number6, "6");
			}
		}

		private void Refresh7()
		{
			int heart7 = 0;
			bool flag = ANT_heart_rate.HR7 == null;
			if (!flag)
			{
				try
				{
					heart7 = Convert.ToInt32(ANT_heart_rate.HR7);
				}
				catch
				{
					heart7 = 0;
				}
				string number7 = heart7.ToString("#000") + " bpm";
				bool flag2 = GlobalData.speedequipment != null;
				if (flag2)
				{
					bool flag3 = ANT_heart_rate.dn7 == GlobalData.speedequipment.Substring(5, 5);
					if (flag3)
					{
						return;
					}
				}
				bool flag4 = GlobalData.cadenceequipment != null;
				if (flag4)
				{
					bool flag5 = ANT_heart_rate.dn7 == GlobalData.cadenceequipment.Substring(5, 5);
					if (flag5)
					{
						return;
					}
				}
				string name7 = MultiLanguage.XinLv + ANT_heart_rate.dn7;
				int Num = this.ObservableObj.Count;
				for (int i = 0; i < Num; i++)
				{
					bool flag6 = this.ObservableObj[i].Name == name7;
					if (flag6)
					{
						this.ObservableObj[i].Number = number7;
						return;
					}
				}
				this.dataLoad(name7, number7, "7");
			}
		}

		private void Refresh8()
		{
			double candence0 = 0.0;
			bool flag = ANT_heart_rate.BC0 == null;
			if (!flag)
			{
				try
				{
					candence0 = Convert.ToDouble(ANT_heart_rate.BC0);
				}
				catch
				{
					candence0 = 0.0;
				}
				string number0 = candence0.ToString("#000") + " rpm";
				bool flag2 = GlobalData.cadenceequipment != null;
				if (flag2)
				{
					bool flag3 = ANT_heart_rate.Dn0 == GlobalData.cadenceequipment.Substring(5, 5);
					if (flag3)
					{
						return;
					}
				}
				bool flag4 = GlobalData.heartRateequipment != null;
				if (flag4)
				{
					bool flag5 = ANT_heart_rate.Dn0 == GlobalData.heartRateequipment.Substring(5, 5);
					if (flag5)
					{
						return;
					}
				}
				string name0 = MultiLanguage.TaPin + ANT_heart_rate.Dn0;
				int Num = this.ObservableObj.Count;
				for (int i = 0; i < Num; i++)
				{
					bool flag6 = this.ObservableObj[i].Name == name0;
					if (flag6)
					{
						this.ObservableObj[i].Number = number0;
						return;
					}
				}
				this.dataLoad(name0, number0, "0");
			}
		}

		private void Refresh9()
		{
			double candence = 0.0;
			bool flag = ANT_heart_rate.BC1 == null;
			if (!flag)
			{
				try
				{
					candence = Convert.ToDouble(ANT_heart_rate.BC1);
				}
				catch
				{
					candence = 0.0;
				}
				string number = candence.ToString("#000") + " rpm";
				bool flag2 = GlobalData.cadenceequipment != null;
				if (flag2)
				{
					bool flag3 = ANT_heart_rate.Dn1 == GlobalData.cadenceequipment.Substring(5, 5);
					if (flag3)
					{
						return;
					}
				}
				bool flag4 = GlobalData.heartRateequipment != null;
				if (flag4)
				{
					bool flag5 = ANT_heart_rate.Dn1 == GlobalData.heartRateequipment.Substring(5, 5);
					if (flag5)
					{
						return;
					}
				}
				string name = MultiLanguage.TaPin + ANT_heart_rate.Dn1;
				int Num = this.ObservableObj.Count;
				for (int i = 0; i < Num; i++)
				{
					bool flag6 = this.ObservableObj[i].Name == name;
					if (flag6)
					{
						this.ObservableObj[i].Number = number;
						return;
					}
				}
				this.dataLoad(name, number, "1");
			}
		}

		private void Refresh10()
		{
			double candence2 = 0.0;
			bool flag = ANT_heart_rate.BC2 == null;
			if (!flag)
			{
				try
				{
					candence2 = Convert.ToDouble(ANT_heart_rate.BC2);
				}
				catch
				{
					candence2 = 0.0;
				}
				string number2 = candence2.ToString("#000") + " rpm";
				bool flag2 = GlobalData.cadenceequipment != null;
				if (flag2)
				{
					bool flag3 = ANT_heart_rate.Dn2 == GlobalData.cadenceequipment.Substring(5, 5);
					if (flag3)
					{
						return;
					}
				}
				bool flag4 = GlobalData.heartRateequipment != null;
				if (flag4)
				{
					bool flag5 = ANT_heart_rate.Dn2 == GlobalData.heartRateequipment.Substring(5, 5);
					if (flag5)
					{
						return;
					}
				}
				string name2 = MultiLanguage.TaPin + ANT_heart_rate.Dn2;
				int Num = this.ObservableObj.Count;
				for (int i = 0; i < Num; i++)
				{
					bool flag6 = this.ObservableObj[i].Name == name2;
					if (flag6)
					{
						this.ObservableObj[i].Number = number2;
						return;
					}
				}
				this.dataLoad(name2, number2, "2");
			}
		}

		private void Refresh11()
		{
			double candence3 = 0.0;
			bool flag = ANT_heart_rate.BC3 == null;
			if (!flag)
			{
				try
				{
					candence3 = Convert.ToDouble(ANT_heart_rate.BC3);
				}
				catch
				{
					candence3 = 0.0;
				}
				string number3 = candence3.ToString("#000") + " rpm";
				bool flag2 = GlobalData.cadenceequipment != null;
				if (flag2)
				{
					bool flag3 = ANT_heart_rate.Dn3 == GlobalData.cadenceequipment.Substring(5, 5);
					if (flag3)
					{
						return;
					}
				}
				bool flag4 = GlobalData.heartRateequipment != null;
				if (flag4)
				{
					bool flag5 = ANT_heart_rate.Dn3 == GlobalData.heartRateequipment.Substring(5, 5);
					if (flag5)
					{
						return;
					}
				}
				string name3 = MultiLanguage.TaPin + ANT_heart_rate.Dn3;
				int Num = this.ObservableObj.Count;
				for (int i = 0; i < Num; i++)
				{
					bool flag6 = this.ObservableObj[i].Name == name3;
					if (flag6)
					{
						this.ObservableObj[i].Number = number3;
						return;
					}
				}
				this.dataLoad(name3, number3, "3");
			}
		}

		private void Refresh12()
		{
			double candence4 = 0.0;
			bool flag = ANT_heart_rate.BC4 == null;
			if (!flag)
			{
				try
				{
					candence4 = Convert.ToDouble(ANT_heart_rate.BC4);
				}
				catch
				{
					candence4 = 0.0;
				}
				string number4 = candence4.ToString("#000") + " rpm";
				bool flag2 = GlobalData.cadenceequipment != null;
				if (flag2)
				{
					bool flag3 = ANT_heart_rate.Dn4 == GlobalData.cadenceequipment.Substring(5, 5);
					if (flag3)
					{
						return;
					}
				}
				bool flag4 = GlobalData.heartRateequipment != null;
				if (flag4)
				{
					bool flag5 = ANT_heart_rate.Dn4 == GlobalData.heartRateequipment.Substring(5, 5);
					if (flag5)
					{
						return;
					}
				}
				string name4 = MultiLanguage.TaPin + ANT_heart_rate.Dn4;
				int Num = this.ObservableObj.Count;
				for (int i = 0; i < Num; i++)
				{
					bool flag6 = this.ObservableObj[i].Name == name4;
					if (flag6)
					{
						this.ObservableObj[i].Number = number4;
						return;
					}
				}
				this.dataLoad(name4, number4, "4");
			}
		}

		private void Refresh13()
		{
			double candence5 = 0.0;
			bool flag = ANT_heart_rate.BC5 == null;
			if (!flag)
			{
				try
				{
					candence5 = Convert.ToDouble(ANT_heart_rate.BC5);
				}
				catch
				{
					candence5 = 0.0;
				}
				string number5 = candence5.ToString("#000") + " rpm";
				bool flag2 = GlobalData.cadenceequipment != null;
				if (flag2)
				{
					bool flag3 = ANT_heart_rate.Dn5 == GlobalData.cadenceequipment.Substring(5, 5);
					if (flag3)
					{
						return;
					}
				}
				bool flag4 = GlobalData.heartRateequipment != null;
				if (flag4)
				{
					bool flag5 = ANT_heart_rate.Dn5 == GlobalData.heartRateequipment.Substring(5, 5);
					if (flag5)
					{
						return;
					}
				}
				string name5 = MultiLanguage.TaPin + ANT_heart_rate.Dn5;
				int Num = this.ObservableObj.Count;
				for (int i = 0; i < Num; i++)
				{
					bool flag6 = this.ObservableObj[i].Name == name5;
					if (flag6)
					{
						this.ObservableObj[i].Number = number5;
						return;
					}
				}
				this.dataLoad(name5, number5, "5");
			}
		}

		private void Refresh14()
		{
			double candence6 = 0.0;
			bool flag = ANT_heart_rate.BC6 == null;
			if (!flag)
			{
				try
				{
					candence6 = Convert.ToDouble(ANT_heart_rate.BC6);
				}
				catch
				{
					candence6 = 0.0;
				}
				string number6 = candence6.ToString("#000") + " rpm";
				bool flag2 = GlobalData.cadenceequipment != null;
				if (flag2)
				{
					bool flag3 = ANT_heart_rate.Dn6 == GlobalData.cadenceequipment.Substring(5, 5);
					if (flag3)
					{
						return;
					}
				}
				bool flag4 = GlobalData.heartRateequipment != null;
				if (flag4)
				{
					bool flag5 = ANT_heart_rate.Dn6 == GlobalData.heartRateequipment.Substring(5, 5);
					if (flag5)
					{
						return;
					}
				}
				string name6 = MultiLanguage.TaPin + ANT_heart_rate.Dn6;
				int Num = this.ObservableObj.Count;
				for (int i = 0; i < Num; i++)
				{
					bool flag6 = this.ObservableObj[i].Name == name6;
					if (flag6)
					{
						this.ObservableObj[i].Number = number6;
						return;
					}
				}
				this.dataLoad(name6, number6, "6");
			}
		}

		private void Refresh15()
		{
			double candence7 = 0.0;
			bool flag = ANT_heart_rate.BC7 == null;
			if (!flag)
			{
				try
				{
					candence7 = Convert.ToDouble(ANT_heart_rate.BC7);
				}
				catch
				{
					candence7 = 0.0;
				}
				string number7 = candence7.ToString("#000") + " rpm";
				bool flag2 = GlobalData.cadenceequipment != null;
				if (flag2)
				{
					bool flag3 = ANT_heart_rate.Dn7 == GlobalData.cadenceequipment.Substring(5, 5);
					if (flag3)
					{
						return;
					}
				}
				bool flag4 = GlobalData.heartRateequipment != null;
				if (flag4)
				{
					bool flag5 = ANT_heart_rate.Dn7 == GlobalData.heartRateequipment.Substring(5, 5);
					if (flag5)
					{
						return;
					}
				}
				string name7 = MultiLanguage.TaPin + ANT_heart_rate.Dn7;
				int Num = this.ObservableObj.Count;
				for (int i = 0; i < Num; i++)
				{
					bool flag6 = this.ObservableObj[i].Name == name7;
					if (flag6)
					{
						this.ObservableObj[i].Number = number7;
						return;
					}
				}
				this.dataLoad(name7, number7, "7");
			}
		}

		private void Refresh16()
		{
			double speed0 = 0.0;
			bool flag = ANT_heart_rate.BS0 == null;
			if (!flag)
			{
				try
				{
					speed0 = Convert.ToDouble(ANT_heart_rate.BS0);
				}
				catch
				{
					speed0 = 0.0;
				}
				string number0 = speed0.ToString("#000") + " km/h";
				bool flag2 = GlobalData.cadenceequipment != null;
				if (flag2)
				{
					bool flag3 = ANT_heart_rate.DN0 == GlobalData.cadenceequipment.Substring(5, 5);
					if (flag3)
					{
						return;
					}
				}
				bool flag4 = GlobalData.heartRateequipment != null;
				if (flag4)
				{
					bool flag5 = ANT_heart_rate.DN0 == GlobalData.heartRateequipment.Substring(5, 5);
					if (flag5)
					{
						return;
					}
				}
				string name0 = MultiLanguage.SuDu + ANT_heart_rate.DN0;
				int Num = this.ObservableObj.Count;
				for (int i = 0; i < Num; i++)
				{
					bool flag6 = this.ObservableObj[i].Name == name0;
					if (flag6)
					{
						this.ObservableObj[i].Number = number0;
						return;
					}
				}
				this.dataLoad(name0, number0, "0");
			}
		}

		private void Refresh17()
		{
			double speed = 0.0;
			bool flag = ANT_heart_rate.BS1 == null;
			if (!flag)
			{
				try
				{
					speed = Convert.ToDouble(ANT_heart_rate.BS1);
				}
				catch
				{
					speed = 0.0;
				}
				string number = speed.ToString("#000") + " km/h";
				bool flag2 = GlobalData.cadenceequipment != null;
				if (flag2)
				{
					bool flag3 = ANT_heart_rate.DN1 == GlobalData.cadenceequipment.Substring(5, 5);
					if (flag3)
					{
						return;
					}
				}
				bool flag4 = GlobalData.heartRateequipment != null;
				if (flag4)
				{
					bool flag5 = ANT_heart_rate.DN1 == GlobalData.heartRateequipment.Substring(5, 5);
					if (flag5)
					{
						return;
					}
				}
				string name = MultiLanguage.SuDu + ANT_heart_rate.DN1;
				int Num = this.ObservableObj.Count;
				for (int i = 0; i < Num; i++)
				{
					bool flag6 = this.ObservableObj[i].Name == name;
					if (flag6)
					{
						this.ObservableObj[i].Number = number;
						return;
					}
				}
				this.dataLoad(name, number, "1");
			}
		}

		private void Refresh18()
		{
			double speed2 = 0.0;
			bool flag = ANT_heart_rate.BS2 == null;
			if (!flag)
			{
				try
				{
					speed2 = Convert.ToDouble(ANT_heart_rate.BS2);
				}
				catch
				{
					speed2 = 0.0;
				}
				string number2 = speed2.ToString("#000") + " km/h";
				bool flag2 = GlobalData.cadenceequipment != null;
				if (flag2)
				{
					bool flag3 = ANT_heart_rate.DN2 == GlobalData.cadenceequipment.Substring(5, 5);
					if (flag3)
					{
						return;
					}
				}
				bool flag4 = GlobalData.heartRateequipment != null;
				if (flag4)
				{
					bool flag5 = ANT_heart_rate.DN2 == GlobalData.heartRateequipment.Substring(5, 5);
					if (flag5)
					{
						return;
					}
				}
				string name2 = MultiLanguage.SuDu + ANT_heart_rate.DN2;
				int Num = this.ObservableObj.Count;
				for (int i = 0; i < Num; i++)
				{
					bool flag6 = this.ObservableObj[i].Name == name2;
					if (flag6)
					{
						this.ObservableObj[i].Number = number2;
						return;
					}
				}
				this.dataLoad(name2, number2, "2");
			}
		}

		private void Refresh19()
		{
			double speed3 = 0.0;
			bool flag = ANT_heart_rate.BS3 == null;
			if (!flag)
			{
				try
				{
					speed3 = Convert.ToDouble(ANT_heart_rate.BS3);
				}
				catch
				{
					speed3 = 0.0;
				}
				string number3 = speed3.ToString("#000") + " km/h";
				bool flag2 = GlobalData.cadenceequipment != null;
				if (flag2)
				{
					bool flag3 = ANT_heart_rate.DN3 == GlobalData.cadenceequipment.Substring(5, 5);
					if (flag3)
					{
						return;
					}
				}
				bool flag4 = GlobalData.heartRateequipment != null;
				if (flag4)
				{
					bool flag5 = ANT_heart_rate.DN3 == GlobalData.heartRateequipment.Substring(5, 5);
					if (flag5)
					{
						return;
					}
				}
				string name3 = MultiLanguage.SuDu + ANT_heart_rate.DN3;
				int Num = this.ObservableObj.Count;
				for (int i = 0; i < Num; i++)
				{
					bool flag6 = this.ObservableObj[i].Name == name3;
					if (flag6)
					{
						this.ObservableObj[i].Number = number3;
						return;
					}
				}
				this.dataLoad(name3, number3, "3");
			}
		}

		private void Refresh20()
		{
			double speed4 = 0.0;
			bool flag = ANT_heart_rate.BS4 == null;
			if (!flag)
			{
				try
				{
					speed4 = Convert.ToDouble(ANT_heart_rate.BS4);
				}
				catch
				{
					speed4 = 0.0;
				}
				string number4 = speed4.ToString("#000") + " km/h";
				bool flag2 = GlobalData.cadenceequipment != null;
				if (flag2)
				{
					bool flag3 = ANT_heart_rate.DN4 == GlobalData.cadenceequipment.Substring(5, 5);
					if (flag3)
					{
						return;
					}
				}
				bool flag4 = GlobalData.heartRateequipment != null;
				if (flag4)
				{
					bool flag5 = ANT_heart_rate.DN4 == GlobalData.heartRateequipment.Substring(5, 5);
					if (flag5)
					{
						return;
					}
				}
				string name4 = MultiLanguage.SuDu + ANT_heart_rate.DN4;
				int Num = this.ObservableObj.Count;
				for (int i = 0; i < Num; i++)
				{
					bool flag6 = this.ObservableObj[i].Name == name4;
					if (flag6)
					{
						this.ObservableObj[i].Number = number4;
						return;
					}
				}
				this.dataLoad(name4, number4, "4");
			}
		}

		private void Refresh21()
		{
			double speed5 = 0.0;
			bool flag = ANT_heart_rate.BS5 == null;
			if (!flag)
			{
				try
				{
					speed5 = Convert.ToDouble(ANT_heart_rate.BS5);
				}
				catch
				{
					speed5 = 0.0;
				}
				string number5 = speed5.ToString("#000") + " km/h";
				bool flag2 = GlobalData.cadenceequipment != null;
				if (flag2)
				{
					bool flag3 = ANT_heart_rate.DN5 == GlobalData.cadenceequipment.Substring(5, 5);
					if (flag3)
					{
						return;
					}
				}
				bool flag4 = GlobalData.heartRateequipment != null;
				if (flag4)
				{
					bool flag5 = ANT_heart_rate.DN5 == GlobalData.heartRateequipment.Substring(5, 5);
					if (flag5)
					{
						return;
					}
				}
				string name5 = MultiLanguage.SuDu + ANT_heart_rate.DN5;
				int Num = this.ObservableObj.Count;
				for (int i = 0; i < Num; i++)
				{
					bool flag6 = this.ObservableObj[i].Name == name5;
					if (flag6)
					{
						this.ObservableObj[i].Number = number5;
						return;
					}
				}
				this.dataLoad(name5, number5, "5");
			}
		}

		private void Refresh22()
		{
			double speed6 = 0.0;
			bool flag = ANT_heart_rate.BS6 == null;
			if (!flag)
			{
				try
				{
					speed6 = Convert.ToDouble(ANT_heart_rate.BS6);
				}
				catch
				{
					speed6 = 0.0;
				}
				string number6 = speed6.ToString("#000") + " km/h";
				bool flag2 = GlobalData.cadenceequipment != null;
				if (flag2)
				{
					bool flag3 = ANT_heart_rate.DN6 == GlobalData.cadenceequipment.Substring(5, 5);
					if (flag3)
					{
						return;
					}
				}
				bool flag4 = GlobalData.heartRateequipment != null;
				if (flag4)
				{
					bool flag5 = ANT_heart_rate.DN6 == GlobalData.heartRateequipment.Substring(5, 5);
					if (flag5)
					{
						return;
					}
				}
				string name6 = MultiLanguage.SuDu + ANT_heart_rate.DN6;
				int Num = this.ObservableObj.Count;
				for (int i = 0; i < Num; i++)
				{
					bool flag6 = this.ObservableObj[i].Name == name6;
					if (flag6)
					{
						this.ObservableObj[i].Number = number6;
						return;
					}
				}
				this.dataLoad(name6, number6, "6");
			}
		}

		private void Refresh23()
		{
			double speed7 = 0.0;
			bool flag = ANT_heart_rate.HR7 == null;
			if (!flag)
			{
				try
				{
					speed7 = Convert.ToDouble(ANT_heart_rate.HR7);
				}
				catch
				{
					speed7 = 0.0;
				}
				string number7 = speed7.ToString("#000") + " km/h";
				bool flag2 = GlobalData.cadenceequipment != null;
				if (flag2)
				{
					bool flag3 = ANT_heart_rate.DN7 == GlobalData.cadenceequipment.Substring(5, 5);
					if (flag3)
					{
						return;
					}
				}
				bool flag4 = GlobalData.heartRateequipment != null;
				if (flag4)
				{
					bool flag5 = ANT_heart_rate.DN7 == GlobalData.heartRateequipment.Substring(5, 5);
					if (flag5)
					{
						return;
					}
				}
				string name7 = MultiLanguage.SuDu + ANT_heart_rate.DN7;
				int Num = this.ObservableObj.Count;
				for (int i = 0; i < Num; i++)
				{
					bool flag6 = this.ObservableObj[i].Name == name7;
					if (flag6)
					{
						this.ObservableObj[i].Number = number7;
						return;
					}
				}
				this.dataLoad(name7, number7, "7");
			}
		}

		private void Centerbutton_Click(object sender, RoutedEventArgs e)
		{
			bool flag = this.listView.SelectedItem == null;
			if (flag)
			{
				ANT_heart_rate.closeChannels();
				base.Close();
			}
			else
			{
				SearchData rd = this.listView.SelectedItem as SearchData;
				bool flag2 = rd == null;
				if (!flag2)
				{
					int channel = Convert.ToInt32(rd.Channel);
					string name = Convert.ToString(rd.Name);
					switch (GlobalData.deviceStyle)
					{
					case 0:
					{
						GlobalData.speedChannel = channel;
						GlobalData.ANT_Connectflag = true;
						GlobalData.speedequipment = name;
						XMLHelper.SpeedDeviceNumberFile("SpeedDeviceNumber");
						bool flag3 = SearchWindow.sudu != null;
						if (flag3)
						{
							SearchWindow.sudu();
						}
						break;
					}
					case 1:
					{
						GlobalData.cadenceChannel = channel;
						GlobalData.cadenceequipment = name;
						XMLHelper.CadenceDeviceNumberFile("CadenceDeviceNumber");
						bool flag4 = SearchWindow.tapin != null;
						if (flag4)
						{
							SearchWindow.tapin();
						}
						break;
					}
					case 2:
					{
						GlobalData.heartChannel = channel;
						GlobalData.heartRateequipment = name;
						XMLHelper.HeartRateDeviceNumberFile("HeartRateDeviceNumber");
						bool flag5 = SearchWindow.xinlv != null;
						if (flag5)
						{
							SearchWindow.xinlv();
						}
						break;
					}
					}
					ANT_heart_rate.closeChannels();
					base.Close();
				}
			}
		}

        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        //public void InitializeComponent()
        //{
        //    bool contentLoaded = this._contentLoaded;
        //    if (!contentLoaded)
        //    {
        //        this._contentLoaded = true;
        //        Uri resourceLocater = new Uri("/Bike;component/other/searchwindow.xaml", UriKind.Relative);
        //        Application.LoadComponent(this, resourceLocater);
        //    }
        //}

        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
        //void IComponentConnector.Connect(int connectionId, object target)
        //{
        //    if (connectionId != 1)
        //    {
        //        if (connectionId != 2)
        //        {
        //            this._contentLoaded = true;
        //        }
        //        else
        //        {
        //            this.Centerbutton = (Button)target;
        //            this.Centerbutton.Click += new RoutedEventHandler(this.Centerbutton_Click);
        //        }
        //    }
        //    else
        //    {
        //        this.listView = (ListView)target;
        //    }
        //}
	}
}
