namespace GraphQL.API
{
    using GraphQL.API.Configurations;
    using GraphQL.API.Mutations;
    using GraphQL.API.Queries;
    using GraphQL.API.Resolvers;
    using GraphQL.API.Subscriptions;
    using GraphQL.API.Types;
    using GraphQL.Core.Repositories;
    using GraphQL.Infrastructure.Data;
    using GraphQL.Infrastructure.Repositories;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using PersonReader;
    using Microsoft.Extensions.Options;
    public class Startup
    {
        private readonly ApiConfiguration apiConfiguration;
        private readonly RestConfiguration restConfiguration;

        public Startup(IConfiguration configuration)
        {
            this.apiConfiguration = configuration.Get<ApiConfiguration>();
            this.restConfiguration = configuration.Get<RestConfiguration>();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Configurations api monfo
            services.AddSingleton(this.apiConfiguration.MongoDbConfiguration);//MongoDbConfiguration
                                                                              //call configuration api Rest
            services.AddSingleton(this.restConfiguration.BaseAddress);
            // Repositories
            services.AddSingleton<ICatalogContext, CatalogContext>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            //external api
            services.AddHttpClient<PersonService>(
             (serviceProvider, httpClient) => httpClient.BaseAddress = this.restConfiguration.BaseAddress
            //(serviceProvider, httpClient) => httpClient.BaseAddress = serviceProvider.GetRequiredService<IOptions<RestConfiguration>>().Value.BaseAddress
            // (serviceProvider, httpClient) => httpClient.BaseAddress = new Uri("https://localhost:7204/")
            );
            // GraphQL
            services

                .AddGraphQLServer().ModifyOptions(opt =>
                    {
                        opt.StrictValidation = false;
                    })
                        .AddQueryType(d => d.Name("Query"))
                       .AddTypeExtension<ProductQuery>()
                    .AddTypeExtension<CategoryQuery>()
                    .AddTypeExtension<PersonQuery>()
                    .AddMutationType(d => d.Name("Mutation"))
                    .AddTypeExtension<ProductMutation>()
                      .AddTypeExtension<CategoryMutation>()
                   .AddSubscriptionType(d => d.Name("Subscription"))
                    .AddTypeExtension<ProductSubscriptions>()
                     .AddType<ProductType>()
                 //  .AddType<QueryType>()  ///here problem
                 .AddType<PersonType>()
                .AddType<CategoryResolver>()

                .AddInMemorySubscriptions();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseWebSockets();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL("/api/graphql");
            });
        }
    }
}
