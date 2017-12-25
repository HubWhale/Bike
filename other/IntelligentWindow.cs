using Microsoft.Win32;
using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media.Imaging;

namespace Bike.Other
{
    public partial class IntelligentWindow : Window, IComponentConnector
	{
		public static ObservableCollection<string> modelList;

        //internal TextBlock Peizhi;

        //internal TextBlock Intelligent1;

        //internal TextBlock SerialBlock;

        //internal TextBlock Intelligent2;

        //internal TextBlock TypeNumber;

        //internal TextBox BluetoothMAC;

        //internal Button Finish;

        //internal TextBox CRC16;

        //internal Image LockState;

        //internal Button Binding;

        //internal ComboBox SericalCom;

        //internal Button Connect;

        //internal TextBlock COMport;

        //internal ComboBox IntelligentCOMport;

        //private bool _contentLoaded;

		public IntelligentWindow()
		{
			InitializeComponent();
			Device.Scan();
			this.IntelligentWindow_Init();
			this.languageInit();
			Device.SETconnect += new Device.DeviceP(this.SetDisConnect);
			IntelligentWindow.modelList = new ObservableCollection<string>();
			IntelligentWindow.modelList.Add("U5-SMART");
			this.IntelligentCOMport.DataContext = IntelligentWindow.modelList;
		}

		private void languageInit()
		{
		}

		private void IntelligentWindow_Init()
		{
			this.SericalCom.DataContext = Device.comList;
			bool flag = Device.comList.Count != 0;
			if (flag)
			{
				Device.port = Device.comList[0];
			}
			this.SericalCom.SelectedValue = Device.port;
			string temp = this.getSericalNumber();
			bool flag2 = temp != "" && temp.Length == 17;
			if (flag2)
			{
				this.BluetoothMAC.Text = temp.Substring(0, 12);
				this.CRC16.Text = temp.Substring(13, 4);
				byte[] mac = new byte[6];
				byte[] crc16 = new byte[2];
				mac = this.HexStringto2Bytes(this.BluetoothMAC.Text);
				crc16 = this.HexStringto2Bytes(this.CRC16.Text);
				GlobalData.LockId = this.getCrc16Code(mac, crc16, 6);
			}
			this.SetConnect();
			this.SetLock();
		}

		private string getSericalNumber()
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.FileName = Environment.CurrentDirectory + "\\LockId.txt";
			bool flag = !File.Exists(openFileDialog.FileName);
			if (flag)
			{
				FileStream f = File.Create(openFileDialog.FileName);
				f.Close();
				f.Dispose();
			}
			StreamReader sr = new StreamReader(openFileDialog.FileName, Encoding.Default);
			string sericalNumber = sr.ReadToEnd();
			sr.Close();
			return sericalNumber;
		}

		private void setSericalNumber(string _string)
		{
			File.WriteAllText(new OpenFileDialog
			{
				FileName = Environment.CurrentDirectory + "\\LockId.txt"
			}.FileName, _string);
		}

		private void SetConnect()
		{
			bool v_ConnectFlag = Device.v_ConnectFlag;
			if (v_ConnectFlag)
			{
				this.Connect.Content = MultiLanguage.Disconnect_t;
				GlobalData.Connectflag = true;
			}
			else
			{
				this.Connect.Content = MultiLanguage.Connect_t;
				GlobalData.Connectflag = false;
				Device.disconnet();
			}
			this.SericalCom.IsEnabled = Device.IsEnabled;
		}

		private void SetDisConnect()
		{
			bool v_ConnectFlag = Device.v_ConnectFlag;
			if (v_ConnectFlag)
			{
				this.Connect.Content = MultiLanguage.Disconnect_t;
				GlobalData.Connectflag = true;
			}
			else
			{
				base.Dispatcher.Invoke(delegate
				{
					this.Connect.Content = MultiLanguage.Connect_t;
				});
				GlobalData.Connectflag = false;
				Device.disconnet();
			}
			base.Dispatcher.Invoke(delegate
			{
				this.SericalCom.IsEnabled = Device.IsEnabled;
			});
		}

		private void SericalCom_PreviewMouseDown(object sender, MouseButtonEventArgs e)
		{
			Device.Scan();
		}

        [AsyncStateMachine(typeof(AsyncStateMachine)), DebuggerStepThrough]
		private void Connet_Click(object sender, RoutedEventArgs e)
		{
            AsyncStateMachine stateMachine = new AsyncStateMachine {
                baseWindow = this,
                sender = sender,
                e = e,
                builder = AsyncVoidMethodBuilder.Create(),
                state = -1
            };
            stateMachine.builder.Start<AsyncStateMachine>(ref stateMachine);
		}

