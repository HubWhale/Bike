using ANT_Managed_Library;
using AntPlus.Profiles.BikeCadence;
using AntPlus.Profiles.HeartRate;
using AntPlus.Types;
using System;
using System.Threading;
using System.Windows;

namespace Bike
{
	public class ANT
	{
		private static readonly byte USER_ANT_CHANNEL = 0;

		public static ushort USER_DEVICENUM = 1;

		private static readonly byte USER_DEVICETYPE = 120;

		private static readonly byte USER_TRANSTYPE = 0;

		private static readonly byte USER_RADIOFREQ = 57;

		private static readonly ushort USER_CHANNELPERIOD = 8070;

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

		private static ANT_Device device0;

		private static ANT_Channel channel0;

		private static ANT_Channel channel1;

		private static ANT_ReferenceLibrary.ChannelType channelType;

		private static byte[] txBuffer = new byte[8];

		private static bool bDone;

		private static bool bDisplay;

		private static bool bBroadcasting;

		private static int iIndex = 0;

		public static string HeartRate = null;

		public static string Tramp = null;

		private static readonly byte USER_ANT_CHANNEL_tramp = 1;

		public static ushort USER_DEVICENUM_tramp = 1;

		private static readonly byte USER_DEVICETYPE_tramp = 122;

		private static readonly ushort USER_CHANNELPERIOD_tramp = 8102;

		public static int time_Last = 0;

		public static int time_Now = 0;

		public static int count_Last = 0;

		public static int count_Now = 0;

		public static int tramp;

		private static HeartRateDisplay heartRateDisplay;

		private static BikeCadenceDisplay bikeCadenceDisplay;

		private static Network networkAntPlus = new Network(ANT.USER_NETWORK_NUM, ANT.USER_NETWORK_KEY, ANT.USER_RADIOFREQ);

		public static void Init()
		{
			try
			{
				bool flag = ANT.device0 == null;
				if (flag)
				{
					ANT.device0 = new ANT_Device();
				}
				ANT.channel0 = ANT.device0.getChannel((int)ANT.USER_ANT_CHANNEL);
				ANT.channel0.channelResponse += new dChannelResponseHandler(ANT.ChannelResponse);
			}
			catch (Exception ex)
			{
				bool flag2 = ANT.device0 == null;
				if (flag2)
				{
					throw new Exception("Could not connect to any device.\nDetails: \n   " + ex.Message);
				}
				throw new Exception("Error connecting to ANT: " + ex.Message);
			}
		}

		public static void Tramp_Init()
		{
			try
			{
				bool flag = ANT.device0 == null;
				if (flag)
				{
					ANT.device0 = new ANT_Device();
				}
				ANT.channel1 = ANT.device0.getChannel((int)ANT.USER_ANT_CHANNEL_tramp);
				ANT.channel1.channelResponse += new dChannelResponseHandler(ANT.Tramp_ChannelResponse);
			}
			catch (Exception ex)
			{
				bool flag2 = ANT.device0 == null;
				if (flag2)
				{
					throw new Exception("Could not connect to any device.\nDetails: \n   " + ex.Message);
				}
				throw new Exception("Error connecting to ANT: " + ex.Message);
			}
		}

		public static void Start()
		{
			ANT.bDone = false;
			ANT.bDisplay = true;
			ANT.bBroadcasting = false;
			ANT.channelType = ANT_ReferenceLibrary.ChannelType.BASE_Slave_Receive_0x00;
			try
			{
				ANT.ConfigureANT();
			}
			catch (Exception ex)
			{
				throw new Exception("Demo failed: " + ex.Message + Environment.NewLine);
			}
		}

