using ANT_Managed_Library;
using AntPlus.Profiles.BikeCadence;
using AntPlus.Profiles.BikeSpeed;
using AntPlus.Profiles.HeartRate;
using AntPlus.Types;
using System;
using System.Windows;
using Bike;

namespace Sensor_Windows
{
	internal class ANT_heart_rate
	{
		public static int bsTemp0 = 0;

		public static int bsTemp1 = 0;

		public static int bsTemp2 = 0;

		public static int bsTemp3 = 0;

		public static int bsTemp4 = 0;

		public static int bsTemp5 = 0;

		public static int bsTemp6 = 0;

		public static int bsTemp7 = 0;

		public static int bcTemp0 = 0;

		public static int bcTemp1 = 0;

		public static int bcTemp2 = 0;

		public static int bcTemp3 = 0;

		public static int bcTemp4 = 0;

		public static int bcTemp5 = 0;

		public static int bcTemp6 = 0;

		public static int bcTemp7 = 0;

		public static int bs_count0 = 0;

		public static int bs_count1 = 0;

		public static int bs_count2 = 0;

		public static int bs_count3 = 0;

		public static int bs_count4 = 0;

		public static int bs_count5 = 0;

		public static int bs_count6 = 0;

		public static int bs_count7 = 0;

		public static int bc_count0 = 0;

		public static int bc_count1 = 0;

		public static int bc_count2 = 0;

		public static int bc_count3 = 0;

		public static int bc_count4 = 0;

		public static int bc_count5 = 0;

		public static int bc_count6 = 0;

		public static int bc_count7 = 0;

		public static string temp = "0";

		public static string Temp = "0";

		public static double lastCandanceTime = 0.0;

		public static double lastCandanceCount = 0.0;

		public static double CandanceTime = 0.0;

		public static double CandanceCount = 0.0;

		public static double lastSpeedTime = 0.0;

		public static double lastSpeedCount = 0.0;

		public static double SpeedTime = 0.0;

		public static double SpeedCount = 0.0;

		public static int bsCount = 0;

		public static int bcCount = 0;

		private static readonly ushort USER_DEVICENUM = 0;

		private static readonly byte USER_DEVICETYPE = 120;

		private static readonly byte USER_DEVICETYPE_tramp = 122;

		private static readonly byte USER_DEVICETYPE_speed = 123;

		private static readonly byte USER_TRANSTYPE = 0;

		private static readonly byte USER_RADIOFREQ = 57;

		private static readonly byte[] USER_NETWORK_KEY = new byte[]
		{
			185,
			165,
			33,
			251,
			189,
			114,
			195,
			69
		};

		private static readonly byte USER_NETWORK_NUM = 0;

		public static bool channelFlag0 = false;

		public static bool channelFlag1 = false;

		public static bool channelFlag2 = false;

		public static bool channelFlag3 = false;

		public static bool channelFlag4 = false;

		public static bool channelFlag5 = false;

		public static bool channelFlag6 = false;

		public static bool channelFlag7 = false;

		public static bool[] channelFlag = new bool[8];

		public static bool[] j = new bool[8];

		public static bool[] p = new bool[8];

		public static bool[] q = new bool[8];

		public static int m = 8;

		public static int n = 8;

		public static int t = 8;

		private static ANT_Device device0;

		private static ANT_Channel channel0;

		private static ANT_Channel channel1;

		private static ANT_Channel channel2;

		private static ANT_Channel channel3;

		private static ANT_Channel channel4;

		private static ANT_Channel channel5;

		private static ANT_Channel channel6;

		private static ANT_Channel channel7;

		private static ANT_Channel[] channel = new ANT_Channel[]
		{
			ANT_heart_rate.channel0,
			ANT_heart_rate.channel1,
			ANT_heart_rate.channel2,
			ANT_heart_rate.channel3,
			ANT_heart_rate.channel4,
			ANT_heart_rate.channel5,
			ANT_heart_rate.channel6,
			ANT_heart_rate.channel7
		};

		private static HeartRateDisplay heartRateDisplay0;

		private static HeartRateDisplay heartRateDisplay1;

		private static HeartRateDisplay heartRateDisplay2;

		private static HeartRateDisplay heartRateDisplay3;

		private static HeartRateDisplay heartRateDisplay4;

		private static HeartRateDisplay heartRateDisplay5;

		private static HeartRateDisplay heartRateDisplay6;

		private static HeartRateDisplay heartRateDisplay7;

		private static HeartRateDisplay[] heartRateDisplay = new HeartRateDisplay[]
		{
			ANT_heart_rate.heartRateDisplay0,
			ANT_heart_rate.heartRateDisplay1,
			ANT_heart_rate.heartRateDisplay2,
			ANT_heart_rate.heartRateDisplay3,
			ANT_heart_rate.heartRateDisplay4,
			ANT_heart_rate.heartRateDisplay5,
			ANT_heart_rate.heartRateDisplay6,
			ANT_heart_rate.heartRateDisplay7
		};

		private static BikeCadenceDisplay bikeCadenceDisplay0;

		private static BikeCadenceDisplay bikeCadenceDisplay1;

		private static BikeCadenceDisplay bikeCadenceDisplay2;

		private static BikeCadenceDisplay bikeCadenceDisplay3;

		private static BikeCadenceDisplay bikeCadenceDisplay4;

		private static BikeCadenceDisplay bikeCadenceDisplay5;

		private static BikeCadenceDisplay bikeCadenceDisplay6;

		private static BikeCadenceDisplay bikeCadenceDisplay7;

		private static BikeCadenceDisplay[] bikeCadenceDisplay = new BikeCadenceDisplay[]
		{
			ANT_heart_rate.bikeCadenceDisplay0,
			ANT_heart_rate.bikeCadenceDisplay1,
			ANT_heart_rate.bikeCadenceDisplay2,
			ANT_heart_rate.bikeCadenceDisplay3,
			ANT_heart_rate.bikeCadenceDisplay4,
			ANT_heart_rate.bikeCadenceDisplay5,
			ANT_heart_rate.bikeCadenceDisplay6,
			ANT_heart_rate.bikeCadenceDisplay7
		};

		private static BikeSpeedDisplay bikeSpeedDisplay0;

		private static BikeSpeedDisplay bikeSpeedDisplay1;

		private static BikeSpeedDisplay bikeSpeedDisplay2;

		private static BikeSpeedDisplay bikeSpeedDisplay3;

		private static BikeSpeedDisplay bikeSpeedDisplay4;

		private static BikeSpeedDisplay bikeSpeedDisplay5;

		private static BikeSpeedDisplay bikeSpeedDisplay6;

		private static BikeSpeedDisplay bikeSpeedDisplay7;

		private static BikeSpeedDisplay[] bikeSpeedDisplay = new BikeSpeedDisplay[]
		{
			ANT_heart_rate.bikeSpeedDisplay0,
			ANT_heart_rate.bikeSpeedDisplay1,
			ANT_heart_rate.bikeSpeedDisplay2,
			ANT_heart_rate.bikeSpeedDisplay3,
			ANT_heart_rate.bikeSpeedDisplay4,
			ANT_heart_rate.bikeSpeedDisplay5,
			ANT_heart_rate.bikeSpeedDisplay6,
			ANT_heart_rate.bikeSpeedDisplay7
		};

		public static Network networkAntPlus = new Network(ANT_heart_rate.USER_NETWORK_NUM, ANT_heart_rate.USER_NETWORK_KEY, ANT_heart_rate.USER_RADIOFREQ);

		public static ANT_ReferenceLibrary.ChannelType channelType;

		private static bool bDone;

		private static bool bDisplay;

		private static bool bBroadcasting;

		private static int iIndex = 0;

		public static string dn0;

		public static string dn1;

		public static string dn2;

		public static string dn3;

		public static string dn4;

		public static string dn5;

		public static string dn6;

		public static string dn7;

		public static string HR0;

		public static string HR1;

		public static string HR2;

		public static string HR3;

		public static string HR4;

		public static string HR5;

		public static string HR6;

		public static string HR7;

		public static string Dn0;

		public static string Dn1;

		public static string Dn2;

		public static string Dn3;

		public static string Dn4;

		public static string Dn5;

		public static string Dn6;

		public static string Dn7;

		public static string BC0;

		public static string BC1;

		public static string BC2;

		public static string BC3;

		public static string BC4;

		public static string BC5;

		public static string BC6;

		public static string BC7;

		public static string DN0;

		public static string DN1;

		public static string DN2;

		public static string DN3;

		public static string DN4;

		public static string DN5;

		public static string DN6;

		public static string DN7;

		public static string BS0;

		public static string BS1;

		public static string BS2;

		public static string BS3;

		public static string BS4;

		public static string BS5;

		public static string BS6;

		public static string BS7;

		public static void clearHrValue()
		{
			ANT_heart_rate.HR0 = null;
			ANT_heart_rate.HR1 = null;
			ANT_heart_rate.HR2 = null;
			ANT_heart_rate.HR3 = null;
			ANT_heart_rate.HR4 = null;
			ANT_heart_rate.HR5 = null;
			ANT_heart_rate.HR6 = null;
			ANT_heart_rate.HR7 = null;
		}

