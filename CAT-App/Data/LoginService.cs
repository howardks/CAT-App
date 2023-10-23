namespace CAT_App.Data
{
	/*
	 * This is a mock login system. In production, this would be replaced
	 * with a secure database. 
	 */
	public class LoginService
	{
		public static readonly Dictionary<string, string> Logins = new() 
		{
			{ "Claudia", "abc123" },
			{ "Louie", "pass999" },
			{ "LixinLi", "bestTeacher" }
		};
	}
}
