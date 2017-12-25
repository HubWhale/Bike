using Sensor_Windows;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Threading;
using Bike.Other;

namespace Bike.Trainings
{
    public partial class VideoWindow : Window, IComponentConnector
	{
		private Video video;

		private bool videoEnd = false;

		private double speed;

		private double C_speed;

		private double power;

		private double distance;

		private double C_distance;

		private double energy;

		private double slope;

		private double slopeMax;

		private double slopeMin;

		private double videoLength;

		private double velocity;

		private double flog;

		private double A;

		private double heart;

		private double tramp;

		private double speed2;

		private double speed3;

		private double speed4;

		private double oldspeed2;

		private double power1;

		private double x;

		private double UpSlopeResistance;

		private double DownSlopeResistance;

		private double Friction;

		private double Friction2;

		private double Horizontal;

		private double oldSpeed;

		private double nowSpeed;

		private int time;

		private int gear;

		private int weight;

		private int bscount;

		private int bccount;

		private int hrcount;

		private long oldTime;

		private long nowTime;

		private string slopelevelstring;

		private string HeartRate;

		private string SpeedDate;

		private string TrampDate;

		private string bstemp;

		private string bctemp;

		private string hrtemp;

		private string bs;

		private string bc;

		private string hr;

		private DispatcherTimer timer = new DispatcherTimer();

		private List<string> slopeArray;

		private int MaxGear = 10;

		private int MinGear = 1;

        //internal MediaElement mediaElement1;

        //internal Label closeLabel;

        //internal Label speedBox;

        //internal Label speedBoxUnit;

        //internal Label powerBox;

        //internal Label powerBoxUnit;

        //internal Label distanceBox;

        //internal Label distanceBoxUnit;

        //internal Label energyBox;

        //internal Label energyBoxUnit;

        //internal Label slopeBox;

        //internal Label slopeBoxUnit;

        //internal Label timeBox;

        //internal Label heartBox;

        //internal Label heartBoxUnit;

        //internal Label tramptBox;

        //internal Label tramptBoxUnit;

        //internal Image P_image;

        //internal Label P_distanceBox;

        //internal Image C_image;

        //internal Label C_distanceBox;

        //private bool _contentLoaded;

		public VideoWindow(Video v)
		{
			ANT_heart_rate.clearBsValue();
			ANT_heart_rate.clearBcValue();
			ANT_heart_rate.clearHrValue();
			this.InitializeComponent();
			this.video = v;
			this.ReadSlope(this.video.SlopePath);
			this.Data_Init();
			this.timer_Init();
		}

		private void Data_Init()
		{
			this.speed = 0.0;
			this.C_speed = 0.0;
			this.power = 0.0;
			this.distance = 0.0;
			this.C_distance = 1.0;
			this.energy = 0.0;
			this.time = 0;
			this.gear = 5;
			this.oldTime = (this.nowTime = DateTime.Now.ToFileTimeUtc());
			this.oldSpeed = (this.nowSpeed = 0.0);
			DateTime NOW = DateTime.Now;
			GlobalData.StartTime = Convert.ToInt64(UTCTimeOperate.ConvertDateTimeInt(NOW)).ToString();
			GlobalData.Time = 0L;
			GlobalData.Distance = "0";
			GlobalData.Velocity = "0";
			GlobalData.TrainingTime = "00:00:00";
		}

		private void timer_Init()
		{
			this.timer.Interval = new TimeSpan(0, 0, 1);
			this.timer.Tick += new EventHandler(this.timer_Tick);
			this.timer.Start();
		}

