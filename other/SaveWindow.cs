using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Bike.Other
{
    public partial class SaveWindow : Window, IComponentConnector
	{
		private string token;

		private string appId;

		private string appKey;

		private string openId;

		private string appSecret;

		private string Secret;

        //internal TextBox saveName;

        //internal Button save;

        //internal Button cancel;

        //internal CheckBox uploading_check;

        //private bool _contentLoaded;

		public SaveWindow()
		{
			this.InitializeComponent();
			this.languageInit();
			this.uploading_check.IsChecked = new bool?(true);
			GlobalData.Uploadingflag = true;
			this.saveName.Text = "UH" + DateTime.Now.ToString("yyyyMMddHHmmss");
		}

		private void save_Click(object sender, RoutedEventArgs e)
		{
			bool flag2 = this.saveName.Text != null;
			if (flag2)
			{
				string name = this.saveName.Text;
				string num_regex = "^[\\d]";
				string xml_regex = "^[xml]";
				string regex = "^[\\u4E00-\\u9FA5a-zA-Z0-9_]+$";
				bool flag3 = string.IsNullOrWhiteSpace(name);
				if (flag3)
				{
					MessageBox.Show(MultiLanguage.Hint1);
				}
				else
				{
					bool flag4 = !Regex.IsMatch(name, regex);
					if (flag4)
					{
						MessageBox.Show(MultiLanguage.Hint2);
					}
					else
					{
						bool flag5 = Regex.IsMatch(name, num_regex);
						if (flag5)
						{
							MessageBox.Show(MultiLanguage.Hint3);
						}
						else
						{
							bool flag6 = Regex.IsMatch(name.ToLower(), xml_regex);
							if (flag6)
							{
								MessageBox.Show(MultiLanguage.Hint4);
							}
							else
							{
								int tim = Convert.ToInt32(XMLHelper.RecordReader(name, 4));
								bool flag7 = tim > 0;
								if (flag7)
								{
									MessageBox.Show(MultiLanguage.Hint5);
								}
								else
								{
									XMLHelper.RecordWriter(name);
									XMLHelper.CurrentFile(name);
									bool uploadingflag = GlobalData.Uploadingflag;
									if (uploadingflag)
									{
										this.appKey = "SK5S68F7DZKKWIL3";
										this.appSecret = "IHL982FMLT1GTJYU5VNDNSS9N7T8EZIA";
										string serverUrl = "http://client.blackbirdsport.com/api/uploadUHRecord";
										this.openId = GlobalData.openId;
										string sign = RejisterPage.getSignature(new SortedDictionary<string, string>
										{
											{
												"ak",
												this.appKey
											},
											{
												"openId",
												this.openId
											},
											{
												"recordTimeStamp",
												GlobalData.StartTime
											}
										}, this.appSecret);
										string postData = string.Format("ak={0}&openId={1}&recordTimeStamp={2}&sign={3}", new object[]
										{
											this.appKey,
											this.openId,
											GlobalData.StartTime,
											sign
										});
										string url = serverUrl + "?" + postData;
										Dictionary<string, string> postData2 = new Dictionary<string, string>();
										string uploadingreturn = SaveWindow.HttpPost(url, postData2, new Dictionary<string, string>
										{
											{
												"uhXML",
												Environment.CurrentDirectory + "\\" + GlobalData.StartTime + ".xml"
											}
										});
										string flag = uploadingreturn.Substring(11, 2);
										bool flag8 = flag == "ok";
										if (flag8)
										{
											MessageBox.Show("训练记录上传成功");
										}
										else
										{
											string error = uploadingreturn.Substring(55, 6);
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
													MessageBox.Show("没有登陆，训练记录上传失败");
												}
											}
											else
											{
												MessageBox.Show("未知其他错误");
											}
										}
									}
									GlobalData.Time = 0L;
									base.Close();
								}
							}
						}
					}
				}
			}
			else
			{
				MessageBox.Show("Please input the name!");
			}
		}

		private void languageInit()
		{
			base.Title = MultiLanguage.SaveWindow_Title_t;
			this.save.Content = MultiLanguage.save;
			this.cancel.Content = MultiLanguage.cancel;
		}

		private void cancel_Click(object sender, RoutedEventArgs e)
		{
			base.Close();
		}

		private void Uploading_check(object sender, RoutedEventArgs e)
		{
			GlobalData.Uploadingflag = true;
		}

		private void un_Uploading_uncheck(object sender, RoutedEventArgs e)
		{
			GlobalData.Uploadingflag = false;
		}

		public static string HttpPost(string url, Dictionary<string, string> postData, Dictionary<string, string> files)
		{
			HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
			CookieContainer cookieContainer = new CookieContainer();
			request.CookieContainer = cookieContainer;
			request.AllowAutoRedirect = true;
			request.Method = "POST";
			string boundary = DateTime.Now.Ticks.ToString("X");
			request.ContentType = "multipart/form-data;charset=utf-8;boundary=" + boundary;
			byte[] itemBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "\r\n");
			byte[] endBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
			Stream postStream = request.GetRequestStream();
			bool flag = postData != null && postData.Count > 0;
			if (flag)
			{
				Dictionary<string, string>.KeyCollection keys = postData.Keys;
				foreach (string key in keys)
				{
					string strHeader = string.Format("Content-Disposition: form-data; name=\"{0}\"\r\n\r\n", key);
					byte[] strByte = Encoding.UTF8.GetBytes(strHeader);
					postStream.Write(strByte, 0, strByte.Length);
					byte[] value = Encoding.UTF8.GetBytes(string.Format("{0}", postData[key]));
					postStream.Write(itemBoundaryBytes, 0, itemBoundaryBytes.Length);
					postStream.Write(value, 0, value.Length);
					postStream.Write(endBoundaryBytes, 0, itemBoundaryBytes.Length);
				}
			}
			bool flag2 = files != null && files.Count > 0;
			if (flag2)
			{
				Dictionary<string, string>.KeyCollection keys2 = files.Keys;
				foreach (string key2 in keys2)
				{
					string filePath = files[key2];
					int pos = filePath.LastIndexOf("\\");
					string fileName = filePath.Substring(pos + 1);
					StringBuilder sbHeader = new StringBuilder(string.Format("Content-Disposition:form-data;name=\"{0}\";filename=\"{1}\"\r\nContent-Type:application/octet-stream\r\n\r\n", key2, fileName));
					byte[] postHeaderBytes = Encoding.UTF8.GetBytes(sbHeader.ToString());
					FileStream fs = new FileStream(files[key2], FileMode.Open, FileAccess.Read);
					byte[] bArr = new byte[fs.Length];
					fs.Read(bArr, 0, bArr.Length);
					fs.Close();
					postStream.Write(itemBoundaryBytes, 0, itemBoundaryBytes.Length);
					postStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
					postStream.Write(bArr, 0, bArr.Length);
					postStream.Write(endBoundaryBytes, 0, itemBoundaryBytes.Length);
				}
			}
			postStream.Close();
			HttpWebResponse response = request.GetResponse() as HttpWebResponse;
			Stream instream = response.GetResponseStream();
			StreamReader sr = new StreamReader(instream, Encoding.UTF8);
			return sr.ReadToEnd();
		}

        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        //public void InitializeComponent()
        //{
        //    bool contentLoaded = this._contentLoaded;
        //    if (!contentLoaded)
        //    {
        //        this._contentLoaded = true;
        //        Uri resourceLocater = new Uri("/Bike;component/other/savewindow.xaml", UriKind.Relative);
        //        Application.LoadComponent(this, resourceLocater);
        //    }
        //}

        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
        //void IComponentConnector.Connect(int connectionId, object target)
        //{
        //    switch (connectionId)
        //    {
        //    case 1:
        //        this.saveName = (TextBox)target;
        //        break;
        //    case 2:
        //        this.save = (Button)target;
        //        this.save.Click += new RoutedEventHandler(this.save_Click);
        //        break;
        //    case 3:
        //        this.cancel = (Button)target;
        //        this.cancel.Click += new RoutedEventHandler(this.cancel_Click);
        //        break;
        //    case 4:
        //        this.uploading_check = (CheckBox)target;
        //        this.uploading_check.Checked += new RoutedEventHandler(this.Uploading_check);
        //        this.uploading_check.Unchecked += new RoutedEventHandler(this.un_Uploading_uncheck);
        //        break;
        //    default:
        //        this._contentLoaded = true;
        //        break;
        //    }
        //}
	}
}
