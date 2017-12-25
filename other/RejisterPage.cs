using FinalClient;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Threading;

namespace Bike.Other
{
    public partial class RejisterPage : Page, IComponentConnector
	{
		public delegate void RegisterW();

		[CompilerGenerated]
		[Serializable]
		private sealed class DifficultyControlPropertyChange
		{
			public static readonly RejisterPage.DifficultyControlPropertyChange difficultyControlChange = new RejisterPage.DifficultyControlPropertyChange();

			public static Action difficultyControlChange__26_7;

			internal void DisLogin()
			{
				GlobalData.Rememberflag = false;
			}
		}

		private readonly ProtocolBase proto;

		public static ObservableCollection<string> loginList;

		private string token;

		private string appId;

		private string appKey;

		private string phoneNumber;

		private string verifycode;

		private string appSecret;

		private string Secret;

        //internal TabControl tabControl;

        //internal Label UserName;

        //internal TextBox UserNametextBox;

        //internal Label Password;

        //internal PasswordBox passwordBox;

        //internal Button Loginbutton;

        //internal Button Exitbutton;

        //internal Label Register;

        //internal Label Forget;

        //internal CheckBox remember_check;

        //internal CheckBox automatic_check;

        //internal Label PhoneNumbe;

        //internal TextBox PhoneNumbertextBox;

        //internal Label Verify;

        //internal TextBox VerifyBox;

        //internal Button Loginbutton2;

        //internal Button Exitbutton2;

        //internal Label Reminder;

        //internal Button SendVerifyButton;

        //internal TextBlock textBlock;

        //internal ComboBox LoginStylecomboBox;

        //internal Label UserInformation;

        //private bool _contentLoaded;

		public static event RejisterPage.RegisterW username;

