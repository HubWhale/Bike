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
    public partial class GeneralWindow : Window, IComponentConnector
	{
        //internal TextBlock Peizhi;

        //internal TextBlock General1;

        //internal TextBlock SpeedTransducer;

        //internal TextBlock General2;

        //internal TextBlock TypeNumber1;

        //internal TextBox SpeedTextBox;

        //internal Button Connect1;

        //internal Button Finish;

        //internal ComboBox SericalCom;

        //private bool _contentLoaded;

		public GeneralWindow()
		{
			this.InitializeComponent();
		}

		private void Connect1_Click(object sender, RoutedEventArgs e)
		{
		}

		private void SericalCom_PreviewMouseDown(object sender, MouseButtonEventArgs e)
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
        //        Uri resourceLocater = new Uri("/Bike;component/other/generalwindow.xaml", UriKind.Relative);
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
        //        this.General1 = (TextBlock)target;
        //        break;
        //    case 4:
        //        this.SpeedTransducer = (TextBlock)target;
        //        break;
        //    case 5:
        //        this.General2 = (TextBlock)target;
        //        break;
        //    case 6:
        //        this.TypeNumber1 = (TextBlock)target;
        //        break;
        //    case 7:
        //        this.SpeedTextBox = (TextBox)target;
        //        break;
        //    case 8:
        //        this.Connect1 = (Button)target;
        //        this.Connect1.Click += new RoutedEventHandler(this.Connect1_Click);
        //        break;
        //    case 9:
        //        this.Finish = (Button)target;
        //        this.Finish.Click += new RoutedEventHandler(this.Finish_Click);
        //        break;
        //    case 10:
        //        this.SericalCom = (ComboBox)target;
        //        this.SericalCom.PreviewMouseDown += new MouseButtonEventHandler(this.SericalCom_PreviewMouseDown);
        //        break;
        //    default:
        //        this._contentLoaded = true;
        //        break;
        //    }
        //}
	}
}
