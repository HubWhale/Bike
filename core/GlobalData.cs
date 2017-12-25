using FinalClient;
using System;

namespace Bike
{
	public class GlobalData
	{
		public static bool LockId;

		public static bool Connectflag;

		public static bool ANT_Connectflag;

		public static bool ANT_Deviceflag;

		public static bool Rejisterflag;

		public static bool BBRejisterflag;

		public static bool Recordflag;

		public static bool Connectionflag = false;

		public static int language;

		public static bool NormalTrainingflag;

		public static bool VideoTrainingflag;

		public static bool Unity3DTrainingflag;

		public static bool Rememberflag;

		public static bool Uploadingflag;

		public static string Automaticflag;

		public static string UserName;

		public static string Password;

		public static string TrackName;

		public static string TrackId;

		public static string RoutebookId;

		public static string StartTime;

		public static string TrainingTime;

		public static long Time;

		public static string Distance;

		public static string distance;

		public static string Velocity;

		public static string velocity;

		public static string[] TrainingDatas;

		public static string[] SpeedDatas;

		public static string[] HeartRateData;

		public static string[] CadenceData;

		public static string[] PowerData;

		public static string RecordName;

		public static string RecordData;

		public static ProtocolBase proto;

		public static MainWindow mainWindow;

		public static ushort Heart_num;

		public static ushort Tramp_num;

		public static string VedioPath = Environment.CurrentDirectory + "\\Video";

		public static string ImagePath = Environment.CurrentDirectory + "\\Image";

		public static string SlopePath = Environment.CurrentDirectory + "\\Slope";

		public static int speedChannel;

		public static int heartChannel;

		public static int cadenceChannel;

		public static int deviceStyle;

		public static int userStyle;

		public static string speedequipment;

		public static string heartRateequipment;

		public static string cadenceequipment;

		public static string phoneNumber;

		public static string verifycode;

		public static string weight;

		public static string openId;

		public static string UserName1;

		public static string Mailbox;

		public static string Phone;

		public static string Sex;

		public static string Nationality;

		public static string Height;

		public static string Weight;

		public static string Size;

		public static void Init()
		{
			GlobalData.LockId = false;
			GlobalData.Connectflag = false;
			GlobalData.Rejisterflag = false;
			GlobalData.BBRejisterflag = false;
			GlobalData.ANT_Deviceflag = true;
			GlobalData.Recordflag = false;
			GlobalData.NormalTrainingflag = false;
			GlobalData.VideoTrainingflag = false;
			GlobalData.Unity3DTrainingflag = false;
			GlobalData.Rememberflag = false;
			GlobalData.Uploadingflag = false;
			GlobalData.Automaticflag = "0";
			GlobalData.Password = null;
			GlobalData.UserName = null;
			GlobalData.TrackName = null;
			GlobalData.TrackId = null;
			GlobalData.RoutebookId = null;
			GlobalData.StartTime = null;
			GlobalData.TrainingTime = "00:00:00";
			GlobalData.Time = 0L;
			GlobalData.Distance = "0";
			GlobalData.distance = "0";
			GlobalData.Velocity = "0";
			GlobalData.velocity = "0";
			GlobalData.language = 0;
			GlobalData.TrainingDatas = new string[16384];
			GlobalData.SpeedDatas = new string[16384];
			GlobalData.HeartRateData = new string[16384];
			GlobalData.CadenceData = new string[16384];
			GlobalData.PowerData = new string[16384];
			GlobalData.RecordName = null;
			GlobalData.RecordData = null;
			GlobalData.deviceStyle = 3;
			GlobalData.speedChannel = 9;
			GlobalData.heartChannel = 9;
			GlobalData.cadenceChannel = 9;
			GlobalData.userStyle = 1;
			GlobalData.speedequipment = null;
			GlobalData.heartRateequipment = null;
			GlobalData.cadenceequipment = null;
			GlobalData.phoneNumber = (GlobalData.verifycode = (GlobalData.weight = (GlobalData.openId = null)));
			GlobalData.UserName1 = null;
			GlobalData.Mailbox = null;
			GlobalData.Phone = null;
			GlobalData.Sex = null;
			GlobalData.Nationality = null;
			GlobalData.Height = null;
			GlobalData.Weight = null;
			GlobalData.Size = null;
		}

