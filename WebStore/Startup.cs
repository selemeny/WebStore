using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebStore
{
	public class Startup
	{
		private IConfiguration configuration { get; }

		public Startup(IConfiguration configuration) => this.configuration = configuration;
		
		
		
		public void ConfigureServices(IServiceCollection services)
		{
			//services.AddMvc(); // dspjd дл€ core 2.2
			services.AddControllersWithViews().AddRazorRuntimeCompilation(); // ƒобавл€ем контроллеры
																			 // AddRazorRuntimeCompilation - при изменении представлени€ - автоматически перекомпилируетс€ при запущенном приложении
		}


		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseBrowserLink();
			}

			app.UseStaticFiles(); // —татические файлы
			app.UseDefaultFiles();

			app.UseRouting();


			app.UseEndpoints(endpoints =>
			{
				endpoints.MapGet("/greetings", async context =>
				{
					await context.Response.WriteAsync(configuration["CustomGreetings"]);
				});

				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}"
					); // http://localhost:5000/Home/Index/id (Home = Controller Name; Index = Action Name; id = параметр)
			});
		}
	}
}
