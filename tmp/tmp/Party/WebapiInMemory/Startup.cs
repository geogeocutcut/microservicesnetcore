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
using Libragri.partyDomain.RepositoriesInMemory;
using Unity;

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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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

        public void ConfigureContainer(IUnityContainer container)
        {
            // Could be used to register more types

            var store = new StoreInMemory<Guid>();

        	container.RegisterInstance<IStore<Guid>>(store);

        	container.RegisterType<IUnitOfWork, UnitOfWorkInMemory>();
        	
        	container.RegisterType<IPartyService, PartyService>();
        	container.RegisterType<IUserService, UserService>();
        	container.RegisterType<IPartyRoleService, PartyRoleService>();
        	container.RegisterType<IRelationshipService, RelationshipService>();
        	container.RegisterType<IRoleService, RoleService>();
        	container.RegisterType<IRoleEnumService, RoleEnumService>();
        }
    }
}