		public static void clearBcValue()
		{
			ANT_heart_rate.BC0 = null;
			ANT_heart_rate.BC1 = null;
			ANT_heart_rate.BC2 = null;
			ANT_heart_rate.BC3 = null;
			ANT_heart_rate.BC4 = null;
			ANT_heart_rate.BC5 = null;
			ANT_heart_rate.BC6 = null;
			ANT_heart_rate.BC7 = null;
		}

		public static void clearBsValue()
		{
			ANT_heart_rate.BS0 = null;
			ANT_heart_rate.BS1 = null;
			ANT_heart_rate.BS2 = null;
			ANT_heart_rate.BS3 = null;
			ANT_heart_rate.BS4 = null;
			ANT_heart_rate.BS5 = null;
			ANT_heart_rate.BS6 = null;
			ANT_heart_rate.BS7 = null;
		}

		public static void closeChannels()
		{
			for (int i = 0; i < 8; i++)
			{
				bool flag = i == GlobalData.speedChannel || i == GlobalData.cadenceChannel || i == GlobalData.heartChannel;
				if (!flag)
				{
					try
					{
						switch (GlobalData.deviceStyle)
						{
						case 0:
						{
							bool flag2 = i == 0;
							if (flag2)
							{
								ANT_heart_rate.bikeSpeedDisplay0.TurnOff();
							}
							else
							{
								ANT_heart_rate.bikeSpeedDisplay[i].TurnOff();
							}
							break;
						}
						case 1:
						{
							bool flag3 = i == 1;
							if (flag3)
							{
								ANT_heart_rate.bikeCadenceDisplay1.TurnOff();
							}
							else
							{
								ANT_heart_rate.bikeCadenceDisplay[i].TurnOff();
							}
							break;
						}
						case 2:
						{
							bool flag4 = i == 2;
							if (flag4)
							{
								ANT_heart_rate.heartRateDisplay2.TurnOff();
							}
							else
							{
								ANT_heart_rate.heartRateDisplay[i].TurnOff();
							}
							break;
						}
						}
					}
					catch
					{
					}
				}
			}
		}

		public static void disconnectHr()
		{
			int a = GlobalData.heartChannel;
			try
			{
				bool flag = a == 2;
				if (flag)
				{
					ANT_heart_rate.channel2.closeChannel();
					ANT_heart_rate.heartRateDisplay2.TurnOff();
				}
				else
				{
					ANT_heart_rate.heartRateDisplay[a].TurnOff();
				}
			}
			catch
			{
			}
			GlobalData.heartChannel = 9;
		}

		public static void disconnectBc()
		{
			int a = GlobalData.cadenceChannel;
			try
			{
				bool flag = a == 1;
				if (flag)
				{
					ANT_heart_rate.channel1.closeChannel();
					ANT_heart_rate.bikeCadenceDisplay1.TurnOff();
				}
				else
				{
					ANT_heart_rate.bikeCadenceDisplay[a].TurnOff();
				}
			}
			catch
			{
			}
			GlobalData.cadenceChannel = 9;
		}

		public static void disconnectBs()
		{
			int a = GlobalData.speedChannel;
			try
			{
				bool flag = a == 0;
				if (flag)
				{
					ANT_heart_rate.channel0.closeChannel();
					ANT_heart_rate.bikeSpeedDisplay0.TurnOff();
				}
				else
				{
					ANT_heart_rate.bikeSpeedDisplay[a].TurnOff();
				}
			}
			catch
			{
			}
			GlobalData.speedChannel = 9;
		}

		public static void hr_Init()
		{
			for (int i = 0; i < 8; i++)
			{
				bool flag = i == GlobalData.speedChannel || i == GlobalData.cadenceChannel || i == GlobalData.heartChannel;
				if (!flag)
				{
					try
					{
						bool flag2 = ANT_heart_rate.device0 == null;
						if (flag2)
						{
							ANT_heart_rate.device0 = new ANT_Device();
						}
						bool flag3 = i == 2;
						if (flag3)
						{
							ANT_heart_rate.channel2 = ANT_heart_rate.device0.getChannel(2);
						}
						else
						{
							ANT_heart_rate.channel[i] = ANT_heart_rate.device0.getChannel(i);
						}
					}
					catch (Exception ex)
					{
						bool flag4 = ANT_heart_rate.device0 == null;
						if (flag4)
						{
							throw new Exception("Could not connect to any device.\nDetails: \n   " + ex.Message);
						}
						throw new Exception("Error connecting to ANT: " + ex.Message);
					}
					bool flag5 = ANT_heart_rate.device0.setNetworkKey(ANT_heart_rate.USER_NETWORK_NUM, ANT_heart_rate.USER_NETWORK_KEY, 500u);
					if (!flag5)
					{
						throw new Exception("Error configuring network key");
					}
					bool flag6 = i == 2;
					if (flag6)
					{
						bool flag7 = ANT_heart_rate.channel2.setChannelID(ANT_heart_rate.USER_DEVICENUM, false, ANT_heart_rate.USER_DEVICETYPE, ANT_heart_rate.USER_TRANSTYPE, 500u);
						if (flag7)
						{
						}
					}
					else
					{
						bool flag8 = ANT_heart_rate.channel[i].setChannelID(ANT_heart_rate.USER_DEVICENUM, false, ANT_heart_rate.USER_DEVICETYPE, ANT_heart_rate.USER_TRANSTYPE, 500u);
						if (!flag8)
						{
							throw new Exception("Error configuring Channel ID");
						}
					}
					switch (i)
					{
					case 0:
						ANT_heart_rate.hrd0(i);
						break;
					case 1:
						ANT_heart_rate.hrd1(i);
						break;
					case 2:
						ANT_heart_rate.hrd2(i);
						break;
					case 3:
						ANT_heart_rate.hrd3(i);
						break;
					case 4:
						ANT_heart_rate.hrd4(i);
						break;
					case 5:
						ANT_heart_rate.hrd5(i);
						break;
					case 6:
						ANT_heart_rate.hrd6(i);
						break;
					case 7:
						ANT_heart_rate.hrd7(i);
						break;
					}
				}
			}
		}

		private static void hrd0(int i)
		{
			ANT_heart_rate.heartRateDisplay[i] = new HeartRateDisplay(ANT_heart_rate.channel[i], ANT_heart_rate.networkAntPlus);
			ANT_heart_rate.heartRateDisplay[i].HeartRateDataReceived += new Action<HeartRateData, uint>(ANT_heart_rate.HeartRateDisplay_HeartRateDataReceived0);
			ANT_heart_rate.heartRateDisplay[i].TurnOn();
		}

		private static void HeartRateDisplay_HeartRateDataReceived0(HeartRateData arg3, uint arg4)
		{
			ANT_Response respChID = ANT_heart_rate.device0.requestMessageAndResponse(0, ANT_ReferenceLibrary.RequestMessageID.CHANNEL_ID_0x51, 500u);
			ANT_heart_rate.dn0 = ((ushort)(((int)respChID.messageContents[2] << 8) + (int)respChID.messageContents[1])).ToString();
			ANT_heart_rate.HR0 = arg3.HeartRate.ToString();
		}

		private static void hrd1(int i)
		{
			ANT_heart_rate.heartRateDisplay[i] = new HeartRateDisplay(ANT_heart_rate.channel[i], ANT_heart_rate.networkAntPlus);
			ANT_heart_rate.heartRateDisplay[i].HeartRateDataReceived += new Action<HeartRateData, uint>(ANT_heart_rate.HeartRateDisplay_HeartRateDataReceived1);
			ANT_heart_rate.heartRateDisplay[i].TurnOn();
		}

		private static void HeartRateDisplay_HeartRateDataReceived1(HeartRateData arg3, uint arg4)
		{
			ANT_Response respChID = ANT_heart_rate.device0.requestMessageAndResponse(1, ANT_ReferenceLibrary.RequestMessageID.CHANNEL_ID_0x51, 500u);
			ANT_heart_rate.dn1 = ((ushort)(((int)respChID.messageContents[2] << 8) + (int)respChID.messageContents[1])).ToString();
			ANT_heart_rate.HR1 = arg3.HeartRate.ToString();
		}

		private static void hrd2(int i)
		{
			ANT_heart_rate.heartRateDisplay2 = new HeartRateDisplay(ANT_heart_rate.channel2, ANT_heart_rate.networkAntPlus);
			ANT_heart_rate.heartRateDisplay2.HeartRateDataReceived += new Action<HeartRateData, uint>(ANT_heart_rate.HeartRateDisplay_HeartRateDataReceived2);
			ANT_heart_rate.heartRateDisplay2.TurnOn();
		}

		private static void HeartRateDisplay_HeartRateDataReceived2(HeartRateData arg3, uint arg4)
		{
			ANT_Response respChID = ANT_heart_rate.device0.requestMessageAndResponse(2, ANT_ReferenceLibrary.RequestMessageID.CHANNEL_ID_0x51, 500u);
			ANT_heart_rate.dn2 = ((ushort)(((int)respChID.messageContents[2] << 8) + (int)respChID.messageContents[1])).ToString();
			ANT_heart_rate.HR2 = arg3.HeartRate.ToString();
		}

		private static void hrd3(int i)
		{
			ANT_heart_rate.heartRateDisplay[i] = new HeartRateDisplay(ANT_heart_rate.channel[i], ANT_heart_rate.networkAntPlus);
			ANT_heart_rate.heartRateDisplay[i].HeartRateDataReceived += new Action<HeartRateData, uint>(ANT_heart_rate.HeartRateDisplay_HeartRateDataReceived3);
			ANT_heart_rate.heartRateDisplay[i].TurnOn();
		}

