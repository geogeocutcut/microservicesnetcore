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
using Core.Webapi;

using Libragri.partyDomain.Model;
using Libragri.partyDomain.IServices;
using Libragri.partyDomain.Services;
using Libragri.partyDomain.IRepositories;
using Libragri.partyDomain.RepositoriesNH;
using NHibernate;

namespace Libragri.partyDomain.Webapi
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
        	

        	services.AddSingleton<NHibernate.Cfg.Configuration>((provider)=>{ 
            var cfg = new NHibernate.Cfg.Configuration();
                cfg.Configure("hibernate.cfg.xml");
                return cfg;
            });

            services.AddSingleton<ISessionFactory>((provider)=>{ 
                var cfg = provider.GetService<NHibernate.Cfg.Configuration>();
                return cfg.BuildSessionFactory();
            });

            services.AddScoped<ISession>((provider)=>{
                var factory = provider.GetService<ISessionFactory>();
                return factory.OpenSession();
            });
            
        	services.AddSingleton<IFactory>(new Factory(services));
        	services.AddScoped<IUnitOfWork, UnitOfWorkNH>();
        	
        	services.AddScoped<IPartyService, PartyService>();
        	services.AddScoped<IUserService, UserService>();
        	services.AddScoped<IPartyRoleService, PartyRoleService>();
        	services.AddScoped<IRelationshipService, RelationshipService>();
        	services.AddScoped<IRoleService, RoleService>();
        	services.AddScoped<IRoleEnumService, RoleEnumService>();

            
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
