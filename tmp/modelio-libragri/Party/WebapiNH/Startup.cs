using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Core.Common;
using Core.Repository;
using Core.Webapi.Middleware;

using Libragri.PartyDomain.Model;
using Libragri.PartyDomain.IServices;
using Libragri.PartyDomain.Services;
using Libragri.PartyDomain.IRepositories;
using Libragri.PartyDomain.RepositoriesNH;
using NHibernate;

namespace Libragri.PartyDomain.Webapi
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
        	

        	services.AddScoped<IPartyUnitOfWork, PartyUnitOfWorkNH>();
        	
        	services.AddScoped<IUserDataService, UserDataService>();
        	services.AddScoped<IUserActivationRequestService, UserActivationRequestService>();
        	services.AddScoped<IUserRefreshTokenService, UserRefreshTokenService>();
        	services.AddScoped<IResetPwdRequestService, ResetPwdRequestService>();
        	services.AddScoped<IUserEventService, UserEventService>();
        	services.AddScoped<IPartyService, PartyService>();
        	services.AddScoped<ICountryService, CountryService>();

            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options => { options.SerializerSettings.Converters.Add(new NhProxyJsonConverter()); })
                .AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
			app.UseMiddleware<ExceptionMiddleware>();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
