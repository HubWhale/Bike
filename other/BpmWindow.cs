using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace Bike.Other
{
    public partial class BpmWindow : Window, IComponentConnector
	{
        //internal TextBlock Peizhi;

        //internal TextBlock HeartRate;

        //internal TextBlock SerialBlock1;

        //internal TextBox Heart_num;

        //internal Button ANT_Connect;

        //internal Button Finish;

        //private bool _contentLoaded;

		public BpmWindow()
		{
			this.InitializeComponent();
			this.languageInit();
		}

		private void languageInit()
		{
			this.Peizhi.Text = MultiLanguage.Peizhi_t;
			this.HeartRate.Text = MultiLanguage.HeartRate_t;
			this.SerialBlock1.Text = MultiLanguage.SerialBlock1_t;
		}

		private void ANT_Connect_Click(object sender, RoutedEventArgs e)
		{
			bool flag = this.Heart_num.Text == null;
			if (flag)
			{
				MessageBox.Show("请输入设备号！");
			}
			else
			{
				GlobalData.Heart_num = Convert.ToUInt16(this.Heart_num.Text);
			}
			ANT.USER_DEVICENUM = GlobalData.Tramp_num;
			try
			{
				ANT.Init();
				ANT.Start();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Connect failed with exception: \n" + ex.Message);
			}
		}

		private void Window_Closed(object sender, EventArgs e)
		{
		}

		private void TitleBar_MouseMove(object sender, MouseEventArgs e)
		{
			bool flag = e.LeftButton == MouseButtonState.Pressed;
			if (flag)
			{
				base.DragMove();
			}
		}

		private void Finish_Click(object sender, RoutedEventArgs e)
		{
			base.Close();
		}

        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        //public void InitializeComponent()
        //{
        //    bool contentLoaded = this._contentLoaded;
        //    if (!contentLoaded)
        //    {
        //        this._contentLoaded = true;
        //        Uri resourceLocater = new Uri("/Bike;component/other/bpmwindow.xaml", UriKind.Relative);
        //        Application.LoadComponent(this, resourceLocater);
        //    }
        //}

        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
        //void IComponentConnector.Connect(int connectionId, object target)
        //{
        //    switch (connectionId)
        //    {
        //    case 1:
        //        ((Grid)target).MouseMove += new MouseEventHandler(this.TitleBar_MouseMove);
        //        break;
        //    case 2:
        //        this.Peizhi = (TextBlock)target;
        //        break;
        //    case 3:
        //        this.HeartRate = (TextBlock)target;
        //        break;
        //    case 4:
        //        this.SerialBlock1 = (TextBlock)target;
        //        break;
        //    case 5:
        //        this.Heart_num = (TextBox)target;
        //        break;
        //    case 6:
        //        this.ANT_Connect = (Button)target;
        //        this.ANT_Connect.Click += new RoutedEventHandler(this.ANT_Connect_Click);
        //        break;
        //    case 7:
        //        this.Finish = (Button)target;
        //        this.Finish.Click += new RoutedEventHandler(this.Finish_Click);
        //        break;
        //    default:
        //        this._contentLoaded = true;
        //        break;
        //    }
        //}
	}
}
