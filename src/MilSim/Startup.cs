using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MilSim.Data;
using MilSim.Models;
using MilSim.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using AspNet.Security.OpenId.Steam;
using AspNet.Security.OpenId;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication;

namespace MilSim
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc(options => {
                options.Filters.Add( new RequireHttpsAttribute() );
            } );

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseIdentity();

            app.UseCookieAuthentication( new CookieAuthenticationOptions()
            {
                AutomaticAuthenticate = true,
                CookieName = "MyApp",
                CookieSecure = CookieSecurePolicy.Always,
                AuthenticationScheme = "Cookies"
            } );

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap = new Dictionary<string, string>();

            //TODO Facebook
            //app.UseFacebookAuthentication( new FacebookOptions() {
            //    AppId = Configuration[ "Authentication:Facebook:AppId" ],
            //    AppSecret = Configuration[ "Authentication:Facebook:AppSecret" ]
            //} );

            //TODO byta till identityserver4 ?
            //STEAM OPTIONS
            var options = new SteamAuthenticationOptions {
                ApplicationKey = "1678D429F67B9AC88EC696082AFE5F06",
                CallbackPath = new PathString( "/LoginSteam" ),
                Events = new OpenIdAuthenticationEvents {
                    OnAuthenticated = context => {
                        var identity = context.Ticket.Principal.Identity as ClaimsIdentity;

                        var subject = identity.Claims.FirstOrDefault( z => z.Type.Contains("nameidentifier" ));

                       // var newIdentity = new ClaimsIdentity(
                       //context.Ticket.AuthenticationScheme,
                       //"given_name",
                       //"role" );

                       // context.Ticket = new AuthenticationTicket(
                       // new ClaimsPrincipal( newIdentity ),
                       // context.Ticket.Properties,
                       // context.Ticket.AuthenticationScheme );
                        
                        //string role;
                        //if( context.Attributes.TryGetValue( "role", out role ) ) {
                        //    context.Identity.AddClaim( new Claim( ClaimTypes.Role, role ) );
                        //}

                        return Task.FromResult( 0 );
                    }
                }
            };

            app.UseSteamAuthentication( options );

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
