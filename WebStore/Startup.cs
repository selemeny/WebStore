using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebStore.Controllers;
using WebStore.DAL.Context;
using WebStore.Data;
using WebStore.Domain.Entities.Identity;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Services.InMemory;
using WebStore.Infrastructure.Services.InSQL;

namespace WebStore
{
	public class Startup
	{
		private IConfiguration configuration { get; }

		public Startup(IConfiguration configuration) => this.configuration = configuration;
		
		
		
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<WebStoreDB>(opt =>
				opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
			services.AddTransient<WebStoreDBInitializer>();

			services.AddIdentity<User, Role>()
				.AddEntityFrameworkStores<WebStoreDB>()
				.AddDefaultTokenProviders();

			// ��������� ������������ Identity 
			services.Configure<IdentityOptions>(opt =>
			{
				opt.Password.RequiredLength = 3;	// ����������� ����� ������ = 
				opt.Password.RequireDigit = false;	// � ������ ������ �����?
				opt.Password.RequireUppercase = false;	// � ������ ����� ������� ��������?
				opt.Password.RequireLowercase = true;	// � ������ ����� ������� ��������?
				opt.Password.RequireNonAlphanumeric = false;	// � ������ ����������� ������ ���� ������� �� ��������
				opt.Password.RequiredUniqueChars = 3;	// � ������ ���������� �������� = 

				//opt.User.AllowedUserNameCharacters = "abcdefghjklmnopqrstuvwxyzABCD....0123456789";	// ����� ����������� �������� ��� User
				opt.User.RequireUniqueEmail = false;    // ���������� Email

				//opt.SignIn.;	// ������������� ��������/email/�������� ...

				opt.Lockout.AllowedForNewUsers = true;	// ����������������� ����������� (false - ���� ������� ������������ ������ �����)
				opt.Lockout.MaxFailedAccessAttempts = 10;	// ���������� ����� �� ������� ������ ������������
				opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);	// ����� ���������� ��� �������� ������
			});


			// ��������� ����������� Cookies
			services.ConfigureApplicationCookie(opt =>
			{
				opt.Cookie.Name = "WebStore";	// ��� ����
				opt.Cookie.HttpOnly = true;   // ���� ������ �� Http ���������
				opt.ExpireTimeSpan = TimeSpan.FromDays(10);   // ����� ����� ����

				opt.LoginPath = "/Account/Login";   // ���� � �����������, ����� ���� �� �����������, � ��� ��������� ����� �����������, �� ����� ���� ����� ��������
				opt.LogoutPath = "/Account/Logout"; // ���� ��� ��������������
				opt.AccessDeniedPath = "/Account/AccessDenied"; // ���� �� ������� �����. ����� ����� �������� � �������


				opt.SlidingExpiration = true;	// �������� ������������� ������ �����, ����� ���� ��� �� ���� ��������������, ����� �� ���������������. ������������ ������.

			});



			//services.AddMvc(); // dspjd ��� core 2.2
			services.AddControllersWithViews().AddRazorRuntimeCompilation(); // ��������� �����������
																			 // AddRazorRuntimeCompilation - ��� ��������� ������������� - ������������� ����������������� ��� ���������� ����������


			// ������������ � �������
			// AddTransient - ������ ��� ����� ����������� ��������� �������
			// AddScoped - ���� ��������� �� ������� ���������
			// AddSingleton - ���� ������ �� ��� ����� ����� ����������
			//services.AddTransient<IEmployeesData, InMemoryEmployeesData>();
			//services.AddScoped<IEmployeesData, InMemoryEmployeesData>();
			//services.AddSingleton<IEmployeesData, InMemoryEmployeesData>();
			services.AddScoped<IEmployeesData, SqlEmployeeData>();
			//services.AddSingleton<IProductData, InMemoryProductData>();
			services.AddScoped<IProductData, SqlProductData>();
		}


		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, WebStoreDBInitializer db)
		{
			db.Initialize();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage(); // ���������� ��� ������ ��� ����������
				app.UseBrowserLink();
			}

			app.UseStaticFiles(); // ����������� �����
			app.UseDefaultFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

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
					); // http://localhost:5000/Home/Index/id (Home = Controller Name; Index = Action Name; id = ��������)
			});
		}
	}
}
