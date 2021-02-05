using Api.Mapper.CategorieMapping;
using ApplicationLayer.DataBase;
using AutoMapper;
using Domain.IRepository;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api
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
            services.AddControllers();
            services.AddDbContext<ApplicationDbContext>(options =>
                                                        options.UseSqlServer(Configuration.GetConnectionString("constr"))
                                                        );
            services.AddScoped<ICategorieRepository, CategorieRepository>();
            services.AddAutoMapper(typeof(CategorieMappings));
            services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("ShopInNet", new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "Shop In Net" ,
                        Version = "1",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                        {
                            Email = "Cherifimahmoud97@gmail.com",
                            Name = "Cherifi Mahmoud",
                            Url = new Uri("https://www.linkedin.com/in/cherifi-mahmoud/")
                        },
                        Description = "Small back-end project <online shop>." +
                                      "I used in this project { Asp.net Core 3.1 || Domain Driven Design || Restful Web Api || Entity Framework Core || Mapper || Swagger Documentation }"

                    });
                }
                );

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/ShopInNet/swagger.json", "Shop In Net");
                options.RoutePrefix = "";
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