		public RejisterPage()
		{
			this.InitializeComponent();
			this.languageInit();
			this.UserNametextBox.Focus();
			bool flag = !GlobalData.Connectionflag || !Connection.isConnected;
			if (flag)
			{
				Connection.Start();
			}
			bool isConnected = Connection.isConnected;
			if (isConnected)
			{
				GlobalData.Connectionflag = true;
			}
			bool rejisterflag = GlobalData.Rejisterflag;
			if (rejisterflag)
			{
				this.UserNametextBox.Text = GlobalData.UserName;
				this.passwordBox.Password = GlobalData.Password;
				this.UserNametextBox.IsEnabled = false;
				this.passwordBox.IsEnabled = false;
				bool rememberflag = GlobalData.Rememberflag;
				if (rememberflag)
				{
					this.remember_check.IsChecked = new bool?(true);
					this.remember_check.IsEnabled = false;
				}
				bool flag2 = GlobalData.Automaticflag == "1";
				if (flag2)
				{
					this.automatic_check.IsChecked = new bool?(true);
					this.automatic_check.IsEnabled = false;
				}
			}
			bool flag3 = !GlobalData.Rejisterflag;
			if (flag3)
			{
				bool flag4 = XMLHelper.UsersGetNum() > 0;
				if (flag4)
				{
					this.UserNametextBox.Text = XMLHelper.UsersReader("UsersFile", 0);
					this.passwordBox.Password = XMLHelper.UsersReader("UsersFile", 1);
					this.remember_check.IsChecked = new bool?(true);
					GlobalData.Rememberflag = true;
					bool flag5 = XMLHelper.UsersReader("UsersFile", 2) == "1";
					if (flag5)
					{
						this.Login();
						this.automatic_check.IsChecked = new bool?(true);
						GlobalData.Automaticflag = "1";
					}
				}
			}
			Connection.login += new Connection.RegisterW(this.LoginReturn);
			Connection.dislogin += new Connection.Registerp(this.DisLogin);
			RejisterPage.loginList = new ObservableCollection<string>();
			RejisterPage.loginList.Add(MultiLanguage.UHuser);
			RejisterPage.loginList.Add(MultiLanguage.BBuser);
			this.LoginStylecomboBox.DataContext = RejisterPage.loginList;
			bool flag6 = GlobalData.userStyle == 1;
			if (flag6)
			{
				this.UserName.Visibility = Visibility.Collapsed;
				this.UserNametextBox.Visibility = Visibility.Collapsed;
				this.Password.Visibility = Visibility.Collapsed;
				this.passwordBox.Visibility = Visibility.Collapsed;
				this.Loginbutton.Visibility = Visibility.Collapsed;
				this.Exitbutton.Visibility = Visibility.Collapsed;
				this.Register.Visibility = Visibility.Collapsed;
				this.Forget.Visibility = Visibility.Collapsed;
				this.remember_check.Visibility = Visibility.Collapsed;
				this.automatic_check.Visibility = Visibility.Collapsed;
				this.PhoneNumbe.Visibility = Visibility.Visible;
				this.PhoneNumbertextBox.Visibility = Visibility.Visible;
				this.Verify.Visibility = Visibility.Visible;
				this.VerifyBox.Visibility = Visibility.Visible;
				this.Loginbutton2.Visibility = Visibility.Visible;
				this.Exitbutton2.Visibility = Visibility.Visible;
				this.Reminder.Visibility = Visibility.Visible;
				this.SendVerifyButton.Visibility = Visibility.Visible;
				this.LoginStylecomboBox.SelectedIndex = 1;
			}
			else
			{
				this.UserName.Visibility = Visibility.Visible;
				this.UserNametextBox.Visibility = Visibility.Visible;
				this.Password.Visibility = Visibility.Visible;
				this.passwordBox.Visibility = Visibility.Visible;
				this.Loginbutton.Visibility = Visibility.Visible;
				this.Exitbutton.Visibility = Visibility.Visible;
				this.Register.Visibility = Visibility.Visible;
				this.Forget.Visibility = Visibility.Visible;
				this.remember_check.Visibility = Visibility.Visible;
				this.automatic_check.Visibility = Visibility.Visible;
				this.PhoneNumbe.Visibility = Visibility.Collapsed;
				this.PhoneNumbertextBox.Visibility = Visibility.Collapsed;
				this.Verify.Visibility = Visibility.Collapsed;
				this.VerifyBox.Visibility = Visibility.Collapsed;
				this.Loginbutton2.Visibility = Visibility.Collapsed;
				this.Exitbutton2.Visibility = Visibility.Collapsed;
				this.Reminder.Visibility = Visibility.Collapsed;
				this.SendVerifyButton.Visibility = Visibility.Collapsed;
				this.LoginStylecomboBox.SelectedIndex = 0;
			}
			bool bBRejisterflag = GlobalData.BBRejisterflag;
			if (bBRejisterflag)
			{
				this.PhoneNumbertextBox.Text = GlobalData.phoneNumber;
				this.VerifyBox.Text = GlobalData.verifycode;
				this.PhoneNumbertextBox.IsEnabled = false;
				this.VerifyBox.IsEnabled = false;
				this.SendVerifyButton.IsEnabled = false;
			}
			else
			{
				bool flag7 = XMLHelper.BBUsersGetNum() > 0;
				if (flag7)
				{
					GlobalData.BBRejisterflag = true;
					GlobalData.phoneNumber = XMLHelper.BBUsersReader("BBUsersFile", 0);
					GlobalData.verifycode = XMLHelper.BBUsersReader("BBUsersFile", 1);
					GlobalData.weight = XMLHelper.BBUsersReader("BBUsersFile", 2);
					GlobalData.openId = XMLHelper.BBUsersReader("BBUsersFile", 3);
					this.PhoneNumbertextBox.Text = GlobalData.phoneNumber;
					this.VerifyBox.Text = GlobalData.verifycode;
					this.PhoneNumbertextBox.IsEnabled = false;
					this.VerifyBox.IsEnabled = false;
					this.SendVerifyButton.IsEnabled = false;
				}
			}
			bool flag8 = XMLHelper.UsersInforGetNum() > 0;
			if (flag8)
			{
				GlobalData.Size = XMLHelper.UsersInforReader("Information", 7);
			}
		}

