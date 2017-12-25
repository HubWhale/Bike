using FinalClient;
using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace Bike.Other
{
    public partial class RegisterWindow : Window, IComponentConnector
	{
		public static RegisterWindow rejw;

		public static ObservableCollection<string> sexList;

		public static ObservableCollection<string> sizeList;

		public static ObservableCollection<string> nationalList;

        //internal TextBlock Title_t;

        //internal Button Cloce_button;

        //internal Label UserName;

        //internal TextBox UserNametextBox;

        //internal Label Password;

        //internal PasswordBox passwordBox;

        //internal Label Passwordagain;

        //internal PasswordBox passwordagainBox;

        //internal Label E_mail;

        //internal TextBox E_mailBox;

        //internal Label Telephone;

        //internal TextBox TelephoneBox;

        //internal Label Sex;

        //internal ComboBox SexBox;

        //internal Label Nationality;

        //internal ComboBox NationalityBox;

        //internal new Label Height;

        //internal TextBox HeightBox;

        //internal Label CM;

        //internal Label Weight;

        //internal TextBox WeightBox;

        //internal Label KG;

        //internal Label Size;

        //internal ComboBox SizeBox;

        //internal Button Registerbutton;

        //internal Button Cancelbutton;

        //private bool _contentLoaded;

		public RegisterWindow()
		{
			this.InitializeComponent();
			this.languageInit();
			Connection.receive += new Connection.RegisterW(this.RegisterReturn);
			RegisterWindow.sexList = new ObservableCollection<string>();
			RegisterWindow.sexList.Add(MultiLanguage.Man);
			RegisterWindow.sexList.Add(MultiLanguage.Woman);
			this.SexBox.DataContext = RegisterWindow.sexList;
			RegisterWindow.sizeList = new ObservableCollection<string>();
			RegisterWindow.sizeList.Add("20\"");
			RegisterWindow.sizeList.Add("22\"");
			RegisterWindow.sizeList.Add("24\"");
			RegisterWindow.sizeList.Add("26\"");
			RegisterWindow.sizeList.Add("27\"");
			RegisterWindow.sizeList.Add("28\"");
			RegisterWindow.sizeList.Add("29\"");
			RegisterWindow.sizeList.Add("700C");
			this.SizeBox.DataContext = RegisterWindow.sizeList;
			RegisterWindow.nationalList = new ObservableCollection<string>();
			RegisterWindow.nationalList.Add(MultiLanguage.China);
			RegisterWindow.nationalList.Add(MultiLanguage.Britain);
			RegisterWindow.nationalList.Add(MultiLanguage.Germany);
			RegisterWindow.nationalList.Add(MultiLanguage.France);
			RegisterWindow.nationalList.Add(MultiLanguage.UnitedStates);
			this.NationalityBox.DataContext = RegisterWindow.nationalList;
		}

		private void Registerbutton_Click(object sender, RoutedEventArgs e)
		{
			string UserName = this.UserNametextBox.Text.Trim();
			string PassWord = this.passwordBox.Password.Trim();
			string PassWordagain = this.passwordagainBox.Password.Trim();
			string E_mail = this.E_mailBox.Text.Trim();
			string Telephone = this.TelephoneBox.Text.Trim();
			string Height = this.HeightBox.Text.Trim();
			string Weight = this.WeightBox.Text.Trim();
			string regex = "^[\\u4E00-\\u9FA5a-zA-Z0-9_]+$";
			string regex2 = "^[a-zA-Z0-9_]+$";
			string regexE = "^(\\w)+(\\.\\w+)*@(\\w)+((\\.\\w{2,3}){1,3})$";
			string regexT = "^[0-9]+$";
			string regexH = "^([0-9]{3})$";
			string regexW = "^([0-9]{2,3})$";
			bool flag = string.IsNullOrWhiteSpace(UserName);
			if (flag)
			{
				MessageBox.Show(MultiLanguage.Hint1);
			}
			else
			{
				bool flag2 = !Regex.IsMatch(UserName, regex);
				if (flag2)
				{
					MessageBox.Show(MultiLanguage.Hint2, MultiLanguage.Warn);
					this.UserNametextBox.Clear();
					this.UserNametextBox.Focus();
				}
				else
				{
					bool flag3 = string.IsNullOrWhiteSpace(PassWord);
					if (flag3)
					{
						MessageBox.Show(MultiLanguage.Hint9);
					}
					else
					{
						bool flag4 = !Regex.IsMatch(PassWord, regex2);
						if (flag4)
						{
							MessageBox.Show(MultiLanguage.Hint10, MultiLanguage.Warn);
							this.passwordBox.Clear();
							this.passwordagainBox.Clear();
							this.passwordBox.Focus();
						}
						else
						{
							bool flag5 = !PassWord.Equals(PassWordagain);
							if (flag5)
							{
								MessageBox.Show(MultiLanguage.Hint11, MultiLanguage.Warn);
								this.passwordBox.Clear();
								this.passwordagainBox.Clear();
								this.passwordBox.Focus();
							}
							else
							{
								bool flag6 = string.IsNullOrWhiteSpace(E_mail);
								if (flag6)
								{
									MessageBox.Show(MultiLanguage.Hint12);
								}
								else
								{
									bool flag7 = !Regex.IsMatch(E_mail, regexE);
									if (flag7)
									{
										MessageBox.Show(MultiLanguage.Hint13, MultiLanguage.Warn);
										this.E_mailBox.Clear();
										this.E_mailBox.Focus();
									}
									else
									{
										bool flag8 = string.IsNullOrWhiteSpace(Telephone);
										if (flag8)
										{
											MessageBox.Show(MultiLanguage.Hint24);
										}
										else
										{
											bool flag9 = !Regex.IsMatch(Telephone, regexT);
											if (flag9)
											{
												MessageBox.Show(MultiLanguage.Hint25, MultiLanguage.Warn);
												this.TelephoneBox.Clear();
												this.TelephoneBox.Focus();
											}
											else
											{
												bool flag10 = this.SexBox.SelectedItem == null;
												if (flag10)
												{
													MessageBox.Show(MultiLanguage.Hint14);
												}
												else
												{
													string Sex = this.SexBox.SelectedItem.ToString();
													bool flag11 = this.NationalityBox.SelectedItem == null;
													if (flag11)
													{
														MessageBox.Show(MultiLanguage.Hint23);
													}
													else
													{
														string Nationality = this.NationalityBox.SelectedItem.ToString();
														bool flag12 = string.IsNullOrWhiteSpace(Height);
														if (flag12)
														{
															MessageBox.Show(MultiLanguage.Hint15);
														}
														else
														{
															bool flag13 = !Regex.IsMatch(Height, regexH);
															if (flag13)
															{
																MessageBox.Show(MultiLanguage.Hint16, MultiLanguage.Warn);
																this.HeightBox.Clear();
																this.HeightBox.Focus();
															}
															else
															{
																bool flag14 = string.IsNullOrWhiteSpace(Weight);
																if (flag14)
																{
																	MessageBox.Show(MultiLanguage.Hint17);
																}
																else
																{
																	bool flag15 = !Regex.IsMatch(Weight, regexW);
																	if (flag15)
																	{
																		MessageBox.Show(MultiLanguage.Hint18, MultiLanguage.Warn);
																		this.WeightBox.Clear();
																		this.WeightBox.Focus();
																	}
																	else
																	{
																		bool flag16 = this.SizeBox.SelectedItem == null;
																		if (flag16)
																		{
																			MessageBox.Show(MultiLanguage.Hint19);
																		}
																		else
																		{
																			string Size = this.SizeBox.SelectedItem.ToString();
																			string size = Size.Substring(0, Size.Length - 1);
																			bool flag17 = Sex == "男" && Sex == "male";
																			string sex;
																			if (flag17)
																			{
																				sex = "1";
																			}
																			else
																			{
																				sex = "0";
																			}
																			CustomProtocol proto = new CustomProtocol();
																			proto.AddString("EB1");
																			proto.AddString(UserName);
																			proto.AddString(PassWord);
																			proto.AddString(E_mail);
																			proto.AddString(Telephone);
																			proto.AddString(sex);
																			proto.AddString(Nationality);
																			proto.AddString(Height);
																			proto.AddString(Weight);
																			proto.AddString(size);
																			Connection.Send(proto);
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		private void Cancelbutton_Click(object sender, RoutedEventArgs e)
		{
			base.Close();
		}

		private void languageInit()
		{
			base.Title = MultiLanguage.Button_register;
			this.Registerbutton.Content = MultiLanguage.register;
			this.Cancelbutton.Content = MultiLanguage.cancel;
			this.Title_t.Text = MultiLanguage.Register_t;
			this.UserName.Content = MultiLanguage.UserName_t;
			this.Password.Content = MultiLanguage.Password_t;
			this.Passwordagain.Content = MultiLanguage.Passwordagain_t;
			this.E_mail.Content = MultiLanguage.E_mail_t;
			this.Sex.Content = MultiLanguage.Gender_t;
			this.Height.Content = MultiLanguage.Height_t;
			this.Weight.Content = MultiLanguage.Weight_t;
			this.Size.Content = MultiLanguage.BikeSise_t;
			this.Telephone.Content = MultiLanguage.Telephone;
			this.Nationality.Content = MultiLanguage.Nationality;
		}

		private void Clocebutton_Click(object sender, RoutedEventArgs e)
		{
			base.Close();
		}

		private void TitleBar_MouseMove(object sender, MouseEventArgs e)
		{
			bool flag = e.LeftButton == MouseButtonState.Pressed;
			if (flag)
			{
				base.DragMove();
			}
		}

		private void RegisterReturn(ProtocolBase proto)
		{
			CustomProtocol protocol = (CustomProtocol)proto;
			int start = 0;
			string str = protocol.GetString(start, ref start);
			int ret = protocol.GetInt(start, ref start);
			int num = ret;
			if (num != 0)
			{
				MessageBox.Show(MultiLanguage.Hint21, MultiLanguage.Warn);
			}
			else
			{
				MessageBox.Show(MultiLanguage.Hint20, MultiLanguage.Warn);
				base.Dispatcher.Invoke(delegate
				{
					base.Close();
				});
			}
		}

		private void Window_Closed(object sender, EventArgs e)
		{
			Connection.receive -= new Connection.RegisterW(this.RegisterReturn);
		}

        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        //public void InitializeComponent()
        //{
        //    bool contentLoaded = this._contentLoaded;
        //    if (!contentLoaded)
        //    {
        //        this._contentLoaded = true;
        //        Uri resourceLocater = new Uri("/Bike;component/other/registerwindow.xaml", UriKind.Relative);
        //        Application.LoadComponent(this, resourceLocater);
        //    }
        //}

        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
        //void IComponentConnector.Connect(int connectionId, object target)
        //{
        //    switch (connectionId)
        //    {
        //    case 1:
        //        ((RegisterWindow)target).Closed += new EventHandler(this.Window_Closed);
        //        break;
        //    case 2:
        //        ((Grid)target).MouseMove += new MouseEventHandler(this.TitleBar_MouseMove);
        //        break;
        //    case 3:
        //        this.Title_t = (TextBlock)target;
        //        break;
        //    case 4:
        //        this.Cloce_button = (Button)target;
        //        this.Cloce_button.Click += new RoutedEventHandler(this.Clocebutton_Click);
        //        break;
        //    case 5:
        //        this.UserName = (Label)target;
        //        break;
        //    case 6:
        //        this.UserNametextBox = (TextBox)target;
        //        break;
        //    case 7:
        //        this.Password = (Label)target;
        //        break;
        //    case 8:
        //        this.passwordBox = (PasswordBox)target;
        //        break;
        //    case 9:
        //        this.Passwordagain = (Label)target;
        //        break;
        //    case 10:
        //        this.passwordagainBox = (PasswordBox)target;
        //        break;
        //    case 11:
        //        this.E_mail = (Label)target;
        //        break;
        //    case 12:
        //        this.E_mailBox = (TextBox)target;
        //        break;
        //    case 13:
        //        this.Telephone = (Label)target;
        //        break;
        //    case 14:
        //        this.TelephoneBox = (TextBox)target;
        //        break;
        //    case 15:
        //        this.Sex = (Label)target;
        //        break;
        //    case 16:
        //        this.SexBox = (ComboBox)target;
        //        break;
        //    case 17:
        //        this.Nationality = (Label)target;
        //        break;
        //    case 18:
        //        this.NationalityBox = (ComboBox)target;
        //        break;
        //    case 19:
        //        this.Height = (Label)target;
        //        break;
        //    case 20:
        //        this.HeightBox = (TextBox)target;
        //        break;
        //    case 21:
        //        this.CM = (Label)target;
        //        break;
        //    case 22:
        //        this.Weight = (Label)target;
        //        break;
        //    case 23:
        //        this.WeightBox = (TextBox)target;
        //        break;
        //    case 24:
        //        this.KG = (Label)target;
        //        break;
        //    case 25:
        //        this.Size = (Label)target;
        //        break;
        //    case 26:
        //        this.SizeBox = (ComboBox)target;
        //        break;
        //    case 27:
        //        this.Registerbutton = (Button)target;
        //        this.Registerbutton.Click += new RoutedEventHandler(this.Registerbutton_Click);
        //        break;
        //    case 28:
        //        this.Cancelbutton = (Button)target;
        //        this.Cancelbutton.Click += new RoutedEventHandler(this.Cancelbutton_Click);
        //        break;
        //    default:
        //        this._contentLoaded = true;
        //        break;
        //    }
        //}
	}
}
