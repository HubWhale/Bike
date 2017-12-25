using System;
using System.Linq;
using System.Text;

namespace FinalClient
{
	public class CustomProtocol : ProtocolBase
	{
		public byte[] bytes;

		public override ProtocolBase Decode(byte[] readbuff, int start, int length)
		{
			CustomProtocol protocol = new CustomProtocol();
			protocol.bytes = new byte[length];
			Array.Copy(readbuff, start, protocol.bytes, 0, length);
			return protocol;
		}

		public override byte[] Encode()
		{
			return this.bytes;
		}

		public override string GetName()
		{
			return this.GetString(0);
		}

		public override string GetDesc()
		{
			string str = "";
			bool flag = this.bytes == null;
			string result;
			if (flag)
			{
				result = str;
			}
			else
			{
				for (int i = 0; i < this.bytes.Length; i++)
				{
					int b = (int)this.bytes[i];
					str = str + b.ToString() + " ";
				}
				result = str;
			}
			return result;
		}

		public void AddString(string str)
		{
			byte[] strBytes = Encoding.UTF8.GetBytes(str);
			int len = strBytes.Length;
			byte[] lenBytes = BitConverter.GetBytes(len);
			bool flag = this.bytes == null;
			if (flag)
			{
				this.bytes = lenBytes.Concat(strBytes).ToArray<byte>();
			}
			else
			{
				this.bytes = this.bytes.Concat(lenBytes).Concat(strBytes).ToArray<byte>();
			}
		}

		public string GetString(int start, ref int end)
		{
			bool flag = this.bytes == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				bool flag2 = this.bytes.Length < start + 4;
				if (flag2)
				{
					result = "";
				}
				else
				{
					int strLen = BitConverter.ToInt32(this.bytes, start);
					bool flag3 = this.bytes.Length < start + 4 + strLen;
					if (flag3)
					{
						result = "";
					}
					else
					{
						string str = Encoding.UTF8.GetString(this.bytes, start + 4, strLen);
						end = start + 4 + strLen;
						result = str;
					}
				}
			}
			return result;
		}

		public string GetString(int start)
		{
			int end = 0;
			return this.GetString(start, ref end);
		}

		public void AddInt(int num)
		{
			byte[] numBytes = BitConverter.GetBytes(num);
			bool flag = this.bytes == null;
			if (flag)
			{
				this.bytes = numBytes;
			}
			else
			{
				this.bytes = this.bytes.Concat(numBytes).ToArray<byte>();
			}
		}

		public int GetInt(int start, ref int end)
		{
			bool flag = this.bytes == null;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				bool flag2 = this.bytes.Length < start + 4;
				if (flag2)
				{
					result = 0;
				}
				else
				{
					end = start + 4;
					result = BitConverter.ToInt32(this.bytes, start);
				}
			}
			return result;
		}

		public int GetInt(int start)
		{
			int end = 0;
			return this.GetInt(start, ref end);
		}

		public void AddFloat(float num)
		{
			byte[] numBytes = BitConverter.GetBytes(num);
			bool flag = this.bytes == null;
			if (flag)
			{
				this.bytes = numBytes;
			}
			else
			{
				this.bytes = this.bytes.Concat(numBytes).ToArray<byte>();
			}
		}

		public float GetFloat(int start, ref int end)
		{
			bool flag = this.bytes == null;
			float result;
			if (flag)
			{
				result = 0f;
			}
			else
			{
				bool flag2 = this.bytes.Length < start + 4;
				if (flag2)
				{
					result = 0f;
				}
				else
				{
					end = start + 4;
					result = BitConverter.ToSingle(this.bytes, start);
				}
			}
			return result;
		}

		public float GetFloat(int start)
		{
			int end = 0;
			return this.GetFloat(start, ref end);
		}
	}
}
