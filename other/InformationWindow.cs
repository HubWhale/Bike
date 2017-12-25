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
    public partial class InformationWindow : Window, IComponentConnector
	{
		public static RegisterWindow rejw;

		public static ObservableCollection<string> sexList;

		public static ObservableCollection<string> sizeList;

		public static ObservableCollection<string> nationalList;

        //internal TextBlock Title_t;

        //internal Button Cloce_button;

        //internal Label UserName;

        //internal TextBox UserNametextBox;

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

        //internal TextBox SizeBox;

        //internal Label cm;

        //internal Button Comfirmbutton;

        //internal Button Cancelbutton;

        //private bool _contentLoaded;

		public InformationWindow()
		{
			this.InitializeComponent();
			this.languageInit();
			Connection.receive += new Connection.RegisterW(this.RegisterReturn);
			InformationWindow.sexList = new ObservableCollection<string>();
			InformationWindow.sexList.Add(MultiLanguage.Woman);
			InformationWindow.sexList.Add(MultiLanguage.Man);
			this.SexBox.DataContext = InformationWindow.sexList;
			InformationWindow.nationalList = new ObservableCollection<string>();
			InformationWindow.nationalList.Add(MultiLanguage.China);
			InformationWindow.nationalList.Add(MultiLanguage.Britain);
			InformationWindow.nationalList.Add(MultiLanguage.Germany);
			InformationWindow.nationalList.Add(MultiLanguage.France);
			InformationWindow.nationalList.Add(MultiLanguage.UnitedStates);
			this.NationalityBox.DataContext = InformationWindow.nationalList;
			bool flag = XMLHelper.UsersInforGetNum() > 0;
			if (flag)
			{
				this.UserNametextBox.Text = XMLHelper.UsersInforReader("Information", 0);
				this.E_mailBox.Text = XMLHelper.UsersInforReader("Information", 1);
				this.TelephoneBox.Text = XMLHelper.UsersInforReader("Information", 2);
				string a = XMLHelper.UsersInforReader("Information", 3);
				this.SexBox.SelectedIndex = Convert.ToInt32(XMLHelper.UsersInforReader("Information", 3));
				this.NationalityBox.SelectedIndex = Convert.ToInt32(XMLHelper.UsersInforReader("Information", 4));
				this.HeightBox.Text = XMLHelper.UsersInforReader("Information", 5);
				this.WeightBox.Text = XMLHelper.UsersInforReader("Information", 6);
				this.SizeBox.Text = XMLHelper.UsersInforReader("Information", 7);
			}
		}

		private void Comfirmbutton_Click(object sender, RoutedEventArgs e)
		{
			string UserName = this.UserNametextBox.Text.Trim();
			string E_mail = this.E_mailBox.Text.Trim();
			string Telephone = this.TelephoneBox.Text.Trim();
			string Height = this.HeightBox.Text.Trim();
			string Weight = this.WeightBox.Text.Trim();
			string Size = this.SizeBox.Text.Trim();
			string regex = "^[\\u4E00-\\u9FA5a-zA-Z0-9_]+$";
			string regexE = "^(\\w)+(\\.\\w+)*@(\\w)+((\\.\\w{2,3}){1,3})$";
			string regexT = "^[0-9]+$";
			string regexH = "^([0-9]{3})$";
			string regexW = "^([0-9]{2,3})$";
			string regexs = "^([0-9]{2,3})$";
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
					bool flag3 = string.IsNullOrWhiteSpace(E_mail);
					if (flag3)
					{
						MessageBox.Show(MultiLanguage.Hint12);
					}
					else
					{
						bool flag4 = !Regex.IsMatch(E_mail, regexE);
						if (flag4)
						{
							MessageBox.Show(MultiLanguage.Hint13, MultiLanguage.Warn);
							this.E_mailBox.Clear();
							this.E_mailBox.Focus();
						}
						else
						{
							bool flag5 = string.IsNullOrWhiteSpace(Telephone);
							if (flag5)
							{
								MessageBox.Show(MultiLanguage.Hint24);
							}
							else
							{
								bool flag6 = !Regex.IsMatch(Telephone, regexT);
								if (flag6)
								{
									MessageBox.Show(MultiLanguage.Hint25, MultiLanguage.Warn);
									this.TelephoneBox.Clear();
									this.TelephoneBox.Focus();
								}
								else
								{
									bool flag7 = this.SexBox.SelectedItem == null;
									if (flag7)
									{
										MessageBox.Show(MultiLanguage.Hint14);
									}
									else
									{
										string Sex = this.SexBox.SelectedItem.ToString();
										bool flag8 = this.NationalityBox.SelectedItem == null;
										if (flag8)
										{
											MessageBox.Show(MultiLanguage.Hint23);
										}
										else
										{
											string Nationality = this.NationalityBox.SelectedItem.ToString();
											bool flag9 = string.IsNullOrWhiteSpace(Height);
											if (flag9)
											{
												MessageBox.Show(MultiLanguage.Hint15);
											}
											else
											{
												bool flag10 = !Regex.IsMatch(Height, regexH);
												if (flag10)
												{
													MessageBox.Show(MultiLanguage.Hint16, MultiLanguage.Warn);
													this.HeightBox.Clear();
													this.HeightBox.Focus();
												}
												else
												{
													bool flag11 = string.IsNullOrWhiteSpace(Weight);
													if (flag11)
													{
														MessageBox.Show(MultiLanguage.Hint17);
													}
													else
													{
														bool flag12 = !Regex.IsMatch(Weight, regexW);
														if (flag12)
														{
															MessageBox.Show(MultiLanguage.Hint18, MultiLanguage.Warn);
															this.WeightBox.Clear();
															this.WeightBox.Focus();
														}
														else
														{
															bool flag13 = string.IsNullOrWhiteSpace(Size);
															if (flag13)
															{
																MessageBox.Show(MultiLanguage.Hint26);
															}
															else
															{
																bool flag14 = !Regex.IsMatch(Size, regexs);
																if (flag14)
																{
																	MessageBox.Show(MultiLanguage.Hint27, MultiLanguage.Warn);
																	this.SizeBox.Clear();
																	this.SizeBox.Focus();
																}
																else
																{
																	bool flag15 = Sex == "男" || Sex == "Male";
																	string sex;
																	if (flag15)
																	{
																		sex = "1";
																	}
																	else
																	{
																		sex = "0";
																	}
																	string nationality = this.NationalityBox.SelectedItem.ToString();
																	string item = this.NationalityBox.SelectedIndex.ToString();
																	string text = nationality;
                                                                    //uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
                                                                    //if (num <= 1865318464u)
                                                                    //{
                                                                    //    if (num <= 641535102u)
                                                                    //    {
                                                                    //        if (num != 472933804u)
                                                                    //        {
                                                                    //            if (num == 641535102u)
                                                                    //            {
                                                                    //                if (text == "Germany")
                                                                    //                {
                                                                    //                    item = "2";
                                                                    //                }
                                                                    //            }
                                                                    //        }
                                                                    //        else if (text == "United States")
                                                                    //        {
                                                                    //            item = "4";
                                                                    //        }
                                                                    //    }
                                                                    //    else if (num != 800012241u)
                                                                    //    {
                                                                    //        if (num != 1457681951u)
                                                                    //        {
                                                                    //            if (num == 1865318464u)
                                                                    //            {
                                                                    //                if (text == "Britain")
                                                                    //                {
                                                                    //                    item = "1";
                                                                    //                }
                                                                    //            }
                                                                    //        }
                                                                    //        else if (text == "中国")
                                                                    //        {
                                                                    //            item = "0";
                                                                    //        }
                                                                    //    }
                                                                    //    else if (text == "德国")
                                                                    //    {
                                                                    //        item = "2";
                                                                    //    }
                                                                    //}
                                                                    //else if (num <= 2066631610u)
                                                                    //{
                                                                    //    if (num != 2056278404u)
                                                                    //    {
                                                                    //        if (num == 2066631610u)
                                                                    //        {
                                                                    //            if (text == "France")
                                                                    //            {
                                                                    //                item = "3";
                                                                    //            }
                                                                    //        }
                                                                    //    }
                                                                    //    else if (text == "美国")
                                                                    //    {
                                                                    //        item = "4";
                                                                    //    }
                                                                    //}
                                                                    //else if (num != 2497348400u)
                                                                    //{
                                                                    //    if (num != 2534444019u)
                                                                    //    {
                                                                    //        if (num == 2657116343u)
                                                                    //        {
                                                                    //            if (text == "法国")
                                                                    //            {
                                                                    //                item = "3";
                                                                    //            }
                                                                    //        }
                                                                    //    }
                                                                    //    else if (text == "英国")
                                                                    //    {
                                                                    //        item = "1";
                                                                    //    }
                                                                    //}
                                                                    //else if (text == "China")
                                                                    //{
                                                                    //    item = "0";
                                                                    //}
																	GlobalData.UserName1 = this.UserNametextBox.Text;
																	GlobalData.Mailbox = this.E_mailBox.Text;
																	GlobalData.Phone = this.TelephoneBox.Text;
																	GlobalData.Sex = sex;
																	GlobalData.Nationality = item;
																	GlobalData.Height = this.HeightBox.Text;
																	GlobalData.Weight = this.WeightBox.Text;
																	GlobalData.Size = this.SizeBox.Text;
																	XMLHelper.UserInformationFile("Information");
																	base.Close();
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
			this.Comfirmbutton.Content = MultiLanguage.center;
			this.Cancelbutton.Content = MultiLanguage.cancel;
			this.Title_t.Text = MultiLanguage.UserInfor;
			this.UserName.Content = MultiLanguage.UserName_t;
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
        //        Uri resourceLocater = new Uri("/Bike;component/other/informationwindow.xaml", UriKind.Relative);
        //        Application.LoadComponent(this, resourceLocater);
        //    }
        //}

        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
        //void IComponentConnector.Connect(int connectionId, object target)
        //{
        //    switch (connectionId)
        //    {
        //    case 1:
        //        ((InformationWindow)target).Closed += new EventHandler(this.Window_Closed);
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
        //        this.E_mail = (Label)target;
        //        break;
        //    case 8:
        //        this.E_mailBox = (TextBox)target;
        //        break;
        //    case 9:
        //        this.Telephone = (Label)target;
        //        break;
        //    case 10:
        //        this.TelephoneBox = (TextBox)target;
        //        break;
        //    case 11:
        //        this.Sex = (Label)target;
        //        break;
        //    case 12:
        //        this.SexBox = (ComboBox)target;
        //        break;
        //    case 13:
        //        this.Nationality = (Label)target;
        //        break;
        //    case 14:
        //        this.NationalityBox = (ComboBox)target;
        //        break;
        //    case 15:
        //        this.Height = (Label)target;
        //        break;
        //    case 16:
        //        this.HeightBox = (TextBox)target;
        //        break;
        //    case 17:
        //        this.CM = (Label)target;
        //        break;
        //    case 18:
        //        this.Weight = (Label)target;
        //        break;
        //    case 19:
        //        this.WeightBox = (TextBox)target;
        //        break;
        //    case 20:
        //        this.KG = (Label)target;
        //        break;
        //    case 21:
        //        this.Size = (Label)target;
        //        break;
        //    case 22:
        //        this.SizeBox = (TextBox)target;
        //        break;
        //    case 23:
        //        this.cm = (Label)target;
        //        break;
        //    case 24:
        //        this.Comfirmbutton = (Button)target;
        //        this.Comfirmbutton.Click += new RoutedEventHandler(this.Comfirmbutton_Click);
        //        break;
        //    case 25:
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
