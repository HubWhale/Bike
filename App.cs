using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Windows;

namespace Bike
{
	public partial class App : Application
	{
        //private bool _contentLoaded;

        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        //public void InitializeComponent()
        //{
        //    bool contentLoaded = this._contentLoaded;
        //    if (!contentLoaded)
        //    {
        //        this._contentLoaded = true;
        //        base.StartupUri = new Uri("MainWindow.xaml", UriKind.Relative);
        //        Uri resourceLocater = new Uri("/Bike;component/app.xaml", UriKind.Relative);
        //        Application.LoadComponent(this, resourceLocater);
        //    }
        //}

		[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode, STAThread]
		public static void Main()
		{
			App app = new App();
			app.InitializeComponent();
			app.Run();
		}
	}
}