		private static void ConfigureANT()
		{
			Thread.Sleep(500);
			bool flag = ANT.device0.setNetworkKey(ANT.USER_NETWORK_NUM, ANT.USER_NETWORK_KEY, 500u);
			if (!flag)
			{
				throw new Exception("Error configuring network key");
			}
			bool flag2 = ANT.channel0.assignChannel(ANT.channelType, ANT.USER_NETWORK_NUM, 500u);
			if (!flag2)
			{
				throw new Exception("Error assigning channel");
			}
			bool flag3 = ANT.channel0.setChannelID(ANT.USER_DEVICENUM, false, ANT.USER_DEVICETYPE, ANT.USER_TRANSTYPE, 500u);
			if (!flag3)
			{
				throw new Exception("Error configuring Channel ID");
			}
			bool flag4 = ANT.channel0.setChannelFreq(ANT.USER_RADIOFREQ, 500u);
			if (!flag4)
			{
				throw new Exception("Error configuring Radio Frequency");
			}
			bool flag5 = ANT.channel0.setChannelPeriod(ANT.USER_CHANNELPERIOD, 500u);
			if (!flag5)
			{
				throw new Exception("Error configuring Channel Period");
			}
			ANT.bBroadcasting = true;
			bool flag6 = ANT.channel0.openChannel(500u);
			if (flag6)
			{
				ANT.heartRateDisplay = new HeartRateDisplay(ANT.channel0, ANT.networkAntPlus);
				ANT.heartRateDisplay.HeartRateDataReceived += new Action<HeartRateData, uint>(ANT.HeartRateDisplay_HeartRateDataReceived);
				ANT.heartRateDisplay.TurnOn();
				ANT.device0.enableRxExtendedMessages(true);
				return;
			}
			ANT.bBroadcasting = false;
			throw new Exception("Error opening channel");
		}

