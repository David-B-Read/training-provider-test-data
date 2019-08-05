
namespace TrainingProviderTestData.Web
{
    using System;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using StructureMap;
    using Application.Configuration;
    using Application.Interfaces;
    using Application.Importers;
    using Application.Repositories;

    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            return ConfigureIoC(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routes => { routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}"); });
        }

        private IServiceProvider ConfigureIoC(IServiceCollection services)
        {
            var container = new Container();
            container.Configure(config =>
            {
                config.Populate(services);
                config.For<IApplicationConfiguration>().Use(x => ConfigurationFactory.GetApplicationConfiguration(Configuration));
                config.For<ITestDataRepository>().Use<TestDataRepository>();
                config.For<IUkrlpDataImporter>().Use<UkrlpDataImporter>();
                config.For<ICompaniesHouseDataImporter>().Use<CompaniesHouseDataImporter>();
                config.For<ICharityDataImporter>().Use<CharityDataImporter>();
            });
            return container.GetInstance<IServiceProvider>();
        }
    }
}