		private static void HeartRateDisplay_HeartRateDataReceived3(HeartRateData arg3, uint arg4)
		{
			ANT_Response respChID = ANT_heart_rate.device0.requestMessageAndResponse(3, ANT_ReferenceLibrary.RequestMessageID.CHANNEL_ID_0x51, 500u);
			ANT_heart_rate.dn3 = ((ushort)(((int)respChID.messageContents[2] << 8) + (int)respChID.messageContents[1])).ToString();
			ANT_heart_rate.HR3 = arg3.HeartRate.ToString();
		}

		private static void hrd4(int i)
		{
			ANT_heart_rate.heartRateDisplay[i] = new HeartRateDisplay(ANT_heart_rate.channel[i], ANT_heart_rate.networkAntPlus);
			ANT_heart_rate.heartRateDisplay[i].HeartRateDataReceived += new Action<HeartRateData, uint>(ANT_heart_rate.HeartRateDisplay_HeartRateDataReceived4);
			ANT_heart_rate.heartRateDisplay[i].TurnOn();
		}

		private static void HeartRateDisplay_HeartRateDataReceived4(HeartRateData arg3, uint arg4)
		{
			ANT_Response respChID = ANT_heart_rate.device0.requestMessageAndResponse(4, ANT_ReferenceLibrary.RequestMessageID.CHANNEL_ID_0x51, 500u);
			ANT_heart_rate.dn4 = ((ushort)(((int)respChID.messageContents[2] << 8) + (int)respChID.messageContents[1])).ToString();
			ANT_heart_rate.HR4 = arg3.HeartRate.ToString();
		}

		private static void hrd5(int i)
		{
			ANT_heart_rate.heartRateDisplay[i] = new HeartRateDisplay(ANT_heart_rate.channel[i], ANT_heart_rate.networkAntPlus);
			ANT_heart_rate.heartRateDisplay[i].HeartRateDataReceived += new Action<HeartRateData, uint>(ANT_heart_rate.HeartRateDisplay_HeartRateDataReceived5);
			ANT_heart_rate.heartRateDisplay[i].TurnOn();
		}

		private static void HeartRateDisplay_HeartRateDataReceived5(HeartRateData arg3, uint arg4)
		{
			ANT_Response respChID = ANT_heart_rate.device0.requestMessageAndResponse(5, ANT_ReferenceLibrary.RequestMessageID.CHANNEL_ID_0x51, 500u);
			ANT_heart_rate.dn5 = ((ushort)(((int)respChID.messageContents[2] << 8) + (int)respChID.messageContents[1])).ToString();
			ANT_heart_rate.HR5 = arg3.HeartRate.ToString();
		}

		private static void hrd6(int i)
		{
			ANT_heart_rate.heartRateDisplay[i] = new HeartRateDisplay(ANT_heart_rate.channel[i], ANT_heart_rate.networkAntPlus);
			ANT_heart_rate.heartRateDisplay[i].HeartRateDataReceived += new Action<HeartRateData, uint>(ANT_heart_rate.HeartRateDisplay_HeartRateDataReceived6);
			ANT_heart_rate.heartRateDisplay[i].TurnOn();
		}

		private static void HeartRateDisplay_HeartRateDataReceived6(HeartRateData arg3, uint arg4)
		{
			ANT_Response respChID = ANT_heart_rate.device0.requestMessageAndResponse(6, ANT_ReferenceLibrary.RequestMessageID.CHANNEL_ID_0x51, 500u);
			ANT_heart_rate.dn6 = ((ushort)(((int)respChID.messageContents[2] << 8) + (int)respChID.messageContents[1])).ToString();
			ANT_heart_rate.HR6 = arg3.HeartRate.ToString();
		}

		private static void hrd7(int i)
		{
			ANT_heart_rate.heartRateDisplay[i] = new HeartRateDisplay(ANT_heart_rate.channel[i], ANT_heart_rate.networkAntPlus);
			ANT_heart_rate.heartRateDisplay[i].HeartRateDataReceived += new Action<HeartRateData, uint>(ANT_heart_rate.HeartRateDisplay_HeartRateDataReceived7);
			ANT_heart_rate.heartRateDisplay[i].TurnOn();
		}

		private static void HeartRateDisplay_HeartRateDataReceived7(HeartRateData arg3, uint arg4)
		{
			ANT_Response respChID = ANT_heart_rate.device0.requestMessageAndResponse(7, ANT_ReferenceLibrary.RequestMessageID.CHANNEL_ID_0x51, 500u);
			ANT_heart_rate.dn7 = ((ushort)(((int)respChID.messageContents[2] << 8) + (int)respChID.messageContents[1])).ToString();
			ANT_heart_rate.HR7 = arg3.HeartRate.ToString();
		}

		public static void bc_Init()
		{
			for (int i = 0; i < 8; i++)
			{
				bool flag = i == GlobalData.speedChannel || i == GlobalData.cadenceChannel || i == GlobalData.heartChannel;
				if (!flag)
				{
					try
					{
						bool flag2 = ANT_heart_rate.device0 == null;
						if (flag2)
						{
							ANT_heart_rate.device0 = new ANT_Device();
						}
						bool flag3 = i == 1;
						if (flag3)
						{
							ANT_heart_rate.channel1 = ANT_heart_rate.device0.getChannel(1);
						}
						else
						{
							ANT_heart_rate.channel[i] = ANT_heart_rate.device0.getChannel(i);
						}
					}
					catch (Exception ex)
					{
						bool flag4 = ANT_heart_rate.device0 == null;
						if (flag4)
						{
							throw new Exception("Could not connect to any device.\nDetails: \n   " + ex.Message);
						}
						throw new Exception("Error connecting to ANT: " + ex.Message);
					}
					bool flag5 = ANT_heart_rate.device0.setNetworkKey(ANT_heart_rate.USER_NETWORK_NUM, ANT_heart_rate.USER_NETWORK_KEY, 500u);
					if (!flag5)
					{
						throw new Exception("Error configuring network key");
					}
					bool flag6 = i == 1;
					if (flag6)
					{
						bool flag7 = ANT_heart_rate.channel1.setChannelID(ANT_heart_rate.USER_DEVICENUM, false, ANT_heart_rate.USER_DEVICETYPE_tramp, ANT_heart_rate.USER_TRANSTYPE, 500u);
						if (flag7)
						{
						}
					}
					else
					{
						bool flag8 = ANT_heart_rate.channel[i].setChannelID(ANT_heart_rate.USER_DEVICENUM, false, ANT_heart_rate.USER_DEVICETYPE_tramp, ANT_heart_rate.USER_TRANSTYPE, 500u);
						if (!flag8)
						{
							throw new Exception("Error configuring Channel ID");
						}
					}
					switch (i)
					{
					case 0:
						ANT_heart_rate.bcd0(i);
						break;
					case 1:
						ANT_heart_rate.bcd1(i);
						break;
					case 2:
						ANT_heart_rate.bcd2(i);
						break;
					case 3:
						ANT_heart_rate.bcd3(i);
						break;
					case 4:
						ANT_heart_rate.bcd4(i);
						break;
					case 5:
						ANT_heart_rate.bcd5(i);
						break;
					case 6:
						ANT_heart_rate.bcd6(i);
						break;
					case 7:
						ANT_heart_rate.bcd7(i);
						break;
					}
				}
			}
		}

		private static void bcd0(int i)
		{
			ANT_heart_rate.bikeCadenceDisplay[i] = new BikeCadenceDisplay(ANT_heart_rate.channel[i], ANT_heart_rate.networkAntPlus);
			ANT_heart_rate.bikeCadenceDisplay[i].BikeCadenceDataReceived += new Action<BikeCadenceData, uint>(ANT_heart_rate.BikeCadenceDisplay_BikeCadenceDataReceived0);
			ANT_heart_rate.bikeCadenceDisplay[i].TurnOn();
		}

		private static void BikeCadenceDisplay_BikeCadenceDataReceived0(BikeCadenceData arg3, uint arg4)
		{
			ANT_Response respChID = ANT_heart_rate.device0.requestMessageAndResponse(0, ANT_ReferenceLibrary.RequestMessageID.CHANNEL_ID_0x51, 500u);
			ANT_heart_rate.Dn0 = ((ushort)(((int)respChID.messageContents[2] << 8) + (int)respChID.messageContents[1])).ToString();
			ANT_heart_rate.BC0 = arg3.Cadence.ToString();
			bool flag = ANT_heart_rate.bcTemp0 == (int)ANT_heart_rate.bikeCadenceDisplay[0].TotalCrankEventCount && ANT_heart_rate.bcTemp0 != 0;
			if (flag)
			{
				ANT_heart_rate.bc_count0++;
			}
			else
			{
				ANT_heart_rate.bc_count0 = 0;
			}
			bool flag2 = ANT_heart_rate.bc_count0 > 11;
			if (flag2)
			{
				ANT_heart_rate.BC0 = "0";
			}
			ANT_heart_rate.bcTemp0 = (int)ANT_heart_rate.bikeCadenceDisplay[0].TotalCrankEventCount;
		}

