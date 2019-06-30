using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PN.WebAPI.DAL;
using PN.WebAPI.Helpers;
using PN.WebAPI.Manager;

namespace PN.WebAPI
{
   public class Startup
   {
      public Startup(IConfiguration configuration)
      {
         Configuration = configuration;
      }

      public IConfiguration Configuration { get; }

      // This method gets called by the runtime. Use this method to add services to the container.
      public void ConfigureServices(IServiceCollection services)
      {
         services.AddEntityFrameworkNpgsql().AddDbContext<EFContext>().BuildServiceProvider();
         services.AddTransient<IUserManager, UserManager>();
         services.AddScoped<IUserAccess, UserAccess>();
         services.AddScoped<IActiveUserAccess, ActiveUserAccess>();

         var appSettingsSection = Configuration.GetSection("AppSettings");
         services.Configure<AppSettings>(appSettingsSection);
       
         var appSettings = appSettingsSection.Get<AppSettings>();
         var key = Encoding.ASCII.GetBytes(appSettings.Secret);
         services.AddAuthentication(x => {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
         })
           .AddJwtBearer(x => {
              x.Events = new JwtBearerEvents
              {
                 OnTokenValidated = context =>
                 {
                    var userService = context.HttpContext.RequestServices.GetRequiredService<IUserManager>();
                    var userID = int.Parse(context.Principal.Identity.Name);
                    var user = userService.GetUserById(userID);
                    if (user == null)
                       context.Fail("Unauthorized");

                    return Task.CompletedTask;
                 }

              };
              x.RequireHttpsMetadata = false;
              x.SaveToken = true;
              x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
              {
                 ValidateIssuerSigningKey = true,
                 IssuerSigningKey = new SymmetricSecurityKey(key),
                 ValidateIssuer = false,
                 ValidateAudience = false
              };
           });

         services.AddSignalR();

            services.AddMvc();
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IHostingEnvironment env)
      {
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
         }
         app.UseAuthentication();  
         app.UseSignalR(routes => { routes.MapHub<MessageHub>("/messageHub"); });
         app.UseMvc();
      }
   }
}
