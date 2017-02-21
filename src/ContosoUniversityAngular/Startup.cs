namespace ContosoUniversityAngular
{
    using AutoMapper;
    using ContosoUniversityAngular.Infrastructure;
    using Data;
    using Features.Courses.Validator;
    using FluentValidation.AspNetCore;
    using Infrastructure.Conventions;
    using MediatR;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc(opt =>
            {
                opt.Conventions.Add(new FeatureConvention());
                opt.Filters.Add(typeof(DbContextTransactionFilter));
                opt.Filters.Add(typeof(ValidatorActionFilter));
            })
            .AddRazorOptions(options =>
            {
                // {0} - Action Name
                // {1} - Controller Name
                // {2} - Area Name
                // {3} - Feature Name
                // Replace normal view location entirely
                options.ViewLocationFormats.Clear();
                options.ViewLocationFormats.Add("/Features/{3}/{1}/{0}.cshtml");
                options.ViewLocationFormats.Add("/Features/{3}/{0}.cshtml");
                options.ViewLocationFormats.Add("/Features/Shared/{0}.cshtml");
                options.ViewLocationExpanders.Add(new FeatureViewLocationExpander());
            })
            .AddFluentValidation(cfg => { cfg.RegisterValidatorsFromAssemblyContaining<Startup>(); });

            services.AddDbContext<UniversityContext>(options =>
                options.UseSqlServer(
                    this.Configuration["Data:DefaultConnection:ConnectionString"]));

            services.AddMediatR(typeof(Startup));
            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<ICoursesValidator, CoursesValidator>();

            Mapper.AssertConfigurationIsValid();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "courses",
                    template: "courses/{*url}",
                    defaults: new
                    {
                        controller = "Home",
                        action = "Courses"
                    });

                routes.MapRoute(
                    name: "departments",
                    template: "departments/{*catch-all}",
                    defaults: new
                    {
                        controller = "Home",
                        action = "Departments"
                    });

                routes.MapRoute(
                    name: "students",
                    template: "students/{*catch-all}",
                    defaults: new
                    {
                        controller = "Home",
                        action = "Students"
                    });

                routes.MapRoute(
                    name: "instructors",
                    template: "instructors/{*catch-all}",
                    defaults: new
                    {
                        controller = "Home",
                        action = "Instructors"
                    });

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            DbInitializer.PopulateDb(app)
                .GetAwaiter()
                .GetResult();
        }
    }
}