		private void Exitbutton_Click(object sender, RoutedEventArgs e)
		{
			this.UserNametextBox.IsEnabled = true;
			this.passwordBox.IsEnabled = true;
			this.UserNametextBox.Clear();
			this.passwordBox.Clear();
			this.UserNametextBox.Focus();
			XMLHelper.UsersDeleter("UsersFile");
			bool rejisterflag = GlobalData.Rejisterflag;
			if (rejisterflag)
			{
				CustomProtocol proto = new CustomProtocol();
				proto.AddString("EE1");
				proto.AddString(GlobalData.UserName);
				Connection.Send(proto);
			}
			this.remember_check.IsChecked = new bool?(false);
			this.remember_check.IsEnabled = true;
			GlobalData.Rememberflag = false;
			this.automatic_check.IsChecked = new bool?(false);
			this.automatic_check.IsEnabled = true;
			GlobalData.Automaticflag = "0";
			GlobalData.Rejisterflag = false;
			bool flag = RejisterPage.username != null;
			if (flag)
			{
				RejisterPage.username();
			}
		}

		private void Register_MouseUp(object sender, RoutedEventArgs e)
		{
			RegisterWindow rw = new RegisterWindow();
			rw.ShowDialog();
		}

		private void Forget_MouseUp(object sender, RoutedEventArgs e)
		{
			bool flag = GlobalData.language == 1;
			if (flag)
			{
				Process.Start("http://www.Bike.com/forget.php");
			}
			else
			{
				Process.Start("http://www.Bike.com/en/forget.php");
			}
		}

		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			bool flag = e.Key == Key.Return;
			if (flag)
			{
				this.Loginbutton_Click(sender, null);
			}
			bool flag2 = e.Key == Key.Escape;
			if (flag2)
			{
				Application.Current.Shutdown();
			}
		}

		private void Loginbutton_Click(object sender, RoutedEventArgs e)
		{
			string UserName = this.UserNametextBox.Text.Trim();
			string PassWord = this.passwordBox.Password.Trim();
			GlobalData.UserName = UserName;
			GlobalData.Password = PassWord;
			bool flag = GlobalData.Connectflag && !Connection.isConnected;
			if (flag)
			{
				Connection.Start();
			}
			CustomProtocol proto = new CustomProtocol();
			proto.AddString("EA1");
			proto.AddString(UserName);
			proto.AddString(PassWord);
			Connection.Send(proto);
			base.Dispatcher.Invoke(delegate
			{
				this.remember_check.IsEnabled = true;
			});
			base.Dispatcher.Invoke(delegate
			{
				this.automatic_check.IsEnabled = true;
			});
		}

		private void languageInit()
		{
			this.UserName.Content = MultiLanguage.UserName;
			this.Password.Content = MultiLanguage.Password;
			this.Loginbutton.Content = MultiLanguage.Button_login;
			this.Exitbutton.Content = MultiLanguage.Button_exit;
			this.Register.Content = MultiLanguage.Button_register;
			this.textBlock.Text = MultiLanguage.Rejister_t;
			this.remember_check.Content = MultiLanguage.Remember;
			this.automatic_check.Content = MultiLanguage.Automatic;
			this.Forget.Content = MultiLanguage.Forget;
			this.UserInformation.Content = MultiLanguage.UserInfor;
			this.Loginbutton2.Content = MultiLanguage.Button_login;
			this.Exitbutton2.Content = MultiLanguage.Button_exit;
			this.Reminder.Content = MultiLanguage.reminder;
			this.SendVerifyButton.Content = MultiLanguage.sendcode;
			this.PhoneNumbe.Content = MultiLanguage.phoneNumbe;
			this.Verify.Content = MultiLanguage.verify;
		}

