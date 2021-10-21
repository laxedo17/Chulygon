using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chulygon.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;

namespace Chulygon
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ChulygonContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("ChulygonConnection")));

            services.AddControllers().AddNewtonsoftJson(s => s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());//necesario para usar NewtonsoftJson e usar Patch, un pouco lioso pero mellora en versions posteriores do framework

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); //activamos Automapper disponhible a traves de Dependency Injection ao resto da aplicacion

            //services.AddScoped<IChulygonRepo, MockChulygonRepo>();

            services.AddScoped<IChulygonRepo, SqlChulygonRepo>(); //cambiamos a chamada que tinhamos aos datos a man a unha chamada a Base de Datos real. Tendo unha interface, cambiando unha linha, xa chega, incluso cambiando de BD a unha SQLite ou o que sexa, coa interface de base a implementacion e so cambiar unha linha aqui, moi facil
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
