using FinalClient;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Bike.Trainings
{
    public partial class U3DPage : Page, INotifyPropertyChanged, IComponentConnector
	{
		public delegate void WindowShow(bool flag);

		private bool uflag = false;

		private Thread UdpRevc;

		private string buttonContent = MultiLanguage.Enter;

        //internal Button button;

        //internal TextBlock textBlock;

        //private bool _contentLoaded;

		public event PropertyChangedEventHandler PropertyChanged;

		public static event U3DPage.WindowShow windowShow;

		public string ButtonContent
		{
			get
			{
				return this.buttonContent;
			}
			set
			{
				bool flag = value != this.buttonContent;
				if (flag)
				{
					this.buttonContent = value;
					bool flag2 = this.PropertyChanged != null;
					if (flag2)
					{
						this.PropertyChanged(this, new PropertyChangedEventArgs("ButtonContent"));
					}
				}
			}
		}

		public U3DPage()
		{
			this.InitializeComponent();
			this.languageInit();
			this.UdpRevc = new Thread(new ThreadStart(this.GearRevc));
			base.DataContext = this;
		}

		private void languageInit()
		{
			this.textBlock.Text = MultiLanguage.Unity3D_t;
		}

		private void button_Click(object sender, RoutedEventArgs e)
		{
			bool flag = !GlobalData.Connectflag;
			if (flag)
			{
				MessageBox.Show(MultiLanguage.Warn13);
			}
			else
			{
				bool flag2 = !Connection.isConnected;
				if (flag2)
				{
					MessageBox.Show(MultiLanguage.Warn31);
				}
				else
				{
					bool flag3 = !this.uflag;
					if (flag3)
					{
						this.uflag = true;
						GlobalData.Unity3DTrainingflag = this.uflag;
						UdpCore.Init();
						this.ButtonContent = MultiLanguage.Button_exit;
						bool flag4 = this.UdpRevc.ThreadState == System.Threading.ThreadState.Unstarted;
						if (flag4)
						{
							this.UdpRevc.Start();
						}
						Process process = Process.Start("DEMO.exe");
						bool flag5 = process != null;
						if (flag5)
						{
							process.EnableRaisingEvents = true;
							process.Exited += new EventHandler(this.Process_Exited);
						}
						bool flag6 = U3DPage.windowShow != null;
						if (flag6)
						{
							U3DPage.windowShow(false);
						}
						GlobalData.mainWindow.WinVisibility = Visibility.Collapsed;
					}
					else
					{
						this.uflag = false;
						GlobalData.Unity3DTrainingflag = this.uflag;
						UdpCore.Uninit();
						this.ButtonContent = MultiLanguage.Enter;
					}
				}
			}
		}

		private void Process_Exited(object sender, EventArgs e)
		{
			this.uflag = false;
			GlobalData.Unity3DTrainingflag = this.uflag;
			UdpCore.Uninit();
			this.ButtonContent = MultiLanguage.Enter;
			GlobalData.mainWindow.WinVisibility = Visibility.Visible;
		}

		private void GearRevc()
		{
			while (GlobalData.Unity3DTrainingflag)
			{
				bool flag = UdpCore.GetGear != 0;
				if (flag)
				{
					GlobalData.SendGear(UdpCore.GetGear);
					UdpCore.GetGear = 0;
				}
				else
				{
					Thread.Sleep(200);
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
        //        Uri resourceLocater = new Uri("/Bike;component/trainings/u3dpage.xaml", UriKind.Relative);
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
        //            this.textBlock = (TextBlock)target;
        //        }
        //    }
        //    else
        //    {
        //        this.button = (Button)target;
        //        this.button.Click += new RoutedEventHandler(this.button_Click);
        //    }
        //}
	}
}