		public void LoginReturn(ProtocolBase proto)
		{
			CustomProtocol protocol = (CustomProtocol)proto;
			int start = 0;
			string str = protocol.GetString(start, ref start);
			int ret = protocol.GetInt(start, ref start);
			int num = ret;
			if (num != 0)
			{
				if (num != 2)
				{
					MessageBox.Show(MultiLanguage.Warn28, MultiLanguage.Warn_t);
					base.Dispatcher.Invoke(delegate
					{
						this.UserNametextBox.IsEnabled = true;
					});
					base.Dispatcher.Invoke(delegate
					{
						this.passwordBox.IsEnabled = true;
					});
					base.Dispatcher.Invoke(delegate
					{
						this.remember_check.IsEnabled = true;
					});
					base.Dispatcher.Invoke(delegate
					{
						this.automatic_check.IsEnabled = true;
					});
					base.Dispatcher.Invoke(delegate
					{
						this.UserNametextBox.Clear();
					});
					base.Dispatcher.Invoke(delegate
					{
						this.passwordBox.Clear();
					});
					base.Dispatcher.Invoke(delegate
					{
						this.UserNametextBox.Focus();
					});
					base.Dispatcher.Invoke(delegate
					{
						this.remember_check.IsChecked = new bool?(false);
					});
					base.Dispatcher.Invoke(delegate
					{
						this.automatic_check.IsChecked = new bool?(false);
					});
					GlobalData.Rejisterflag = false;
				}
				else
				{
					MessageBox.Show(MultiLanguage.Warn29, MultiLanguage.Warn_t);
					base.Dispatcher.Invoke(delegate
					{
						this.UserNametextBox.IsEnabled = true;
					});
					base.Dispatcher.Invoke(delegate
					{
						this.passwordBox.IsEnabled = true;
					});
					base.Dispatcher.Invoke(delegate
					{
						this.remember_check.IsEnabled = true;
					});
					base.Dispatcher.Invoke(delegate
					{
						this.automatic_check.IsEnabled = true;
					});
					base.Dispatcher.Invoke(delegate
					{
						this.UserNametextBox.Clear();
					});
					base.Dispatcher.Invoke(delegate
					{
						this.passwordBox.Clear();
					});
					base.Dispatcher.Invoke(delegate
					{
						this.UserNametextBox.Focus();
					});
					base.Dispatcher.Invoke(delegate
					{
						this.remember_check.IsChecked = new bool?(false);
					});
					base.Dispatcher.Invoke(delegate
					{
						this.automatic_check.IsChecked = new bool?(false);
					});
					GlobalData.Rejisterflag = false;
				}
			}
			else
			{
				MessageBox.Show(MultiLanguage.Hint22, MultiLanguage.Warn);
				base.Dispatcher.Invoke(delegate
				{
					this.UserNametextBox.IsEnabled = false;
				});
				base.Dispatcher.Invoke(delegate
				{
					this.passwordBox.IsEnabled = false;
				});
				base.Dispatcher.Invoke(delegate
				{
					this.remember_check.IsEnabled = false;
				});
				base.Dispatcher.Invoke(delegate
				{
					this.automatic_check.IsEnabled = false;
				});
				GlobalData.Rejisterflag = true;
				XMLHelper.UsersWriter("UsersFile");
				bool flag = RejisterPage.username != null;
				if (flag)
				{
					RejisterPage.username();
				}
			}
		}

		private void Remember_check(object sender, RoutedEventArgs e)
		{
			GlobalData.Rememberflag = true;
		}

		private void Automatic_check(object sender, RoutedEventArgs e)
		{
			GlobalData.Rememberflag = true;
			GlobalData.Automaticflag = "1";
			this.remember_check.IsChecked = new bool?(true);
		}

		private void Remember_uncheck(object sender, RoutedEventArgs e)
		{
			GlobalData.Rememberflag = false;
		}

