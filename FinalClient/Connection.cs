using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Timers;
using System.Windows;
using Bike;

namespace FinalClient
{
	public class Connection
	{
		public delegate void RegisterW(ProtocolBase proto);

		public delegate void Registerp();

		public static Timer heartTimer = new Timer(100000.0);

		public static double lastTime = 0.0;

		public static Socket socket;

		private const int BUFFER_SIZE = 8192;

		public static byte[] readBuff = new byte[8192];

		public static int buffCount = 0;

		public static int ret = 2;

		public static byte[] lenBytes = new byte[4];

		public static int msgLength = 0;

		public static ProtocolBase protocol;

		public static List<ProtocolBase> msgList = new List<ProtocolBase>();

		private static List<TrainingData> dataList = new List<TrainingData>(50);

		public static string ID = "";

		public static bool isConnected = false;

		public static event Connection.RegisterW receive;

		public static event Connection.RegisterW record;

		public static event Connection.RegisterW recordD;

		public static event Connection.RegisterW login;

		public static event Connection.Registerp dislogin;

		public static void Start()
		{
			Connection.protocol = new CustomProtocol();
			Connection.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			try
			{
				Connection.socket.Connect("106.14.15.66", 1234);
				Connection.isConnected = true;
				CustomProtocol protoHeart = new CustomProtocol();
				protoHeart.AddString("EZ1");
				Connection.Send(protoHeart);
				Connection.lastTime = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds;
				Connection.heartTimer.AutoReset = false;
				Connection.heartTimer.Elapsed += new ElapsedEventHandler(Connection.HeartTimer_Elapsed);
				Connection.heartTimer.Start();
			}
			catch
			{
				MessageBox.Show(MultiLanguage.Hint6, MultiLanguage.Warn);
				Connection.isConnected = false;
				return;
			}
			Connection.socket.BeginReceive(Connection.readBuff, 0, 8192, SocketFlags.None, new AsyncCallback(Connection.ReceiveCallback), null);
		}

		private static void HeartTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			double currTime = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds;
			bool flag = currTime - Connection.lastTime > 150.0;
			if (flag)
			{
				Connection.isConnected = false;
				bool flag2 = Connection.login != null;
				if (flag2)
				{
					Connection.dislogin();
				}
			}
			else
			{
				CustomProtocol protoHeart = new CustomProtocol();
				protoHeart.AddString("EZ1");
				Connection.Send(protoHeart);
				Connection.heartTimer.Start();
			}
		}

		private static void ReceiveCallback(IAsyncResult ar)
		{
			try
			{
				int count = Connection.socket.EndReceive(ar);
				Connection.buffCount += count;
				Connection.ProcessData();
				Connection.socket.BeginReceive(Connection.readBuff, 0, 8192, SocketFlags.None, new AsyncCallback(Connection.ReceiveCallback), null);
			}
			catch (Exception e_47)
			{
				Connection.isConnected = false;
				Connection.socket.Close();
			}
		}

		private static void ProcessData()
		{
			bool flag = Connection.buffCount < 4;
			if (!flag)
			{
				Array.Copy(Connection.readBuff, Connection.lenBytes, 4);
				Connection.msgLength = BitConverter.ToInt32(Connection.lenBytes, 0);
				bool flag2 = Connection.buffCount < Connection.msgLength + 4;
				if (!flag2)
				{
					ProtocolBase proto = Connection.protocol.Decode(Connection.readBuff, 4, Connection.msgLength);
					Connection.HandleMsg(proto);
					Connection.msgList.Add(proto);
					int count = Connection.buffCount - Connection.msgLength - 4;
					Array.Copy(Connection.readBuff, 4 + Connection.msgLength, Connection.readBuff, 0, count);
					Connection.buffCount = count;
					bool flag3 = Connection.buffCount > 0;
					if (flag3)
					{
						Connection.ProcessData();
					}
				}
			}
		}

		private static void HandleMsg(ProtocolBase proto)
		{
			string str = proto.GetName();
			string a = str;
			if (!(a == "EA0"))
			{
				if (!(a == "EB0"))
				{
					if (!(a == "EC0"))
					{
						if (!(a == "EF0"))
						{
							if (a == "EZ0")
							{
								Connection.lastTime = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds;
							}
						}
						else
						{
							bool flag = Connection.recordD != null;
							if (flag)
							{
								Connection.recordD(proto);
							}
						}
					}
					else
					{
						bool flag2 = Connection.record != null;
						if (flag2)
						{
							Connection.record(proto);
						}
					}
				}
				else
				{
					bool flag3 = Connection.receive != null;
					if (flag3)
					{
						Connection.receive(proto);
					}
				}
			}
			else
			{
				bool flag4 = Connection.login != null;
				if (flag4)
				{
					Connection.login(proto);
				}
			}
		}

		public static void Send(ProtocolBase proto)
		{
			byte[] bytes = proto.Encode();
			byte[] length = BitConverter.GetBytes(bytes.Length);
			byte[] sendBuff = length.Concat(bytes).ToArray<byte>();
			try
			{
				Connection.socket.Send(sendBuff);
			}
			catch (Exception e_2E)
			{
			}
		}

		public static void Close()
		{
			Connection.isConnected = false;
			Connection.socket.Shutdown(SocketShutdown.Both);
			Connection.socket.Close();
		}
	}
}
