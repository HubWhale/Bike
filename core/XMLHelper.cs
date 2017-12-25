using System;
using System.IO;
using System.Xml;
using Bike.Trainings;

namespace Bike
{
	public class XMLHelper
	{
		private static string RecordsFileName = Environment.CurrentDirectory + "\\RecordsData.xml";

		private static string SettingFileName = Environment.CurrentDirectory + "\\SettingData.xml";

		private static string UsersFileName = Environment.CurrentDirectory + "\\UsersData.xml";

		private static string BBUsersFileName = Environment.CurrentDirectory + "\\BBUsersData.xml";

		public static string VedioConfige = Environment.CurrentDirectory + "\\VedioConfige.xml";

		public static string UsersInfoFileName = Environment.CurrentDirectory + "\\UsersInfoData.xml";

		private static string SpeedDeviceNumber = Environment.CurrentDirectory + "\\SpeedDeviceNumber.xml";

		private static string HeartRateDeviceNumber = Environment.CurrentDirectory + "\\HeartRateDeviceNumber.xml";

		private static string CadenceDeviceNumber = Environment.CurrentDirectory + "\\CadenceDeviceNumber.xml";

		public static void Init()
		{
			bool flag = !File.Exists(XMLHelper.RecordsFileName);
			if (flag)
			{
				XMLHelper.CreateXmlDocument(XMLHelper.RecordsFileName, "rroot", "1.0", "utf-8", null);
			}
			bool flag2 = !File.Exists(XMLHelper.SettingFileName);
			if (flag2)
			{
				XMLHelper.CreateXmlDocument(XMLHelper.SettingFileName, "sroot", "1.0", "utf-8", null);
			}
			bool flag3 = !File.Exists(XMLHelper.UsersFileName);
			if (flag3)
			{
				XMLHelper.CreateXmlDocument(XMLHelper.UsersFileName, "uroot", "1.0", "utf-8", null);
			}
			bool flag4 = !File.Exists(XMLHelper.BBUsersFileName);
			if (flag4)
			{
				XMLHelper.CreateXmlDocument(XMLHelper.BBUsersFileName, "broot", "1.0", "utf-8", null);
			}
			bool flag5 = !File.Exists(XMLHelper.VedioConfige);
			if (flag5)
			{
				XMLHelper.CreateXmlDocument(XMLHelper.VedioConfige, "vroot", "1.0", "utf-8", null);
			}
			bool flag6 = !File.Exists(XMLHelper.UsersInfoFileName);
			if (flag6)
			{
				XMLHelper.CreateXmlDocument(XMLHelper.UsersInfoFileName, "iroot", "1.0", "utf-8", null);
			}
			bool flag7 = !File.Exists(XMLHelper.SpeedDeviceNumber);
			if (flag7)
			{
				XMLHelper.CreateXmlDocument(XMLHelper.SpeedDeviceNumber, "kroot", "1.0", "utf-8", null);
			}
			bool flag8 = !File.Exists(XMLHelper.HeartRateDeviceNumber);
			if (flag8)
			{
				XMLHelper.CreateXmlDocument(XMLHelper.HeartRateDeviceNumber, "oroot", "1.0", "utf-8", null);
			}
			bool flag9 = !File.Exists(XMLHelper.CadenceDeviceNumber);
			if (flag9)
			{
				XMLHelper.CreateXmlDocument(XMLHelper.CadenceDeviceNumber, "broot", "1.0", "utf-8", null);
			}
		}

		public static int getNum(string FileName, string _string)
		{
			XmlNode xmlNode = XMLHelper.GetXmlNodeByXpath(FileName, _string);
			bool flag = xmlNode != null;
			int result;
			if (flag)
			{
				result = xmlNode.ChildNodes.Count;
			}
			else
			{
				result = 0;
			}
			return result;
		}

