using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;
using Bike.Controls;

namespace Bike.Trainings
{
    public partial class VideoPage : Page, INotifyPropertyChanged, IComponentConnector
	{
		private ObservableCollection<Video> videoList;

		private static ObservableCollection<Video> showList;

		private string path = Environment.CurrentDirectory;

		private string videoPath = Environment.CurrentDirectory + "\\Video";

		private string imagePath = Environment.CurrentDirectory + "\\Image";

		private string slopePath = Environment.CurrentDirectory + "\\Slope";

		private string vedioType = ".mp4|.avi|.wma|.rmvb|.flash|.3gp|.mov|.asf|.flv|.swf|.mpeg|.mid";

		private string imageType = ".jpg|.bmp|.jpeg|.png";

		private DirectoryInfo vedioFolder;

		private DirectoryInfo ImageFolder;

		private bool distanceFlag = false;

		private bool timeFlag = false;

		private bool difficultFlag = false;

		private string inputPath;

		private FolderBrowserDialog folderDialog;

		private Thread inputFile;

        //internal System.Windows.Controls.ListBox VideoList;

        //internal System.Windows.Controls.Label label;

        //internal System.Windows.Controls.TextBox searchTexBox;

        //internal System.Windows.Controls.Label l1;

        //internal System.Windows.Controls.Label l2;

        //internal System.Windows.Controls.Label l3;

        //internal System.Windows.Controls.Label time;

        //internal System.Windows.Controls.Label diffculty;

        //internal System.Windows.Controls.Label distance;

        //internal System.Windows.Controls.Label refresh;

        //internal System.Windows.Controls.Button inputButton;

        //internal System.Windows.Controls.Button Video_Start;

        //internal System.Windows.Controls.Button Video_Start1;

        //private bool _contentLoaded;

		public event PropertyChangedEventHandler PropertyChanged;

		public ObservableCollection<Video> ShowList
		{
			get
			{
				return VideoPage.showList;
			}
			set
			{
				bool flag = VideoPage.showList != value;
				if (flag)
				{
					VideoPage.showList = value;
					bool flag2 = this.PropertyChanged != null;
					if (flag2)
					{
						this.PropertyChanged(this, new PropertyChangedEventArgs("ShowList"));
					}
				}
			}
		}

		public VideoPage()
		{
			this.InitializeComponent();
			this.languageInit();
			this.folderDialog = new FolderBrowserDialog();
			this.inputFile = new Thread(new ThreadStart(this.InputFile));
			base.DataContext = this;
			bool flag = !Directory.Exists(this.videoPath);
			if (flag)
			{
				System.Windows.MessageBox.Show(MultiLanguage.Warn7, MultiLanguage.Warn_t, MessageBoxButton.OK, MessageBoxImage.Exclamation);
				Directory.CreateDirectory(this.videoPath);
			}
			bool flag2 = !Directory.Exists(this.imagePath);
			if (flag2)
			{
				Directory.CreateDirectory(this.imagePath);
			}
			bool flag3 = !Directory.Exists(this.slopePath);
			if (flag3)
			{
				System.Windows.MessageBox.Show(MultiLanguage.Warn8, MultiLanguage.Warn_t, MessageBoxButton.OK, MessageBoxImage.Exclamation);
				Directory.CreateDirectory(this.slopePath);
			}
			bool flag4 = this.videoList == null;
			if (flag4)
			{
				this.videoList = new ObservableCollection<Video>();
				this.vedioFolder = new DirectoryInfo(this.videoPath);
				this.ImageFolder = new DirectoryInfo(this.imagePath);
				this.GetVedioInfo(0);
				bool flag5 = this.ShowList == null;
				if (flag5)
				{
					this.ShowList = new ObservableCollection<Video>(this.videoList);
				}
			}
		}

		private void languageInit()
		{
			this.inputButton.Content = MultiLanguage.addvideo;
			this.time.Content = MultiLanguage.Button_time;
			this.diffculty.Content = MultiLanguage.Button_difficult;
			this.distance.Content = MultiLanguage.Button_distance;
			this.refresh.Content = MultiLanguage.Button_refresh;
			this.Video_Start.Content = MultiLanguage.Start;
		}

