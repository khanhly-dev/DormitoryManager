using Dormitory.Admin.Application.Catalog.AreaRepository;
using Dormitory.Admin.Application.Catalog.ContractRepositoty;
using Dormitory.Admin.Application.Catalog.ContractTimeConfigRepository;
using Dormitory.Admin.Application.Catalog.CriteriaRepository;
using Dormitory.Admin.Application.Catalog.Dashboard;
using Dormitory.Admin.Application.Catalog.FacilityRepository;
using Dormitory.Admin.Application.Catalog.RoomRepository;
using Dormitory.Admin.Application.Catalog.ServiceRepository;
using Dormitory.Admin.Application.Catalog.StudentRepository;
using Dormitory.Admin.Application.Catalog.UserRepository;
using Dormitory.Domain.Shared.Constant;
using Dormitory.EntityFrameworkCore.AdminEntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Admin.Api
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

            services.AddTransient<IUserRepo, UserRepo>();
            services.AddTransient<IAreaRepo, AreaRepo>();
            services.AddTransient<IFacilityRepo, FacilityRepo>();
            services.AddTransient<IRoomRepo, RoomRepo>();
            services.AddTransient<IServiceRepo, ServiceRepo>();
            services.AddTransient<ICriteriaRepo, CriteriaRepo>();
            services.AddTransient<IStudentRepo, StudentRepo>();
            services.AddTransient<IContractTimeConfigRepo, ContractTimeConfigRepo>();
            services.AddTransient<IContractRepo, ContractRepo>();
            services.AddTransient<IDashboardRepo, DashboardRepo>();

            services.AddDirectoryBrowser();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Dormitory.Admin.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dormitory.Admin.Api v1"));
            }
            app.UseHttpsRedirection();

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