		private void timer_Uninit()
		{
			this.timer.Tick -= new EventHandler(this.timer_Tick);
			this.timer.Stop();
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			bool flag = GlobalData.Connectflag || GlobalData.ANT_Connectflag;
			if (flag)
			{
				bool flag2 = !this.videoEnd;
				if (flag2)
				{
					this.time++;
					this.timeBox.Content = this.timeGet(this.time);
					GlobalData.TrainingTime = this.timeGet(this.time);
					this.Refresh();
					GlobalData.SpeedDatas[(int)(checked((IntPtr)GlobalData.Time))] = (this.speed * 1000.0).ToString("#000000") + ";";
					checked
					{
						GlobalData.HeartRateData[(int)((IntPtr)GlobalData.Time)] = this.heart.ToString("#000") + ";";
						GlobalData.CadenceData[(int)((IntPtr)GlobalData.Time)] = this.tramp.ToString("#000") + ";";
						GlobalData.PowerData[(int)((IntPtr)GlobalData.Time)] = this.power.ToString("#0000.0") + ";";
					}
					GlobalData.Time += 1L;
				}
			}
			else
			{
				MessageBox.Show(MultiLanguage.Warn15, MultiLanguage.Warn16);
				this.videoEnd = true;
				base.Close();
			}
			bool connectflag = GlobalData.Connectflag;
			if (connectflag)
			{
				bool flag3 = Device.SpeedData != null;
				if (flag3)
				{
					this.speed = Convert.ToDouble(Device.SpeedData) / 10.0;
					bool flag4 = this.speed > 75.0;
					if (flag4)
					{
						this.speed = 75.0;
					}
					this.mediaElement1.SpeedRatio = this.speed / Device.videoAveSpeed;
					this.mediaElement1.Play();
				}
				else
				{
					this.mediaElement1.Pause();
				}
			}
			else
			{
				bool flag5 = this.SpeedDate != null;
				if (flag5)
				{
					bool flag6 = this.speed2 > 75.0;
					if (flag6)
					{
						this.speed2 = 75.0;
					}
					this.mediaElement1.SpeedRatio = this.speed2 / Device.videoAveSpeed;
					this.mediaElement1.Play();
				}
				else
				{
					this.mediaElement1.Pause();
				}
			}
			double currentPlaybackTime = this.mediaElement1.Position.TotalSeconds;
			int slopeIndex = 0;
			bool flag7 = currentPlaybackTime != 0.0;
			if (flag7)
			{
				slopeIndex = (int)(currentPlaybackTime / (this.videoLength / (double)this.slopeArray.Count<string>()));
			}
			bool flag8 = slopeIndex < this.slopeArray.Count<string>();
			string nowslope;
			if (flag8)
			{
				nowslope = this.slopeArray[slopeIndex];
			}
			else
			{
				nowslope = this.slopeArray[this.slopeArray.Count<string>() - 1];
			}
			double doubleslope = double.Parse(nowslope);
			this.slope = doubleslope;
			this.slopeBox.Content = doubleslope.ToString("#0.00");
			bool flag9 = doubleslope < -2.0;
			int currentGear;
			if (flag9)
			{
				currentGear = 1;
			}
			else
			{
				bool flag10 = doubleslope >= -2.0 && doubleslope < 0.0;
				if (flag10)
				{
					currentGear = 2;
				}
				else
				{
					bool flag11 = doubleslope >= 0.0 && doubleslope < 0.5;
					if (flag11)
					{
						currentGear = 3;
					}
					else
					{
						bool flag12 = doubleslope >= 0.5 && doubleslope < 1.0;
						if (flag12)
						{
							currentGear = 4;
						}
						else
						{
							bool flag13 = doubleslope >= 1.0 && doubleslope < 1.5;
							if (flag13)
							{
								currentGear = 5;
							}
							else
							{
								bool flag14 = doubleslope >= 1.5 && doubleslope < 2.0;
								if (flag14)
								{
									currentGear = 6;
								}
								else
								{
									bool flag15 = doubleslope >= 2.0 && doubleslope < 2.5;
									if (flag15)
									{
										currentGear = 7;
									}
									else
									{
										bool flag16 = doubleslope >= 2.5 && doubleslope < 3.0;
										if (flag16)
										{
											currentGear = 8;
										}
										else
										{
											bool flag17 = doubleslope >= 3.0 && doubleslope < 3.5;
											if (flag17)
											{
												currentGear = 9;
											}
											else
											{
												bool flag18 = doubleslope >= 3.5 && doubleslope < 4.0;
												if (flag18)
												{
													currentGear = 10;
												}
												else
												{
													bool flag19 = doubleslope >= 4.0 && doubleslope < 4.5;
													if (flag19)
													{
														currentGear = 11;
													}
													else
													{
														bool flag20 = doubleslope >= 4.5 && doubleslope < 5.0;
														if (flag20)
														{
															currentGear = 12;
														}
														else
														{
															bool flag21 = doubleslope >= 5.0 && doubleslope < 5.5;
															if (flag21)
															{
																currentGear = 13;
															}
															else
															{
																bool flag22 = doubleslope >= 5.5 && doubleslope < 6.0;
																if (flag22)
																{
																	currentGear = 14;
																}
																else
																{
																	bool flag23 = doubleslope >= 6.0 && doubleslope < 6.5;
																	if (flag23)
																	{
																		currentGear = 15;
																	}
																	else
																	{
																		bool flag24 = doubleslope >= 6.5 && doubleslope < 7.0;
																		if (flag24)
																		{
																			currentGear = 16;
																		}
																		else
																		{
																			currentGear = 17;
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
			}
			bool flag25 = currentGear != this.gear;
			if (flag25)
			{
				GlobalData.SendGear(currentGear);
			}
			this.gear = currentGear;
		}

		private void Refresh()
		{
			bool connectflag = GlobalData.Connectflag;
			if (connectflag)
			{
				bool flag = Device.SpeedData == null;
				if (flag)
				{
					this.speed = 0.0;
				}
				else
				{
					try
					{
						this.speed = Convert.ToDouble(Device.SpeedData) / 10.0;
						bool flag2 = XMLHelper.UsersInforGetNum() > 0;
						if (flag2)
						{
							this.weight = Convert.ToInt32(XMLHelper.UsersInforReader("Information", 6));
						}
						else
						{
							bool flag3 = GlobalData.weight != null;
							if (flag3)
							{
								this.weight = Convert.ToInt32(GlobalData.weight);
							}
							else
							{
								this.weight = 75;
							}
						}
						this.Horizontal = 9.8067 * Math.Cos(Math.Atan(this.slope / 100.0)) * (double)(this.weight + 7) * 0.005;
						this.x = Math.Atan(this.slope / 100.0);
						this.Friction = 9.8067 * (double)(this.weight + 7) * Math.Sin(this.x);
						this.Friction2 = -9.8067 * (double)(this.weight + 7) * Math.Sin(this.x);
						this.speed2 = 0.0;
						this.speed3 = 15.0;
						while (Math.Abs(this.speed3 - this.speed2) > 1.0)
						{
							bool flag4 = this.speed > 0.0;
							if (flag4)
							{
								bool flag5 = this.gear < 3;
								if (flag5)
								{
									this.DownSlopeResistance = 0.19657071 * (this.speed3 / 3.6) * (this.speed3 / 3.6);
									this.speed2 = 3.6 * (this.power + this.Friction2 * this.speed3 / 3.6) / (this.DownSlopeResistance + this.Horizontal);
									bool flag6 = this.speed2 > 75.0 || this.speed2 < 0.0;
									if (flag6)
									{
										this.speed2 = 75.0;
									}
								}
								else
								{
									this.UpSlopeResistance = 0.19657071 * (this.speed3 / 3.6) * (this.speed3 / 3.6);
									this.speed2 = 3.6 * this.power / (this.UpSlopeResistance + this.Horizontal + this.Friction);
								}
								bool flag7 = this.speed2 <= 0.0;
								if (flag7)
								{
									this.speed2 = 0.0;
								}
							}
							else
							{
								this.speed2 = 0.0;
							}
							bool flag8 = this.power == 0.0;
							if (flag8)
							{
								this.speed2 = 0.0;
							}
							this.speed3 = ((this.speed3 - this.speed2 > 0.0) ? (this.speed3 - 0.1) : (this.speed3 + 0.1));
						}
						bool flag9 = Math.Abs(this.oldspeed2 - this.speed2) > 5.0;
						if (flag9)
						{
							this.speed2 = 0.5 * (this.oldspeed2 + this.speed2);
						}
						this.oldspeed2 = this.speed2;
					}
					catch
					{
						this.speed = 0.0;
					}
				}
			}
			else
			{
				bool flag10 = GlobalData.speedChannel >= 0 && GlobalData.speedChannel < 8;
				if (flag10)
				{
					switch (GlobalData.speedChannel)
					{
					case 0:
						this.SpeedDate = ANT_heart_rate.BS0;
						break;
					case 1:
						this.SpeedDate = ANT_heart_rate.BS1;
						break;
					case 2:
						this.SpeedDate = ANT_heart_rate.BS2;
						break;
					case 3:
						this.SpeedDate = ANT_heart_rate.BS3;
						break;
					case 4:
						this.SpeedDate = ANT_heart_rate.BS4;
						break;
					case 5:
						this.SpeedDate = ANT_heart_rate.BS5;
						break;
					case 6:
						this.SpeedDate = ANT_heart_rate.BS6;
						break;
					case 7:
						this.SpeedDate = ANT_heart_rate.BS7;
						break;
					}
					this.bs = this.SpeedDate;
					bool flag11 = this.bstemp == this.bs;
					if (flag11)
					{
						this.bscount++;
					}
					else
					{
						this.bscount = 0;
					}
					bool flag12 = this.SpeedDate == null;
					if (flag12)
					{
						this.speed = 0.0;
					}
					else
					{
						try
						{
							this.speed = Convert.ToDouble(this.SpeedDate);
							this.bstemp = this.speed.ToString();
						}
						catch
						{
							this.speed = 0.0;
						}
						bool flag13 = this.bscount >= 5;
						if (flag13)
						{
							this.speed = 0.0;
							ANT_heart_rate.BS0 = null;
							this.bstemp = this.speed.ToString();
						}
					}
				}
			}
			bool flag14 = this.speed != 0.0;
			if (flag14)
			{
				bool connectflag2 = GlobalData.Connectflag;
				if (connectflag2)
				{
					this.power = GlobalData.getPower(this.speed, this.gear);
				}
				else
				{
					this.power = GlobalData.getPowerYZ(this.speed) * 0.84;
					bool flag15 = XMLHelper.UsersInforGetNum() > 0;
					if (flag15)
					{
						this.weight = Convert.ToInt32(XMLHelper.UsersInforReader("Information", 6));
					}
					else
					{
						bool flag16 = GlobalData.weight != null;
						if (flag16)
						{
							this.weight = Convert.ToInt32(GlobalData.weight);
						}
						else
						{
							this.weight = 75;
						}
					}
					this.power1 = (double)(this.weight * 4);
					bool flag17 = this.power1 > this.power;
					if (flag17)
					{
						this.power = GlobalData.getPowerYZ(this.speed) * 0.84;
					}
					else
					{
						this.power = this.power1;
					}
				}
			}
			else
			{
				this.power = 0.0;
			}
			this.powerBox.Content = this.power.ToString("#0.0");
			bool flag18 = !GlobalData.Connectflag;
			if (flag18)
			{
				bool flag19 = XMLHelper.UsersInforGetNum() > 0;
				if (flag19)
				{
					this.weight = Convert.ToInt32(XMLHelper.UsersInforReader("Information", 6));
				}
				else
				{
					bool flag20 = GlobalData.weight != null;
					if (flag20)
					{
						this.weight = Convert.ToInt32(GlobalData.weight);
					}
					else
					{
						this.weight = 75;
					}
				}
				this.Horizontal = 9.8067 * Math.Cos(Math.Atan(this.slope / 100.0)) * (double)(this.weight + 7) * 0.005;
				this.x = Math.Atan(this.slope / 100.0);
				this.Friction = 9.8067 * (double)(this.weight + 7) * Math.Sin(this.x);
				this.Friction2 = -9.8067 * (double)(this.weight + 7) * Math.Sin(this.x);
				this.speed2 = 0.0;
				this.speed3 = 15.0;
				while (Math.Abs(this.speed3 - this.speed2) > 1.0)
				{
					bool flag21 = this.speed > 0.0;
					if (flag21)
					{
						bool flag22 = this.gear < 3;
						if (flag22)
						{
							this.DownSlopeResistance = 0.19657071 * (this.speed3 / 3.6) * (this.speed3 / 3.6);
							this.speed2 = 3.6 * (this.power + this.Friction2 * this.speed3 / 3.6) / (this.DownSlopeResistance + this.Horizontal);
							bool flag23 = this.speed2 > 75.0 || this.speed2 < 0.0;
							if (flag23)
							{
								this.speed2 = 75.0;
							}
						}
						else
						{
							this.UpSlopeResistance = 0.19657071 * (this.speed3 / 3.6) * (this.speed3 / 3.6);
							this.speed2 = 3.6 * this.power / (this.UpSlopeResistance + this.Horizontal + this.Friction);
						}
						bool flag24 = this.speed2 <= 0.0;
						if (flag24)
						{
							this.speed2 = 0.0;
						}
					}
					else
					{
						this.speed2 = 0.0;
					}
					bool flag25 = this.power == 0.0;
					if (flag25)
					{
						this.speed2 = 0.0;
					}
					this.speed3 = ((this.speed3 - this.speed2 > 0.0) ? (this.speed3 - 0.1) : (this.speed3 + 0.1));
				}
			}
			bool flag26 = Math.Abs(this.oldspeed2 - this.speed2) > 5.0;
			if (flag26)
			{
				this.speed2 = 0.5 * (this.oldspeed2 + this.speed2);
			}
			this.oldspeed2 = this.speed2;
			this.speed = this.speed2;
			this.speedBox.Content = this.speed.ToString("#0.0");
			this.distance += this.speed * 10.0 * 25.0 / 9.0 / 100000.0;
			bool flag27 = this.C_distance > this.distance;
			if (flag27)
			{
				double C_Left = this.C_distanceBox.Margin.Left;
				double P_Left = this.P_distanceBox.Margin.Left;
				double C_Top = this.C_distanceBox.Margin.Top;
				double P_Top = this.P_distanceBox.Margin.Top;
				double C_Left2 = this.C_image.Margin.Left;
				double P_Left2 = this.P_image.Margin.Left;
				double C_Top2 = this.C_image.Margin.Top;
				double P_Top2 = this.P_image.Margin.Top;
				bool flag28 = P_Top == 246.0;
				if (flag28)
				{
					this.P_distanceBox.Margin = new Thickness(P_Left, P_Top + 30.0, 0.0, 0.0);
					this.C_distanceBox.Margin = new Thickness(C_Left, C_Top - 30.0, 0.0, 0.0);
					this.P_image.Margin = new Thickness(P_Left2, P_Top2 + 30.0, 0.0, 0.0);
					this.C_image.Margin = new Thickness(C_Left2, C_Top2 - 30.0, 0.0, 0.0);
				}
			}
			bool flag29 = this.C_distance < this.distance;
			if (flag29)
			{
				double C_Left3 = this.C_distanceBox.Margin.Left;
				double P_Left3 = this.P_distanceBox.Margin.Left;
				double C_Top3 = this.C_distanceBox.Margin.Top;
				double P_Top3 = this.P_distanceBox.Margin.Top;
				double C_Left4 = this.C_image.Margin.Left;
				double P_Left4 = this.P_image.Margin.Left;
				double C_Top4 = this.C_image.Margin.Top;
				double P_Top4 = this.P_image.Margin.Top;
				bool flag30 = P_Top3 == 276.0;
				if (flag30)
				{
					this.P_distanceBox.Margin = new Thickness(P_Left3, P_Top3 - 30.0, 0.0, 0.0);
					this.C_distanceBox.Margin = new Thickness(C_Left3, C_Top3 + 30.0, 0.0, 0.0);
					this.P_image.Margin = new Thickness(P_Left4, P_Top4 - 30.0, 0.0, 0.0);
					this.C_image.Margin = new Thickness(C_Left4, C_Top4 + 30.0, 0.0, 0.0);
				}
			}
			bool flag31 = this.C_distance - this.distance >= 0.2;
			if (flag31)
			{
				this.flog = 0.7;
			}
			bool flag32 = this.C_distance - this.distance >= 0.05 && this.C_distance - this.distance < 0.2;
			if (flag32)
			{
				this.flog = 0.8;
			}
			bool flag33 = this.C_distance - this.distance >= 0.0 && this.C_distance - this.distance < 0.05;
			if (flag33)
			{
				this.flog = 0.9;
			}
			bool flag34 = this.distance - this.C_distance >= 0.0 && this.distance - this.C_distance < 0.05;
			if (flag34)
			{
				this.flog = 1.1;
			}
			bool flag35 = this.distance - this.C_distance >= 0.05 && this.distance - this.C_distance < 0.2;
			if (flag35)
			{
				this.flog = 1.2;
			}
			bool flag36 = this.distance - this.C_distance >= 0.2;
			if (flag36)
			{
				this.flog = 1.3;
			}
			this.C_speed = this.speed * this.flog;
			this.C_distance += this.C_speed * 10.0 * 25.0 / 9.0 / 100000.0;
			this.distanceBox.Content = this.distance.ToString("#0.0");
			this.P_distanceBox.Content = this.distance.ToString("#00.000") + "Km";
			this.C_distanceBox.Content = this.C_distance.ToString("#00.000") + "Km";
			GlobalData.Distance = this.distance.ToString("#0.0");
			GlobalData.distance = (this.distance * 1000.0).ToString("#0");
			this.velocity = this.distance * 3600.0 / (double)GlobalData.Time;
			GlobalData.Velocity = this.velocity.ToString("#0.0");
			GlobalData.velocity = (this.velocity * 1000.0).ToString("#0");
			this.energy += this.power * 0.239 / 1000.0;
			this.energyBox.Content = this.energy.ToString("#0.0");
			bool flag37 = GlobalData.heartChannel >= 0 && GlobalData.heartChannel < 8;
			if (flag37)
			{
				switch (GlobalData.heartChannel)
				{
				case 0:
					this.HeartRate = ANT_heart_rate.HR0;
					break;
				case 1:
					this.HeartRate = ANT_heart_rate.HR1;
					break;
				case 2:
					this.HeartRate = ANT_heart_rate.HR2;
					break;
				case 3:
					this.HeartRate = ANT_heart_rate.HR3;
					break;
				case 4:
					this.HeartRate = ANT_heart_rate.HR4;
					break;
				case 5:
					this.HeartRate = ANT_heart_rate.HR5;
					break;
				case 6:
					this.HeartRate = ANT_heart_rate.HR6;
					break;
				case 7:
					this.HeartRate = ANT_heart_rate.HR7;
					break;
				}
				this.hr = this.HeartRate;
				bool flag38 = this.hrtemp == this.hr;
				if (flag38)
				{
					this.hrcount++;
				}
				else
				{
					this.hrcount = 0;
				}
			}
			bool flag39 = this.HeartRate == null;
			if (flag39)
			{
				this.heart = 0.0;
			}
			else
			{
				try
				{
					this.heart = Convert.ToDouble(this.HeartRate);
					this.hrtemp = this.heart.ToString();
				}
				catch
				{
					this.heart = 0.0;
				}
				bool flag40 = this.hrcount >= 10;
				if (flag40)
				{
					this.heart = 0.0;
					ANT_heart_rate.HR2 = null;
					this.hrtemp = this.heart.ToString();
				}
			}
			this.heartBox.Content = this.heart.ToString("#0");
			bool flag41 = GlobalData.cadenceChannel >= 0 && GlobalData.cadenceChannel < 8;
			if (flag41)
			{
				switch (GlobalData.cadenceChannel)
				{
				case 0:
					this.TrampDate = ANT_heart_rate.BC0;
					break;
				case 1:
					this.TrampDate = ANT_heart_rate.BC1;
					break;
				case 2:
					this.TrampDate = ANT_heart_rate.BC2;
					break;
				case 3:
					this.TrampDate = ANT_heart_rate.BC3;
					break;
				case 4:
					this.TrampDate = ANT_heart_rate.BC4;
					break;
				case 5:
					this.TrampDate = ANT_heart_rate.BC5;
					break;
				case 6:
					this.TrampDate = ANT_heart_rate.BC6;
					break;
				case 7:
					this.TrampDate = ANT_heart_rate.BC7;
					break;
				}
				this.bc = this.TrampDate;
				bool flag42 = this.bctemp == this.bc;
				if (flag42)
				{
					this.bccount++;
				}
				else
				{
					this.bccount = 0;
				}
			}
			bool flag43 = this.TrampDate == null;
			if (flag43)
			{
				this.tramp = 0.0;
			}
			else
			{
				try
				{
					this.tramp = Convert.ToDouble(this.TrampDate);
					this.bctemp = this.tramp.ToString();
				}
				catch
				{
					this.tramp = 0.0;
				}
				bool flag44 = this.bccount >= 10;
				if (flag44)
				{
					this.tramp = 0.0;
					ANT_heart_rate.BC1 = null;
					this.bctemp = this.tramp.ToString();
				}
			}
			this.tramptBox.Content = this.tramp.ToString("#0");
		}

		private string timeGet(int time)
		{
			bool flag = time >= 86400;
			if (flag)
			{
				time = 0;
			}
			return string.Concat(new string[]
			{
				(time / 3600).ToString("#00"),
				":",
				(time % 3600 / 60).ToString("#00"),
				":",
				(time % 3600 % 60).ToString("#00")
			});
		}

		public VideoWindow()
		{
			this.InitializeComponent();
		}

		private void mediaElement1_MediaOpened(object sender, RoutedEventArgs e)
		{
			this.videoLength = this.mediaElement1.NaturalDuration.TimeSpan.TotalSeconds;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			this.mediaController();
		}

		private void ReadSlope(string slopePath)
		{
			StreamReader sr = new StreamReader(slopePath, Encoding.Default);
			string slopeSet = sr.ReadToEnd();
			this.slopeArray = new List<string>(slopeSet.Split(new char[]
			{
				','
			}));
			List<double> doubleSlopeArray = new List<double>();
			for (int i = 0; i < this.slopeArray.Count<string>(); i++)
			{
				string _slope = this.slopeArray[i];
				doubleSlopeArray.Add(double.Parse(_slope));
			}
			this.slopeMax = (this.slopeMin = doubleSlopeArray[0]);
			for (int j = 1; j < this.slopeArray.Count<string>(); j++)
			{
				bool flag = this.slopeMax < doubleSlopeArray[j];
				if (flag)
				{
					this.slopeMax = doubleSlopeArray[j];
				}
				bool flag2 = this.slopeMin > doubleSlopeArray[j];
				if (flag2)
				{
					this.slopeMin = doubleSlopeArray[j];
				}
			}
		}

		private void mediaController()
		{
			try
			{
				this.mediaElement1.LoadedBehavior = MediaState.Manual;
				this.mediaElement1.Source = new Uri(this.video.VideoPath);
				this.mediaElement1.Play();
			}
			catch (Exception ex_3A)
			{
				MessageBox.Show(MultiLanguage.Note5);
				base.Close();
			}
		}

		private void mediaElement1_MediaEnded(object sender, RoutedEventArgs e)
		{
			this.videoEnd = true;
			MessageBox.Show(MultiLanguage.Warn17, MultiLanguage.Finish_t);
			base.Close();
		}

		private void closeLabel_MouseUp(object sender, MouseButtonEventArgs e)
		{
			base.Close();
		}

		private void Window_KeyUp(object sender, KeyEventArgs e)
		{
			bool flag = e.Key == Key.Escape;
			if (flag)
			{
				base.Close();
			}
		}

		private void Window_Closing(object sender, CancelEventArgs e)
		{
			bool flag = !this.videoEnd;
			if (flag)
			{
				bool flag2 = MessageBox.Show(MultiLanguage.Warn18, MultiLanguage.Stop_t, MessageBoxButton.YesNo) != MessageBoxResult.Yes;
				if (flag2)
				{
					e.Cancel = true;
				}
			}
		}

		private void Window_Closed(object sender, EventArgs e)
		{
			GlobalData.VideoTrainingflag = false;
			this.timer_Uninit();
			SaveWindow sw = new SaveWindow();
			sw.ShowDialog();
		}

        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        //public void InitializeComponent()
        //{
        //    bool contentLoaded = this._contentLoaded;
        //    if (!contentLoaded)
        //    {
        //        this._contentLoaded = true;
        //        Uri resourceLocater = new Uri("/Bike;component/trainings/videowindow.xaml", UriKind.Relative);
        //        Application.LoadComponent(this, resourceLocater);
        //    }
        //}

        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
        //void IComponentConnector.Connect(int connectionId, object target)
        //{
        //    switch (connectionId)
        //    {
        //    case 1:
        //        ((VideoWindow)target).KeyUp += new KeyEventHandler(this.Window_KeyUp);
        //        ((VideoWindow)target).Closing += new CancelEventHandler(this.Window_Closing);
        //        ((VideoWindow)target).Loaded += new RoutedEventHandler(this.Window_Loaded);
        //        ((VideoWindow)target).Closed += new EventHandler(this.Window_Closed);
        //        break;
        //    case 2:
        //        this.mediaElement1 = (MediaElement)target;
        //        this.mediaElement1.MediaEnded += new RoutedEventHandler(this.mediaElement1_MediaEnded);
        //        this.mediaElement1.MediaOpened += new RoutedEventHandler(this.mediaElement1_MediaOpened);
        //        break;
        //    case 3:
        //        this.closeLabel = (Label)target;
        //        this.closeLabel.MouseUp += new MouseButtonEventHandler(this.closeLabel_MouseUp);
        //        break;
        //    case 4:
        //        this.speedBox = (Label)target;
        //        break;
        //    case 5:
        //        this.speedBoxUnit = (Label)target;
        //        break;
        //    case 6:
        //        this.powerBox = (Label)target;
        //        break;
        //    case 7:
        //        this.powerBoxUnit = (Label)target;
        //        break;
        //    case 8:
        //        this.distanceBox = (Label)target;
        //        break;
        //    case 9:
        //        this.distanceBoxUnit = (Label)target;
        //        break;
        //    case 10:
        //        this.energyBox = (Label)target;
        //        break;
        //    case 11:
        //        this.energyBoxUnit = (Label)target;
        //        break;
        //    case 12:
        //        this.slopeBox = (Label)target;
        //        break;
        //    case 13:
        //        this.slopeBoxUnit = (Label)target;
        //        break;
        //    case 14:
        //        this.timeBox = (Label)target;
        //        break;
        //    case 15:
        //        this.heartBox = (Label)target;
        //        break;
        //    case 16:
        //        this.heartBoxUnit = (Label)target;
        //        break;
        //    case 17:
        //        this.tramptBox = (Label)target;
        //        break;
        //    case 18:
        //        this.tramptBoxUnit = (Label)target;
        //        break;
        //    case 19:
        //        this.P_image = (Image)target;
        //        break;
        //    case 20:
        //        this.P_distanceBox = (Label)target;
        //        break;
        //    case 21:
        //        this.C_image = (Image)target;
        //        break;
        //    case 22:
        //        this.C_distanceBox = (Label)target;
        //        break;
        //    default:
        //        this._contentLoaded = true;
        //        break;
        //    }
        //}
	}
}