		private bool getCrc16Code(byte[] source, byte[] check, int len)
		{
			byte[] temp = new byte[len + 2];
			temp[0] = (temp[1] = 255);
			for (int x = 0; x < source.Length; x++)
			{
				temp[x + 2] = source[x];
			}
			len += 2;
			ushort crcd = 65535;
			bool flag = len < 0;
			if (flag)
			{
				crcd = 0;
			}
			else
			{
				len--;
				ushort i = 0;
				while ((int)i <= len)
				{
					crcd ^= (ushort)temp[(int)i];
					for (ushort j = 0; j <= 7; j += 1)
					{
						bool flag2 = (crcd & 1) > 0;
						if (flag2)
						{
							crcd = (ushort)(crcd >> 1 ^ 40961);
						}
						else
						{
							crcd = (ushort)(crcd >> 1);
						}
					}
					i += 1;
				}
			}
			return check[0] == (byte)((crcd & 65280) >> 8) && check[1] == (byte)(crcd & 255);
		}

		private byte[] HexStringto2Bytes(string _string)
		{
			byte[] b = new byte[_string.Length / 2];
			for (int i = 0; i < b.Length; i++)
			{
				b[i] = Convert.ToByte(_string.Substring(i * 2, 2), 16);
			}
			return b;
		}

		private void SetLock()
		{
			bool flag = GlobalData.LockId || Device.v_ConnectFlag;
			if (flag)
			{
				this.Binding.Content = MultiLanguage.Unlock_t;
				this.LockState.Source = new BitmapImage(new Uri("/Bike;component/Images/check.png", UriKind.Relative));
				this.BluetoothMAC.IsEnabled = false;
				this.CRC16.IsEnabled = false;
			}
			else
			{
				this.Binding.Content = MultiLanguage.Lock_t;
				this.LockState.Source = new BitmapImage(new Uri("/Bike;component/Images/cancel.png", UriKind.Relative));
				this.BluetoothMAC.IsEnabled = true;
				this.CRC16.IsEnabled = true;
			}
		}

		private void Binding_Click(object sender, RoutedEventArgs e)
		{
			bool lockId = GlobalData.LockId;
			if (lockId)
			{
				bool comPortIsOpen = Device.ComPortIsOpen;
				if (comPortIsOpen)
				{
					MessageBox.Show(MultiLanguage.Warn3);
					return;
				}
				GlobalData.LockId = false;
			}
			else
			{
				byte[] mac = new byte[6];
				byte[] crc16 = new byte[2];
				string temp = this.BluetoothMAC.Text;
				string temp2 = this.CRC16.Text;
				bool flag = temp.Length != 12 || temp2.Length != 4;
				if (flag)
				{
					MessageBox.Show(MultiLanguage.Warn4);
					return;
				}
				mac = this.HexStringto2Bytes(temp);
				crc16 = this.HexStringto2Bytes(temp2);
				GlobalData.LockId = this.getCrc16Code(mac, crc16, 6);
				bool lockId2 = GlobalData.LockId;
				if (lockId2)
				{
					string temp3 = temp + "-" + temp2;
					bool flag2 = temp3 != this.getSericalNumber();
					if (flag2)
					{
						this.setSericalNumber(temp3);
					}
				}
			}
			this.SetLock();
		}

		private void Finish_Click(object sender, RoutedEventArgs e)
		{
			base.Close();
		}

		private void IntelligentCom_PreviewMouseDown(object sender, MouseButtonEventArgs e)
		{
		}

