using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using xZoneAPI.Data;
using Microsoft.EntityFrameworkCore;
using xZoneAPI.mappers;
using xZoneAPI.Repositories.TaskRepo;
using xZoneAPI.Repositories.Skills;
using xZoneAPI.Repositories.Badges;
using xZoneAPI.Repositories.Ranks;
using xZoneAPI.Repositories.SectionRepo;
using xZoneAPI.Repositories.TaskRepo;
using xZoneAPI.Repositories.ProjectRepo;
using xZoneAPI.Repositories.RoadmapRepo;
using xZoneAPI.Logic;
using xZoneAPI.badgesLogic;
using xZoneAPI.Repositories.AccountRepo;
using xZoneAPI.Repositories.AccountBadges;
using xZoneAPI.Repositories.ZoneRepo;
using xZoneAPI.Repositories.PostRepo;
using xZoneAPI.Recommenders;

namespace xZoneAPI
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
            services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.AddCors();
            services.Configure<AppSettings>(appSettingsSection);
            services.AddScoped<IAccountRepo, AccountRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IAccountSkillRepo, AccountSkillRepo>();
            services.AddScoped<IAccountBadgeRepo, AccountBadgeRepo>();
            services.AddScoped<IBadgeRepo, BadgeRepo>();
            services.AddScoped<IRankRepo, RankRepo>();
            services.AddScoped<ISkillRepo, SkillRepo>();
            services.AddScoped<ISectionRepository, SectionRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IProjectTaskRepository, ProjectTaskRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IRoadmapRepository, RoadmapRepository>();
            services.AddScoped<IBadgesSetFactory, BadgesSetFactory>();
            services.AddScoped<IGamificationLogic, GamificationLogic>();
            services.AddScoped<IZoneRepository, ZoneRepository>();
            services.AddScoped<IZoneMembersRepository, ZoneMembersRepository>();
            services.AddScoped<IZoneSkillRepository, ZoneSkillRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IFriendRepository, FriendRepository>();
            services.AddScoped<IFriendRequestRepository, FriendRequestRepository>();
            services.AddScoped<IZoneTaskRepository, AccountZoneTaskRepository>();
            services.AddScoped<IAccountZoneTaskRepo, AccountZoneTaskRepo>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IZoneRecommender, ZoneRecommender>();
            services.AddAutoMapper(typeof(xZoneMapper));
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            });
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("xZoneAPISpec",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "xZone API",
                        Version = "1",
                    });
                options.CustomSchemaIds(type => type.ToString());

                var xmlCommentFile = $"{ Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var cmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
                options.IncludeXmlComments(cmlCommentsFullPath);
            });
            services.AddControllers();
            var appSetting = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSetting.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            /*
            services.AddControllers();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "xZone API", Version = "v1" });
            });*/
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env ,IApiVersionDescriptionProvider provider)
        {
          /*  if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
               
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "xZoneAPI v1");
            });*/
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.

            app.UseSwaggerUI(options =>
            {
                foreach (var desc in provider.ApiVersionDescriptions)
                    options.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json",
                        desc.GroupName.ToUpperInvariant());
             //   options.RoutePrefix = "";
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseDefaultFiles();

            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            );

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseHttpsRedirection();
         
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
