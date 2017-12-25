using Sensor_Windows;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Bike.Other
{
    public partial class DevicesPage : Page, IComponentConnector
	{
        //internal RadioButton chinese_radio;

        //internal RadioButton english_radio;

        //internal TabControl tabControl;

        //internal TextBlock IntelligentText;

        //internal TextBlock PuTong;

        //internal TextBlock GeneralText;

        //internal TextBlock TaPin;

        //internal TextBlock RpmText;

        //internal TextBlock Xinlv;

        //internal TextBlock BpmText;

        //internal Button Intelligent;

        //internal Button General;

        //internal Button Rpm;

        //internal Button Bpm;

        //internal TextBlock textBlock;

        //private bool _contentLoaded;

		public DevicesPage()
		{
			Device.Scan();
			this.InitializeComponent();
			this.languageInit();
			SearchWindow.sudu += new SearchWindow.RegisterW(this.sudu);
			SearchWindow.tapin += new SearchWindow.RegisterW(this.tapin);
			SearchWindow.xinlv += new SearchWindow.RegisterW(this.xinlv);
			bool flag = GlobalData.speedequipment != null;
			if (flag)
			{
				this.PuTong.Text = GlobalData.speedequipment;
				this.PuTong.Visibility = Visibility.Visible;
			}
			bool flag2 = GlobalData.heartRateequipment != null;
			if (flag2)
			{
				this.Xinlv.Text = GlobalData.heartRateequipment;
				this.Xinlv.Visibility = Visibility.Visible;
			}
			bool flag3 = GlobalData.cadenceequipment != null;
			if (flag3)
			{
				this.TaPin.Text = GlobalData.cadenceequipment;
				this.TaPin.Visibility = Visibility.Visible;
			}
		}

		private void languageInit()
		{
			this.textBlock.Text = MultiLanguage.Devices_t;
			this.IntelligentText.Text = MultiLanguage.Intelligent;
			this.GeneralText.Text = MultiLanguage.General;
			this.RpmText.Text = MultiLanguage.Rpm;
			this.BpmText.Text = MultiLanguage.Bpm;
		}

		private void Chinese_Checked(object sender, RoutedEventArgs e)
		{
			bool flag = GlobalData.language == 1;
			if (!flag)
			{
				XMLHelper.setLanguage(1);
				GlobalData.language = 1;
				MessageBox.Show(MultiLanguage.Hint);
			}
		}

		private void English_Checked(object sender, RoutedEventArgs e)
		{
			bool flag = GlobalData.language == 0;
			if (!flag)
			{
				XMLHelper.setLanguage(0);
				GlobalData.language = 0;
				MessageBox.Show(MultiLanguage.Hint);
			}
		}

		private void Intelligent_Click(object sender, RoutedEventArgs e)
		{
			IntelligentWindow ip = new IntelligentWindow();
			ip.ShowDialog();
		}

		private void General_Click(object sender, RoutedEventArgs e)
		{
			ANT_heart_rate.clearBsValue();
			ANT_heart_rate.clearBcValue();
			ANT_heart_rate.clearHrValue();
			bool flag = GlobalData.speedChannel == 9;
			if (flag)
			{
				try
				{
					ANT_heart_rate.bs_Init();
				}
				catch (Exception ex)
				{
					MessageBox.Show("Connect failed with exception: \n" + ex.Message);
				}
				GlobalData.deviceStyle = 0;
				SearchWindow gp = new SearchWindow();
				gp.ShowDialog();
			}
			else
			{
				bool flag2 = GlobalData.speedChannel >= 0 && GlobalData.speedChannel < 8;
				if (flag2)
				{
					ANT_heart_rate.disconnectBs();
					ANT_heart_rate.clearBsValue();
					this.PuTong.Visibility = Visibility.Collapsed;
					GlobalData.speedequipment = null;
					XMLHelper.SpeedDeviceNumberDeleter("SpeedDeviceNumber");
					GlobalData.ANT_Connectflag = false;
				}
			}
		}

		private void Rpm_Click(object sender, RoutedEventArgs e)
		{
			ANT_heart_rate.clearBsValue();
			ANT_heart_rate.clearBcValue();
			ANT_heart_rate.clearHrValue();
			bool flag = GlobalData.cadenceChannel == 9;
			if (flag)
			{
				try
				{
					ANT_heart_rate.bc_Init();
				}
				catch (Exception ex)
				{
					MessageBox.Show("Connect failed with exception: \n" + ex.Message);
				}
				GlobalData.deviceStyle = 1;
				SearchWindow gp = new SearchWindow();
				gp.ShowDialog();
			}
			else
			{
				bool flag2 = GlobalData.cadenceChannel >= 0 && GlobalData.cadenceChannel < 8;
				if (flag2)
				{
					ANT_heart_rate.disconnectBc();
					ANT_heart_rate.clearBcValue();
					this.TaPin.Visibility = Visibility.Collapsed;
					GlobalData.cadenceequipment = null;
					XMLHelper.CadenceDeviceNumberDeleter("CadenceDeviceNumber");
				}
			}
		}

		private void Bpm_Click(object sender, RoutedEventArgs e)
		{
			ANT_heart_rate.clearBsValue();
			ANT_heart_rate.clearBcValue();
			ANT_heart_rate.clearHrValue();
			bool flag = GlobalData.heartChannel == 9;
			if (flag)
			{
				try
				{
					ANT_heart_rate.hr_Init();
				}
				catch (Exception ex)
				{
					MessageBox.Show("Connect failed with exception: \n" + ex.Message);
				}
				GlobalData.deviceStyle = 2;
				SearchWindow bpm = new SearchWindow();
				bpm.ShowDialog();
			}
			else
			{
				bool flag2 = GlobalData.heartChannel >= 0 && GlobalData.heartChannel < 8;
				if (flag2)
				{
					ANT_heart_rate.disconnectHr();
					ANT_heart_rate.clearHrValue();
					this.Xinlv.Visibility = Visibility.Collapsed;
					GlobalData.heartRateequipment = null;
					XMLHelper.HeartRateDeviceNumberDeleter("HeartRateDeviceNumber");
				}
			}
		}

		private void DevicesPage_Unloaded(object sender, RoutedEventArgs e)
		{
			SearchWindow.sudu -= new SearchWindow.RegisterW(this.sudu);
			SearchWindow.tapin -= new SearchWindow.RegisterW(this.tapin);
			SearchWindow.xinlv -= new SearchWindow.RegisterW(this.xinlv);
		}

		public void sudu()
		{
			base.Dispatcher.Invoke(delegate
			{
				this.PuTong.Visibility = Visibility.Visible;
			});
			base.Dispatcher.Invoke(delegate
			{
				this.PuTong.Text = GlobalData.speedequipment;
			});
		}

		public void tapin()
		{
			base.Dispatcher.Invoke(delegate
			{
				this.TaPin.Visibility = Visibility.Visible;
			});
			base.Dispatcher.Invoke(delegate
			{
				this.TaPin.Text = GlobalData.cadenceequipment;
			});
		}

		public void xinlv()
		{
			base.Dispatcher.Invoke(delegate
			{
				this.Xinlv.Visibility = Visibility.Visible;
			});
			base.Dispatcher.Invoke(delegate
			{
				this.Xinlv.Text = GlobalData.heartRateequipment;
			});
		}

        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        //public void InitializeComponent()
        //{
        //    bool contentLoaded = this._contentLoaded;
        //    if (!contentLoaded)
        //    {
        //        this._contentLoaded = true;
        //        Uri resourceLocater = new Uri("/Bike;component/other/devicepage.xaml", UriKind.Relative);
        //        Application.LoadComponent(this, resourceLocater);
        //    }
        //}

        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
        //void IComponentConnector.Connect(int connectionId, object target)
        //{
        //    switch (connectionId)
        //    {
        //    case 1:
        //        ((DevicesPage)target).Unloaded += new RoutedEventHandler(this.DevicesPage_Unloaded);
        //        break;
        //    case 2:
        //        this.chinese_radio = (RadioButton)target;
        //        this.chinese_radio.Checked += new RoutedEventHandler(this.Chinese_Checked);
        //        break;
        //    case 3:
        //        this.english_radio = (RadioButton)target;
        //        this.english_radio.Checked += new RoutedEventHandler(this.English_Checked);
        //        break;
        //    case 4:
        //        this.tabControl = (TabControl)target;
        //        break;
        //    case 5:
        //        this.IntelligentText = (TextBlock)target;
        //        break;
        //    case 6:
        //        this.PuTong = (TextBlock)target;
        //        break;
        //    case 7:
        //        this.GeneralText = (TextBlock)target;
        //        break;
        //    case 8:
        //        this.TaPin = (TextBlock)target;
        //        break;
        //    case 9:
        //        this.RpmText = (TextBlock)target;
        //        break;
        //    case 10:
        //        this.Xinlv = (TextBlock)target;
        //        break;
        //    case 11:
        //        this.BpmText = (TextBlock)target;
        //        break;
        //    case 12:
        //        this.Intelligent = (Button)target;
        //        this.Intelligent.Click += new RoutedEventHandler(this.Intelligent_Click);
        //        break;
        //    case 13:
        //        this.General = (Button)target;
        //        this.General.Click += new RoutedEventHandler(this.General_Click);
        //        break;
        //    case 14:
        //        this.Rpm = (Button)target;
        //        this.Rpm.Click += new RoutedEventHandler(this.Rpm_Click);
        //        break;
        //    case 15:
        //        this.Bpm = (Button)target;
        //        this.Bpm.Click += new RoutedEventHandler(this.Bpm_Click);
        //        break;
        //    case 16:
        //        this.textBlock = (TextBlock)target;
        //        break;
        //    default:
        //        this._contentLoaded = true;
        //        break;
        //    }
        //}
	}
}
