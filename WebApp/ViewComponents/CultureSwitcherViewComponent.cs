using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApp.Models;

namespace WebApp.ViewComponents
{
	public class CultureSwitcherViewComponent : ViewComponent
	{
		private readonly IOptions<RequestLocalizationOptions> _localizationOptions;

		public CultureSwitcherViewComponent(IOptions<RequestLocalizationOptions> localizationOptions)
		{
			_localizationOptions = localizationOptions;
		}

		public IViewComponentResult Invoke()
		{
			var cultureFuture = HttpContext.Features.Get<IRequestCultureFeature>();
			var model = new CultureSwitcherModel
			{
				SupportedCultures = _localizationOptions.Value.SupportedUICultures,
				CurrentUICulture = cultureFuture.RequestCulture.UICulture
			};
			return View(model);
		}
	}
}
