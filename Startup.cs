using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using realtime.AllHubContexts;
using realtime.Interfaces;
using realtime.Services;
using RealTime.HostedServices;
using RealTime.Models;
using RealTime.Models.DbContexts;
using RealTime.Models.Handlers;
using RealTime.Services;
using StackExchange.Redis;

namespace RealTime
{
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

       
        public void ConfigureServices(IServiceCollection services)
        {
            const string dbserver = "DbServerLocal";
            services.AddDbContext<RealTimeDbContext>(options => {
                options.EnableSensitiveDataLogging();
                options.UseNpgsql(Configuration.GetConnectionString (dbserver));
            });
            

            services.AddIdentity<AppUser,IdentityRole>()
                .AddEntityFrameworkStores<RealTimeDbContext>();

            services.Configure<IdentityOptions>(options=>
            {
                options.Password.RequiredLength=3;
                options.Password.RequireDigit=false;
                options.Password.RequireLowercase=false;
                options.Password.RequireNonAlphanumeric=false;
                options.Password.RequireUppercase=false;
            });

            services.AddMemoryCache();
            
            services.AddSingleton<InMemoryCacheService>();

            services.AddSession(session=>{
                session.Cookie.Name="aRealtimeSession";
                session.IdleTimeout=TimeSpan.FromMinutes(10);
                
            });

            services.AddRouting(routing=>{
                routing.LowercaseUrls=true;
            });

            
           /*  services.AddDistributedRedisCache(options=>{
                options.Configuration="localhost";
                options.InstanceName="RealTimeInstance";
            }); */
            services.AddSingleton<IRedisCache,RedisCacheService>();

            services.AddSingleton<IConnectionMultiplexer>(options => {
                return ConnectionMultiplexer.Connect(Configuration.GetConnectionString("RedisCache"));
            });

            services.AddSignalR();
            services.ConfigureApplicationCookie(options=>{
                options.Cookie.Name="aRihannaCookieRealtime";
                options.Cookie.MaxAge=TimeSpan.FromHours(2);
                options.LoginPath="/login";
                options.LogoutPath="/logout";
            });
            services.AddAuthentication();

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddSingleton<IMessageSaver, MessageSaver>();
            services.AddHostedService<PeriodicSaveToDbHostedService>();
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();
        }

        
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {

            if (env.IsDevelopment()) 
            {
                //app.UseDeveloperExceptionPage();
                //app.UseExceptionHandler();
                app.UseExceptionHandler("/Home/Error");


            } else 
            {

                app.UseExceptionHandler ("/Home/Error");
                
                app.UseHsts ();
            }

            //app.UseHttpsRedirection ();
            app.UseSession();
            app.UseStaticFiles ();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization ();

            app.UseEndpoints (endpoints => {
                endpoints.MapControllerRoute (
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapHub<GameHubContext>("/gameHub");
                endpoints.MapHub<SingleChatHubContext>("/dmChatHub");
            });
        }
    }
}