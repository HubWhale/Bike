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
    public partial class RpmWindow : Window, IComponentConnector
	{
        //internal TextBlock Peizhi;

        //internal TextBlock RpmTransducer;

        //internal TextBlock SerialBlock1;

        //internal TextBox Tramp_num;

        //internal Button Tramp_Connect;

        //internal Button Finish;

        //private bool _contentLoaded;

		public RpmWindow()
		{
			this.InitializeComponent();
		}

		private void Tramp_Connect_Click(object sender, RoutedEventArgs e)
		{
			GlobalData.Tramp_num = Convert.ToUInt16(this.Tramp_num.Text);
			ANT.USER_DEVICENUM_tramp = GlobalData.Tramp_num;
			try
			{
				ANT.Tramp_Init();
				ANT.Tramp_Start();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Connect failed with exception: \n" + ex.Message);
			}
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
        //        Uri resourceLocater = new Uri("/Bike;component/other/rpmwindow.xaml", UriKind.Relative);
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
        //        this.RpmTransducer = (TextBlock)target;
        //        break;
        //    case 4:
        //        this.SerialBlock1 = (TextBlock)target;
        //        break;
        //    case 5:
        //        this.Tramp_num = (TextBox)target;
        //        break;
        //    case 6:
        //        this.Tramp_Connect = (Button)target;
        //        this.Tramp_Connect.Click += new RoutedEventHandler(this.Tramp_Connect_Click);
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
