using System.IO;
using AutoMapper;
using Orient.Base.Net.Core.Api.Core.Business.Caching;
using Orient.Base.Net.Core.Api.Core.Business.Filters;
using Orient.Base.Net.Core.Api.Core.Business.IoC;
using Orient.Base.Net.Core.Api.Core.Business.Models;
using Orient.Base.Net.Core.Api.Core.Business.Services;
using Orient.Base.Net.Core.Api.Core.Common.Constants;
using Orient.Base.Net.Core.Api.Core.Common.Extensions;
using Orient.Base.Net.Core.Api.Core.DataAccess;
using Orient.Base.Net.Core.Api.Core.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using Serilog.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using Orient.Base.Net.Core.Api.Core.DataAccess.Repositories.Base;
using Orient.Base.Net.Core.Api.Core.Common.Helpers;
using Orient.Base.Net.Core.Api.Core.Business.Profiles;
using System.Linq;
using Orient.Base.Net.Core.Api.Core.Common.Utilities;
using System.Collections.Generic;
using Orient.Base.Net.Core.Api.Core.Business.Services.Hubs;
using System;
using Orient.Base.Net.Core.Api.Core.Entities.Enums;
using RazorLight;
using System.Reflection;
using Orient.Base.Net.Core.Api.Core.Business.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Orient.Base.Net.Core.Api.Core.Business.Filters.Models;
using System.Configuration;