		private static void ChannelResponse(ANT_Response response)
		{
			try
			{
				ANT_ReferenceLibrary.ANTMessageID responseID = (ANT_ReferenceLibrary.ANTMessageID)response.responseID;
				if (responseID != ANT_ReferenceLibrary.ANTMessageID.RESPONSE_EVENT_0x40)
				{
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
							goto IL_226;
						}
						break;
					}
					bool flag = ANT.bDisplay;
					if (flag)
					{
						bool flag2 = response.isExtended();
						if (flag2)
						{
							ANT_ChannelID chID = response.getDeviceIDfromExt();
						}
						bool flag3 = response.responseID == 78 || response.responseID == 93;
						if (!flag3)
						{
							bool flag4 = response.responseID == 79 || response.responseID == 94;
							if (flag4)
							{
							}
						}
					}
					else
					{
						string[] expr_1F2 = new string[]
						{
							"|",
							"/",
							"_",
							"\\"
						};
						ANT.iIndex &= 3;
					}
				}
				else
				{
					switch (response.getChannelEventCode())
					{
					case ANT_ReferenceLibrary.ANTEventID.EVENT_RX_SEARCH_TIMEOUT_0x01:
						MessageBox.Show("Search Timeout");
						break;
					case ANT_ReferenceLibrary.ANTEventID.EVENT_TX_0x03:
					{
						byte[] expr_8A_cp_0 = ANT.txBuffer;
						int expr_8A_cp_1 = 0;
						expr_8A_cp_0[expr_8A_cp_1] += 1;
						bool flag5 = ANT.bBroadcasting;
						if (flag5)
						{
							ANT.channel0.sendBroadcastData(ANT.txBuffer);
							bool flag6 = ANT.bDisplay;
							if (flag6)
							{
							}
						}
						else
						{
							string[] ac = new string[]
							{
								"|",
								"/",
								"_",
								"\\"
							};
							Console.Write("Tx: " + ac[ANT.iIndex++] + "\r");
							ANT.iIndex &= 3;
						}
						break;
					}
					case ANT_ReferenceLibrary.ANTEventID.EVENT_TRANSFER_RX_FAILED_0x04:
						MessageBox.Show("Burst receive has failed");
						break;
					case ANT_ReferenceLibrary.ANTEventID.EVENT_TRANSFER_TX_COMPLETED_0x05:
						MessageBox.Show("Transfer Completed");
						break;
					case ANT_ReferenceLibrary.ANTEventID.EVENT_TRANSFER_TX_FAILED_0x06:
						MessageBox.Show("Transfer Failed");
						break;
					case ANT_ReferenceLibrary.ANTEventID.EVENT_CHANNEL_CLOSED_0x07:
					{
						bool flag7 = ANT.channel0.unassignChannel(500u);
						if (flag7)
						{
							ANT.bDone = true;
						}
						break;
					}
					}
				}
				IL_226:;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Channel response processing failed with exception: " + ex.Message);
			}
		}

		private static void DeviceResponse(ANT_Response response)
		{
			ANT_ReferenceLibrary.ANTMessageID responseID = (ANT_ReferenceLibrary.ANTMessageID)response.responseID;
			if (responseID == ANT_ReferenceLibrary.ANTMessageID.RESPONSE_EVENT_0x40)
			{
				ANT_ReferenceLibrary.ANTMessageID messageID = response.getMessageID();
				switch (messageID)
				{
				case ANT_ReferenceLibrary.ANTMessageID.UNASSIGN_CHANNEL_0x41:
				case ANT_ReferenceLibrary.ANTMessageID.ASSIGN_CHANNEL_0x42:
				case ANT_ReferenceLibrary.ANTMessageID.CHANNEL_MESG_PERIOD_0x43:
				case ANT_ReferenceLibrary.ANTMessageID.CHANNEL_RADIO_FREQ_0x45:
				case ANT_ReferenceLibrary.ANTMessageID.NETWORK_KEY_0x46:
				case ANT_ReferenceLibrary.ANTMessageID.OPEN_CHANNEL_0x4B:
				case ANT_ReferenceLibrary.ANTMessageID.CHANNEL_ID_0x51:
				{
					bool flag = response.getChannelEventCode() > ANT_ReferenceLibrary.ANTEventID.RESPONSE_NO_ERROR_0x00;
					if (flag)
					{
					}
					break;
				}
				case ANT_ReferenceLibrary.ANTMessageID.CHANNEL_SEARCH_TIMEOUT_0x44:
				case ANT_ReferenceLibrary.ANTMessageID.RADIO_TX_POWER_0x47:
				case ANT_ReferenceLibrary.ANTMessageID.RADIO_CW_MODE_0x48:
				case (ANT_ReferenceLibrary.ANTMessageID)73:
				case ANT_ReferenceLibrary.ANTMessageID.SYSTEM_RESET_0x4A:
				case ANT_ReferenceLibrary.ANTMessageID.BROADCAST_DATA_0x4E:
				case ANT_ReferenceLibrary.ANTMessageID.ACKNOWLEDGED_DATA_0x4F:
				case ANT_ReferenceLibrary.ANTMessageID.BURST_DATA_0x50:
					break;
				case ANT_ReferenceLibrary.ANTMessageID.CLOSE_CHANNEL_0x4C:
				{
					bool flag2 = response.getChannelEventCode() == ANT_ReferenceLibrary.ANTEventID.CHANNEL_IN_WRONG_STATE_0x15;
					if (flag2)
					{
						bool flag3 = ANT.channel0.unassignChannel(500u);
						if (flag3)
						{
							ANT.bDone = true;
						}
					}
					break;
				}
				case ANT_ReferenceLibrary.ANTMessageID.REQUEST_0x4D:
				{
					bool flag4 = response.getChannelEventCode() == ANT_ReferenceLibrary.ANTEventID.INVALID_MESSAGE_0x28;
					if (flag4)
					{
					}
					break;
				}
				default:
					if (messageID == ANT_ReferenceLibrary.ANTMessageID.RX_EXT_MESGS_ENABLE_0x66)
					{
						bool flag5 = response.getChannelEventCode() == ANT_ReferenceLibrary.ANTEventID.INVALID_MESSAGE_0x28;
						if (!flag5)
						{
							bool flag6 = response.getChannelEventCode() > ANT_ReferenceLibrary.ANTEventID.RESPONSE_NO_ERROR_0x00;
							if (flag6)
							{
							}
						}
					}
					break;
				}
			}
		}

		public static void Tramp_Start()
		{
			ANT.bDone = false;
			ANT.bDisplay = true;
			ANT.bBroadcasting = false;
			ANT.channelType = ANT_ReferenceLibrary.ChannelType.BASE_Slave_Receive_0x00;
			try
			{
				ANT.Tramp_ConfigureANT();
			}
			catch (Exception ex)
			{
				throw new Exception("Demo failed: " + ex.Message + Environment.NewLine);
			}
		}

		private static void Tramp_ConfigureANT()
		{
			Thread.Sleep(500);
			bool flag = ANT.device0.setNetworkKey(ANT.USER_NETWORK_NUM, ANT.USER_NETWORK_KEY, 500u);
			if (!flag)
			{
				throw new Exception("Error configuring network key");
			}
			bool flag2 = ANT.channel1.assignChannel(ANT.channelType, ANT.USER_NETWORK_NUM, 500u);
			if (!flag2)
			{
				throw new Exception("Error assigning channel");
			}
			bool flag3 = ANT.channel1.setChannelID(ANT.USER_DEVICENUM_tramp, false, ANT.USER_DEVICETYPE_tramp, ANT.USER_TRANSTYPE, 500u);
			if (!flag3)
			{
				throw new Exception("Error configuring Channel ID");
			}
			bool flag4 = ANT.channel1.setChannelFreq(ANT.USER_RADIOFREQ, 500u);
			if (!flag4)
			{
				throw new Exception("Error configuring Radio Frequency");
			}
			bool flag5 = ANT.channel1.setChannelPeriod(ANT.USER_CHANNELPERIOD_tramp, 500u);
			if (!flag5)
			{
				throw new Exception("Error configuring Channel Period");
			}
			ANT.bBroadcasting = true;
			bool flag6 = ANT.channel1.openChannel(500u);
			if (flag6)
			{
				ANT.bikeCadenceDisplay = new BikeCadenceDisplay(ANT.channel1, ANT.networkAntPlus);
				ANT.bikeCadenceDisplay.BikeCadenceDataReceived += new Action<BikeCadenceData, uint>(ANT.bikeCadenceDisplay_bikeCadenceDataReceived);
				ANT.bikeCadenceDisplay.TurnOn();
				ANT.device0.enableRxExtendedMessages(true);
				return;
			}
			ANT.bBroadcasting = false;
			throw new Exception("Error opening channel");
		}

		private static void bikeCadenceDisplay_bikeCadenceDataReceived(BikeCadenceData arg1, uint arg2)
		{
			ANT.Tramp = arg1.Cadence.ToString();
		}

		private static void HeartRateDisplay_HeartRateDataReceived(HeartRateData arg3, uint arg4)
		{
			ANT.HeartRate = arg3.HeartRate.ToString();
		}

		private static void Tramp_ChannelResponse(ANT_Response response)
		{
			try
			{
				ANT_ReferenceLibrary.ANTMessageID responseID = (ANT_ReferenceLibrary.ANTMessageID)response.responseID;
				if (responseID != ANT_ReferenceLibrary.ANTMessageID.RESPONSE_EVENT_0x40)
				{
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
							goto IL_226;
						}
						break;
					}
					bool flag = ANT.bDisplay;
					if (flag)
					{
						bool flag2 = response.isExtended();
						if (flag2)
						{
							ANT_ChannelID chID = response.getDeviceIDfromExt();
						}
						bool flag3 = response.responseID == 78 || response.responseID == 93;
						if (!flag3)
						{
							bool flag4 = response.responseID == 79 || response.responseID == 94;
							if (flag4)
							{
							}
						}
					}
					else
					{
						string[] expr_1F2 = new string[]
						{
							"|",
							"/",
							"_",
							"\\"
						};
						ANT.iIndex &= 3;
					}
				}
				else
				{
					switch (response.getChannelEventCode())
					{
					case ANT_ReferenceLibrary.ANTEventID.EVENT_RX_SEARCH_TIMEOUT_0x01:
						MessageBox.Show("Search Timeout");
						break;
					case ANT_ReferenceLibrary.ANTEventID.EVENT_TX_0x03:
					{
						byte[] expr_8A_cp_0 = ANT.txBuffer;
						int expr_8A_cp_1 = 0;
						expr_8A_cp_0[expr_8A_cp_1] += 1;
						bool flag5 = ANT.bBroadcasting;
						if (flag5)
						{
							ANT.channel1.sendBroadcastData(ANT.txBuffer);
							bool flag6 = ANT.bDisplay;
							if (flag6)
							{
							}
						}
						else
						{
							string[] ac = new string[]
							{
								"|",
								"/",
								"_",
								"\\"
							};
							Console.Write("Tx: " + ac[ANT.iIndex++] + "\r");
							ANT.iIndex &= 3;
						}
						break;
					}
					case ANT_ReferenceLibrary.ANTEventID.EVENT_TRANSFER_RX_FAILED_0x04:
						MessageBox.Show("Burst receive has failed");
						break;
					case ANT_ReferenceLibrary.ANTEventID.EVENT_TRANSFER_TX_COMPLETED_0x05:
						MessageBox.Show("Transfer Completed");
						break;
					case ANT_ReferenceLibrary.ANTEventID.EVENT_TRANSFER_TX_FAILED_0x06:
						MessageBox.Show("Transfer Failed");
						break;
					case ANT_ReferenceLibrary.ANTEventID.EVENT_CHANNEL_CLOSED_0x07:
					{
						bool flag7 = ANT.channel1.unassignChannel(500u);
						if (flag7)
						{
							ANT.bDone = true;
						}
						break;
					}
					}
				}
				IL_226:;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Channel response processing failed with exception: " + ex.Message);
			}
		}

		private static void Tramp_DeviceResponse(ANT_Response response)
		{
			ANT_ReferenceLibrary.ANTMessageID responseID = (ANT_ReferenceLibrary.ANTMessageID)response.responseID;
			if (responseID == ANT_ReferenceLibrary.ANTMessageID.RESPONSE_EVENT_0x40)
			{
				ANT_ReferenceLibrary.ANTMessageID messageID = response.getMessageID();
				switch (messageID)
				{
				case ANT_ReferenceLibrary.ANTMessageID.UNASSIGN_CHANNEL_0x41:
				case ANT_ReferenceLibrary.ANTMessageID.ASSIGN_CHANNEL_0x42:
				case ANT_ReferenceLibrary.ANTMessageID.CHANNEL_MESG_PERIOD_0x43:
				case ANT_ReferenceLibrary.ANTMessageID.CHANNEL_RADIO_FREQ_0x45:
				case ANT_ReferenceLibrary.ANTMessageID.NETWORK_KEY_0x46:
				case ANT_ReferenceLibrary.ANTMessageID.OPEN_CHANNEL_0x4B:
				case ANT_ReferenceLibrary.ANTMessageID.CHANNEL_ID_0x51:
				{
					bool flag = response.getChannelEventCode() > ANT_ReferenceLibrary.ANTEventID.RESPONSE_NO_ERROR_0x00;
					if (flag)
					{
					}
					break;
				}
				case ANT_ReferenceLibrary.ANTMessageID.CHANNEL_SEARCH_TIMEOUT_0x44:
				case ANT_ReferenceLibrary.ANTMessageID.RADIO_TX_POWER_0x47:
				case ANT_ReferenceLibrary.ANTMessageID.RADIO_CW_MODE_0x48:
				case (ANT_ReferenceLibrary.ANTMessageID)73:
				case ANT_ReferenceLibrary.ANTMessageID.SYSTEM_RESET_0x4A:
				case ANT_ReferenceLibrary.ANTMessageID.BROADCAST_DATA_0x4E:
				case ANT_ReferenceLibrary.ANTMessageID.ACKNOWLEDGED_DATA_0x4F:
				case ANT_ReferenceLibrary.ANTMessageID.BURST_DATA_0x50:
					break;
				case ANT_ReferenceLibrary.ANTMessageID.CLOSE_CHANNEL_0x4C:
				{
					bool flag2 = response.getChannelEventCode() == ANT_ReferenceLibrary.ANTEventID.CHANNEL_IN_WRONG_STATE_0x15;
					if (flag2)
					{
						bool flag3 = ANT.channel1.unassignChannel(500u);
						if (flag3)
						{
							ANT.bDone = true;
						}
					}
					break;
				}
				case ANT_ReferenceLibrary.ANTMessageID.REQUEST_0x4D:
				{
					bool flag4 = response.getChannelEventCode() == ANT_ReferenceLibrary.ANTEventID.INVALID_MESSAGE_0x28;
					if (flag4)
					{
					}
					break;
				}
				default:
					if (messageID == ANT_ReferenceLibrary.ANTMessageID.RX_EXT_MESGS_ENABLE_0x66)
					{
						bool flag5 = response.getChannelEventCode() == ANT_ReferenceLibrary.ANTEventID.INVALID_MESSAGE_0x28;
						if (!flag5)
						{
							bool flag6 = response.getChannelEventCode() > ANT_ReferenceLibrary.ANTEventID.RESPONSE_NO_ERROR_0x00;
							if (flag6)
							{
							}
						}
					}
					break;
				}
			}
		}
	}
}
