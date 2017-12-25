using ANT_Managed_Library;
using System;
using System.Threading;
using System.Windows;

namespace Bike
{
	public class ANT_TAPING
	{
		private static readonly byte USER_ANT_CHANNEL = 0;

		private static readonly ushort USER_DEVICENUM = 0;

		private static readonly byte USER_DEVICETYPE = 123;

		private static readonly byte USER_TRANSTYPE = 0;

		private static readonly byte USER_RADIOFREQ = 57;

		private static readonly ushort USER_CHANNELPERIOD = 8118;

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

		private static ANT_ReferenceLibrary.ChannelType channelType;

		private static byte[] txBuffer = new byte[8];

		private static bool bDone;

		private static bool bDisplay;

		private static bool bBroadcasting;

		private static int iIndex = 0;

		public static int time_Last = 0;

		public static int time_Now = 0;

		public static int count_Last = 0;

		public static int count_Now = 0;

		public static string Tramp = null;

		public static void Init()
		{
			try
			{
				ANT_TAPING.device0 = new ANT_Device();
				ANT_TAPING.device0.deviceResponse += new ANT_Device.dDeviceResponseHandler(ANT_TAPING.DeviceResponse);
				ANT_TAPING.channel0 = ANT_TAPING.device0.getChannel((int)ANT_TAPING.USER_ANT_CHANNEL);
				ANT_TAPING.channel0.channelResponse += new dChannelResponseHandler(ANT_TAPING.ChannelResponse);
			}
			catch (Exception ex)
			{
				bool flag = ANT_TAPING.device0 == null;
				if (flag)
				{
					throw new Exception("Could not connect to any device.\nDetails: \n   " + ex.Message);
				}
				throw new Exception("Error connecting to ANT: " + ex.Message);
			}
		}

		public static void Start()
		{
			ANT_TAPING.bDone = false;
			ANT_TAPING.bDisplay = true;
			ANT_TAPING.bBroadcasting = false;
			ANT_TAPING.channelType = ANT_ReferenceLibrary.ChannelType.BASE_Slave_Receive_0x00;
			try
			{
				ANT_TAPING.ConfigureANT();
			}
			catch (Exception ex)
			{
				throw new Exception("Demo failed: " + ex.Message + Environment.NewLine);
			}
		}

		private static void ConfigureANT()
		{
			ANT_TAPING.device0.ResetSystem();
			Thread.Sleep(500);
			bool flag = ANT_TAPING.device0.setNetworkKey(ANT_TAPING.USER_NETWORK_NUM, ANT_TAPING.USER_NETWORK_KEY, 500u);
			if (!flag)
			{
				throw new Exception("Error configuring network key");
			}
			bool flag2 = ANT_TAPING.channel0.assignChannel(ANT_TAPING.channelType, ANT_TAPING.USER_NETWORK_NUM, 500u);
			if (!flag2)
			{
				throw new Exception("Error assigning channel");
			}
			bool flag3 = ANT_TAPING.channel0.setChannelID(ANT_TAPING.USER_DEVICENUM, false, ANT_TAPING.USER_DEVICETYPE, ANT_TAPING.USER_TRANSTYPE, 500u);
			if (!flag3)
			{
				throw new Exception("Error configuring Channel ID");
			}
			bool flag4 = ANT_TAPING.channel0.setChannelFreq(ANT_TAPING.USER_RADIOFREQ, 500u);
			if (!flag4)
			{
				throw new Exception("Error configuring Radio Frequency");
			}
			bool flag5 = ANT_TAPING.channel0.setChannelPeriod(ANT_TAPING.USER_CHANNELPERIOD, 500u);
			if (!flag5)
			{
				throw new Exception("Error configuring Channel Period");
			}
			ANT_TAPING.bBroadcasting = true;
			bool flag6 = ANT_TAPING.channel0.openChannel(500u);
			if (flag6)
			{
				ANT_TAPING.device0.enableRxExtendedMessages(true);
				return;
			}
			ANT_TAPING.bBroadcasting = false;
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
							goto IL_2C9;
						}
						break;
					}
					bool flag = ANT_TAPING.bDisplay;
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
						byte time_MSB = response.messageContents[6];
						byte time_LSB = response.messageContents[5];
						int Time = ((int)time_MSB << 8) + (int)time_LSB;
						byte count_MSB = response.messageContents[8];
						byte count_LSB = response.messageContents[7];
						int Count = ((int)count_MSB << 8) + (int)count_LSB;
						bool flag5 = ANT_TAPING.time_Last == 0 || ANT_TAPING.time_Last == Time;
						if (flag5)
						{
							ANT_TAPING.time_Last = Time;
							ANT_TAPING.count_Last = Count;
						}
						else
						{
							int tramp = (Count - ANT_TAPING.count_Last) * 60 * 1024 / (Time - ANT_TAPING.time_Last);
							ANT_TAPING.time_Last = Time;
							ANT_TAPING.count_Last = Count;
						}
						ANT_TAPING.Tramp = ANT_TAPING.Tramp.ToString();
					}
					else
					{
						string[] expr_295 = new string[]
						{
							"|",
							"/",
							"_",
							"\\"
						};
						ANT_TAPING.iIndex &= 3;
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
						byte[] expr_8A_cp_0 = ANT_TAPING.txBuffer;
						int expr_8A_cp_1 = 0;
						expr_8A_cp_0[expr_8A_cp_1] += 1;
						bool flag6 = ANT_TAPING.bBroadcasting;
						if (flag6)
						{
							ANT_TAPING.channel0.sendBroadcastData(ANT_TAPING.txBuffer);
							bool flag7 = ANT_TAPING.bDisplay;
							if (flag7)
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
							Console.Write("Tx: " + ac[ANT_TAPING.iIndex++] + "\r");
							ANT_TAPING.iIndex &= 3;
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
						bool flag8 = ANT_TAPING.channel0.unassignChannel(500u);
						if (flag8)
						{
							ANT_TAPING.bDone = true;
						}
						break;
					}
					}
				}
				IL_2C9:;
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
						bool flag3 = ANT_TAPING.channel0.unassignChannel(500u);
						if (flag3)
						{
							ANT_TAPING.bDone = true;
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
