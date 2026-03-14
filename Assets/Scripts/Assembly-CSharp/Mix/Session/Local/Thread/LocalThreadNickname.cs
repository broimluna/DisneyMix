using Disney.Mix.SDK;

namespace Mix.Session.Local.Thread
{
	public class LocalThreadNickname : IChatThreadNickname
	{
		public string Nickname { get; set; }

		public bool Applied
		{
			get
			{
				return true;
			}
		}
	}
}