		private void Automatic_uncheck(object sender, RoutedEventArgs e)
		{
			GlobalData.Automaticflag = "0";
		}

		private void Login()
		{
			string UserName = this.UserNametextBox.Text.Trim();
			string PassWord = this.passwordBox.Password.Trim();
			GlobalData.UserName = UserName;
			GlobalData.Password = PassWord;
			bool flag = GlobalData.Connectflag && !Connection.isConnected;
			if (flag)
			{
				Connection.Start();
			}
			CustomProtocol proto = new CustomProtocol();
			proto.AddString("EA1");
			proto.AddString(UserName);
			proto.AddString(PassWord);
			Connection.Send(proto);
			base.Dispatcher.Invoke(delegate
			{
				this.remember_check.IsEnabled = true;
			});
			base.Dispatcher.Invoke(delegate
			{
				this.automatic_check.IsEnabled = true;
			});
		}

		private void DisLogin()
		{
			base.Dispatcher.Invoke(delegate
			{
				this.UserNametextBox.IsEnabled = true;
			});
			base.Dispatcher.Invoke(delegate
			{
				this.passwordBox.IsEnabled = true;
			});
			base.Dispatcher.Invoke(delegate
			{
				this.UserNametextBox.Clear();
			});
			base.Dispatcher.Invoke(delegate
			{
				this.passwordBox.Clear();
			});
			base.Dispatcher.Invoke(delegate
			{
				this.UserNametextBox.Focus();
			});
			XMLHelper.UsersDeleter("UsersFile");
			base.Dispatcher.Invoke(delegate
			{
				this.remember_check.IsChecked = new bool?(false);
			});
			base.Dispatcher.Invoke(delegate
			{
				this.remember_check.IsEnabled = true;
			});
			Dispatcher arg_D9_0 = base.Dispatcher;
			Action arg_D9_1;
			if ((arg_D9_1 = RejisterPage.DifficultyControlPropertyChange.difficultyControlChange__26_7) == null)
			{
				arg_D9_1 = (RejisterPage.DifficultyControlPropertyChange.difficultyControlChange__26_7 = new Action(RejisterPage.DifficultyControlPropertyChange.difficultyControlChange.DisLogin));
			}
			arg_D9_0.Invoke(arg_D9_1);
			base.Dispatcher.Invoke(delegate
			{
				this.automatic_check.IsChecked = new bool?(false);
			});
			base.Dispatcher.Invoke(delegate
			{
				this.automatic_check.IsEnabled = true;
			});
			GlobalData.Automaticflag = "0";
			GlobalData.Rejisterflag = false;
			bool flag = RejisterPage.username != null;
			if (flag)
			{
				RejisterPage.username();
			}
		}

		private void Page_Unloaded(object sender, RoutedEventArgs e)
		{
			Connection.login -= new Connection.RegisterW(this.LoginReturn);
		}

		private void UserInformation_MouseUp(object sender, MouseButtonEventArgs e)
		{
			InformationWindow iw = new InformationWindow();
			iw.ShowDialog();
		}