		private static void bcd1(int i)
		{
			ANT_heart_rate.bikeCadenceDisplay1 = new BikeCadenceDisplay(ANT_heart_rate.channel1, ANT_heart_rate.networkAntPlus);
			ANT_heart_rate.bikeCadenceDisplay1.BikeCadenceDataReceived += new Action<BikeCadenceData, uint>(ANT_heart_rate.BikeCadenceDisplay_BikeCadenceDataReceived1);
			ANT_heart_rate.bikeCadenceDisplay1.TurnOn();
		}

		private static void BikeCadenceDisplay_BikeCadenceDataReceived1(BikeCadenceData arg3, uint arg4)
		{
			ANT_Response respChID = ANT_heart_rate.device0.requestMessageAndResponse(1, ANT_ReferenceLibrary.RequestMessageID.CHANNEL_ID_0x51, 500u);
			ANT_heart_rate.Dn1 = ((ushort)(((int)respChID.messageContents[2] << 8) + (int)respChID.messageContents[1])).ToString();
			ANT_heart_rate.BC1 = arg3.Cadence.ToString();
			bool flag = ANT_heart_rate.bcTemp1 == (int)ANT_heart_rate.bikeCadenceDisplay1.TotalCrankEventCount && ANT_heart_rate.bcTemp1 != 0;
			if (flag)
			{
				ANT_heart_rate.bc_count1++;
			}
			else
			{
				ANT_heart_rate.bc_count1 = 0;
			}
			bool flag2 = ANT_heart_rate.bc_count1 > 11;
			if (flag2)
			{
				ANT_heart_rate.BC1 = "0";
			}
			ANT_heart_rate.bcTemp1 = (int)ANT_heart_rate.bikeCadenceDisplay1.TotalCrankEventCount;
		}

		private static void bcd2(int i)
		{
			ANT_heart_rate.bikeCadenceDisplay[i] = new BikeCadenceDisplay(ANT_heart_rate.channel[i], ANT_heart_rate.networkAntPlus);
			ANT_heart_rate.bikeCadenceDisplay[i].BikeCadenceDataReceived += new Action<BikeCadenceData, uint>(ANT_heart_rate.BikeCadenceDisplay_BikeCadenceDataReceived2);
			ANT_heart_rate.bikeCadenceDisplay[i].TurnOn();
		}

		private static void BikeCadenceDisplay_BikeCadenceDataReceived2(BikeCadenceData arg3, uint arg4)
		{
			ANT_Response respChID = ANT_heart_rate.device0.requestMessageAndResponse(2, ANT_ReferenceLibrary.RequestMessageID.CHANNEL_ID_0x51, 500u);
			ANT_heart_rate.Dn2 = ((ushort)(((int)respChID.messageContents[2] << 8) + (int)respChID.messageContents[1])).ToString();
			ANT_heart_rate.BC2 = arg3.Cadence.ToString();
			bool flag = ANT_heart_rate.bcTemp2 == (int)ANT_heart_rate.bikeCadenceDisplay[2].TotalCrankEventCount && ANT_heart_rate.bcTemp2 != 0;
			if (flag)
			{
				ANT_heart_rate.bc_count2++;
			}
			else
			{
				ANT_heart_rate.bc_count2 = 0;
			}
			bool flag2 = ANT_heart_rate.bc_count2 > 11;
			if (flag2)
			{
				ANT_heart_rate.BC2 = "0";
			}
			ANT_heart_rate.bcTemp2 = (int)ANT_heart_rate.bikeCadenceDisplay[2].TotalCrankEventCount;
		}

		private static void bcd3(int i)
		{
			ANT_heart_rate.bikeCadenceDisplay[i] = new BikeCadenceDisplay(ANT_heart_rate.channel[i], ANT_heart_rate.networkAntPlus);
			ANT_heart_rate.bikeCadenceDisplay[i].BikeCadenceDataReceived += new Action<BikeCadenceData, uint>(ANT_heart_rate.BikeCadenceDisplay_BikeCadenceDataReceived3);
			ANT_heart_rate.bikeCadenceDisplay[i].TurnOn();
		}

		private static void BikeCadenceDisplay_BikeCadenceDataReceived3(BikeCadenceData arg3, uint arg4)
		{
			ANT_Response respChID = ANT_heart_rate.device0.requestMessageAndResponse(3, ANT_ReferenceLibrary.RequestMessageID.CHANNEL_ID_0x51, 500u);
			ANT_heart_rate.Dn3 = ((ushort)(((int)respChID.messageContents[2] << 8) + (int)respChID.messageContents[1])).ToString();
			ANT_heart_rate.BC3 = arg3.Cadence.ToString();
			bool flag = ANT_heart_rate.bcTemp3 == (int)ANT_heart_rate.bikeCadenceDisplay[3].TotalCrankEventCount && ANT_heart_rate.bcTemp3 != 0;
			if (flag)
			{
				ANT_heart_rate.bc_count3++;
			}
			else
			{
				ANT_heart_rate.bc_count3 = 0;
			}
			bool flag2 = ANT_heart_rate.bc_count3 > 11;
			if (flag2)
			{
				ANT_heart_rate.BC3 = "0";
			}
			ANT_heart_rate.bcTemp3 = (int)ANT_heart_rate.bikeCadenceDisplay[3].TotalCrankEventCount;
		}

		private static void bcd4(int i)
		{
			ANT_heart_rate.bikeCadenceDisplay[i] = new BikeCadenceDisplay(ANT_heart_rate.channel[i], ANT_heart_rate.networkAntPlus);
			ANT_heart_rate.bikeCadenceDisplay[i].BikeCadenceDataReceived += new Action<BikeCadenceData, uint>(ANT_heart_rate.BikeCadenceDisplay_BikeCadenceDataReceived4);
			ANT_heart_rate.bikeCadenceDisplay[i].TurnOn();
		}

		private static void BikeCadenceDisplay_BikeCadenceDataReceived4(BikeCadenceData arg3, uint arg4)
		{
			ANT_Response respChID = ANT_heart_rate.device0.requestMessageAndResponse(4, ANT_ReferenceLibrary.RequestMessageID.CHANNEL_ID_0x51, 500u);
			ANT_heart_rate.Dn4 = ((ushort)(((int)respChID.messageContents[2] << 8) + (int)respChID.messageContents[1])).ToString();
			ANT_heart_rate.BC4 = arg3.Cadence.ToString();
			bool flag = ANT_heart_rate.bcTemp4 == (int)ANT_heart_rate.bikeCadenceDisplay[4].TotalCrankEventCount && ANT_heart_rate.bcTemp4 != 0;
			if (flag)
			{
				ANT_heart_rate.bc_count4++;
			}
			else
			{
				ANT_heart_rate.bc_count4 = 0;
			}
			bool flag2 = ANT_heart_rate.bc_count4 > 11;
			if (flag2)
			{
				ANT_heart_rate.BC4 = "0";
			}
			ANT_heart_rate.bcTemp4 = (int)ANT_heart_rate.bikeCadenceDisplay[4].TotalCrankEventCount;
		}

		private static void bcd5(int i)
		{
			ANT_heart_rate.bikeCadenceDisplay[i] = new BikeCadenceDisplay(ANT_heart_rate.channel[i], ANT_heart_rate.networkAntPlus);
			ANT_heart_rate.bikeCadenceDisplay[i].BikeCadenceDataReceived += new Action<BikeCadenceData, uint>(ANT_heart_rate.BikeCadenceDisplay_BikeCadenceDataReceived5);
			ANT_heart_rate.bikeCadenceDisplay[i].TurnOn();
		}

		private static void BikeCadenceDisplay_BikeCadenceDataReceived5(BikeCadenceData arg3, uint arg4)
		{
			ANT_Response respChID = ANT_heart_rate.device0.requestMessageAndResponse(5, ANT_ReferenceLibrary.RequestMessageID.CHANNEL_ID_0x51, 500u);
			ANT_heart_rate.Dn5 = ((ushort)(((int)respChID.messageContents[2] << 8) + (int)respChID.messageContents[1])).ToString();
			ANT_heart_rate.BC5 = arg3.Cadence.ToString();
			bool flag = ANT_heart_rate.bcTemp5 == (int)ANT_heart_rate.bikeCadenceDisplay[5].TotalCrankEventCount && ANT_heart_rate.bcTemp5 != 0;
			if (flag)
			{
				ANT_heart_rate.bc_count5++;
			}
			else
			{
				ANT_heart_rate.bc_count5 = 0;
			}
			bool flag2 = ANT_heart_rate.bc_count5 > 11;
			if (flag2)
			{
				ANT_heart_rate.BC5 = "0";
			}
			ANT_heart_rate.bcTemp5 = (int)ANT_heart_rate.bikeCadenceDisplay[5].TotalCrankEventCount;
		}

		private static void bcd6(int i)
		{
			ANT_heart_rate.bikeCadenceDisplay[i] = new BikeCadenceDisplay(ANT_heart_rate.channel[i], ANT_heart_rate.networkAntPlus);
			ANT_heart_rate.bikeCadenceDisplay[i].BikeCadenceDataReceived += new Action<BikeCadenceData, uint>(ANT_heart_rate.BikeCadenceDisplay_BikeCadenceDataReceived6);
			ANT_heart_rate.bikeCadenceDisplay[i].TurnOn();
		}

