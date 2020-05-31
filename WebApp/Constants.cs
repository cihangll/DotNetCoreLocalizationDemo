using System.Collections.Generic;
using System.Globalization;

namespace WebApp
{
	public class Constants
	{
		public const string DefaultCulture = "tr";
		public static IEnumerable<CultureInfo> GetSupportedCultures()
		{
			yield return new CultureInfo("tr");
			yield return new CultureInfo("en");
		}
	}
}
