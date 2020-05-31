using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Reflection;
using WebApp.Resources;
using WebApp.RouteModelConventions;
using WebApp.Services;

namespace WebApp
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<RequestLocalizationOptions>(options =>
			{
				var supportedCultures = Constants.GetSupportedCultures().ToList();
				options.DefaultRequestCulture = new RequestCulture(Constants.DefaultCulture);
				// Formatting numbers, dates, etc.
				options.SupportedCultures = supportedCultures;
				// UI strings that we have localized.
				options.SupportedUICultures = supportedCultures;
				options.RequestCultureProviders.Insert(0, new RouteDataRequestCultureProvider { Options = options });
			});

			//AddLocalization Adds the localization services to the services container. The code above also sets the resources path to "Resources".
			//Register IStringLocalization
			services.AddLocalization(options => options.ResourcesPath = "Resources");
			services.AddSingleton<CommonLocalizationService>();

			services.AddHttpContextAccessor();

			services.AddRazorPages(options =>
			{
				options.Conventions.Add(new CultureTemplatePageRouteModelConvention());
			})
			//Register IHtmlLocalization
			//Adds support for localized view files. For example Index.en.cshtml, Index.tr.cshtml
			.AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
			//AddDataAnnotationsLocalization Adds support for localized DataAnnotations validation messages through IStringLocalizer abstractions.
			.AddDataAnnotationsLocalization(options =>
			{
				options.DataAnnotationLocalizerProvider = (type, factory) =>
				{
					var assemblyName = new AssemblyName(typeof(CommonResources).GetTypeInfo().Assembly.FullName);
					return factory.Create(nameof(CommonResources), assemblyName.Name);
				};
			});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			var localizationOptions = app.ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value;
			app.UseRequestLocalization(localizationOptions);

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapRazorPages();
			});
		}
	}
}
