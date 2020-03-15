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
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Services;

namespace WebStore
{
	public class Startup
	{
		private IConfiguration configuration { get; }

		public Startup(IConfiguration configuration) => this.configuration = configuration;
		
		
		
		public void ConfigureServices(IServiceCollection services)
		{
			//services.AddMvc(); // dspjd для core 2.2
			services.AddControllersWithViews().AddRazorRuntimeCompilation(); // Добавляем контроллеры
																			 // AddRazorRuntimeCompilation - при изменении представления - автоматически перекомпилируется при запущенном приложении


			// Регистрируем в сервисе
			// AddTransient - каждый раз будет создаваться экземпляр сервиса
			// AddScoped - один экземпляр на область видимости
			// AddSingleton - один объект на все время жизни приложения
			//services.AddTransient<IEmployeesData, InMemoryEmployeesData>();
			//services.AddScoped<IEmployeesData, InMemoryEmployeesData>();
			services.AddSingleton<IEmployeesData, InMemoryEmployeesData>();
			services.AddSingleton<IProductData, InMemoryProductData>();
		}


		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage(); // Отображает все ошибки при разработки
				app.UseBrowserLink();
			}

			app.UseStaticFiles(); // Статические файлы
			app.UseDefaultFiles();

			app.UseRouting();

			app.UseWelcomePage("/welcome");

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
