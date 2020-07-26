using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using Web.Models.Mapping;
using Web.InfrastructureServices.Filters;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.Web.Services;
using CleanArchitecture.Web.Interfaces;
using CleanArchitecture.Infrastructure.Repository;
using CleanArchitecture.Infrastructure.DataContext;
using Microsoft.Extensions.Logging;
using CleanArchitecture.Infrastructure.DataSeed;
using CleanArchitecture.Core.Interfaces.Repositories;

namespace Web {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {
            services.AddLogging();
            services.AddDependencies();
            services.AddDatabases(Configuration);
            services.AddIdentity();
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddCustomMVC();
            services.AddResponseCaching();
            services.AddNewtonsoftJson();
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            services.AddHttpContextAccessor();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory logger) {
            if (env.IsDevelopment()) {
                app.EnsureDatabaseIsCreatedAndSeeded();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            } else {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
                app.UseCookiePolicy(new CookiePolicyOptions {
                    HttpOnly = HttpOnlyPolicy.Always,
                    Secure = CookieSecurePolicy.Always,
                    MinimumSameSitePolicy = SameSiteMode.None
                });
            }

            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseResponseCaching();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapRazorPages();
            });
        }
    }

    public static class CustomExtensionMethods {
        public static void AddDependencies(this IServiceCollection services) {
            services.AddSingleton<SqlLiteConnectionFactory>();
            services.AddScoped<GlobalPurposeFilter>();
            services.AddScoped<ITeamPageService, TeamPageService>();
            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddAutoMapper(typeof(Startup));
        }

        public static void AddCustomMVC(this IServiceCollection services) {
            services.AddControllers(options => {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
            });

            services.AddMvc(options => {
                options.Filters.AddService<GlobalPurposeFilter>();
                options.CacheProfiles.Add("Default_30",
                    new CacheProfile() {
                        Duration = 30
                    });
            });

            services.AddMvc().AddRazorPagesOptions(options => {
                options.Conventions.AddPageRoute("/Team/Index", "");
            })
            .AddFluentValidation(options => {
                options.RegisterValidatorsFromAssemblyContaining<Startup>();
            });

            services.AddCors(options => {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
        }

        public static void AddDatabases(this IServiceCollection services, IConfiguration configuration) {
            var connectionFactory = services.BuildServiceProvider().GetService<SqlLiteConnectionFactory>();
            services.AddDbContext<ApplicationDataContext>(options => 
                options.UseSqlite(connectionFactory.Create(configuration.GetConnectionString("ApplicationConnection"))));
            services.AddDbContext<IdentityDataContext>(options => 
                options.UseSqlite(connectionFactory.Create(configuration.GetConnectionString("IdentityConnection"))));
        }

        public static void AddIdentity(this IServiceCollection services) {
            services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<IdentityDataContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options => {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
            });
        }

        public static void AddNewtonsoftJson(this IServiceCollection services) {
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
        }
    }
}
