using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Bike
{
	internal static class UdpCore
	{
		public static UdpClient udpSend;

		public static UdpClient udpRecv;

		private static IPEndPoint RemotePoint;

		private static IPEndPoint LocalPoint;

		private static int LocalPort = 10001;

		private static int RemotePort = 10002;

		private static Thread thread;

		public static bool RunningFlag = false;

		public static string recvString = null;

		public static int GetGear = 0;

		public static void Init()
		{
			UdpCore.RemotePoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), UdpCore.RemotePort);
			UdpCore.LocalPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), UdpCore.LocalPort);
			UdpCore.udpSend = new UdpClient(0);
			UdpCore.udpRecv = new UdpClient(UdpCore.LocalPoint);
			UdpCore.RunningFlag = true;
			UdpCore.thread = new Thread(new ThreadStart(UdpCore.Recv));
			UdpCore.thread.Start();
		}

		public static void Uninit()
		{
			UdpCore.RunningFlag = false;
			UdpCore.udpSend.Close();
			UdpCore.udpRecv.Close();
			UdpCore.thread.Abort();
		}

		public static void Send(string data)
		{
			byte[] sendBuffer = Encoding.Default.GetBytes(data);
			UdpCore.udpSend.Send(sendBuffer, sendBuffer.Length, UdpCore.RemotePoint);
		}

		public static void SendSpeed(string data)
		{
			bool flag = data.Length != 4;
			if (!flag)
			{
				int num = int.Parse(data);
				byte[] sendBuffer = new byte[]
				{
					255,
					255,
					1,
					(byte)(num >> 8),
					(byte)num,
					238
				};
				UdpCore.udpSend.Send(sendBuffer, sendBuffer.Length, UdpCore.RemotePoint);
			}
		}

		public static void SendWheel(string data)
		{
			bool flag = data.Length != 4;
			if (!flag)
			{
				int num = int.Parse(data);
				byte[] sendBuffer = new byte[]
				{
					255,
					255,
					2,
					(byte)(num >> 8),
					(byte)num,
					238
				};
				UdpCore.udpSend.Send(sendBuffer, sendBuffer.Length, UdpCore.RemotePoint);
			}
		}

		private static void Recv()
		{
			while (UdpCore.RunningFlag)
			{
				try
				{
					byte[] revcBuffer = UdpCore.udpRecv.Receive(ref UdpCore.LocalPoint);
					bool unity3DTrainingflag = GlobalData.Unity3DTrainingflag;
					if (unity3DTrainingflag)
					{
						UdpCore.GetGear = int.Parse(revcBuffer[3].ToString());
					}
					UdpCore.recvString = Encoding.Default.GetString(revcBuffer);
				}
				catch
				{
					Thread.Sleep(200);
				}
			}
		}
	}
}