		private static void BikeCadenceDisplay_BikeCadenceDataReceived6(BikeCadenceData arg3, uint arg4)
		{
			ANT_Response respChID = ANT_heart_rate.device0.requestMessageAndResponse(6, ANT_ReferenceLibrary.RequestMessageID.CHANNEL_ID_0x51, 500u);
			ANT_heart_rate.Dn6 = ((ushort)(((int)respChID.messageContents[2] << 8) + (int)respChID.messageContents[1])).ToString();
			ANT_heart_rate.BC6 = arg3.Cadence.ToString();
			bool flag = ANT_heart_rate.bcTemp6 == (int)ANT_heart_rate.bikeCadenceDisplay[6].TotalCrankEventCount && ANT_heart_rate.bcTemp6 != 0;
			if (flag)
			{
				ANT_heart_rate.bc_count6++;
			}
			else
			{
				ANT_heart_rate.bc_count6 = 0;
			}
			bool flag2 = ANT_heart_rate.bc_count6 > 11;
			if (flag2)
			{
				ANT_heart_rate.BC6 = "0";
			}
			ANT_heart_rate.bcTemp6 = (int)ANT_heart_rate.bikeCadenceDisplay[6].TotalCrankEventCount;
		}

		private static void bcd7(int i)
		{
			ANT_heart_rate.bikeCadenceDisplay[i] = new BikeCadenceDisplay(ANT_heart_rate.channel[i], ANT_heart_rate.networkAntPlus);
			ANT_heart_rate.bikeCadenceDisplay[i].BikeCadenceDataReceived += new Action<BikeCadenceData, uint>(ANT_heart_rate.BikeCadenceDisplay_BikeCadenceDataReceived7);
			ANT_heart_rate.bikeCadenceDisplay[i].TurnOn();
		}

		private static void BikeCadenceDisplay_BikeCadenceDataReceived7(BikeCadenceData arg3, uint arg4)
		{
			ANT_Response respChID = ANT_heart_rate.device0.requestMessageAndResponse(7, ANT_ReferenceLibrary.RequestMessageID.CHANNEL_ID_0x51, 500u);
			ANT_heart_rate.Dn7 = ((ushort)(((int)respChID.messageContents[2] << 8) + (int)respChID.messageContents[1])).ToString();
			ANT_heart_rate.BC7 = arg3.Cadence.ToString();
			bool flag = ANT_heart_rate.bcTemp7 == (int)ANT_heart_rate.bikeCadenceDisplay[7].TotalCrankEventCount && ANT_heart_rate.bcTemp7 != 0;
			if (flag)
			{
				ANT_heart_rate.bc_count7++;
			}
			else
			{
				ANT_heart_rate.bc_count7 = 0;
			}
			bool flag2 = ANT_heart_rate.bc_count7 > 11;
			if (flag2)
			{
				ANT_heart_rate.BC7 = "0";
			}
			ANT_heart_rate.bcTemp7 = (int)ANT_heart_rate.bikeCadenceDisplay[7].TotalCrankEventCount;
		}

		public static void bs_Init()
		{
			for (int i = 0; i < 8; i++)
			{
				bool flag = i == GlobalData.speedChannel || i == GlobalData.cadenceChannel || i == GlobalData.heartChannel;
				if (!flag)
				{
					try
					{
						bool flag2 = ANT_heart_rate.device0 == null;
						if (flag2)
						{
							ANT_heart_rate.device0 = new ANT_Device();
						}
						bool flag3 = i == 0;
						if (flag3)
						{
							ANT_heart_rate.channel0 = ANT_heart_rate.device0.getChannel(0);
						}
						else
						{
							ANT_heart_rate.channel[i] = ANT_heart_rate.device0.getChannel(i);
						}
					}
					catch (Exception ex)
					{
						bool flag4 = ANT_heart_rate.device0 == null;
						if (flag4)
						{
							throw new Exception("Could not connect to any device.\nDetails: \n   " + ex.Message);
						}
						throw new Exception("Error connecting to ANT: " + ex.Message);
					}
					bool flag5 = ANT_heart_rate.device0.setNetworkKey(ANT_heart_rate.USER_NETWORK_NUM, ANT_heart_rate.USER_NETWORK_KEY, 500u);
					if (!flag5)
					{
						throw new Exception("Error configuring network key");
					}
					bool flag6 = i == 0;
					if (flag6)
					{
						bool flag7 = ANT_heart_rate.channel0.setChannelID(ANT_heart_rate.USER_DEVICENUM, false, ANT_heart_rate.USER_DEVICETYPE_speed, ANT_heart_rate.USER_TRANSTYPE, 500u);
						if (flag7)
						{
						}
					}
					else
					{
						bool flag8 = ANT_heart_rate.channel[i].setChannelID(ANT_heart_rate.USER_DEVICENUM, false, ANT_heart_rate.USER_DEVICETYPE_speed, ANT_heart_rate.USER_TRANSTYPE, 500u);
						if (!flag8)
						{
							throw new Exception("Error configuring Channel ID");
						}
					}
					switch (i)
					{
					case 0:
						ANT_heart_rate.bsd0(i);
						break;
					case 1:
						ANT_heart_rate.bsd1(i);
						break;
					case 2:
						ANT_heart_rate.bsd2(i);
						break;
					case 3:
						ANT_heart_rate.bsd3(i);
						break;
					case 4:
						ANT_heart_rate.bsd4(i);
						break;
					case 5:
						ANT_heart_rate.bsd5(i);
						break;
					case 6:
						ANT_heart_rate.bsd6(i);
						break;
					case 7:
						ANT_heart_rate.bsd7(i);
						break;
					}
				}
			}
		}

		private static void bsd0(int i)
		{
			ANT_heart_rate.bikeSpeedDisplay0 = new BikeSpeedDisplay(ANT_heart_rate.channel0, ANT_heart_rate.networkAntPlus);
			ANT_heart_rate.bikeSpeedDisplay0.BikeSpeedDataReceived += new Action<BikeSpeedData, uint>(ANT_heart_rate.BikeSpeedDisplay_BikeSpeedDataReceived0);
			ANT_heart_rate.bikeSpeedDisplay0.TurnOn();
		}

		private static void BikeSpeedDisplay_BikeSpeedDataReceived0(BikeSpeedData arg3, uint arg4)
		{
			ANT_Response respChID = ANT_heart_rate.device0.requestMessageAndResponse(0, ANT_ReferenceLibrary.RequestMessageID.CHANNEL_ID_0x51, 500u);
			ANT_heart_rate.DN0 = ((ushort)(((int)respChID.messageContents[2] << 8) + (int)respChID.messageContents[1])).ToString();
			bool flag = GlobalData.Size != null;
			double size;
			if (flag)
			{
				size = Convert.ToDouble(GlobalData.Size) / 100.0;
			}
			else
			{
				size = 2.1;
			}
			ANT_heart_rate.BS0 = (arg3.Speed / 2.1 * size).ToString();
			bool flag2 = ANT_heart_rate.bsTemp0 == (int)ANT_heart_rate.bikeSpeedDisplay0.TotalWheelEventCount && ANT_heart_rate.bsTemp0 != 0;
			if (flag2)
			{
				ANT_heart_rate.bs_count0++;
			}
			else
			{
				ANT_heart_rate.bs_count0 = 0;
			}
			bool flag3 = ANT_heart_rate.bs_count0 > 11;
			if (flag3)
			{
				ANT_heart_rate.BS0 = "0";
			}
			ANT_heart_rate.bsTemp0 = (int)ANT_heart_rate.bikeSpeedDisplay0.TotalWheelEventCount;
		}

		private static void bsd1(int i)
		{
			ANT_heart_rate.bikeSpeedDisplay[i] = new BikeSpeedDisplay(ANT_heart_rate.channel[i], ANT_heart_rate.networkAntPlus);
			ANT_heart_rate.bikeSpeedDisplay[i].BikeSpeedDataReceived += new Action<BikeSpeedData, uint>(ANT_heart_rate.BikeSpeedDisplay_BikeSpeedDataReceived1);
			ANT_heart_rate.bikeSpeedDisplay[i].TurnOn();
		}

		private static void BikeSpeedDisplay_BikeSpeedDataReceived1(BikeSpeedData arg3, uint arg4)
		{
			ANT_Response respChID = ANT_heart_rate.device0.requestMessageAndResponse(1, ANT_ReferenceLibrary.RequestMessageID.CHANNEL_ID_0x51, 500u);
			ANT_heart_rate.DN1 = ((ushort)(((int)respChID.messageContents[2] << 8) + (int)respChID.messageContents[1])).ToString();
			bool flag = GlobalData.Size != null;
			double size;
			if (flag)
			{
				size = Convert.ToDouble(GlobalData.Size) / 100.0;
			}
			else
			{
				size = 2.1;
			}
			ANT_heart_rate.BS1 = (arg3.Speed / 2.1 * size).ToString();
			bool flag2 = ANT_heart_rate.bsTemp1 == (int)ANT_heart_rate.bikeSpeedDisplay[1].TotalWheelEventCount && ANT_heart_rate.bsTemp1 != 0;
			if (flag2)
			{
				ANT_heart_rate.bs_count1++;
			}
			else
			{
				ANT_heart_rate.bs_count1 = 0;
			}
			bool flag3 = ANT_heart_rate.bs_count1 > 11;
			if (flag3)
			{
				ANT_heart_rate.BS1 = "0";
			}
			ANT_heart_rate.bsTemp1 = (int)ANT_heart_rate.bikeSpeedDisplay[1].TotalWheelEventCount;
		}

