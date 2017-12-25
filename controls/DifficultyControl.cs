using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Bike.Controls
{
    public partial class DifficultyControl : UserControl, IComponentConnector
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class DifficultyControlPropertyChange
		{
			public static readonly DifficultyControl.DifficultyControlPropertyChange difficultyControlChange = new DifficultyControl.DifficultyControlPropertyChange();

			internal void ControlChange(DependencyObject sender, DependencyPropertyChangedEventArgs e)
			{
				DifficultyControl control = (DifficultyControl)sender;
				control.Difficulty = (string)e.NewValue;
			}
		}

		public static readonly DependencyProperty DifficultyProperty = DependencyProperty.Register("Difficulty", typeof(string), typeof(DifficultyControl), new UIPropertyMetadata(null, new PropertyChangedCallback(DifficultyControl.DifficultyControlPropertyChange.difficultyControlChange.ControlChange)));

		private string zero = "/Bike;component/Images/00.png";

		private string one = "/Bike;component/Images/05.png";

		private string two = "/Bike;component/Images/10.png";

		private string[] imageSource = new string[5];

        //internal Image image;

        //internal Image image_Copy;

        //internal Image image_Copy1;

        //internal Image image_Copy2;

        //internal Image image_Copy3;

        //private bool _contentLoaded;

		public string Difficulty
		{
			get
			{
				return (string)base.GetValue(DifficultyControl.DifficultyProperty);
			}
			set
			{
				base.SetValue(DifficultyControl.DifficultyProperty, value);
				bool flag = !string.IsNullOrWhiteSpace(value);
				if (flag)
				{
					this.SetDifficulty(int.Parse(this.Difficulty));
				}
			}
		}

		public string[] ImageStr
		{
			get
			{
				return this.imageSource;
			}
			set
			{
				bool flag = this.imageSource != value;
				if (flag)
				{
					this.imageSource = value;
				}
			}
		}

		public DifficultyControl()
		{
			this.InitializeComponent();
			base.DataContext = this;
			this.SetDifficulty(0);
		}

		private void SetDifficulty(int d)
		{
			int i;
			for (i = 0; i < d / 2; i++)
			{
				this.ImageStr[i] = this.two;
			}
			bool flag = d % 2 != 0;
			if (flag)
			{
				this.ImageStr[i] = this.one;
				i++;
			}
			while (i < 5)
			{
				this.ImageStr[i] = this.zero;
				i++;
			}
		}

        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        //public void InitializeComponent()
        //{
        //    bool contentLoaded = this._contentLoaded;
        //    if (!contentLoaded)
        //    {
        //        this._contentLoaded = true;
        //        Uri resourceLocater = new Uri("/Bike;component/controls/difficultycontrol.xaml", UriKind.Relative);
        //        Application.LoadComponent(this, resourceLocater);
        //    }
        //}

        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
        //void IComponentConnector.Connect(int connectionId, object target)
        //{
        //    switch (connectionId)
        //    {
        //    case 1:
        //        this.image = (Image)target;
        //        break;
        //    case 2:
        //        this.image_Copy = (Image)target;
        //        break;
        //    case 3:
        //        this.image_Copy1 = (Image)target;
        //        break;
        //    case 4:
        //        this.image_Copy2 = (Image)target;
        //        break;
        //    case 5:
        //        this.image_Copy3 = (Image)target;
        //        break;
        //    default:
        //        this._contentLoaded = true;
        //        break;
        //    }
        //}
	}
}