		private void changeLoginStyle(object sender, EventArgs e)
		{
			bool flag = this.LoginStylecomboBox.SelectionBoxItem.ToString() == MultiLanguage.BBuser;
			if (flag)
			{
				GlobalData.userStyle = 1;
				this.UserName.Visibility = Visibility.Collapsed;
				this.UserNametextBox.Visibility = Visibility.Collapsed;
				this.Password.Visibility = Visibility.Collapsed;
				this.passwordBox.Visibility = Visibility.Collapsed;
				this.Loginbutton.Visibility = Visibility.Collapsed;
				this.Exitbutton.Visibility = Visibility.Collapsed;
				this.Register.Visibility = Visibility.Collapsed;
				this.Forget.Visibility = Visibility.Collapsed;
				this.remember_check.Visibility = Visibility.Collapsed;
				this.automatic_check.Visibility = Visibility.Collapsed;
				this.PhoneNumbe.Visibility = Visibility.Visible;
				this.PhoneNumbertextBox.Visibility = Visibility.Visible;
				this.Verify.Visibility = Visibility.Visible;
				this.VerifyBox.Visibility = Visibility.Visible;
				this.Loginbutton2.Visibility = Visibility.Visible;
				this.Exitbutton2.Visibility = Visibility.Visible;
				this.Reminder.Visibility = Visibility.Visible;
				this.SendVerifyButton.Visibility = Visibility.Visible;
			}
			bool flag2 = this.LoginStylecomboBox.SelectionBoxItem.ToString() == MultiLanguage.UHuser;
			if (flag2)
			{
				GlobalData.userStyle = 0;
				this.UserName.Visibility = Visibility.Visible;
				this.UserNametextBox.Visibility = Visibility.Visible;
				this.Password.Visibility = Visibility.Visible;
				this.passwordBox.Visibility = Visibility.Visible;
				this.Loginbutton.Visibility = Visibility.Visible;
				this.Exitbutton.Visibility = Visibility.Visible;
				this.Register.Visibility = Visibility.Visible;
				this.Forget.Visibility = Visibility.Visible;
				this.remember_check.Visibility = Visibility.Visible;
				this.automatic_check.Visibility = Visibility.Visible;
				this.PhoneNumbe.Visibility = Visibility.Collapsed;
				this.PhoneNumbertextBox.Visibility = Visibility.Collapsed;
				this.Verify.Visibility = Visibility.Collapsed;
				this.VerifyBox.Visibility = Visibility.Collapsed;
				this.Loginbutton2.Visibility = Visibility.Collapsed;
				this.Exitbutton2.Visibility = Visibility.Collapsed;
				this.Reminder.Visibility = Visibility.Collapsed;
				this.SendVerifyButton.Visibility = Visibility.Collapsed;
			}
		}

