using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO.Ports;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows;

namespace Bike
{
	public class Device
	{
		public delegate void DeviceP();

		private static SerialPort ComPort = new SerialPort();

		public static string[] ports;

		public static string port = null;

		public static bool ComPortIsOpen = false;

		private static bool WaitClose = false;

		public static bool IsEnabled = true;

		public static ObservableCollection<string> comList;

		private static bool Sending = false;

		private static Thread _ComSend;

		private static Queue recQueue = new Queue();

		private static string SendSetData = null;

		private static string recTemp = null;

		public static string RecComData = null;

		public static string _RecComData = null;

		public static string ATData = null;

		public static string SpeedData = null;

		public static string WheelData = null;

		public static string RemoteControl = null;

		public static bool v_ConnectFlag = false;

		public static bool r_ConnectFlag = false;

		private static int tick = 0;

		public static string NRFMAC = null;

		public static string BluetoothMAC = null;

		public static double videoAveSpeed = 25.0;

		public static DateTime lastTime = DateTime.Now;

		public static event Device.DeviceP SETconnect;

		public static void Init()
		{
			Device.ports = SerialPort.GetPortNames();
			Device.comList = new ObservableCollection<string>();
			bool flag = Device.ports.Length != 0;
			if (flag)
			{
				for (int i = 0; i < Device.ports.Length; i++)
				{
					bool flag2 = Device.ports[i] != "COM1";
					if (flag2)
					{
						Device.comList.Add(Device.ports[i]);
					}
				}
			}
			Device.ComPort.ReadTimeout = 8000;
			Device.ComPort.WriteTimeout = 8000;
			Device.ComPort.ReadBufferSize = 1024;
			Device.ComPort.WriteBufferSize = 1024;
			Device.ComPort.DataReceived += new SerialDataReceivedEventHandler(Device.ComReceive);
			Thread _ComRec = new Thread(new ThreadStart(Device.ComRec));
			_ComRec.Start();
		}

		public static void connect(string st)
		{
			bool flag = !Device.ComPortIsOpen;
			if (flag)
			{
				try
				{
					Device.ComPort.PortName = st;
					Device.ComPort.BaudRate = 9600;
					Device.ComPort.Parity = Parity.None;
					Device.ComPort.DataBits = 8;
					Device.ComPort.StopBits = StopBits.One;
					Device.ComPort.Open();
					Device.port = st;
				}
				catch
				{
					MessageBox.Show("无法连接设备，请重试");
					return;
				}
				Device.ComPortIsOpen = true;
				Device.WaitClose = false;
				Device.IsEnabled = false;
			}
		}

		public static void disconnet()
		{
			bool comPortIsOpen = Device.ComPortIsOpen;
			if (comPortIsOpen)
			{
				try
				{
					Device.ComPort.DiscardOutBuffer();
					Device.ComPort.DiscardInBuffer();
					Device.WaitClose = true;
					Device.ComPort.Close();
					Device.WaitClose = false;
					Device.IsEnabled = true;
					Device.ComPortIsOpen = false;
					Device.port = null;
				}
				catch
				{
					bool flag = !Device.ComPort.IsOpen;
					if (flag)
					{
						Device.SetComLose();
					}
					else
					{
						MessageBox.Show("无法断开设备，原因未知！");
					}
					return;
				}
				Device.v_ConnectFlag = false;
			}
		}

		private static void SetComLose()
		{
			Device.WaitClose = true;
			Device.WaitClose = false;
			Device.ComPortIsOpen = false;
		}

		public static void Scan()
		{
			Device.comList.Clear();
			Device.ports = new string[SerialPort.GetPortNames().Length];
			Device.ports = SerialPort.GetPortNames();
			bool flag = Device.ports.Length != 0;
			if (flag)
			{
				for (int i = 0; i < Device.ports.Length; i++)
				{
					bool flag2 = Device.ports[i] != "COM1";
					if (flag2)
					{
						Device.comList.Add(Device.ports[i]);
					}
				}
			}
		}

		public static void Send(string data)
		{
			bool sending = Device.Sending;
			if (!sending)
			{
				Device._ComSend = new Thread(new ParameterizedThreadStart(Device.ComSend));
				Device.SendSetData = data;
				Device._ComSend.Start(Device.SendSetData);
			}
		}

