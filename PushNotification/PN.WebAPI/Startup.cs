using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PN.WebAPI.DAL;
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

         // services.AddScoped<IMessageHub, MessageHub>();
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
         app.UseSignalR(routes => { routes.MapHub<MessageHub>("/messageHub"); });
         app.UseMvc();
      }
   }
}