		private void GetVedioInfo(int index)
		{
			bool flag = this.vedioFolder == null;
			if (!flag)
			{
				this.videoList.Clear();
				FileInfo[] files = this.vedioFolder.GetFiles();
				for (int i = 0; i < files.Length; i++)
				{
					FileInfo fileInfo = files[i];
					string extension = fileInfo.Extension;
					bool flag2 = extension == null;
					if (!flag2)
					{
						bool flag3 = !Regex.IsMatch(extension.ToLower(), this.vedioType, RegexOptions.ExplicitCapture);
						if (!flag3)
						{
							string name = fileInfo.Name;
							Video v = new Video();
							v.VideoPath = fileInfo.FullName;
							v.Name = name.Substring(0, name.Length - fileInfo.Extension.Length);
							string slope = this.slopePath + "\\" + v.Name + ".txt";
							bool flag4 = !File.Exists(slope);
							if (flag4)
							{
								v.Note = MultiLanguage.Warn9;
							}
							else
							{
								v.SlopePath = slope;
							}
							v.ImagePath = Environment.CurrentDirectory + "\\loading.png";
							v.Time = fileInfo.CreationTime;
							this.GetVedioConfige(v);
							this.videoList.Add(v);
						}
					}
				}
				bool flag5 = this.ImageFolder == null;
				if (!flag5)
				{
					FileInfo[] files2 = this.ImageFolder.GetFiles();
					for (int j = 0; j < files2.Length; j++)
					{
						FileInfo imageinfo = files2[j];
						string imageExtension = imageinfo.Extension;
						bool flag6 = !Regex.IsMatch(imageExtension.ToLower(), this.imageType);
						if (!flag6)
						{
							string imageName = imageinfo.Name;
							imageName = imageName.Substring(0, imageName.Length - imageExtension.Length);
							bool existFlag = false;
							foreach (Video v2 in this.videoList)
							{
								bool flag7 = v2.Name == imageName;
								if (flag7)
								{
									existFlag = true;
									v2.ImagePath = imageinfo.FullName;
								}
							}
							bool flag8 = !existFlag && index == 0;
							if (flag8)
							{
								File.Delete(imageinfo.FullName);
							}
						}
					}
				}
			}
		}

		private void GetVedioConfige(Video v)
		{
			string[] data = XMLHelper.VedioConfigeRead(XMLHelper.VedioConfige, v.Name);
			bool flag = data == null || data.Length != 2;
			if (!flag)
			{
				double distance = 0.0;
				int difficuty = 0;
				double.TryParse(data[0], out distance);
				int.TryParse(data[1], out difficuty);
				v.Diffculty = difficuty;
				v.Distance = distance;
			}
		}

		private void InputFile()
		{
			bool flag = string.IsNullOrWhiteSpace(this.inputPath);
			if (!flag)
			{
				DirectoryInfo inputFolder = new DirectoryInfo(this.inputPath);
				FileInfo[] files = inputFolder.GetFiles();
				for (int i = 0; i < files.Length; i++)
				{
					FileInfo file = files[i];
					string vedioName = file.Name;
					string extension = file.Extension;
					bool flag2 = !Regex.IsMatch(extension, this.vedioType);
					if (flag2)
					{
					}
				}
			}
		}

