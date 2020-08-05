using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using realtime.AllHubContexts;
using realtime.Areas.Account.Models.DbContexts;
using realtime.Interfaces;
using realtime.Models.DbContexts;
using realtime.Services;
using RealTime.Areas.Account.Models;
using StackExchange.Redis;

namespace RealTime {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

       
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AuthDbContext>(options => {
                options.UseSqlServer (Configuration.GetConnectionString ("SqlServerLocal"));
            });
            

            services.AddDbContext<RealTimeContext>(options => {
                options.UseSqlServer (Configuration.GetConnectionString ("SqlServerLocal"));
            });

            services.AddIdentity<AppUser, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<AuthDbContext>();

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
                session.Cookie.Name="RealtimeSession";
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
                options.Cookie.Name="RihannaCookieRealtime";
                options.Cookie.MaxAge=TimeSpan.FromHours(2);
                options.LoginPath="/login";
                options.LogoutPath="/logout";
            });
            services.AddAuthentication();

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
        }

        
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {

            if (env.IsDevelopment()) 
            {
                app.UseDeveloperExceptionPage();

            } else 
            {

                app.UseExceptionHandler ("/Home/Error");
                
                app.UseHsts ();
            }

            app.UseHttpsRedirection ();
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