namespace Orient.Base.Net.Core.Api
{
	public class Startup
	{
		/// <summary>
		/// 
		/// </summary>
		public static IConfigurationRoot Configuration;
        private readonly IHostingEnvironment _hostingEnvironment;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="env"></param>
		public Startup(IHostingEnvironment env)
		{
            _hostingEnvironment = env;
            var builder = new ConfigurationBuilder()
			  .SetBasePath(env.ContentRootPath)
			  .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

			builder.AddEnvironmentVariables();
			Configuration = builder.Build();


			var logPath = Configuration["AppSettings:LoggingPath"] + "Orient-{Date}-" + System.Environment.MachineName + ".txt";
			Log.Logger = new LoggerConfiguration()
			  .MinimumLevel.Warning()
			  .WriteTo.RollingFile(logPath, retainedFileCountLimit: 15)
			  .CreateLogger();
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			// Add service and create Policy with options
			services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy",
				  builder => builder.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader()
					.AllowCredentials());
			});

			services.AddMvc().AddJsonOptions(opt =>
			{
				opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
			})
			  .AddJsonOptions(opt =>
			  {
				  opt.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
			  });

			services.AddSingleton(Configuration);
			services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));


			services.AddLogging(loggingBuilder =>
			{
				loggingBuilder.AddSerilog(dispose: true);
				loggingBuilder.SetMinimumLevel(LogLevel.Information);
				loggingBuilder.AddFilter<SerilogLoggerProvider>(null, LogLevel.Trace);
			});
			services.AddSingleton<ILoggerProvider, SerilogLoggerProvider>();

            //Config Automapper map
            Mapper.Initialize(config =>
            {
                config.AddProfile<UserProfile>();
            });

			var conn = Configuration.GetConnectionString("DefaultConnectionString");
			services.AddDbContextPool<OrientNetCoreDbContext>(options => options.UseSqlServer(conn));

			//Register Repository
			services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            //Config Ldap

            //Register Service
            services.AddScoped<IRoleService, RoleService>();
			services.AddScoped<ISSOAuthService, SSOAuthService>();
			services.AddScoped<IUserService, UserService>();

            //Register MemoryCacheManager
            services.AddScoped<ICacheManager, MemoryCacheManager>();
			
			// Set Service Provider for IoC Helper
			IoCHelper.SetServiceProvider(services.BuildServiceProvider());

			services.AddMvc(option =>
			{
				option.Filters.Add<HandleExceptionFilterAttribute>();
			});

			// Register the Swagger generator, defining one or more Swagger documents
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info
				{
					Version = "v1",
					Title = "Beverages API",
					Description = "ASP.NET Core API.",
					TermsOfService = "None",
					Contact = new Contact { Name = "THANH UCHIHA", Email = "thanh.nguyenvan@orientsoftware.com", Url = "" },
				});

				c.DescribeAllParametersInCamelCase();
				c.OperationFilter<AccessTokenHeaderParameterOperationFilter>();

				// Set the comments path for the Swagger JSON and UI.
				var basePath = PlatformServices.Default.Application.ApplicationBasePath;
				var xmlPath = Path.Combine(basePath, "Orient.Base.Net.Core.Api.xml");
				c.IncludeXmlComments(xmlPath);
			});

			services.AddAuthentication(Microsoft.AspNetCore.Server.IISIntegration.IISDefaults.AuthenticationScheme);
            
            // Add RazorLight
            services.AddScoped<IRazorLightEngine>(sp =>
            {
                var engine = new RazorLightEngineBuilder()
                    .UseFilesystemProject(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location))
                    .UseMemoryCachingProvider()
                    .Build();
                return engine;
            });

			// config signal R
			services.AddSignalR();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			// global policy - assign here or on each controller
			app.UseCors("CorsPolicy");

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				loggerFactory.AddSerilog();
				loggerFactory.AddConsole(Configuration.GetSection("Logging"));
				loggerFactory.AddDebug(LogLevel.Debug);
			}
			else if (env.IsProduction())
			{
				loggerFactory.AddSerilog();
				loggerFactory.AddConsole(Configuration.GetSection("Logging"));
				loggerFactory.AddDebug(LogLevel.Warning);
			}

			// Enable middleware to serve generated Swagger as a JSON endpoint.
			app.UseSwagger();

			// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Orient API V1");
			});

 

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "login",
                    template: "login",
                    defaults: new { controller = "api/sso", action = "login" }
                );
            });

            // Auto run migration
            RunMigration(app);

			// Initialize Data
			InitDataRole();
			InitUserAdmin();
			InitUser();

			// config signal R 
			app.UseSignalR(routes =>
			{
				routes.MapHub<NotificationHub>("/chatHub");
			});

        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="app"></param>
		private void RunMigration(IApplicationBuilder app)
		{
			using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
			{
				scope.ServiceProvider.GetRequiredService<OrientNetCoreDbContext>().Database.Migrate();
			}
		}

		private void InitDataRole()
		{
			var roleRepository = IoCHelper.GetInstance<IRepository<Role>>();
			var roles = new[]
			{
				new Role {
					Id = RoleConstants.SuperAdminId,
					Name = "Super Admin"
				},
				new Role
				{
					Id = RoleConstants.BODId,
					Name = "BOD"
				},
				new Role
				{
					Id = RoleConstants.HRMId,
					Name = "HR Manager"
				},
				new Role
				{
					Id = RoleConstants.HRId,
					Name = "HR"
				},
				new Role
				{
					Id = RoleConstants.DevId,
					Name = "Dev"
				}
			};

			roleRepository.GetDbContext().Roles.AddIfNotExist(x => x.Name, roles);
			roleRepository.GetDbContext().SaveChanges();
		}

		private void InitUserAdmin()
		{
			var userRepository = IoCHelper.GetInstance<IRepository<User>>();
			if (userRepository.GetAll().Count() > 1)
			{
				return;// It's already init
			}

			var user = new User();
			user.Name = "Super Admin";
			user.Email = "phung.dinh@orientsoftware.com";
			user.Mobile = "0983260830";

			var password = "orient@123";
			password.GeneratePassword(out string saltKey, out string hashPass);

			user.Password = hashPass;
			user.PasswordSalt = saltKey;

			user.UserInRoles = new List<UserInRole>()
					{
						new UserInRole()
						{
							UserId = UserConstants.SuperAdminUserId,
							RoleId = RoleConstants.SuperAdminId
						}
					};

			var users = new[]
			{
				user
			};

			userRepository.GetDbContext().Users.AddIfNotExist(x => x.Email, users);
			userRepository.GetDbContext().SaveChanges();
		}

		private void InitUser()
		{
			var userRepository = IoCHelper.GetInstance<IRepository<User>>();

			var userDevs = new User[] {
				new User() {
					Email = "quang.nguyen@orientsoftware.com",
					Name = "Quang Nguyen",
				},
				new User() {
					Email = "thanh.nguyenvan@orientsoftware.com",
					Name = "Thanh Nguyen",
				},
				new User() {
					Email = "phuong.trinh@orientsoftware.com",
					Name = "Phuong Trinh",
				}
			};

			var userHRs = new User[] {
				new User() {
					Email = "quan.le@orientsoftware.com",
					Name = "Quan Le",
				},
				new User() {
					Email = "liu.huynh@orientsoftware.com",
					Name = "Liu Huynh",
				}
			};

			var userHRMs = new User[] {
				new User() {
					Email = "thoai.tran@orientsoftware.com",
					Name = "Thoai Tran",
				}
			};

			var userBODs = new User[] {
				new User() {
					Email = "trinh.vo@orientsoftware.com",
					Name = "Trinh Vo",
				}
			};

			foreach (var user in userDevs)
			{
				user.UserInRoles = new List<UserInRole>()
				{
					new UserInRole()
					{
						UserId = user.Id,
						RoleId = RoleConstants.DevId
					}
				};
			}

			foreach (var user in userHRs)
			{
				user.UserInRoles = new List<UserInRole>()
				{
					new UserInRole()
					{
						UserId = user.Id,
						RoleId = RoleConstants.HRId
					}
				};
			}

			foreach (var user in userHRMs)
			{
				user.UserInRoles = new List<UserInRole>()
				{
					new UserInRole()
					{
						UserId = user.Id,
						RoleId = RoleConstants.HRMId
					}
				};
			}

			foreach (var user in userBODs)
			{
				user.UserInRoles = new List<UserInRole>()
				{
					new UserInRole()
					{
						UserId = user.Id,
						RoleId = RoleConstants.BODId
					}
				};
			}

			var users = userDevs.Concat(userHRs).Concat(userHRMs).Concat(userBODs).ToArray();

			foreach (var user in users)
			{
				user.Mobile = "01234567890";

				var password = "orient@123";
				password.GeneratePassword(out string saltKey, out string hashPass);

				user.Password = hashPass;
				user.PasswordSalt = saltKey;
			}

			userRepository.GetDbContext().Users.AddIfNotExist(x => x.Email, users);
			userRepository.GetDbContext().SaveChanges();
		}
	}
}
