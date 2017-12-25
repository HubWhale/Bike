using System;

namespace FinalClient
{
	public class ProtocolBase
	{
		public virtual ProtocolBase Decode(byte[] readbuff, int start, int length)
		{
			return new ProtocolBase();
		}

		public virtual byte[] Encode()
		{
			return new byte[0];
		}

		public virtual string GetName()
		{
			return "";
		}

		public virtual string GetDesc()
		{
			return "";
		}
	}
}