		private static void bsd2(int i)
		{
			ANT_heart_rate.bikeSpeedDisplay[i] = new BikeSpeedDisplay(ANT_heart_rate.channel[i], ANT_heart_rate.networkAntPlus);
			ANT_heart_rate.bikeSpeedDisplay[i].BikeSpeedDataReceived += new Action<BikeSpeedData, uint>(ANT_heart_rate.BikeSpeedDisplay_BikeSpeedDataReceived2);
			ANT_heart_rate.bikeSpeedDisplay[i].TurnOn();
		}

		private static void BikeSpeedDisplay_BikeSpeedDataReceived2(BikeSpeedData arg3, uint arg4)
		{
			ANT_Response respChID = ANT_heart_rate.device0.requestMessageAndResponse(2, ANT_ReferenceLibrary.RequestMessageID.CHANNEL_ID_0x51, 500u);
			ANT_heart_rate.DN2 = ((ushort)(((int)respChID.messageContents[2] << 8) + (int)respChID.messageContents[1])).ToString();
			bool flag = GlobalData.Size != null;
			double size;
			if (flag)
			{
				size = Convert.ToDouble(GlobalData.Size) / 100.0;
			}
			else
			{
				size = 2.1;
			}
			ANT_heart_rate.BS2 = (arg3.Speed / 2.1 * size).ToString();
			bool flag2 = ANT_heart_rate.bsTemp2 == (int)ANT_heart_rate.bikeSpeedDisplay[2].TotalWheelEventCount && ANT_heart_rate.bsTemp2 != 0;
			if (flag2)
			{
				ANT_heart_rate.bs_count2++;
			}
			else
			{
				ANT_heart_rate.bs_count2 = 0;
			}
			bool flag3 = ANT_heart_rate.bs_count2 > 11;
			if (flag3)
			{
				ANT_heart_rate.BS2 = "0";
			}
			ANT_heart_rate.bsTemp2 = (int)ANT_heart_rate.bikeSpeedDisplay[2].TotalWheelEventCount;
		}

		private static void bsd3(int i)
		{
			ANT_heart_rate.bikeSpeedDisplay[i] = new BikeSpeedDisplay(ANT_heart_rate.channel[i], ANT_heart_rate.networkAntPlus);
			ANT_heart_rate.bikeSpeedDisplay[i].BikeSpeedDataReceived += new Action<BikeSpeedData, uint>(ANT_heart_rate.BikeSpeedDisplay_BikeSpeedDataReceived3);
			ANT_heart_rate.bikeSpeedDisplay[i].TurnOn();
		}

		private static void BikeSpeedDisplay_BikeSpeedDataReceived3(BikeSpeedData arg3, uint arg4)
		{
			ANT_Response respChID = ANT_heart_rate.device0.requestMessageAndResponse(3, ANT_ReferenceLibrary.RequestMessageID.CHANNEL_ID_0x51, 500u);
			ANT_heart_rate.DN3 = ((ushort)(((int)respChID.messageContents[2] << 8) + (int)respChID.messageContents[1])).ToString();
			bool flag = GlobalData.Size != null;
			double size;
			if (flag)
			{
				size = Convert.ToDouble(GlobalData.Size) / 100.0;
			}
			else
			{
				size = 2.1;
			}
			ANT_heart_rate.BS3 = (arg3.Speed / 2.1 * size).ToString();
			bool flag2 = ANT_heart_rate.bsTemp3 == (int)ANT_heart_rate.bikeSpeedDisplay[3].TotalWheelEventCount && ANT_heart_rate.bsTemp3 != 0;
			if (flag2)
			{
				ANT_heart_rate.bs_count3++;
			}
			else
			{
				ANT_heart_rate.bs_count3 = 0;
			}
			bool flag3 = ANT_heart_rate.bs_count3 > 11;
			if (flag3)
			{
				ANT_heart_rate.BS3 = "0";
			}
			ANT_heart_rate.bsTemp3 = (int)ANT_heart_rate.bikeSpeedDisplay[3].TotalWheelEventCount;
		}

		private static void bsd4(int i)
		{
			ANT_heart_rate.bikeSpeedDisplay[i] = new BikeSpeedDisplay(ANT_heart_rate.channel[i], ANT_heart_rate.networkAntPlus);
			ANT_heart_rate.bikeSpeedDisplay[i].BikeSpeedDataReceived += new Action<BikeSpeedData, uint>(ANT_heart_rate.BikeSpeedDisplay_BikeSpeedDataReceived4);
			ANT_heart_rate.bikeSpeedDisplay[i].TurnOn();
		}

		private static void BikeSpeedDisplay_BikeSpeedDataReceived4(BikeSpeedData arg3, uint arg4)
		{
			ANT_Response respChID = ANT_heart_rate.device0.requestMessageAndResponse(4, ANT_ReferenceLibrary.RequestMessageID.CHANNEL_ID_0x51, 500u);
			ANT_heart_rate.DN4 = ((ushort)(((int)respChID.messageContents[2] << 8) + (int)respChID.messageContents[1])).ToString();
			bool flag = GlobalData.Size != null;
			double size;
			if (flag)
			{
				size = Convert.ToDouble(GlobalData.Size) / 100.0;
			}
			else
			{
				size = 2.1;
			}
			ANT_heart_rate.BS4 = (arg3.Speed / 2.1 * size).ToString();
			bool flag2 = ANT_heart_rate.bsTemp4 == (int)ANT_heart_rate.bikeSpeedDisplay[4].TotalWheelEventCount && ANT_heart_rate.bsTemp4 != 0;
			if (flag2)
			{
				ANT_heart_rate.bs_count4++;
			}
			else
			{
				ANT_heart_rate.bs_count4 = 0;
			}
			bool flag3 = ANT_heart_rate.bs_count4 > 11;
			if (flag3)
			{
				ANT_heart_rate.BS4 = "0";
			}
			ANT_heart_rate.bsTemp4 = (int)ANT_heart_rate.bikeSpeedDisplay[4].TotalWheelEventCount;
		}

		private static void bsd5(int i)
		{
			ANT_heart_rate.bikeSpeedDisplay[i] = new BikeSpeedDisplay(ANT_heart_rate.channel[i], ANT_heart_rate.networkAntPlus);
			ANT_heart_rate.bikeSpeedDisplay[i].BikeSpeedDataReceived += new Action<BikeSpeedData, uint>(ANT_heart_rate.BikeSpeedDisplay_BikeSpeedDataReceived5);
			ANT_heart_rate.bikeSpeedDisplay[i].TurnOn();
		}

		private static void BikeSpeedDisplay_BikeSpeedDataReceived5(BikeSpeedData arg3, uint arg4)
		{
			ANT_Response respChID = ANT_heart_rate.device0.requestMessageAndResponse(5, ANT_ReferenceLibrary.RequestMessageID.CHANNEL_ID_0x51, 500u);
			ANT_heart_rate.DN5 = ((ushort)(((int)respChID.messageContents[2] << 8) + (int)respChID.messageContents[1])).ToString();
			bool flag = GlobalData.Size != null;
			double size;
			if (flag)
			{
				size = Convert.ToDouble(GlobalData.Size) / 100.0;
			}
			else
			{
				size = 2.1;
			}
			ANT_heart_rate.BS5 = (arg3.Speed / 2.1 * size).ToString();
			bool flag2 = ANT_heart_rate.bsTemp5 == (int)ANT_heart_rate.bikeSpeedDisplay[5].TotalWheelEventCount && ANT_heart_rate.bsTemp5 != 0;
			if (flag2)
			{
				ANT_heart_rate.bs_count5++;
			}
			else
			{
				ANT_heart_rate.bs_count5 = 0;
			}
			bool flag3 = ANT_heart_rate.bs_count5 > 11;
			if (flag3)
			{
				ANT_heart_rate.BS5 = "0";
			}
			ANT_heart_rate.bsTemp5 = (int)ANT_heart_rate.bikeSpeedDisplay[5].TotalWheelEventCount;
		}

		private static void bsd6(int i)
		{
			ANT_heart_rate.bikeSpeedDisplay[i] = new BikeSpeedDisplay(ANT_heart_rate.channel[i], ANT_heart_rate.networkAntPlus);
			ANT_heart_rate.bikeSpeedDisplay[i].BikeSpeedDataReceived += new Action<BikeSpeedData, uint>(ANT_heart_rate.BikeSpeedDisplay_BikeSpeedDataReceived6);
			ANT_heart_rate.bikeSpeedDisplay[i].TurnOn();
		}

		private static void BikeSpeedDisplay_BikeSpeedDataReceived6(BikeSpeedData arg3, uint arg4)
		{
			ANT_Response respChID = ANT_heart_rate.device0.requestMessageAndResponse(6, ANT_ReferenceLibrary.RequestMessageID.CHANNEL_ID_0x51, 500u);
			ANT_heart_rate.DN6 = ((ushort)(((int)respChID.messageContents[2] << 8) + (int)respChID.messageContents[1])).ToString();
			bool flag = GlobalData.Size != null;
			double size;
			if (flag)
			{
				size = Convert.ToDouble(GlobalData.Size) / 100.0;
			}
			else
			{
				size = 2.1;
			}
			ANT_heart_rate.BS6 = (arg3.Speed / 2.1 * size).ToString();
			bool flag2 = ANT_heart_rate.bsTemp6 == (int)ANT_heart_rate.bikeSpeedDisplay[6].TotalWheelEventCount && ANT_heart_rate.bsTemp6 != 0;
			if (flag2)
			{
				ANT_heart_rate.bs_count6++;
			}
			else
			{
				ANT_heart_rate.bs_count6 = 0;
			}
			bool flag3 = ANT_heart_rate.bs_count6 > 11;
			if (flag3)
			{
				ANT_heart_rate.BS6 = "0";
			}
			ANT_heart_rate.bsTemp6 = (int)ANT_heart_rate.bikeSpeedDisplay[6].TotalWheelEventCount;
		}

