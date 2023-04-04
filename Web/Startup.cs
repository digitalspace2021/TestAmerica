using Business.Implementations;
using Business.Interfaces;
using Data.Implementations;
using Data.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TestAmerica.Entity.Contexts;

namespace Web
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        /// <summary>
        /// Constuctor
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// Configuration
        /// </summary>
        public IConfiguration Configuration { get; }
        /// <summary>
        /// Configure Services
        /// </summary>
        /// <param name="services"></param>
        /// 
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
              options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
          );

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.IgnoreNullValues = true;
                options.JsonSerializerOptions.WriteIndented = true;
            });
            services.AddDbContext<ApplicationDbContext>();

            //Swagger
            services.AddSwaggerGen(doc =>
            {
                doc.SwaggerDoc("v1", new OpenApiInfo { Title = "TestAmerica", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPatch = Path.Combine(AppContext.BaseDirectory, xmlFile);
                doc.IncludeXmlComments(xmlPatch);
            });


            services.AddTransient<IDepartamentoBusiness, DepartamentoBusiness>();
            services.AddTransient<IDepartamentoData, DepartamentoData>();
            services.AddTransient<ICiudadBusiness, CiudadBusiness>();
            services.AddTransient<ICiudadData, CiudadData>();
            services.AddTransient<IProductoBusiness, ProductoBusiness>();
            services.AddTransient<IProductoData, ProductoData>();
            services.AddTransient<IItemBusiness, ItemBusiness>();
            services.AddTransient<IItemData, ItemData>();
            services.AddTransient<IPedidoBusiness, PedidoBusiness>();
            services.AddTransient<IPedidoData, PedidoData>();
            services.AddTransient<IVendedorBusiness, VendedorBusiness>();
            services.AddTransient<IVendedorData, VendedorData>();
            services.AddTransient<IClienteBusiness, ClienteBusiness>();
            services.AddTransient<IClienteData, ClienteData>();


            services.AddAuthorization();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });
            app.UseAuthentication();


            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "MercadosBD V1");
                options.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();
            //app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseCors(MyAllowSpecificOrigins);
        }
    }
}