		public static void SendGear(int gear)
		{
			bool flag = !GlobalData.Connectflag;
			if (!flag)
			{
				bool flag2 = gear >= 17;
				if (flag2)
				{
					gear = 17;
					Device.Send("1231721");
				}
				else
				{
					bool flag3 = gear <= 0;
					if (flag3)
					{
						gear = 1;
					}
					string temp = "123" + gear.ToString("00") + "21";
					Device.Send(temp);
				}
			}
		}

		public static double getPower(double x, int y)
		{
			double temp;
			switch (y)
			{
			case 1:
				temp = 6.23E-06 * x * x * x * x - 0.000744 * x * x * x + 0.0593426 * x * x + 0.8774777 * x - 0.4313465;
				break;
			case 2:
				temp = -7.44E-07 * x * x * x * x + 0.0001793 * x * x * x + 0.0248203 * x * x + 1.5536117 * x - 1.1136392;
				break;
			case 3:
				temp = -2.33E-06 * x * x * x * x + 0.0003797 * x * x * x + 0.0212882 * x * x + 2.0925407 * x - 2.238826;
				break;
			case 4:
				temp = 1.66E-06 * x * x * x * x - 0.000367 * x * x * x + 0.0629841 * x * x + 1.7568913 * x - 1.5171909;
				break;
			case 5:
				temp = 3.1E-06 * x * x * x * x - 0.000626 * x * x * x + 0.0746448 * x * x + 2.0832546 * x - 2.1573242;
				break;
			case 6:
				temp = 2.98E-06 * x * x * x * x - 0.000825 * x * x * x + 0.0985809 * x * x + 2.024082 * x - 1.7324831;
				break;
			case 7:
				temp = 3.88E-06 * x * x * x * x - 0.001118 * x * x * x + 0.1209495 * x * x + 1.9609357 * x - 1.5069679;
				break;
			case 8:
				temp = 8.42E-06 * x * x * x * x - 0.001854 * x * x * x + 0.1606566 * x * x + 1.7966073 * x - 0.9176688;
				break;
			case 9:
				temp = 6.77E-06 * x * x * x * x - 0.001764 * x * x * x + 0.1700021 * x * x + 2.0124593 * x - 1.3333603;
				break;
			case 10:
				temp = 1.64E-05 * x * x * x * x - 0.003368 * x * x * x + 0.2562707 * x * x + 1.3454824 * x - 0.6286831;
				break;
			case 11:
				temp = 6.17E-06 * x * x * x * x - 0.001935 * x * x * x + 0.2042027 * x * x + 2.2845659 * x - 1.2569891;
				break;
			case 12:
				temp = 1.18E-05 * x * x * x * x - 0.002859 * x * x * x + 0.2537639 * x * x + 1.7640186 * x - 0.1588211;
				break;
			case 13:
				temp = 1.76E-05 * x * x * x * x - 0.003799 * x * x * x + 0.31067829 * x * x + 1.1527567 * x + 0.53762671;
				break;
			case 14:
				temp = 1.51E-06 * x * x * x * x - 0.00135 * x * x * x + 0.2011007 * x * x + 3.0245033 * x - 2.4501215;
				break;
			case 15:
				temp = 1.06E-06 * x * x * x * x - 0.001252 * x * x * x + 0.212719 * x * x + 3.0560831 * x - 1.5723267;
				break;
			case 16:
				temp = 5.96E-06 * x * x * x * x - 0.001964 * x * x * x + 0.25208822 * x * x + 3.0671692 * x - 1.7155435;
				break;
			default:
				temp = 6.55E-06 * x * x * x * x - 0.002277 * x * x * x + 0.2902504 * x * x + 2.7615807 * x - 0.4792189;
				break;
			}
			bool flag = temp < 0.0;
			if (flag)
			{
				temp = 0.0;
			}
			return temp;
		}

		public static double getPowerYZ(double x)
		{
			double temp = 0.0;
			bool flag = x >= 0.0 && x < 35.0;
			if (flag)
			{
				temp = 4.63E-07 * x * x * x * x * x - 0.000112 * x * x * x * x + 0.010095 * x * x * x - 0.083987 * x * x + 2.949981 * x + 0.035852;
			}
			bool flag2 = x >= 35.0;
			if (flag2)
			{
				temp = -1.94E-05 * x * x * x * x + 0.003543 * x * x * x + 0.105038 * x * x + 1.06784 * x + 1.814714;
			}
			bool flag3 = temp < 0.0;
			if (flag3)
			{
				temp = 0.0;
			}
			return temp;
		}
	}
}