		private static void bsd7(int i)
		{
			ANT_heart_rate.bikeSpeedDisplay[i] = new BikeSpeedDisplay(ANT_heart_rate.channel[i], ANT_heart_rate.networkAntPlus);
			ANT_heart_rate.bikeSpeedDisplay[i].BikeSpeedDataReceived += new Action<BikeSpeedData, uint>(ANT_heart_rate.BikeSpeedDisplay_BikeSpeedDataReceived7);
			ANT_heart_rate.bikeSpeedDisplay[i].TurnOn();
		}

		private static void BikeSpeedDisplay_BikeSpeedDataReceived7(BikeSpeedData arg3, uint arg4)
		{
			ANT_Response respChID = ANT_heart_rate.device0.requestMessageAndResponse(7, ANT_ReferenceLibrary.RequestMessageID.CHANNEL_ID_0x51, 500u);
			ANT_heart_rate.DN7 = ((ushort)(((int)respChID.messageContents[2] << 8) + (int)respChID.messageContents[1])).ToString();
			bool flag = GlobalData.Size != null;
			double size;
			if (flag)
			{
				size = Convert.ToDouble(GlobalData.Size) / 100.0;
			}
			else
			{
				size = 2.1;
			}
			ANT_heart_rate.BS7 = (arg3.Speed / 2.1 * size).ToString();
			bool flag2 = ANT_heart_rate.bsTemp7 == (int)ANT_heart_rate.bikeSpeedDisplay[7].TotalWheelEventCount && ANT_heart_rate.bsTemp7 != 0;
			if (flag2)
			{
				ANT_heart_rate.bs_count7++;
			}
			else
			{
				ANT_heart_rate.bs_count7 = 0;
			}
			bool flag3 = ANT_heart_rate.bs_count7 > 11;
			if (flag3)
			{
				ANT_heart_rate.BS7 = "0";
			}
			ANT_heart_rate.bsTemp7 = (int)ANT_heart_rate.bikeSpeedDisplay[7].TotalWheelEventCount;
		}

		public static void hrChannelResponse(ANT_Response response)
		{
			try
			{
				ANT_ReferenceLibrary.ANTMessageID responseID = (ANT_ReferenceLibrary.ANTMessageID)response.responseID;
				switch (responseID)
				{
				case ANT_ReferenceLibrary.ANTMessageID.BROADCAST_DATA_0x4E:
				case ANT_ReferenceLibrary.ANTMessageID.ACKNOWLEDGED_DATA_0x4F:
				case ANT_ReferenceLibrary.ANTMessageID.BURST_DATA_0x50:
					break;
				default:
					switch (responseID)
					{
					case ANT_ReferenceLibrary.ANTMessageID.EXT_BROADCAST_DATA_0x5D:
					case ANT_ReferenceLibrary.ANTMessageID.EXT_ACKNOWLEDGED_DATA_0x5E:
					case ANT_ReferenceLibrary.ANTMessageID.EXT_BURST_DATA_0x5F:
						break;
					default:
						goto IL_52;
					}
					break;
				}
				byte hr = response.messageContents[8];
				ANT_heart_rate.HR2 = hr.ToString();
				IL_52:;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Channel response processing failed with exception: " + ex.Message);
			}
		}

		public static void bcChannelResponse(ANT_Response response)
		{
			try
			{
				ANT_ReferenceLibrary.ANTMessageID responseID = (ANT_ReferenceLibrary.ANTMessageID)response.responseID;
				switch (responseID)
				{
				case ANT_ReferenceLibrary.ANTMessageID.BROADCAST_DATA_0x4E:
				case ANT_ReferenceLibrary.ANTMessageID.ACKNOWLEDGED_DATA_0x4F:
				case ANT_ReferenceLibrary.ANTMessageID.BURST_DATA_0x50:
					break;
				default:
					switch (responseID)
					{
					case ANT_ReferenceLibrary.ANTMessageID.EXT_BROADCAST_DATA_0x5D:
					case ANT_ReferenceLibrary.ANTMessageID.EXT_ACKNOWLEDGED_DATA_0x5E:
					case ANT_ReferenceLibrary.ANTMessageID.EXT_BURST_DATA_0x5F:
						break;
					default:
						goto IL_1ED;
					}
					break;
				}
				ANT_heart_rate.CandanceTime = (double)(((int)response.messageContents[6] << 8) + (int)response.messageContents[5]);
				ANT_heart_rate.CandanceCount = (double)(((int)response.messageContents[8] << 8) + (int)response.messageContents[7]);
				bool flag = ANT_heart_rate.lastCandanceTime == 0.0 || ANT_heart_rate.lastCandanceCount == 0.0;
				if (flag)
				{
					ANT_heart_rate.lastCandanceCount = ANT_heart_rate.CandanceCount;
					ANT_heart_rate.lastCandanceTime = ANT_heart_rate.CandanceTime;
				}
				else
				{
					bool flag2 = ANT_heart_rate.CandanceCount == ANT_heart_rate.lastCandanceCount;
					if (flag2)
					{
						ANT_heart_rate.bcCount++;
					}
					else
					{
						ANT_heart_rate.bcCount = 0;
					}
					bool flag3 = ANT_heart_rate.CandanceTime == ANT_heart_rate.lastCandanceTime || ANT_heart_rate.CandanceCount == ANT_heart_rate.lastCandanceCount;
					if (flag3)
					{
						ANT_heart_rate.BC1 = ANT_heart_rate.temp;
						ANT_heart_rate.lastCandanceTime = ANT_heart_rate.CandanceTime;
						ANT_heart_rate.lastCandanceCount = ANT_heart_rate.CandanceCount;
					}
					else
					{
						bool flag4 = ANT_heart_rate.CandanceCount - ANT_heart_rate.lastCandanceCount < 0.0 || ANT_heart_rate.CandanceTime - ANT_heart_rate.lastCandanceTime < 0.0;
						if (flag4)
						{
							ANT_heart_rate.BC1 = ANT_heart_rate.temp;
							ANT_heart_rate.lastCandanceTime = ANT_heart_rate.CandanceTime;
							ANT_heart_rate.lastCandanceCount = ANT_heart_rate.CandanceCount;
						}
						else
						{
							ANT_heart_rate.BC1 = (61440.0 * (ANT_heart_rate.CandanceCount - ANT_heart_rate.lastCandanceCount) / (ANT_heart_rate.CandanceTime - ANT_heart_rate.lastCandanceTime)).ToString();
							ANT_heart_rate.lastCandanceTime = ANT_heart_rate.CandanceTime;
							ANT_heart_rate.lastCandanceCount = ANT_heart_rate.CandanceCount;
							ANT_heart_rate.temp = ANT_heart_rate.BC1;
						}
					}
					bool flag5 = ANT_heart_rate.bcCount >= 6;
					if (flag5)
					{
						ANT_heart_rate.BC1 = "0";
						ANT_heart_rate.temp = ANT_heart_rate.BC1;
					}
				}
				IL_1ED:;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Channel response processing failed with exception: " + ex.Message);
			}
		}

