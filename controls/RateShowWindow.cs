using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Bike.Controls
{
    public partial class RateShowWindow : Window, INotifyPropertyChanged, IComponentConnector
	{
		private string vedioType = ".mp4|.avi|.wma|.rmvb|.flash|.3gp|.mov|.asf|.flv|.swf|.mpeg|.mid";

		private string imageType = ".jpg|.bmp|.jpeg|.png";

		private string path;

		private ObservableCollection<InputVedio> vedioList;

		private Thread thread;

		private string buttonName = MultiLanguage.startInput;

        //internal ListBox listBox;

        //internal Button inputButton;

        //internal Label label;

        //private bool _contentLoaded;

		public event PropertyChangedEventHandler PropertyChanged;

		public string ButtonName
		{
			get
			{
				return this.buttonName;
			}
			set
			{
				bool flag = this.buttonName != value;
				if (flag)
				{
					this.buttonName = value;
					bool flag2 = this.PropertyChanged != null;
					if (flag2)
					{
						this.PropertyChanged(this, new PropertyChangedEventArgs("ButtonName"));
					}
				}
			}
		}

		public RateShowWindow(string _path)
		{
			this.InitializeComponent();
			this.vedioList = new ObservableCollection<InputVedio>();
			this.listBox.DataContext = this.vedioList;
			base.DataContext = this;
			this.path = _path;
			this.ReadVedioFile();
		}

		private void ReadVedioFile()
		{
			bool flag = !Directory.Exists(this.path);
			if (flag)
			{
				MessageBox.Show(MultiLanguage.Warn20);
			}
			else
			{
				DirectoryInfo directory = new DirectoryInfo(this.path);
				Dictionary<string, FileInfo> vedioDict = new Dictionary<string, FileInfo>();
				Dictionary<string, FileInfo> imageDict = new Dictionary<string, FileInfo>();
				Dictionary<string, FileInfo> slopeDict = new Dictionary<string, FileInfo>();
				Dictionary<string, FileInfo> configeDict = new Dictionary<string, FileInfo>();
				FileInfo[] files = directory.GetFiles();
				for (int i = 0; i < files.Length; i++)
				{
					FileInfo file = files[i];
					string extension = file.Extension;
					string name = file.Name.Substring(0, file.Name.Length - extension.Length);
					bool flag2 = Regex.IsMatch(extension.ToLower(), this.vedioType);
					if (flag2)
					{
						bool flag3 = !vedioDict.ContainsKey(name);
						if (flag3)
						{
							vedioDict.Add(name, file);
						}
					}
					else
					{
						bool flag4 = Regex.IsMatch(extension.ToLower(), this.imageType);
						if (flag4)
						{
							bool flag5 = !imageDict.ContainsKey(name);
							if (flag5)
							{
								imageDict.Add(name, file);
							}
						}
						else
						{
							bool flag6 = Regex.IsMatch(extension.ToLower(), ".txt");
							if (flag6)
							{
								bool flag7 = !slopeDict.ContainsKey(name);
								if (flag7)
								{
									slopeDict.Add(name, file);
								}
							}
							else
							{
								bool flag8 = Regex.IsMatch(extension.ToLower(), ".ini");
								if (flag8)
								{
									bool flag9 = !configeDict.ContainsKey(name);
									if (flag9)
									{
										configeDict.Add(name, file);
									}
								}
							}
						}
					}
				}
				foreach (string key in vedioDict.Keys)
				{
					InputVedio v = new InputVedio();
					v.Name = key;
					v.VedioExtension = vedioDict[key].Extension;
					v.VideoPath = vedioDict[key].FullName;
					this.vedioList.Add(v);
					bool flag10 = !imageDict.ContainsKey(key);
					if (flag10)
					{
						v.Note = MultiLanguage.Note1;
					}
					else
					{
						v.ImagePath = imageDict[key].FullName;
						v.ImageExtension = imageDict[key].Extension;
						bool flag11 = !slopeDict.ContainsKey(key);
						if (flag11)
						{
							v.Note = MultiLanguage.Note1;
						}
						else
						{
							v.SlopePath = slopeDict[key].FullName;
							bool flag12 = !configeDict.ContainsKey(key);
							if (flag12)
							{
								v.Note = MultiLanguage.Note1;
							}
							else
							{
								bool flag13 = !this.ReadConfigeFile(configeDict[key], v);
								if (flag13)
								{
									v.Note = MultiLanguage.Note2;
									break;
								}
								v.CanInput = true;
							}
						}
					}
				}
			}
		}

		private bool ReadConfigeFile(FileInfo file, InputVedio v)
		{
			bool result;
			using (StreamReader fs = new StreamReader(file.FullName))
			{
				string diffStr = fs.ReadLine();
				string[] diff = diffStr.Split(new char[]
				{
					'='
				});
				bool flag = diff.Length != 2;
				if (flag)
				{
					result = false;
				}
				else
				{
					int difficulty;
					bool flag2 = !int.TryParse(diff[1], out difficulty);
					if (flag2)
					{
						result = false;
					}
					else
					{
						v.Diffculty = difficulty;
						string disStr = fs.ReadLine();
						string[] disS = disStr.Split(new char[]
						{
							'='
						});
						bool flag3 = disS.Length != 2;
						if (flag3)
						{
							result = false;
						}
						else
						{
							double distance;
							bool flag4 = !double.TryParse(disS[1], out distance);
							if (flag4)
							{
								result = false;
							}
							else
							{
								v.Distance = distance;
								fs.Dispose();
								fs.Close();
								result = true;
							}
						}
					}
				}
			}
			return result;
		}

		private void InputVedio()
		{
			foreach (InputVedio v in this.vedioList)
			{
				bool flag = !v.CanInput;
				if (!flag)
				{
					string[] vdata = XMLHelper.VedioConfigeRead(XMLHelper.VedioConfige, v.Name);
					bool flag2 = vdata != null;
					if (flag2)
					{
						v.Note = MultiLanguage.Note3;
					}
					else
					{
						v.CopyVedio();
					}
				}
			}
			this.ButtonName = MultiLanguage.FinishInput;
		}

		private void InputClick(object sender, RoutedEventArgs e)
		{
			bool flag = this.ButtonName == MultiLanguage.FinishInput;
			if (flag)
			{
				base.Close();
			}
			else
			{
				bool flag2 = this.ButtonName != MultiLanguage.startInput;
				if (!flag2)
				{
					this.ButtonName = MultiLanguage.Inputing;
					this.thread = new Thread(new ThreadStart(this.InputVedio));
					this.thread.Start();
				}
			}
		}

		private void Window_Closing(object sender, CancelEventArgs e)
		{
			bool flag = this.ButtonName == MultiLanguage.Inputing;
			if (flag)
			{
				bool flag2 = MessageBox.Show(MultiLanguage.Warn21, MultiLanguage.Warn22, MessageBoxButton.YesNo) != MessageBoxResult.Yes;
				if (flag2)
				{
					e.Cancel = true;
				}
			}
		}

		private void DeleteClick(object sender, RoutedEventArgs e)
		{
			bool flag = this.ButtonName == MultiLanguage.Inputing;
			if (flag)
			{
				MessageBox.Show(MultiLanguage.Warn23);
			}
			else
			{
				bool flag2 = this.listBox.SelectedItem == null;
				if (!flag2)
				{
					InputVedio v = this.listBox.SelectedItem as InputVedio;
					bool flag3 = v == null;
					if (!flag3)
					{
						this.vedioList.Remove(v);
					}
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
        //        Uri resourceLocater = new Uri("/Bike;component/controls/rateshowwindow.xaml", UriKind.Relative);
        //        Application.LoadComponent(this, resourceLocater);
        //    }
        //}

        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
        //void IComponentConnector.Connect(int connectionId, object target)
        //{
        //    switch (connectionId)
        //    {
        //    case 1:
        //        ((RateShowWindow)target).Closing += new CancelEventHandler(this.Window_Closing);
        //        break;
        //    case 2:
        //        this.listBox = (ListBox)target;
        //        break;
        //    case 3:
        //        ((MenuItem)target).Click += new RoutedEventHandler(this.DeleteClick);
        //        break;
        //    case 4:
        //        this.inputButton = (Button)target;
        //        this.inputButton.Click += new RoutedEventHandler(this.InputClick);
        //        break;
        //    case 5:
        //        this.label = (Label)target;
        //        break;
        //    default:
        //        this._contentLoaded = true;
        //        break;
        //    }
        //}
	}
}