		private static void ComSend(object obj)
		{
			Device.Sending = true;
			string sendData = Device.SendSetData;
			byte[] sendBuffer = Encoding.Default.GetBytes(sendData);
			try
			{
				int sendTimes = sendBuffer.Length / 1000;
				for (int i = 0; i < sendTimes; i++)
				{
					Device.ComPort.Write(sendBuffer, i * 1000, 1000);
				}
				bool flag = sendBuffer.Length % 1000 != 0;
				if (flag)
				{
					Device.ComPort.Write(sendBuffer, sendTimes * 1000, sendBuffer.Length % 1000);
				}
			}
			catch
			{
				bool flag2 = !Device.ComPort.IsOpen;
				if (flag2)
				{
					Device.SetComLose();
				}
				else
				{
					MessageBox.Show("无法发送数据，原因未知！");
				}
			}
			Device.Sending = false;
			Device._ComSend.Abort();
		}

		private static void ComReceive(object sender, SerialDataReceivedEventArgs e)
		{
			bool waitClose = Device.WaitClose;
			if (!waitClose)
			{
				Thread.Sleep(10);
				try
				{
					byte[] recBuffer = new byte[Device.ComPort.BytesToRead];
					Device.ComPort.Read(recBuffer, 0, recBuffer.Length);
					Device.recQueue.Enqueue(recBuffer);
				}
				catch
				{
				}
			}
		}

		private static void ComRec()
		{
			while (true)
			{
				DateTime currentTime = DateTime.Now;
				bool flag = GlobalData.Connectflag && (currentTime - Device.lastTime).TotalSeconds > 10.0;
				if (flag)
				{
					Device.v_ConnectFlag = false;
					GlobalData.Connectflag = false;
					Device.IsEnabled = true;
					bool flag2 = Device.SETconnect != null;
					if (flag2)
					{
						Device.SETconnect();
					}
				}
				bool flag3 = Device.recQueue.Count > 0;
				if (flag3)
				{
					Device.lastTime = DateTime.Now;
					byte[] recBuffer = (byte[])Device.recQueue.Dequeue();
					string recData = Encoding.Default.GetString(recBuffer);
					Device.recTemp += recData;
					Device.RecComData = Device.recTemp;
					bool flag4 = Device.recTemp.Length > 2;
					if (flag4)
					{
						bool flag5 = Device.recTemp.Substring(Device.recTemp.Length - 2).Equals("21") || Device.recTemp.Substring(Device.recTemp.Length - 2).Equals("\r\n");
						if (flag5)
						{
							bool flag6 = Device.RecComData.Substring(Device.recTemp.Length - 2).Equals("21");
							if (flag6)
							{
								int i = Device.RecComData.Length / 10;
								for (int j = 0; j < i; j++)
								{
									Device._RecComData = Device.RecComData.Substring(10 * j, 10);
									bool flag7 = Device._RecComData.Substring(0, 4).Equals("1230");
									if (flag7)
									{
										Device.SpeedData = Device._RecComData.Substring(4, 4);
										bool unity3DTrainingflag = GlobalData.Unity3DTrainingflag;
										if (unity3DTrainingflag)
										{
											UdpCore.SendSpeed(Device.SpeedData);
										}
										bool flag8 = !Device.v_ConnectFlag || !GlobalData.Connectflag;
										if (flag8)
										{
											Device.v_ConnectFlag = true;
											GlobalData.Connectflag = true;
										}
										Device.tick = 0;
									}
									bool flag9 = Device._RecComData.Substring(0, 4).Equals("1231");
									if (flag9)
									{
										Device.WheelData = Device._RecComData.Substring(4, 4);
										bool unity3DTrainingflag2 = GlobalData.Unity3DTrainingflag;
										if (unity3DTrainingflag2)
										{
											string temp = Device.WheelData.Substring(1, 3);
											int temp2 = (int)Convert.ToInt16(temp);
											bool flag10 = temp2 > 45;
											if (flag10)
											{
												Device.WheelData = Device.WheelData.Substring(0, 1) + "045";
											}
											UdpCore.SendWheel(Device.WheelData);
										}
										bool flag11 = !Device.r_ConnectFlag || !GlobalData.Connectflag;
										if (flag11)
										{
											Device.r_ConnectFlag = true;
										}
										Device.tick = 0;
									}
									bool flag12 = Device._RecComData.Substring(0, 4).Equals("1232");
									if (flag12)
									{
										Device.RemoteControl = Device._RecComData.Substring(5, 1);
										Device.tick = 0;
									}
								}
							}
							else
							{
								Device.ATData = Device.RecComData;
							}
							Device.recTemp = null;
						}
					}
					else
					{
						Thread.Sleep(100);
					}
					Device.tick++;
					bool flag13 = Device.tick > 60;
					if (flag13)
					{
						bool flag14 = Device.v_ConnectFlag || GlobalData.Connectflag;
						if (flag14)
						{
							Device.v_ConnectFlag = false;
							GlobalData.Connectflag = false;
						}
						Device.tick = 0;
					}
				}
			}
		}
	}
}