		public static void bsChannelResponse(ANT_Response response)
		{
			try
			{
				ANT_ReferenceLibrary.ANTMessageID responseID = (ANT_ReferenceLibrary.ANTMessageID)response.responseID;
				switch (responseID)
				{
				case ANT_ReferenceLibrary.ANTMessageID.BROADCAST_DATA_0x4E:
				case ANT_ReferenceLibrary.ANTMessageID.ACKNOWLEDGED_DATA_0x4F:
				case ANT_ReferenceLibrary.ANTMessageID.BURST_DATA_0x50:
					break;
				default:
					switch (responseID)
					{
					case ANT_ReferenceLibrary.ANTMessageID.EXT_BROADCAST_DATA_0x5D:
					case ANT_ReferenceLibrary.ANTMessageID.EXT_ACKNOWLEDGED_DATA_0x5E:
					case ANT_ReferenceLibrary.ANTMessageID.EXT_BURST_DATA_0x5F:
						break;
					default:
						goto IL_246;
					}
					break;
				}
				ANT_heart_rate.SpeedTime = (double)(((int)response.messageContents[6] << 8) + (int)response.messageContents[5]);
				ANT_heart_rate.SpeedCount = (double)(((int)response.messageContents[8] << 8) + (int)response.messageContents[7]);
				bool flag = ANT_heart_rate.lastSpeedTime == 0.0 || ANT_heart_rate.lastSpeedCount == 0.0;
				if (flag)
				{
					ANT_heart_rate.lastSpeedCount = ANT_heart_rate.SpeedCount;
					ANT_heart_rate.lastSpeedTime = ANT_heart_rate.SpeedTime;
				}
				else
				{
					bool flag2 = ANT_heart_rate.SpeedCount == ANT_heart_rate.lastSpeedCount;
					if (flag2)
					{
						ANT_heart_rate.bsCount++;
					}
					else
					{
						ANT_heart_rate.bsCount = 0;
					}
					bool flag3 = ANT_heart_rate.SpeedTime == ANT_heart_rate.lastSpeedTime || ANT_heart_rate.SpeedCount == ANT_heart_rate.lastSpeedCount;
					if (flag3)
					{
						ANT_heart_rate.BS0 = ANT_heart_rate.Temp;
						ANT_heart_rate.lastSpeedTime = ANT_heart_rate.SpeedTime;
						ANT_heart_rate.lastSpeedCount = ANT_heart_rate.SpeedCount;
					}
					else
					{
						bool flag4 = ANT_heart_rate.SpeedCount - ANT_heart_rate.lastSpeedCount < 0.0 || ANT_heart_rate.SpeedTime - ANT_heart_rate.lastSpeedTime < 0.0;
						if (flag4)
						{
							ANT_heart_rate.BS0 = ANT_heart_rate.Temp;
							ANT_heart_rate.lastSpeedTime = ANT_heart_rate.SpeedTime;
							ANT_heart_rate.lastSpeedCount = ANT_heart_rate.SpeedCount;
						}
						else
						{
							ANT_heart_rate.BS0 = (7741.4400000000005 * (ANT_heart_rate.SpeedCount - ANT_heart_rate.lastSpeedCount) / (ANT_heart_rate.SpeedTime - ANT_heart_rate.lastSpeedTime)).ToString();
							bool flag5 = GlobalData.Size != null;
							double size;
							if (flag5)
							{
								size = Convert.ToDouble(GlobalData.Size) / 100.0;
							}
							else
							{
								size = 2.1;
							}
							ANT_heart_rate.BS0 = (Convert.ToDouble(ANT_heart_rate.BS0) / 2.1 * size).ToString();
							ANT_heart_rate.lastSpeedTime = ANT_heart_rate.SpeedTime;
							ANT_heart_rate.lastSpeedCount = ANT_heart_rate.SpeedCount;
							ANT_heart_rate.Temp = ANT_heart_rate.BS0;
						}
					}
					bool flag6 = ANT_heart_rate.bsCount >= 6;
					if (flag6)
					{
						ANT_heart_rate.BS0 = "0";
						ANT_heart_rate.Temp = ANT_heart_rate.BS0;
					}
				}
				IL_246:;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Channel response processing failed with exception: " + ex.Message);
			}
		}

		public static void reset()
		{
			bool flag = ANT_heart_rate.device0 == null;
			if (flag)
			{
				ANT_heart_rate.device0 = new ANT_Device();
			}
			ANT_heart_rate.device0.ResetSystem();
		}

		public static void antBinding(string speedDeviceNumber, string candenceDeviceNumber, string HeartRateDeviceNumber)
		{
			bool flag = speedDeviceNumber != null;
			if (flag)
			{
				GlobalData.speedChannel = 0;
				try
				{
					bool flag2 = ANT_heart_rate.device0 == null;
					if (flag2)
					{
						ANT_heart_rate.device0 = new ANT_Device();
					}
					ANT_heart_rate.channel0 = ANT_heart_rate.device0.getChannel(0);
					ANT_heart_rate.channel0.channelResponse += new dChannelResponseHandler(ANT_heart_rate.bsChannelResponse);
				}
				catch (Exception ex)
				{
					bool flag3 = ANT_heart_rate.device0 == null;
					if (flag3)
					{
						MessageBox.Show(MultiLanguage.Warn32);
						GlobalData.ANT_Deviceflag = false;
						GlobalData.speedChannel = 9;
						return;
					}
					throw new Exception("Error connecting to ANT: " + ex.Message);
				}
				bool flag4 = ANT_heart_rate.device0.setNetworkKey(ANT_heart_rate.USER_NETWORK_NUM, ANT_heart_rate.USER_NETWORK_KEY, 500u);
				if (!flag4)
				{
					throw new Exception("Error configuring network key");
				}
				bool flag5 = ANT_heart_rate.channel0.assignChannel(ANT_ReferenceLibrary.ChannelType.BASE_Slave_Receive_0x00, ANT_heart_rate.USER_NETWORK_NUM, 500u);
				if (flag5)
				{
				}
				bool flag6 = ANT_heart_rate.channel0.setChannelFreq(ANT_heart_rate.USER_RADIOFREQ, 500u);
				if (flag6)
				{
				}
				bool flag7 = ANT_heart_rate.channel0.setChannelPeriod(8118, 500u);
				if (flag7)
				{
				}
				bool flag8 = ANT_heart_rate.channel0.setChannelID((ushort)Convert.ToInt32(speedDeviceNumber), false, ANT_heart_rate.USER_DEVICETYPE_speed, ANT_heart_rate.USER_TRANSTYPE, 500u);
				if (flag8)
				{
				}
				ANT_heart_rate.channel0.setChannelSearchTimeout(255);
				bool flag9 = ANT_heart_rate.channel0.openChannel(500u);
				if (flag9)
				{
				}
			}
			bool flag10 = candenceDeviceNumber != null;
			if (flag10)
			{
				GlobalData.cadenceChannel = 1;
				try
				{
					bool flag11 = ANT_heart_rate.device0 == null;
					if (flag11)
					{
						ANT_heart_rate.device0 = new ANT_Device();
					}
					ANT_heart_rate.channel1 = ANT_heart_rate.device0.getChannel(1);
					ANT_heart_rate.channel1.channelResponse += new dChannelResponseHandler(ANT_heart_rate.bcChannelResponse);
				}
				catch (Exception ex2)
				{
					bool flag12 = ANT_heart_rate.device0 == null;
					if (flag12)
					{
						MessageBox.Show(MultiLanguage.Warn32);
						GlobalData.ANT_Deviceflag = false;
						GlobalData.cadenceChannel = 9;
						return;
					}
					throw new Exception("Error connecting to ANT: " + ex2.Message);
				}
				bool flag13 = ANT_heart_rate.device0.setNetworkKey(ANT_heart_rate.USER_NETWORK_NUM, ANT_heart_rate.USER_NETWORK_KEY, 500u);
				if (!flag13)
				{
					throw new Exception("Error configuring network key");
				}
				bool flag14 = ANT_heart_rate.channel1.assignChannel(ANT_ReferenceLibrary.ChannelType.BASE_Slave_Receive_0x00, ANT_heart_rate.USER_NETWORK_NUM, 500u);
				if (flag14)
				{
				}
				bool flag15 = ANT_heart_rate.channel1.setChannelFreq(ANT_heart_rate.USER_RADIOFREQ, 500u);
				if (flag15)
				{
				}
				bool flag16 = ANT_heart_rate.channel1.setChannelPeriod(8102, 500u);
				if (flag16)
				{
				}
				bool flag17 = ANT_heart_rate.channel1.setChannelID((ushort)Convert.ToInt32(candenceDeviceNumber), false, ANT_heart_rate.USER_DEVICETYPE_tramp, ANT_heart_rate.USER_TRANSTYPE, 500u);
				if (flag17)
				{
				}
				ANT_heart_rate.channel1.setChannelSearchTimeout(255);
				bool flag18 = ANT_heart_rate.channel1.openChannel(500u);
				if (flag18)
				{
				}
			}
			bool flag19 = HeartRateDeviceNumber != null;
			if (flag19)
			{
				GlobalData.heartChannel = 2;
				try
				{
					bool flag20 = ANT_heart_rate.device0 == null;
					if (flag20)
					{
						ANT_heart_rate.device0 = new ANT_Device();
					}
					ANT_heart_rate.channel2 = ANT_heart_rate.device0.getChannel(2);
					ANT_heart_rate.channel2.channelResponse += new dChannelResponseHandler(ANT_heart_rate.hrChannelResponse);
				}
				catch (Exception ex3)
				{
					bool flag21 = ANT_heart_rate.device0 == null;
					if (flag21)
					{
						MessageBox.Show(MultiLanguage.Warn32);
						GlobalData.ANT_Deviceflag = false;
						GlobalData.heartChannel = 9;
						return;
					}
					throw new Exception("Error connecting to ANT: " + ex3.Message);
				}
				bool flag22 = ANT_heart_rate.device0.setNetworkKey(ANT_heart_rate.USER_NETWORK_NUM, ANT_heart_rate.USER_NETWORK_KEY, 500u);
				if (flag22)
				{
				}
				bool flag23 = ANT_heart_rate.channel2.assignChannel(ANT_ReferenceLibrary.ChannelType.BASE_Slave_Receive_0x00, ANT_heart_rate.USER_NETWORK_NUM, 500u);
				if (flag23)
				{
				}
				bool flag24 = ANT_heart_rate.channel2.setChannelID((ushort)Convert.ToInt32(HeartRateDeviceNumber), false, ANT_heart_rate.USER_DEVICETYPE, ANT_heart_rate.USER_TRANSTYPE, 500u);
				if (flag24)
				{
				}
				bool flag25 = ANT_heart_rate.channel2.setChannelFreq(ANT_heart_rate.USER_RADIOFREQ, 500u);
				if (flag25)
				{
				}
				bool flag26 = ANT_heart_rate.channel2.setChannelPeriod(8070, 500u);
				if (flag26)
				{
				}
				ANT_heart_rate.channel2.setChannelSearchTimeout(255);
				bool flag27 = ANT_heart_rate.channel2.openChannel(500u);
				if (flag27)
				{
				}
			}
		}
	}
}