		private void Video_Start_Click(object sender, RoutedEventArgs e)
		{
			bool flag = this.VideoList.SelectedItem == null;
			if (flag)
			{
				System.Windows.MessageBox.Show(MultiLanguage.Warn10, MultiLanguage.Warn_t, MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
			else
			{
				Video v = this.VideoList.SelectedItem as Video;
				bool flag2 = v == null;
				if (flag2)
				{
					System.Windows.MessageBox.Show(MultiLanguage.Warn11, MultiLanguage.Warn_t, MessageBoxButton.OK, MessageBoxImage.Exclamation);
				}
				else
				{
					bool flag3 = string.IsNullOrEmpty(v.SlopePath);
					if (flag3)
					{
						System.Windows.MessageBox.Show(MultiLanguage.Warn12, MultiLanguage.Warn_t, MessageBoxButton.OK, MessageBoxImage.Exclamation);
					}
					else
					{
						bool flag4 = !GlobalData.Connectflag && !GlobalData.ANT_Connectflag;
						if (flag4)
						{
							System.Windows.MessageBox.Show(MultiLanguage.Warn13);
						}
						else
						{
							bool flag5 = !GlobalData.BBRejisterflag;
							if (flag5)
							{
								System.Windows.MessageBox.Show(MultiLanguage.Warn31);
							}
							else
							{
								bool flag6 = System.Windows.MessageBox.Show(MultiLanguage.Warn14, MultiLanguage.VideoTraining_t, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
								if (flag6)
								{
									VideoWindow vw = new VideoWindow(v);
									GlobalData.TrackName = v.Name;
									bool flag7 = !GlobalData.VideoTrainingflag;
									if (flag7)
									{
										GlobalData.VideoTrainingflag = true;
									}
									vw.ShowDialog();
								}
							}
						}
					}
				}
			}
		}

		private void Page_Unloaded(object sender, RoutedEventArgs e)
		{
		}

		private void Default_MouseUp(object sender, MouseButtonEventArgs e)
		{
			this.GetVedioInfo(1);
			VideoPage.showList.Clear();
			for (int i = 0; i < this.videoList.Count; i++)
			{
				VideoPage.showList.Add(this.videoList[i]);
			}
		}

		private void Distance_MouseUp(object sender, MouseButtonEventArgs e)
		{
			VideoPage.showList.Clear();
			for (int i = 0; i < this.videoList.Count; i++)
			{
				Video v = this.videoList[i];
				bool flag = VideoPage.showList.Count <= 0;
				if (flag)
				{
					VideoPage.showList.Add(v);
				}
				else
				{
					int j;
					for (j = 0; j < VideoPage.showList.Count; j++)
					{
						bool flag2 = this.distanceFlag;
						if (flag2)
						{
							bool flag3 = v.Distance > VideoPage.showList[j].Distance;
							if (flag3)
							{
								VideoPage.showList.Insert(j, v);
								break;
							}
						}
						else
						{
							bool flag4 = v.Distance < VideoPage.showList[j].Distance;
							if (flag4)
							{
								VideoPage.showList.Insert(j, v);
								break;
							}
						}
					}
					bool flag5 = j == VideoPage.showList.Count;
					if (flag5)
					{
						VideoPage.showList.Add(v);
					}
				}
			}
			this.distanceFlag = !this.distanceFlag;
		}

		private void Difficulty_MouseUp(object sender, MouseButtonEventArgs e)
		{
			VideoPage.showList.Clear();
			for (int i = 0; i < this.videoList.Count; i++)
			{
				Video v = this.videoList[i];
				bool flag = VideoPage.showList.Count <= 0;
				if (flag)
				{
					VideoPage.showList.Add(v);
				}
				else
				{
					int j;
					for (j = 0; j < VideoPage.showList.Count; j++)
					{
						bool flag2 = this.difficultFlag;
						if (flag2)
						{
							bool flag3 = v.Diffculty > VideoPage.showList[j].Diffculty;
							if (flag3)
							{
								VideoPage.showList.Insert(j, v);
								break;
							}
						}
						else
						{
							bool flag4 = v.Diffculty < VideoPage.showList[j].Diffculty;
							if (flag4)
							{
								VideoPage.showList.Insert(j, v);
								break;
							}
						}
					}
					bool flag5 = j == VideoPage.showList.Count;
					if (flag5)
					{
						VideoPage.showList.Add(v);
					}
				}
			}
			this.difficultFlag = !this.difficultFlag;
		}

		private void Time_MouseUp(object sender, MouseButtonEventArgs e)
		{
			VideoPage.showList.Clear();
			for (int i = 0; i < this.videoList.Count; i++)
			{
				Video v = this.videoList[i];
				bool flag = VideoPage.showList.Count <= 0;
				if (flag)
				{
					VideoPage.showList.Add(v);
				}
				else
				{
					int j;
					for (j = 0; j < VideoPage.showList.Count; j++)
					{
						bool flag2 = this.timeFlag;
						if (flag2)
						{
							bool flag3 = v.Time > VideoPage.showList[j].Time;
							if (flag3)
							{
								VideoPage.showList.Insert(j, v);
								break;
							}
						}
						else
						{
							bool flag4 = v.Time < VideoPage.showList[j].Time;
							if (flag4)
							{
								VideoPage.showList.Insert(j, v);
								break;
							}
						}
					}
					bool flag5 = j == VideoPage.showList.Count;
					if (flag5)
					{
						VideoPage.showList.Add(v);
					}
				}
			}
			this.timeFlag = !this.timeFlag;
		}

		private void search_TextChanged(object sender, TextChangedEventArgs e)
		{
			string searchStr = this.searchTexBox.Text;
			string[] sear = searchStr.Split(new char[]
			{
				' '
			});
			VideoPage.showList.Clear();
			for (int i = 0; i < this.videoList.Count; i++)
			{
				int j;
				for (j = 0; j < sear.Length; j++)
				{
					bool flag = !this.videoList[i].Name.Contains(sear[j]);
					if (flag)
					{
						break;
					}
				}
				bool flag2 = j == sear.Length;
				if (flag2)
				{
					VideoPage.showList.Add(this.videoList[i]);
				}
			}
		}

		private void Input_Click(object sender, RoutedEventArgs e)
		{
			bool flag = this.folderDialog.ShowDialog() == DialogResult.OK;
			if (flag)
			{
				this.inputPath = this.folderDialog.SelectedPath;
				RateShowWindow rs = new RateShowWindow(this.inputPath);
				rs.ShowDialog();
			}
		}

		private void VedioDelete_Click(object sender, RoutedEventArgs e)
		{
			bool flag = this.VideoList.SelectedItem == null;
			if (!flag)
			{
				Video v = this.VideoList.SelectedItem as Video;
				bool flag2 = v == null;
				if (!flag2)
				{
					bool flag3 = System.Windows.MessageBox.Show(MultiLanguage.Warn25 + v.Name + "?", MultiLanguage.Warn22, MessageBoxButton.YesNo) != MessageBoxResult.Yes;
					if (!flag3)
					{
						this.videoList.Remove(v);
						this.ShowList.Remove(v);
						File.Delete(v.SlopePath);
						File.Delete(v.VideoPath);
						XMLHelper.VedioConofigeDelete(XMLHelper.VedioConfige, v.Name);
					}
				}
			}
		}

		private void Video_Download_Click(object sender, RoutedEventArgs e)
		{
			bool flag = GlobalData.language == 1;
			if (flag)
			{
				Process.Start("http://www.Bike.com/video.php");
			}
			else
			{
				Process.Start("http://www.Bike.com/en/video.php");
			}
		}

        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        //public void InitializeComponent()
        //{
        //    bool contentLoaded = this._contentLoaded;
        //    if (!contentLoaded)
        //    {
        //        this._contentLoaded = true;
        //        Uri resourceLocater = new Uri("/Bike;component/trainings/videopage.xaml", UriKind.Relative);
        //        System.Windows.Application.LoadComponent(this, resourceLocater);
        //    }
        //}

        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
        //void IComponentConnector.Connect(int connectionId, object target)
        //{
        //    switch (connectionId)
        //    {
        //    case 1:
        //        ((VideoPage)target).Unloaded += new RoutedEventHandler(this.Page_Unloaded);
        //        break;
        //    case 2:
        //        this.VideoList = (System.Windows.Controls.ListBox)target;
        //        break;
        //    case 3:
        //        ((System.Windows.Controls.MenuItem)target).Click += new RoutedEventHandler(this.VedioDelete_Click);
        //        break;
        //    case 4:
        //        this.label = (System.Windows.Controls.Label)target;
        //        break;
        //    case 5:
        //        this.searchTexBox = (System.Windows.Controls.TextBox)target;
        //        this.searchTexBox.TextChanged += new TextChangedEventHandler(this.search_TextChanged);
        //        break;
        //    case 6:
        //        this.l1 = (System.Windows.Controls.Label)target;
        //        break;
        //    case 7:
        //        this.l2 = (System.Windows.Controls.Label)target;
        //        break;
        //    case 8:
        //        this.l3 = (System.Windows.Controls.Label)target;
        //        break;
        //    case 9:
        //        this.time = (System.Windows.Controls.Label)target;
        //        this.time.MouseUp += new MouseButtonEventHandler(this.Time_MouseUp);
        //        break;
        //    case 10:
        //        this.diffculty = (System.Windows.Controls.Label)target;
        //        this.diffculty.MouseUp += new MouseButtonEventHandler(this.Difficulty_MouseUp);
        //        break;
        //    case 11:
        //        this.distance = (System.Windows.Controls.Label)target;
        //        this.distance.MouseUp += new MouseButtonEventHandler(this.Distance_MouseUp);
        //        break;
        //    case 12:
        //        this.refresh = (System.Windows.Controls.Label)target;
        //        this.refresh.MouseUp += new MouseButtonEventHandler(this.Default_MouseUp);
        //        break;
        //    case 13:
        //        this.inputButton = (System.Windows.Controls.Button)target;
        //        this.inputButton.Click += new RoutedEventHandler(this.Input_Click);
        //        break;
        //    case 14:
        //        this.Video_Start = (System.Windows.Controls.Button)target;
        //        this.Video_Start.Click += new RoutedEventHandler(this.Video_Start_Click);
        //        break;
        //    case 15:
        //        this.Video_Start1 = (System.Windows.Controls.Button)target;
        //        this.Video_Start1.Click += new RoutedEventHandler(this.Video_Download_Click);
        //        break;
        //    default:
        //        this._contentLoaded = true;
        //        break;
        //    }
        //}
	}
}
