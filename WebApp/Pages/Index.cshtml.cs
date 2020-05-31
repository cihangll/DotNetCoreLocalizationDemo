using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace WebApp.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;
		private readonly IStringLocalizer<IndexModel> _stringLocalizer;

		public IndexModel(ILogger<IndexModel> logger, IStringLocalizer<IndexModel> stringLocalizer)
		{
			_logger = logger;
			_stringLocalizer = stringLocalizer;
		}

		public void OnGet()
		{
			_logger.LogInformation(_stringLocalizer["About Title"]);
		}
	}
}
