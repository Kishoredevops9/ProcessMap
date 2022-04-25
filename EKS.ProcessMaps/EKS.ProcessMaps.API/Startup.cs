namespace EKS.ProcessMaps.API
{
    using System;
    using System.IO;
    using System.Reflection;
    using AutoMapper;
    using global::EKS.Common.ExceptionHandler;
    using global::EKS.Common.Logging;
    using global::EKS.Common.Notification;
    using global::EKS.ProcessMaps.API.EKS.ProcessMaps.Business.Interfaces.PublishedContent;
    using global::EKS.ProcessMaps.API.EKS.ProcessMaps.Business.PublishedContent;
    using global::EKS.ProcessMaps.Business;
    using global::EKS.ProcessMaps.Business.Interfaces;
    using global::EKS.ProcessMaps.DA;
    using global::EKS.ProcessMaps.DA.Interfaces;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.AzureAD.UI;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Identity.Web;
    using Microsoft.Identity.Web.TokenCacheProviders.InMemory;
    using Microsoft.OpenApi.Models;
    using Newtonsoft.Json;

    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        private readonly string policyName = string.Empty;
        private readonly string allowURL = string.Empty;

        /// <summary>
        /// Constructor - Startup
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;

            // policyName = configuration.GetSection("CORSValues").GetSection("PolicyName").Value;
            // allowURL = configuration.GetSection("CORSValues").GetSection("AllowURL").Value.Split(',');
            this.policyName = "FrontEndWebApp";
            this.allowURL = "http://localhost:4200";
        }

        /// <summary>
        /// Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// ConfigureServices - This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: this.policyName, options => options.WithOrigins(this.allowURL)
                .AllowAnyMethod()
                .AllowAnyHeader());
            });
            services.AddProtectedWebApi(this.Configuration)
                    .AddProtectedWebApiCallsProtectedWebApi(this.Configuration)
                    .AddInMemoryTokenCaches();
            services.AddMvc().AddNewtonsoftJson();
            services.AddDbContext<KnowledgeMapContext>(opts => opts.UseSqlServer(this.Configuration["ConnectionString:ProcessMaps"]));
            services.AddDbContext<PublishContentContext>(opts => opts.UseSqlServer(this.Configuration["ConnectionString:PublishedContentDB"]));

            AddDependencyInjection(services);

            services.AddAutoMapper(typeof(Startup));
            
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ProcessMap API",
                    Version = "v1",
                    Description = "An API to perform ProcessMap operations",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "EKS ProcessMap",

                        Url = new Uri("https://twitter.com/ESW"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "ProcessMap API LIC",
                        Url = new Uri("https://example.com/license"),
                    },
                });

                // Set the comments path for the Swagger JSON and UI.
                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        /// <summary>
        /// Configure - This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                Logger.IsLocalLogging = true;
                Logger.InstrumentationKey = string.Empty;

                // added to test on azure
                // Logger.IsLocalLogging = false;
                // Logger.InstrumentationKey = Configuration["ApplicationInsights:InstrumentationKey"];
            }
            else
            {
                app.UseDeveloperExceptionPage();
                Logger.IsLocalLogging = true;

                // Logger.InstrumentationKey = this.Configuration["ApplicationInsights:InstrumentationKey"];
            }

            app.UseMiddleware<ExceptionMiddlewareESW>();

            app.UseDefaultFiles();
            app.UseStaticFiles(); // For the wwwroot folder

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProcessMap API V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


        private static void AddDependencyInjection(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IPublishContentRepository<>), typeof(PublishContentRepository<>));
            services.AddScoped(typeof(IKnowledgePackRepository), typeof(KnowledgePackRepository));
            services.AddScoped(typeof(IProcessMapRepository), typeof(ProcessMapRepository));
            services.AddScoped(typeof(IKnowledgeAssetRepository), typeof(KnowledgeAssetRepository));
            
            // Added For ProcessMaps
            services.AddScoped<IProcessMapsAppService, ProcessMapsAppService>();

            // Added For Swimlanes
            services.AddScoped<IActivityGroupsAppService, ActivityGroupsAppService>();

            // Added For ProcessMapMeta
            services.AddScoped<IProcessMapMetaAppService, ProcessMapMetaAppService>();

            // Added For ActivityBlocks
            services.AddScoped<IActivitiesAppService, ActivitiesAppService>();

            // Added For UserPrefences
            services.AddScoped<IUserPreferencesAppService, UserPreferencesAppService>();

            // Added For Activity Connections
            services.AddScoped<IActivityConnectionsAppService, ActivityConnectionsAppService>();

            // Added For Activity Block Types
            services.AddScoped<IActivityBlockTypesAppService, ActivityBlockTypesAppService>();

            // Added For Phases
            services.AddScoped<IPhasesAppService, PhasesAppService>();

            // Added For Activity Pages
            services.AddScoped<IActivityPagesAppService, ActivityPagesAppService>();

            services.AddScoped<IPrivateAssetsAppService, PrivateAssetsAppService>();

            // Added For logger
            services.AddSingleton<ILogManager, Logger>();

            // Added For Notification
            services.AddSingleton<IEmailNotification, EmailNotification>();

            // Added For KPacksMap
            services.AddScoped<IKPacksAppService, KPacksMapAppService>();

            services.AddScoped<IKnowledgeAssetsAppService, KnowledgeAssetsAppService>();

            // For Security Filter Logic
            services.AddScoped<ICustomAppService, CustomAppService>();

            // For AuthorizationLog
            services.AddScoped<IAuthorizationLogService, AuthorizationLogService>();

            services.AddScoped<IProcessMapCommonAppService, ProcessMapCommonAppService>();

            // For public steps
            services.AddScoped<IPublicStepsAppService, PublicStepsAppService>();

            services.AddScoped<IExportExcelAppService, ExportExcelAppService>();

            services.AddScoped<IKnowledgeAssetTransferService, KnowledgeAssetTransferService>();

            services.AddScoped<IKnowledgeAssetAppService, KnowledgeAssetAppService>();
            
            services.AddScoped<IContainerItemAppService, ContainerItemAppService>();

            services.AddScoped<IKnowledgeAssetExportAppService, KnowledgeAssetExportAppService>();

            services.AddScoped<IKnowledgeAssetCloneAppService, KnowledgeAssetCloneAppService>();
            
            services.AddScoped<IMigrateMapsAppService, MigrateMapsAppService>();
            
            services.AddScoped<IPublishedCommonAppService, PublishedCommonAppService>();
        }
    }
}
