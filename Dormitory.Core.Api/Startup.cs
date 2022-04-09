using Dormitory.Core.Application.Catalog.CoreRepository;
using Dormitory.Core.Application.Catalog.ProcessRepository;
using Dormitory.Domain.Shared.Constant;
using Dormitory.EntityFrameworkCore.AdminEntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Core.Api
{
    public class Startup
    {
        readonly string MyAllowSpecificorigin = "_myAllowSpecificorigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddAuthentication("Oauth")
                .AddJwtBearer("Oauth", config =>
                {
                    var secretBytes = Encoding.UTF8.GetBytes(CoreConstant.Secret);
                    var key = new SymmetricSecurityKey(secretBytes);

                    config.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = CoreConstant.Issuer,
                        ValidAudience = CoreConstant.Audiance,
                        IssuerSigningKey = key
                    };
                });

            services.AddCors(option =>
            {
                option.AddPolicy("_myAllowSpecificorigins",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyMethod()
                    );
            });

            services.AddDbContext<AdminSolutionDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("AdminDatabase")));

            services.AddTransient<ICoreRepo, CoreRepo>();
            services.AddTransient<IProcessRepo, ProcessRepo>();

            services.AddControllersWithViews();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Dormitory.Core.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dormitory.Core.Api v1"));
            }

            app.UseRouting();
            app.UseCors(builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.AllowAnyOrigin();
            });
            app.UseCors(MyAllowSpecificorigin);

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
