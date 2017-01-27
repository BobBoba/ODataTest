using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace ODataTest
{
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

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                //.Filter.ByIncludingOnly()
                .CreateLogger();

        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();

            services.AddSingleton<IDemoDataSources, DemoDataSources>();
            // Add framework services.
            services.AddMvc();
            //.AddWebApiConventions();
            //EnableQueryAttribute
            services.AddOData<IDemoDataSources>(builder =>
            {
                //builder.EntitySet<Person>("People");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddSerilog();

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //app.UseMvc();
            app.UseOData("odata");
        }
    }

    public class Person
    {
        [Key]
        public String ID { get; set; }

        [Required]
        public String Name { get; set; }

        public String Description { get; set; }

        public List<Trip> Trips { get; set; }
    }

    public class Trip
    {
        [Key]
        public String ID { get; set; }

        [Required]
        public String Name { get; set; }
    }

    public interface IDemoDataSources
    {
        IQueryable<Person> People { get; }
        IQueryable<Trip> Trips { get; }
    }

    public class DemoDataSources : IDemoDataSources
    {
        //private static DemoDataSources instance = null;

        //public static DemoDataSources Instance
        //{
        //    get
        //    {
        //        if (instance == null)
        //        {
        //            instance = new DemoDataSources();
        //        }
        //        return instance;
        //    }
        //}

        private List<Person> People;
        private List<Trip> Trips;

        IQueryable<Person> IDemoDataSources.People => People.AsQueryable();
        IQueryable<Trip> IDemoDataSources.Trips => Trips.AsQueryable();


        public DemoDataSources()
        {
            
            People = new List<Person>();
            Trips = new List<Trip>();
        
            Trips.AddRange(new List<Trip>()
            {
                new Trip()
                {
                    ID = "0",
                    Name = "Trip 0"
                },
                new Trip()
                {
                    ID = "1",
                    Name = "Trip 1"
                },
                new Trip()
                {
                    ID = "2",
                    Name = "Trip 2"
                },
                new Trip()
                {
                    ID = "3",
                    Name = "Trip 3"
                }
            });

            People.AddRange(new List<Person>
            {
                new Person()
                {
                    ID = "001",
                    Name = "Angel",
                    Trips = new List<Trip> {Trips[0], Trips[1]}
                },
                new Person()
                {
                    ID = "002",
                    Name = "Clyde",
                    Description = "Contrary to popular belief, Lorem Ipsum is not simply random text.",
                    Trips = new List<Trip> {Trips[2], Trips[3]}
                },
                new Person()
                {
                    ID = "003",
                    Name = "Elaine",
                    Description =
                        "It has roots in a piece of classical Latin literature from 45 BC, making Lorems over 2000 years old."
                }
            });
        }
    }
}