		private static string HttpGetConnectToServer(string serverUrl, string postData)
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serverUrl + "?" + postData);
			request.Method = "GET";
			request.ContentType = "application/x-www-form-urlencoded";
			request.Credentials = CredentialCache.DefaultCredentials;
			request.Timeout = 10000;
			string res;
			string result;
			try
			{
				HttpWebResponse response = (HttpWebResponse)request.GetResponse();
				StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
				res = reader.ReadToEnd();
				reader.Close();
			}
			catch (Exception ex)
			{
				result = "{\"error\":\"connectToServer\",\"error_description\":\"" + ex.Message + "\"}";
				return result;
			}
			result = res;
			return result;
		}

		private void SendVerify(object sender, RoutedEventArgs e)
		{
			this.appKey = "SK5S68F7DZKKWIL3";
			this.appSecret = "IHL982FMLT1GTJYU5VNDNSS9N7T8EZIA";
			this.phoneNumber = this.PhoneNumbertextBox.Text;
			string serverUrl = "http://client.blackbirdsport.com/api/requestConnectVerifyCode";
			string sign = RejisterPage.getSignature(new SortedDictionary<string, string>
			{
				{
					"ak",
					this.appKey
				},
				{
					"userId",
					this.phoneNumber
				}
			}, this.appSecret);
			this.Secret = sign;
			string postData = string.Format("ak={0}&userId={1}&sign={2}", this.appKey, this.phoneNumber, this.Secret);
			string @return = RejisterPage.HttpGetConnectToServer(serverUrl, postData);
			string flag = @return.Substring(11, 2);
			bool flag2 = flag == "ok";
			if (flag2)
			{
				MessageBox.Show("请输入手机验证码");
			}
			else
			{
				string error = @return.Substring(65, 6);
				string a = error;
				if (!(a == "000001"))
				{
					if (!(a == "010001"))
					{
						if (!(a == "010007"))
						{
							if (a == "010010")
							{
								MessageBox.Show("用户不存在");
							}
						}
						else
						{
							MessageBox.Show("验证码错误");
						}
					}
					else
					{
						MessageBox.Show("ak或签名错误");
					}
				}
				else
				{
					MessageBox.Show("未知其他错误");
				}
			}
		}

		private void Loginbutton2_Click(object sender, RoutedEventArgs e)
		{
			this.appKey = "SK5S68F7DZKKWIL3";
			this.appSecret = "IHL982FMLT1GTJYU5VNDNSS9N7T8EZIA";
			this.verifycode = this.VerifyBox.Text;
			string serverUrl = "http://client.blackbirdsport.com/api/bikeConnect";
			string sign2 = RejisterPage.getSignature(new SortedDictionary<string, string>
			{
				{
					"ak",
					this.appKey
				},
				{
					"userId",
					this.phoneNumber
				},
				{
					"verifyCode",
					this.verifycode
				}
			}, this.appSecret);
			this.Secret = sign2;
			string postData = string.Format("ak={0}&userId={1}&verifyCode={2}&sign={3}", new object[]
			{
				this.appKey,
				this.phoneNumber,
				this.verifycode,
				this.Secret
			});
			string return2 = RejisterPage.HttpGetConnectToServer(serverUrl, postData);
			string flag = return2.Substring(11, 2);
			bool flag2 = flag == "ok";
			if (flag2)
			{
				MessageBox.Show("登陆成功！");
				this.PhoneNumbertextBox.IsEnabled = false;
				this.VerifyBox.IsEnabled = false;
				this.SendVerifyButton.IsEnabled = false;
				GlobalData.BBRejisterflag = true;
				GlobalData.phoneNumber = this.phoneNumber;
				GlobalData.verifycode = this.verifycode;
				for (int i = 57; i <= 60; i++)
				{
					bool flag3 = return2.Substring(i, 1) == ",";
					if (flag3)
					{
						GlobalData.weight = return2.Substring(57, i - 57);
					}
				}
				GlobalData.openId = return2.Substring(return2.Length - 9, 6);
				XMLHelper.BBUsersWriter("BBUsersFile");
			}
			else
			{
				string error = return2.Substring(52, 6);
				string a = error;
				if (!(a == "000001"))
				{
					if (!(a == "010001"))
					{
						if (!(a == "010007"))
						{
							if (a == "010010")
							{
								MessageBox.Show("用户不存在");
							}
						}
						else
						{
							MessageBox.Show("验证码错误");
						}
					}
					else
					{
						MessageBox.Show("ak或签名错误");
					}
				}
				else
				{
					MessageBox.Show("未知其他错误");
				}
			}
		}

		private void Exitbutton2_Click(object sender, RoutedEventArgs e)
		{
			XMLHelper.BBUsersDeleter("BBUsersFile");
			GlobalData.BBRejisterflag = false;
			this.PhoneNumbertextBox.Text = null;
			this.VerifyBox.Text = null;
			this.PhoneNumbertextBox.IsEnabled = true;
			this.VerifyBox.IsEnabled = true;
			this.SendVerifyButton.IsEnabled = true;
		}

		public static string getSignature(IDictionary<string, string> parameters, string secret)
		{
			IDictionary<string, string> sortedParams = new SortedDictionary<string, string>(parameters);
			IEnumerator<KeyValuePair<string, string>> iterator = sortedParams.GetEnumerator();
			StringBuilder basestring = new StringBuilder();
			while (iterator.MoveNext())
			{
				KeyValuePair<string, string> current = iterator.Current;
				string key = current.Key;
				current = iterator.Current;
				string value = current.Value;
				bool flag = !string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value);
				if (flag)
				{
					basestring.Append(key).Append("=").Append(value);
				}
			}
			basestring.Append(secret);
			MD5 md5 = MD5.Create();
			byte[] bytes = md5.ComputeHash(Encoding.Default.GetBytes(basestring.ToString()));
			return Convert.ToBase64String(bytes);
		}

        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        //public void InitializeComponent()
        //{
        //    bool contentLoaded = this._contentLoaded;
        //    if (!contentLoaded)
        //    {
        //        this._contentLoaded = true;
        //        Uri resourceLocater = new Uri("/Bike;component/other/rejisterpage.xaml", UriKind.Relative);
        //        Application.LoadComponent(this, resourceLocater);
        //    }
        //}

        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
        //void IComponentConnector.Connect(int connectionId, object target)
        //{
        //    switch (connectionId)
        //    {
        //    case 1:
        //        ((RejisterPage)target).KeyDown += new KeyEventHandler(this.Window_KeyDown);
        //        ((RejisterPage)target).Unloaded += new RoutedEventHandler(this.Page_Unloaded);
        //        break;
        //    case 2:
        //        this.tabControl = (TabControl)target;
        //        break;
        //    case 3:
        //        this.UserName = (Label)target;
        //        break;
        //    case 4:
        //        this.UserNametextBox = (TextBox)target;
        //        break;
        //    case 5:
        //        this.Password = (Label)target;
        //        break;
        //    case 6:
        //        this.passwordBox = (PasswordBox)target;
        //        break;
        //    case 7:
        //        this.Loginbutton = (Button)target;
        //        this.Loginbutton.Click += new RoutedEventHandler(this.Loginbutton_Click);
        //        break;
        //    case 8:
        //        this.Exitbutton = (Button)target;
        //        this.Exitbutton.Click += new RoutedEventHandler(this.Exitbutton_Click);
        //        break;
        //    case 9:
        //        this.Register = (Label)target;
        //        this.Register.MouseUp += new MouseButtonEventHandler(this.Register_MouseUp);
        //        break;
        //    case 10:
        //        this.Forget = (Label)target;
        //        this.Forget.MouseUp += new MouseButtonEventHandler(this.Forget_MouseUp);
        //        break;
        //    case 11:
        //        this.remember_check = (CheckBox)target;
        //        this.remember_check.Checked += new RoutedEventHandler(this.Remember_check);
        //        this.remember_check.Unchecked += new RoutedEventHandler(this.Remember_uncheck);
        //        break;
        //    case 12:
        //        this.automatic_check = (CheckBox)target;
        //        this.automatic_check.Checked += new RoutedEventHandler(this.Automatic_check);
        //        this.automatic_check.Unchecked += new RoutedEventHandler(this.Automatic_uncheck);
        //        break;
        //    case 13:
        //        this.PhoneNumbe = (Label)target;
        //        break;
        //    case 14:
        //        this.PhoneNumbertextBox = (TextBox)target;
        //        break;
        //    case 15:
        //        this.Verify = (Label)target;
        //        break;
        //    case 16:
        //        this.VerifyBox = (TextBox)target;
        //        break;
        //    case 17:
        //        this.Loginbutton2 = (Button)target;
        //        this.Loginbutton2.Click += new RoutedEventHandler(this.Loginbutton2_Click);
        //        break;
        //    case 18:
        //        this.Exitbutton2 = (Button)target;
        //        this.Exitbutton2.Click += new RoutedEventHandler(this.Exitbutton2_Click);
        //        break;
        //    case 19:
        //        this.Reminder = (Label)target;
        //        break;
        //    case 20:
        //        this.SendVerifyButton = (Button)target;
        //        this.SendVerifyButton.Click += new RoutedEventHandler(this.SendVerify);
        //        break;
        //    case 21:
        //        this.textBlock = (TextBlock)target;
        //        break;
        //    case 22:
        //        this.LoginStylecomboBox = (ComboBox)target;
        //        this.LoginStylecomboBox.DropDownClosed += new EventHandler(this.changeLoginStyle);
        //        break;
        //    case 23:
        //        this.UserInformation = (Label)target;
        //        this.UserInformation.MouseUp += new MouseButtonEventHandler(this.UserInformation_MouseUp);
        //        break;
        //    default:
        //        this._contentLoaded = true;
        //        break;
        //    }
        //}
	}
}
