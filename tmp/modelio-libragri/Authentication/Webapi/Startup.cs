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

using Core.Webapi.Middleware;

using Libragri.AuthenticationDomain.IServices;
using Libragri.AuthenticationDomain.Services;
using Libragri.AuthenticationDomain.IRepositories;
using Libragri.AuthenticationDomain.RepositoriesNH;
using Libragri.AuthenticationDomain.Webapi.Model;

//using MongoDB.Driver;

namespace Libragri.AuthenticationDomain.Webapi
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
        	
            
        	services.AddScoped<IAuthenticationUnitOfWork, AuthenticationUnitOfWorkNH>();

            // var conectionStr = Configuration.GetSection("StoreSettings").GetValue("ConnectionStr","");
            // var dbName=Configuration.GetSection("StoreSettings").GetValue("DatabaseName","");
            // IMongoClient cli  =  new MongoClient(conectionStr);

            // services.AddSingleton<IMongoDatabase>(cli.GetDatabase(dbName));
            // services.AddScoped<IAuthenticationUnitOfWork, AuthenticationUnitOfWorkMongodb>();
        	
        	services.AddScoped<IProfileService, ProfileService>();
        	services.AddScoped<IUserService, UserService>();
        	services.AddScoped<IUserActivationRequestService, UserActivationRequestService>();
        	services.AddScoped<IUserRefreshTokenService, UserRefreshTokenService>();
        	services.AddScoped<IResetPwdRequestService, ResetPwdRequestService>();
        	services.AddScoped<IUserEventService, UserEventService>();

            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options => { options.SerializerSettings.Converters.Add(new NhProxyJsonConverter()); })
                .AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            
            services.AddOptions();
            services.Configure<IdentityProviderSettings>(Configuration.GetSection("IdentityProviderSettings"));
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
