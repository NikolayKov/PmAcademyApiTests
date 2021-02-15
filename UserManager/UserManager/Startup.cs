using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;
using MongoDB.Driver;
using UserManager.Clients;
using UserManager.Models;
using MongoClient = UserManager.Clients.MongoClient;

namespace UserManager
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

            var externalApi = Configuration.GetSection("ExternalApi");

            Uri endPointA = new Uri(externalApi.Value);
            HttpClient httpClient = new HttpClient()
            {
                BaseAddress = endPointA,
            };
            var reqresApiClient = new ReqresApiClient(httpClient);
            services.AddSingleton<HttpClient>(httpClient);
            services.AddSingleton<ReqresApiClient>(new ReqresApiClient(httpClient));

            string connectionString = "mongodb://localhost:27017";
            MongoDB.Driver.MongoClient mongoClient = new MongoDB.Driver.MongoClient(connectionString);
            var customMongoClient = new MongoClient(mongoClient);

            var database = mongoClient.GetDatabase("UserManager");
            var collection = database.GetCollection<UserDto>("Users");
            var keys = Builders<UserDto>.IndexKeys.Ascending(x => x.Email);
            collection.Indexes.CreateOneAsync(new CreateIndexModel<UserDto>(keys, new CreateIndexOptions()
            {
                Unique = true
            }));

            services.AddSingleton<UserManager.Clients.MongoClient>(customMongoClient);

            var userManager = new Managers.UserManager(reqresApiClient, customMongoClient);
            services.AddSingleton<Managers.UserManager>(userManager);

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