		private void Window_Closed(object sender, EventArgs e)
		{
			Device.SETconnect -= new Device.DeviceP(this.SetDisConnect);
		}

        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        //public void InitializeComponent()
        //{
        //    bool contentLoaded = this._contentLoaded;
        //    if (!contentLoaded)
        //    {
        //        this._contentLoaded = true;
        //        Uri resourceLocater = new Uri("/Bike;component/other/intelligentwindow.xaml", UriKind.Relative);
        //        Application.LoadComponent(this, resourceLocater);
        //    }
        //}

        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
        //void IComponentConnector.Connect(int connectionId, object target)
        //{
        //    switch (connectionId)
        //    {
        //    case 1:
        //        this.Peizhi = (TextBlock)target;
        //        break;
        //    case 2:
        //        this.Intelligent1 = (TextBlock)target;
        //        break;
        //    case 3:
        //        this.SerialBlock = (TextBlock)target;
        //        break;
        //    case 4:
        //        this.Intelligent2 = (TextBlock)target;
        //        break;
        //    case 5:
        //        this.TypeNumber = (TextBlock)target;
        //        break;
        //    case 6:
        //        this.BluetoothMAC = (TextBox)target;
        //        break;
        //    case 7:
        //        this.Finish = (Button)target;
        //        this.Finish.Click += new RoutedEventHandler(this.Finish_Click);
        //        break;
        //    case 8:
        //        this.CRC16 = (TextBox)target;
        //        break;
        //    case 9:
        //        this.LockState = (Image)target;
        //        break;
        //    case 10:
        //        this.Binding = (Button)target;
        //        this.Binding.Click += new RoutedEventHandler(this.Binding_Click);
        //        break;
        //    case 11:
        //        this.SericalCom = (ComboBox)target;
        //        this.SericalCom.PreviewMouseDown += new MouseButtonEventHandler(this.SericalCom_PreviewMouseDown);
        //        break;
        //    case 12:
        //        this.Connect = (Button)target;
        //        this.Connect.Click += new RoutedEventHandler(this.Connet_Click);
        //        break;
        //    case 13:
        //        this.COMport = (TextBlock)target;
        //        break;
        //    case 14:
        //        this.IntelligentCOMport = (ComboBox)target;
        //        this.IntelligentCOMport.PreviewMouseDown += new MouseButtonEventHandler(this.IntelligentCom_PreviewMouseDown);
        //        break;
        //    default:
        //        this._contentLoaded = true;
        //        break;
        //    }
        //}

        
        [CompilerGenerated]
        private sealed class AsyncStateMachine : IAsyncStateMachine
        {
            public int state;
            public IntelligentWindow baseWindow;
            public AsyncVoidMethodBuilder builder;
            private TaskAwaiter task;
            private string comStr;
            public RoutedEventArgs e;
            public object sender;

            void System.Runtime.CompilerServices.IAsyncStateMachine.MoveNext()
            {
                int num = this.state;
                try
                {
                    TaskAwaiter awaiter;
                    if (num != 0)
                    {
                        if (GlobalData.LockId)
                        {
                            if (!Device.ComPortIsOpen)
                            {
                                if (this.baseWindow.BluetoothMAC.Text != "")
                                {
                                    if (this.baseWindow.SericalCom.SelectedItem != null)
                                    {
                                        this.comStr = this.baseWindow.SericalCom.SelectedItem.ToString();
                                        Device.connect(this.comStr);
                                        this.baseWindow.Connect.Content = MultiLanguage.Connecting_t;
                                        Device.BluetoothMAC = "0x" + this.baseWindow.BluetoothMAC.Text;
                                        Device.Send("AT+CONA" + Device.BluetoothMAC + "\r\n");
                                        awaiter = Task.Delay(0x7d0).GetAwaiter();
                                        if (!awaiter.IsCompleted)
                                        {
                                            this.state = num = 0;
                                            this.task = awaiter;
                                            IntelligentWindow.AsyncStateMachine stateMachine = this;
                                            this.builder.AwaitUnsafeOnCompleted<TaskAwaiter, IntelligentWindow.AsyncStateMachine>(ref awaiter, ref stateMachine);
                                            return;
                                        }
                                        goto Label_016E;
                                    }
                                    MessageBox.Show(MultiLanguage.Warn2, MultiLanguage.Warn_t, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                                    goto Label_01B9;
                                }
                                goto Label_0191;
                            }
                            goto Label_0189;
                        }
                        MessageBox.Show(MultiLanguage.Warn1, MultiLanguage.Warn_t, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        goto Label_01B9;
                    }
                    awaiter = this.task;
                    this.task = new TaskAwaiter();
                    this.state = num = -1;
                Label_016E:
                    awaiter.GetResult();
                    awaiter = new TaskAwaiter();
                    this.comStr = null;
                    goto Label_0191;
                Label_0189:
                    Device.disconnet();
                Label_0191:
                    this.baseWindow.SetConnect();
                }
                catch (Exception exception)
                {
                    this.state = -2;
                    this.builder.SetException(exception);
                    return;
                }
            Label_01B9:
                this.state = -2;
                this.builder.SetResult();
            }

            void System.Runtime.CompilerServices.IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
            {
            }
            
        }
	}
}
