using System.Collections.Generic;
using System.Globalization;

namespace WebApp.Models
{
	public class CultureSwitcherModel
	{
		public CultureInfo CurrentUICulture { get; set; }
		public IList<CultureInfo> SupportedCultures { get; set; }
	}
}