		public static string getName(string FileName, string _string, int num)
		{
			XmlNode xmlNode = XMLHelper.GetXmlNodeByXpath(FileName, _string);
			bool flag = num < xmlNode.ChildNodes.Count;
			string result;
			if (flag)
			{
				result = xmlNode.ChildNodes[num].Name;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public static int RecordGetNum()
		{
			return XMLHelper.getNum(XMLHelper.RecordsFileName, "/rroot");
		}

		public static string RecordGetName(int num)
		{
			return XMLHelper.getName(XMLHelper.RecordsFileName, "/rroot", num);
		}

		public static void RecordWriter(string _string)
		{
			XMLHelper.CreateOrUpdateXmlNodeByXPath(XMLHelper.RecordsFileName, "/rroot", _string, null);
			XMLHelper.CreateOrUpdateXmlNodeByXPath(XMLHelper.RecordsFileName, "/rroot/" + _string, "TrackName", GlobalData.TrackName);
			XMLHelper.CreateOrUpdateXmlNodeByXPath(XMLHelper.RecordsFileName, "/rroot/" + _string, "TrackId", GlobalData.TrackId);
			XMLHelper.CreateOrUpdateXmlNodeByXPath(XMLHelper.RecordsFileName, "/rroot/" + _string, "RoutebookId", GlobalData.RoutebookId);
			XMLHelper.CreateOrUpdateXmlNodeByXPath(XMLHelper.RecordsFileName, "/rroot/" + _string, "StartTime", GlobalData.StartTime);
			XMLHelper.CreateOrUpdateXmlNodeByXPath(XMLHelper.RecordsFileName, "/rroot/" + _string, "TotalTime", GlobalData.Time.ToString());
			XMLHelper.CreateOrUpdateXmlNodeByXPath(XMLHelper.RecordsFileName, "/rroot/" + _string, "Distance", GlobalData.Distance);
			XMLHelper.CreateOrUpdateXmlNodeByXPath(XMLHelper.RecordsFileName, "/rroot/" + _string, "Velocity", GlobalData.Velocity);
		}

		public static void RecordDeleter(string _string)
		{
			XMLHelper.DeleteXmlNodeByXPath(XMLHelper.RecordsFileName, "/rroot/" + _string);
		}

		public static string RecordReader(string _string, int num)
		{
			XmlNode xmlNode = XMLHelper.GetXmlNodeByXpath(XMLHelper.RecordsFileName, "/rroot/" + _string);
			bool flag = xmlNode != null && num < 11;
			string result;
			if (flag)
			{
				result = xmlNode.ChildNodes[num].InnerText;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public static void setLanguage(int num)
		{
			bool flag = num < 2;
			if (flag)
			{
				XMLHelper.CreateOrUpdateXmlNodeByXPath(XMLHelper.SettingFileName, "/sroot", "language", num.ToString());
			}
		}

		public static int getLanguage()
		{
			XmlNode xmlNode = XMLHelper.GetXmlNodeByXpath(XMLHelper.SettingFileName, "/sroot/language");
			bool flag = xmlNode != null;
			int result;
			if (flag)
			{
				result = int.Parse(xmlNode.InnerText);
			}
			else
			{
				result = 0;
			}
			return result;
		}

		public static void UsersWriter(string _string)
		{
			XMLHelper.CreateOrUpdateXmlNodeByXPath(XMLHelper.UsersFileName, "/uroot", _string, null);
			XMLHelper.CreateOrUpdateXmlNodeByXPath(XMLHelper.UsersFileName, "/uroot/" + _string, "UserName", GlobalData.UserName);
			XMLHelper.CreateOrUpdateXmlNodeByXPath(XMLHelper.UsersFileName, "/uroot/" + _string, "Password", GlobalData.Password);
			XMLHelper.CreateOrUpdateXmlNodeByXPath(XMLHelper.UsersFileName, "/uroot/" + _string, "Automaticflag", GlobalData.Automaticflag);
		}

		public static void UsersDeleter(string _string)
		{
			XMLHelper.DeleteXmlNodeByXPath(XMLHelper.UsersFileName, "/uroot/" + _string);
		}

		public static string UsersReader(string _string, int num)
		{
			XmlNode xmlNode = XMLHelper.GetXmlNodeByXpath(XMLHelper.UsersFileName, "/uroot/" + _string);
			bool flag = xmlNode != null && num < 3;
			string result;
			if (flag)
			{
				result = xmlNode.ChildNodes[num].InnerText;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public static int UsersGetNum()
		{
			return XMLHelper.getNum(XMLHelper.UsersFileName, "/uroot");
		}

		public static void BBUsersWriter(string _string)
		{
			XMLHelper.CreateOrUpdateXmlNodeByXPath(XMLHelper.BBUsersFileName, "/broot", _string, null);
			XMLHelper.CreateOrUpdateXmlNodeByXPath(XMLHelper.BBUsersFileName, "/broot/" + _string, "PhoneNumber", GlobalData.phoneNumber);
			XMLHelper.CreateOrUpdateXmlNodeByXPath(XMLHelper.BBUsersFileName, "/broot/" + _string, "Verifycode", GlobalData.verifycode);
			XMLHelper.CreateOrUpdateXmlNodeByXPath(XMLHelper.BBUsersFileName, "/broot/" + _string, "Weight", GlobalData.weight);
			XMLHelper.CreateOrUpdateXmlNodeByXPath(XMLHelper.BBUsersFileName, "/broot/" + _string, "OpenId", GlobalData.openId);
		}

		public static void BBUsersDeleter(string _string)
		{
			XMLHelper.DeleteXmlNodeByXPath(XMLHelper.BBUsersFileName, "/broot/" + _string);
		}

		public static string BBUsersReader(string _string, int num)
		{
			XmlNode xmlNode = XMLHelper.GetXmlNodeByXpath(XMLHelper.BBUsersFileName, "/broot/" + _string);
			bool flag = xmlNode != null && num < 4;
			string result;
			if (flag)
			{
				result = xmlNode.ChildNodes[num].InnerText;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public static int BBUsersGetNum()
		{
			return XMLHelper.getNum(XMLHelper.BBUsersFileName, "/broot");
		}

		public static void UserInformationFile(string _string)
		{
			XMLHelper.CreateOrUpdateXmlNodeByXPath(XMLHelper.UsersInfoFileName, "/iroot", _string, null);
			XMLHelper.CreateOrUpdateXmlNodeByXPath(XMLHelper.UsersInfoFileName, "/iroot/" + _string, "UserName", GlobalData.UserName1);
			XMLHelper.CreateOrUpdateXmlNodeByXPath(XMLHelper.UsersInfoFileName, "/iroot/" + _string, "MailBox", GlobalData.Mailbox);
			XMLHelper.CreateOrUpdateXmlNodeByXPath(XMLHelper.UsersInfoFileName, "/iroot/" + _string, "Phone", GlobalData.Phone);
			XMLHelper.CreateOrUpdateXmlNodeByXPath(XMLHelper.UsersInfoFileName, "/iroot/" + _string, "Sex", GlobalData.Sex);
			XMLHelper.CreateOrUpdateXmlNodeByXPath(XMLHelper.UsersInfoFileName, "/iroot/" + _string, "Nationality", GlobalData.Nationality);
			XMLHelper.CreateOrUpdateXmlNodeByXPath(XMLHelper.UsersInfoFileName, "/iroot/" + _string, "Height", GlobalData.Height);
			XMLHelper.CreateOrUpdateXmlNodeByXPath(XMLHelper.UsersInfoFileName, "/iroot/" + _string, "Weight", GlobalData.Weight);
			XMLHelper.CreateOrUpdateXmlNodeByXPath(XMLHelper.UsersInfoFileName, "/iroot/" + _string, "Size", GlobalData.Size);
		}

		public static string UsersInforReader(string _string, int num)
		{
			XmlNode xmlNode = XMLHelper.GetXmlNodeByXpath(XMLHelper.UsersInfoFileName, "/iroot/" + _string);
			bool flag = xmlNode != null && num < 8;
			string result;
			if (flag)
			{
				result = xmlNode.ChildNodes[num].InnerText;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public static int UsersInforGetNum()
		{
			return XMLHelper.getNum(XMLHelper.UsersInfoFileName, "/iroot");
		}

		public static string[] VedioConfigeRead(string path, string vedioName)
		{
			string name = vedioName.Replace(" ", "");
			XmlNode xmlNode = XMLHelper.GetXmlNodeByXpath(path, "/vroot/" + name);
			bool flag = xmlNode == null;
			string[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string[] data = new string[xmlNode.ChildNodes.Count];
				for (int i = 0; i < data.Length; i++)
				{
					data[i] = xmlNode.ChildNodes[i].InnerText;
				}
				result = data;
			}
			return result;
		}

		public static void VedioConfigeWrite(string path, Video v)
		{
			string name = v.Name.Replace(" ", "");
			XMLHelper.CreateOrUpdateXmlNodeByXPath(path, "/vroot", name, null);
			XMLHelper.CreateOrUpdateXmlNodeByXPath(path, "/vroot/" + name, "Distance", v.Distance.ToString());
			XMLHelper.CreateOrUpdateXmlNodeByXPath(path, "/vroot/" + name, "Difficulty", v.DiffcultyStr);
		}

		public static void VedioConofigeDelete(string path, string vedioName)
		{
			string name = vedioName.Replace(" ", "");
			XMLHelper.DeleteXmlNodeByXPath(path, "/vroot/" + name);
		}

		public static void CurrentFile(string _string)
		{
			string NowFileName = Environment.CurrentDirectory + "\\" + GlobalData.StartTime + ".xml";
			bool flag = !File.Exists(NowFileName);
			if (flag)
			{
				XMLHelper.CreateXmlDocument(NowFileName, "nroot", "1.0", null, null);
			}
			string temp = null;
			string temp2 = null;
			string temp3 = null;
			string temp4 = null;
			XMLHelper.CreateOrUpdateXmlNodeByXPath(NowFileName, "/nroot", _string, null);
			XMLHelper.CreateOrUpdateXmlNodeByXPath(NowFileName, "/nroot/" + _string, "TrackName", GlobalData.TrackName);
			XMLHelper.CreateOrUpdateXmlNodeByXPath(NowFileName, "/nroot/" + _string, "TrackId", GlobalData.TrackId);
			XMLHelper.CreateOrUpdateXmlNodeByXPath(NowFileName, "/nroot/" + _string, "RoutebookId", GlobalData.RoutebookId);
			XMLHelper.CreateOrUpdateXmlNodeByXPath(NowFileName, "/nroot/" + _string, "StartTime", GlobalData.StartTime);
			XMLHelper.CreateOrUpdateXmlNodeByXPath(NowFileName, "/nroot/" + _string, "TotalTime", GlobalData.Time.ToString());
			XMLHelper.CreateOrUpdateXmlNodeByXPath(NowFileName, "/nroot/" + _string, "Distance", GlobalData.distance);
			XMLHelper.CreateOrUpdateXmlNodeByXPath(NowFileName, "/nroot/" + _string, "Velocity", GlobalData.velocity);
			int i = 0;
			while ((long)i < GlobalData.Time)
			{
				temp += GlobalData.SpeedDatas[i];
				temp2 += GlobalData.HeartRateData[i];
				temp3 += GlobalData.CadenceData[i];
				temp4 += GlobalData.PowerData[i];
				i++;
			}
			XMLHelper.CreateOrUpdateXmlNodeByXPath(NowFileName, "/nroot/" + _string, "SpeedData", temp);
			XMLHelper.CreateOrUpdateXmlNodeByXPath(NowFileName, "/nroot/" + _string, "HeartRateData", temp2);
			XMLHelper.CreateOrUpdateXmlNodeByXPath(NowFileName, "/nroot/" + _string, "CadenceData", temp3);
			XMLHelper.CreateOrUpdateXmlNodeByXPath(NowFileName, "/nroot/" + _string, "PowerData", temp4);
		}

		public static string CurrentReader(string CurrentFile, string _string, int num)
		{
			XmlNode xmlNode = XMLHelper.GetXmlNodeByXpath(CurrentFile, "/nroot/" + _string);
			bool flag = xmlNode != null && num < 11;
			string result;
			if (flag)
			{
				result = xmlNode.ChildNodes[num].InnerText;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public static void SpeedDeviceNumberFile(string _string)
		{
			XMLHelper.CreateOrUpdateXmlNodeByXPath(XMLHelper.SpeedDeviceNumber, "/kroot", _string, null);
			XMLHelper.CreateOrUpdateXmlNodeByXPath(XMLHelper.SpeedDeviceNumber, "/kroot/" + _string, "SpeedDeviceNumber", GlobalData.speedequipment);
		}

		public static string SpeedDeviceNumberReader(string _string, int num)
		{
			XmlNode xmlNode = XMLHelper.GetXmlNodeByXpath(XMLHelper.SpeedDeviceNumber, "/kroot/" + _string);
			bool flag = xmlNode != null && num < 1;
			string result;
			if (flag)
			{
				result = xmlNode.ChildNodes[num].InnerText;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public static int SpeedDeviceNumberGetNum()
		{
			return XMLHelper.getNum(XMLHelper.SpeedDeviceNumber, "/kroot");
		}

		public static void SpeedDeviceNumberDeleter(string _string)
		{
			XMLHelper.DeleteXmlNodeByXPath(XMLHelper.SpeedDeviceNumber, "/kroot/" + _string);
		}

		public static void HeartRateDeviceNumberFile(string _string)
		{
			XMLHelper.CreateOrUpdateXmlNodeByXPath(XMLHelper.HeartRateDeviceNumber, "/oroot", _string, null);
			XMLHelper.CreateOrUpdateXmlNodeByXPath(XMLHelper.HeartRateDeviceNumber, "/oroot/" + _string, "HeartRateDeviceNumber", GlobalData.heartRateequipment);
		}

		public static string HeartRateDeviceNumberReader(string _string, int num)
		{
			XmlNode xmlNode = XMLHelper.GetXmlNodeByXpath(XMLHelper.HeartRateDeviceNumber, "/oroot/" + _string);
			bool flag = xmlNode != null && num < 1;
			string result;
			if (flag)
			{
				result = xmlNode.ChildNodes[num].InnerText;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public static int HeartRateDeviceNumberGetNum()
		{
			return XMLHelper.getNum(XMLHelper.HeartRateDeviceNumber, "/oroot");
		}

		public static void HeartRateDeviceNumberDeleter(string _string)
		{
			XMLHelper.DeleteXmlNodeByXPath(XMLHelper.HeartRateDeviceNumber, "/oroot/" + _string);
		}

		public static void CadenceDeviceNumberFile(string _string)
		{
			XMLHelper.CreateOrUpdateXmlNodeByXPath(XMLHelper.CadenceDeviceNumber, "/broot", _string, null);
			XMLHelper.CreateOrUpdateXmlNodeByXPath(XMLHelper.CadenceDeviceNumber, "/broot/" + _string, "CadenceDeviceNumber", GlobalData.cadenceequipment);
		}

		public static string CadenceDeviceNumberReader(string _string, int num)
		{
			XmlNode xmlNode = XMLHelper.GetXmlNodeByXpath(XMLHelper.CadenceDeviceNumber, "/broot/" + _string);
			bool flag = xmlNode != null && num < 1;
			string result;
			if (flag)
			{
				result = xmlNode.ChildNodes[num].InnerText;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public static int CadenceDeviceNumberGetNum()
		{
			return XMLHelper.getNum(XMLHelper.CadenceDeviceNumber, "/broot");
		}

		public static void CadenceDeviceNumberDeleter(string _string)
		{
			XMLHelper.DeleteXmlNodeByXPath(XMLHelper.CadenceDeviceNumber, "/broot/" + _string);
		}

		public static XmlNode GetXmlNodeByXpath(string xmlFileName, string xpath)
		{
			XmlDocument xmlDoc = new XmlDocument();
			XmlNode result;
			try
			{
				xmlDoc.Load(xmlFileName);
				XmlNode xmlNode = xmlDoc.SelectSingleNode(xpath);
				result = xmlNode;
			}
			catch (Exception ex_1C)
			{
				result = null;
			}
			return result;
		}

		public static XmlNodeList GetXmlNodeListByXpath(string xmlFileName, string xpath)
		{
			XmlDocument xmlDoc = new XmlDocument();
			XmlNodeList result;
			try
			{
				xmlDoc.Load(xmlFileName);
				XmlNodeList xmlNodeList = xmlDoc.SelectNodes(xpath);
				result = xmlNodeList;
			}
			catch (Exception ex_1C)
			{
				result = null;
			}
			return result;
		}

		public static XmlAttribute GetXmlAttribute(string xmlFileName, string xpath, string xmlAttributeName)
		{
			string content = string.Empty;
			XmlDocument xmlDoc = new XmlDocument();
			XmlAttribute xmlAttribute = null;
			try
			{
				xmlDoc.Load(xmlFileName);
				XmlNode xmlNode = xmlDoc.SelectSingleNode(xpath);
				bool flag = xmlNode != null;
				if (flag)
				{
					bool flag2 = xmlNode.Attributes.Count > 0;
					if (flag2)
					{
						xmlAttribute = xmlNode.Attributes[xmlAttributeName];
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return xmlAttribute;
		}

		public static bool CreateXmlDocument(string xmlFileName, string rootNodeName, string version, string encoding, string standalone)
		{
			bool isSuccess = false;
			try
			{
				XmlDocument xmlDoc = new XmlDocument();
				XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration(version, encoding, standalone);
				XmlNode root = xmlDoc.CreateElement(rootNodeName);
				xmlDoc.AppendChild(xmlDeclaration);
				xmlDoc.AppendChild(root);
				xmlDoc.Save(xmlFileName);
				isSuccess = true;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return isSuccess;
		}

		public static bool CreateXmlNodeByXPath(string xmlFileName, string xpath, string xmlNodeName, string innerText, string xmlAttributeName, string value)
		{
			bool isSuccess = false;
			XmlDocument xmlDoc = new XmlDocument();
			try
			{
				xmlDoc.Load(xmlFileName);
				XmlNode xmlNode = xmlDoc.SelectSingleNode(xpath);
				bool flag = xmlNode != null;
				if (flag)
				{
					XmlElement subElement = xmlDoc.CreateElement(xmlNodeName);
					subElement.InnerXml = innerText;
					bool flag2 = !string.IsNullOrEmpty(xmlAttributeName) && !string.IsNullOrEmpty(value);
					if (flag2)
					{
						XmlAttribute xmlAttribute = xmlDoc.CreateAttribute(xmlAttributeName);
						xmlAttribute.Value = value;
						subElement.Attributes.Append(xmlAttribute);
					}
					xmlNode.AppendChild(subElement);
				}
				xmlDoc.Save(xmlFileName);
				isSuccess = true;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return isSuccess;
		}

		public static bool CreateOrUpdateXmlNodeByXPath(string xmlFileName, string xpath, string xmlNodeName, string innerText)
		{
			bool isSuccess = false;
			bool isExistsNode = false;
			XmlDocument xmlDoc = new XmlDocument();
			try
			{
				xmlDoc.Load(xmlFileName);
				XmlNode xmlNode = xmlDoc.SelectSingleNode(xpath);
				bool flag = xmlNode != null;
				if (flag)
				{
					foreach (XmlNode node in xmlNode.ChildNodes)
					{
						bool flag2 = node.Name.ToLower() == xmlNodeName.ToLower();
						if (flag2)
						{
							node.InnerXml = innerText;
							isExistsNode = true;
							break;
						}
					}
					bool flag3 = !isExistsNode;
					if (flag3)
					{
						XmlElement subElement = xmlDoc.CreateElement(xmlNodeName);
						subElement.InnerXml = innerText;
						xmlNode.AppendChild(subElement);
					}
				}
				xmlDoc.Save(xmlFileName);
				isSuccess = true;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return isSuccess;
		}

		public static bool CreateOrUpdateXmlAttributeByXPath(string xmlFileName, string xpath, string xmlAttributeName, string value)
		{
			bool isSuccess = false;
			bool isExistsAttribute = false;
			XmlDocument xmlDoc = new XmlDocument();
			try
			{
				xmlDoc.Load(xmlFileName);
				XmlNode xmlNode = xmlDoc.SelectSingleNode(xpath);
				bool flag = xmlNode != null;
				if (flag)
				{
					foreach (XmlAttribute attribute in xmlNode.Attributes)
					{
						bool flag2 = attribute.Name.ToLower() == xmlAttributeName.ToLower();
						if (flag2)
						{
							attribute.Value = value;
							isExistsAttribute = true;
							break;
						}
					}
					bool flag3 = !isExistsAttribute;
					if (flag3)
					{
						XmlAttribute xmlAttribute = xmlDoc.CreateAttribute(xmlAttributeName);
						xmlAttribute.Value = value;
						xmlNode.Attributes.Append(xmlAttribute);
					}
				}
				xmlDoc.Save(xmlFileName);
				isSuccess = true;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return isSuccess;
		}

		public static bool DeleteXmlNodeByXPath(string xmlFileName, string xpath)
		{
			bool isSuccess = false;
			XmlDocument xmlDoc = new XmlDocument();
			try
			{
				xmlDoc.Load(xmlFileName);
				XmlNode xmlNode = xmlDoc.SelectSingleNode(xpath);
				bool flag = xmlNode != null;
				if (flag)
				{
					xmlNode.ParentNode.RemoveChild(xmlNode);
				}
				xmlDoc.Save(xmlFileName);
				isSuccess = true;
			}
			catch (Exception ex_3E)
			{
			}
			return isSuccess;
		}

		public static bool DeleteXmlAttributeByXPath(string xmlFileName, string xpath, string xmlAttributeName)
		{
			bool isSuccess = false;
			bool isExistsAttribute = false;
			XmlDocument xmlDoc = new XmlDocument();
			try
			{
				xmlDoc.Load(xmlFileName);
				XmlNode xmlNode = xmlDoc.SelectSingleNode(xpath);
				XmlAttribute xmlAttribute = null;
				bool flag = xmlNode != null;
				if (flag)
				{
					foreach (XmlAttribute attribute in xmlNode.Attributes)
					{
						bool flag2 = attribute.Name.ToLower() == xmlAttributeName.ToLower();
						if (flag2)
						{
							xmlAttribute = attribute;
							isExistsAttribute = true;
							break;
						}
					}
					bool flag3 = isExistsAttribute;
					if (flag3)
					{
						xmlNode.Attributes.Remove(xmlAttribute);
					}
				}
				xmlDoc.Save(xmlFileName);
				isSuccess = true;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return isSuccess;
		}

		public static bool DeleteAllXmlAttributeByXPath(string xmlFileName, string xpath)
		{
			bool isSuccess = false;
			XmlDocument xmlDoc = new XmlDocument();
			try
			{
				xmlDoc.Load(xmlFileName);
				XmlNode xmlNode = xmlDoc.SelectSingleNode(xpath);
				bool flag = xmlNode != null;
				if (flag)
				{
					xmlNode.Attributes.RemoveAll();
				}
				xmlDoc.Save(xmlFileName);
				isSuccess = true;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return isSuccess;
		}
	}
}
